using BatchGanjoorLinkApprover.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMuseum.Models.Ganjoor;
using RMuseum.Models.Ganjoor.ViewModels;
using RMuseum.Models.GanjoorIntegration.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchGanjoorLinkApprover
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            txtEmail.Text = Settings.Default.Email;
            txtPassword.Text = Settings.Default.Password;

            if (Settings.Default.TrustedUserIdSet != null)
            {
                foreach (var item in Settings.Default.TrustedUserIdSet)
                {
                    lstTrustedUsers.Items.Add(item);
                }
            }

            if (Settings.Default.ProtectedCategoryIdSet != null)
            {
                foreach (var item in Settings.Default.ProtectedCategoryIdSet)
                {
                    lstProtectedCatgories.Items.Add(item);
                }
            }

            if(Settings.Default.UntrustedUserIdSet != null)
            {
                foreach (var item in Settings.Default.UntrustedUserIdSet)
                {
                    lstUnsafeUsers.Items.Add(item);
                }
            }

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;
            Application.DoEvents();

            DialogResult = DialogResult.None;
            LoginViewModel model = new LoginViewModel()
            {
                Username = txtEmail.Text,
                Password = txtPassword.Text,
                ClientAppName = "Desktop Ganjoor",
                Language = "fa-IR"
            };

            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var loginUrl = "https://api.ganjoor.net/api/users/login";
                var response = await httpClient.PostAsync(loginUrl, stringContent);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    Enabled = true;
                    MessageBox.Show(response.ToString());
                    return;
                }
                response.EnsureSuccessStatusCode();

                var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                Properties.Settings.Default.Email = txtEmail.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                Properties.Settings.Default.MuseumToken = result["token"].ToString();
                Properties.Settings.Default.SessionId = Guid.Parse(result["sessionId"].ToString());
                Properties.Settings.Default.UserId = Guid.Parse(result["user"]["id"].ToString());
                Properties.Settings.Default.Save();
            }

            Enabled = true;
            Cursor = Cursors.Default;
            DialogResult = DialogResult.OK;
        }

        private async Task<bool> TokenIsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(Settings.Default.MuseumToken))
                    return false;
                using (HttpClient httpClient = new HttpClient())
                {



                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    HttpResponseMessage response = await httpClient.GetAsync("https://api.ganjoor.net/api/artifacts/ganjoor?status=0&notSynced=false");
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        Cursor = Cursors.Default;
                        Properties.Settings.Default.Save();
                        return true;
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        return false;
                    }

                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



        private async void btnApprove_Click(object sender, EventArgs e)
        {
            if (!await TokenIsValid())
            {
                MessageBox.Show("لطفا دوباره وارد شوید.");
                return;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                lblStatus.Text = "دریافت فهرست ...";

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();



                HttpResponseMessage response = await httpClient.GetAsync("https://api.ganjoor.net/api/artifacts/ganjoor?status=0&notSynced=true");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(response.ToString());
                    return;
                }

                response.EnsureSuccessStatusCode();

                var links = JsonConvert.DeserializeObject<GanjoorLinkViewModel[]>(await response.Content.ReadAsStringAsync());

                lblStatus.Text = "بررسی ...";

                Application.DoEvents();

                foreach (GanjoorLinkViewModel link in links)
                {
                    if (link.SuggestedBy.Id == Properties.Settings.Default.UserId)
                    {
                        int nRetry = 0;
                        while (true)
                        {
                            nRetry++;
                            lblStatus.Text = $"{nRetry} : {link.GanjoorUrl}";
                            Application.DoEvents();

                            HttpResponseMessage approveResponse = await httpClient.PutAsync($"https://api.ganjoor.net/api/artifacts/ganjoor/review/{link.Id}/1", null);
                            if (approveResponse.StatusCode == HttpStatusCode.OK)
                            {
                                approveResponse.EnsureSuccessStatusCode();
                                break;
                            }
                        }
                    }
                }

                lblStatus.Text = "پایان";

            }
        }

        private async void btnApproveEdits_Click(object sender, EventArgs e)
        {
            if (!await TokenIsValid())
            {
                MessageBox.Show("لطفا دوباره وارد شوید.");
                return;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);


                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                int skip = 0;
                bool approvedMeanings = chkMeanings.Checked;
                do
                {
                    lblStatus.Text = $"بعدی ...";
                    Application.DoEvents();


                    HttpResponseMessage response = await httpClient.GetAsync($"https://api.ganjoor.net/api/ganjoor/poem/correction/next?skip={skip}");
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {

                        break;
                    }
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    var correction = JsonConvert.DeserializeObject<GanjoorPoemCorrectionViewModel>(await response.Content.ReadAsStringAsync());
                    if (correction == null)
                    {
                        break;
                    }

                    bool trusted = false;
                    foreach (var item in Settings.Default.TrustedUserIdSet)
                    {
                        if (correction.UserId == Guid.Parse(item))
                        {
                            trusted = true; break;
                        }
                    }

                    if (!trusted)
                    {
                        if (!approvedMeanings)
                        {
                            skip++;
                            continue;
                        }
                    }

                    HttpResponseMessage resPoem = await httpClient.GetAsync($"https://api.ganjoor.net/api/ganjoor/poem/{correction.PoemId}?catInfo=true&catPoems=false&rhymes=false&recitations=false&images=false&songs=false&comments=false&verseDetails=false&navigation=false&relatedpoems=false");
                    if (resPoem.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(resPoem.ToString());
                        return;
                    }

                    resPoem.EnsureSuccessStatusCode();

                    var result = JObject.Parse(await resPoem.Content.ReadAsStringAsync());

                    if(!approvedMeanings)
                    {
                        string catId = result["category"]["cat"]["id"].ToString();
                        if (Settings.Default.ProtectedCategoryIdSet != null)
                            foreach (var item in Settings.Default.ProtectedCategoryIdSet)
                            {
                                if (item.ToString() == catId)
                                {
                                    skip++;
                                    continue;
                                }
                            }
                    }
                    


                    if (correction.Rhythm2 != null)
                    {
                        skip++;
                        continue;
                    }

                    lblStatus.Text = "بررسی ...";
                    Application.DoEvents();

                    bool onlyMeaning = true;

                    if (correction.Title != null)
                    {
                        correction.Result = CorrectionReviewResult.Approved;
                        onlyMeaning = false;
                    }

                    if (correction.Rhythm != null)
                    {
                        correction.RhythmResult = CorrectionReviewResult.Approved;
                        onlyMeaning = false;
                    }

                    if (correction.RhymeLetters != null)
                    {
                        correction.RhymeLettersReviewResult = CorrectionReviewResult.Approved;
                        onlyMeaning = false;
                    }

                    if(correction.PoemSummary != null)
                    {
                        correction.SummaryReviewResult = CorrectionReviewResult.Approved;
                    }

                    foreach (var v in correction.VerseOrderText)
                    {
                        if (!string.IsNullOrEmpty(v.Text))
                        {
                            v.Result = CorrectionReviewResult.Approved;
                            onlyMeaning = false;
                        }
                        if (!string.IsNullOrEmpty(v.CoupletSummary))
                        {
                            if(v.Result == CorrectionReviewResult.NotReviewed)
                            {
                                v.Result = CorrectionReviewResult.NotChanged;
                            }
                            v.SummaryReviewResult = CorrectionReviewResult.Approved;
                        }
                    }

                    if(approvedMeanings && !onlyMeaning)
                    {
                        skip++;
                        continue;
                    }

                    HttpResponseMessage approveResponse = await httpClient.PostAsync($"https://api.ganjoor.net/api/ganjoor/correction/moderate",
                        new StringContent(JsonConvert.SerializeObject(correction), Encoding.UTF8, "application/json")
                        );
                    if (approveResponse.StatusCode == HttpStatusCode.OK)
                    {
                        approveResponse.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        var err = await approveResponse.Content.ReadAsStringAsync();
                        if(err != null)
                        {
                            
                        }
                        skip++;
                    }

                } while (true);

                Cursor = Cursors.Default;
                lblStatus.Text = "پایان";

            }
        }

        private async void btnApproveMetres_Click(object sender, EventArgs e)
        {
            if (!await TokenIsValid())
            {
                MessageBox.Show("لطفا دوباره وارد شوید.");
                return;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);


                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                int skip = 0;
                do
                {
                    lblStatus.Text = $"بعدی ...";
                    Application.DoEvents();


                    HttpResponseMessage response = await httpClient.GetAsync($"https://api.ganjoor.net/api/ganjoor/section/correction/next?skip={skip}");
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {

                        break;
                    }
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    var correction = JsonConvert.DeserializeObject<GanjoorPoemSectionCorrectionViewModel>(await response.Content.ReadAsStringAsync());
                    if (correction == null)
                    {
                        break;
                    }


                    if (
                        correction.UserId != Guid.Parse("5f298291-c765-4dd7-0f45-08d747fdebdb")
                        &&
                         correction.UserId != Guid.Parse("a9cb3463-e442-455a-12a3-08d72ca64fca")
                        ) //کاربر سیستمی
                    {
                        skip++;
                        continue;
                    }
                    if (correction.Rhythm2 != null)
                    {
                        skip++;
                        continue;
                    }

                    lblStatus.Text = "بررسی ...";
                    Application.DoEvents();


                    if (correction.Rhythm != null)
                    {
                        correction.RhythmResult = CorrectionReviewResult.Approved;
                    }

                    if (correction.RhymeLetters != null)
                    {
                        correction.RhymeLettersReviewResult = CorrectionReviewResult.Approved;
                    }




                    HttpResponseMessage approveResponse = await httpClient.PostAsync($"https://api.ganjoor.net/api/ganjoor/section/moderate",
                        new StringContent(JsonConvert.SerializeObject(correction), Encoding.UTF8, "application/json")
                        );
                    if (approveResponse.StatusCode == HttpStatusCode.OK)
                    {
                        approveResponse.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        skip++;
                    }

                } while (true);

                Cursor = Cursors.Default;
                lblStatus.Text = "پایان";

            }
        }

        private void btnAddTrusted_Click(object sender, EventArgs e)
        {
            if (Settings.Default.TrustedUserIdSet == null)
            {
                Settings.Default.TrustedUserIdSet = new System.Collections.Specialized.StringCollection();
            }
            Settings.Default.TrustedUserIdSet.Add(txtUserId.Text);
            Settings.Default.Save();

            lstTrustedUsers.Items.Add(txtUserId.Text);
        }

        private void btnDelTrusted_Click(object sender, EventArgs e)
        {
            Settings.Default.TrustedUserIdSet.Remove(lstTrustedUsers.SelectedItem.ToString());
            Settings.Default.Save();

            lstTrustedUsers.Items.Remove(lstTrustedUsers.SelectedItem.ToString());
        }

        private async void lstTrustedUsers_DoubleClickAsync(object sender, EventArgs e)
        {
            if (lstTrustedUsers.SelectedItem != null)
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                    lblStatus.Text = "دریافت اطلاعات کاربر ...";

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();



                    HttpResponseMessage response = await httpClient.GetAsync($"https://api.ganjoor.net/api/users/{lstTrustedUsers.SelectedItem}");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    var result = JObject.Parse(await response.Content.ReadAsStringAsync());

                    MessageBox.Show(result["nickName"].ToString());

                }
            }
        }

        private void btnAddProtected_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCategoryId.Text, out var _))
            {
                return;
            }
            if (Settings.Default.ProtectedCategoryIdSet == null)
            {
                Settings.Default.ProtectedCategoryIdSet = new System.Collections.Specialized.StringCollection();
            }
            Settings.Default.ProtectedCategoryIdSet.Add(txtCategoryId.Text);
            Settings.Default.Save();

            lstProtectedCatgories.Items.Add(txtCategoryId.Text);
        }

        private async void lstProtectedCatgories_DoubleClick(object sender, EventArgs e)
        {
            if (lstProtectedCatgories.SelectedItem != null)
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                    lblStatus.Text = "دریافت اطلاعات بخش ...";

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();



                    HttpResponseMessage resCat = await httpClient.GetAsync($"https://api.ganjoor.net/api/ganjoor/cat/{lstProtectedCatgories.SelectedItem}?poems=false&mainSections=false");
                    if (resCat.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(resCat.ToString());
                        return;
                    }

                    resCat.EnsureSuccessStatusCode();

                    var result = JObject.Parse(await resCat.Content.ReadAsStringAsync());

                    MessageBox.Show(result["poet"]["name"] + " : " + result["cat"]["title"].ToString());

                }
            }
        }

        private async void btnNaskban_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                lblStatus.Text = "فراخوانی ...";

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                LoginViewModel model = new LoginViewModel()
                {
                    Username = txtEmail.Text,
                    Password = txtPassword.Text,
                    ClientAppName = "Desktop Ganjoor",
                    Language = "fa-IR"
                };
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage resCat = await httpClient.PostAsync($"https://api.ganjoor.net/api/ganjoor/naskban", stringContent);
                if (resCat.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(resCat.ToString());
                    return;
                }

                resCat.EnsureSuccessStatusCode();


                MessageBox.Show("همگامسازی شروع شد.");

                lblStatus.Text = "آماده";

            }
        }

        private async void btnApproveUserMeters_Click(object sender, EventArgs e)
        {
            if (!await TokenIsValid())
            {
                MessageBox.Show("لطفا دوباره وارد شوید.");
                return;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);


                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                int skip = 0;
                do
                {
                    lblStatus.Text = $"بعدی ...";
                    Application.DoEvents();


                    HttpResponseMessage response = await httpClient.GetAsync($"https://api.ganjoor.net/api/ganjoor/section/correction/next?skip={skip}");
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        break;
                    }
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    var correction = JsonConvert.DeserializeObject<GanjoorPoemSectionCorrectionViewModel>(await response.Content.ReadAsStringAsync());
                    if (correction == null)
                    {
                        break;
                    }


                    bool trusted = false;
                    foreach (var item in Settings.Default.TrustedUserIdSet)
                    {
                        if (correction.UserId == Guid.Parse(item))
                        {
                            trusted = true; break;
                        }
                    }

                    if (!trusted)
                    {
                        skip++;
                        continue;
                    }

                    if (correction.Rhythm2 != null)
                    {
                        skip++;
                        continue;
                    }

                    if (correction.BreakFromVerse1VOrder != null)
                    {
                        skip++;
                        continue;
                    }

                    lblStatus.Text = "بررسی ...";
                    Application.DoEvents();


                    if (correction.Rhythm != null)
                    {
                        correction.RhythmResult = CorrectionReviewResult.Approved;
                    }

                    if (correction.RhymeLetters != null)
                    {
                        correction.RhymeLettersReviewResult = CorrectionReviewResult.Approved;
                    }

                    if (correction.Language != null)
                    {
                        correction.LanguageReviewResult = CorrectionReviewResult.Approved;
                    }

                    if (correction.PoemFormat != null)
                    {
                        correction.PoemFormatReviewResult = CorrectionReviewResult.Approved;
                    }


                    HttpResponseMessage approveResponse = await httpClient.PostAsync($"https://api.ganjoor.net/api/ganjoor/section/moderate",
                        new StringContent(JsonConvert.SerializeObject(correction), Encoding.UTF8, "application/json")
                        );
                    if (approveResponse.StatusCode == HttpStatusCode.OK)
                    {
                        approveResponse.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        skip++;
                    }

                } while (true);

                Cursor = Cursors.Default;
                lblStatus.Text = "پایان";

            }
        }

        private async void btnNaskbanFix_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                lblStatus.Text = "فراخوانی ...";

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                LoginViewModel model = new LoginViewModel()
                {
                    Username = txtEmail.Text,
                    Password = txtPassword.Text,
                    ClientAppName = "Desktop Ganjoor",
                    Language = "fa-IR"
                };
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage resCat = await httpClient.PutAsync($"https://api.ganjoor.net/api/ganjoor/naskban", stringContent);
                if (resCat.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(resCat.ToString());
                    return;
                }

                resCat.EnsureSuccessStatusCode();


                MessageBox.Show("تصحیح شروع شد.");

                lblStatus.Text = "آماده";

            }
        }

        private void btnAddUnsafeUser_Click(object sender, EventArgs e)
        {

            if (Settings.Default.UntrustedUserIdSet == null)
            {
                Settings.Default.UntrustedUserIdSet = new System.Collections.Specialized.StringCollection();
            }
            Settings.Default.UntrustedUserIdSet.Add(txtUnsafeUserId.Text);
            Settings.Default.Save();

            lstUnsafeUsers.Items.Add(txtUnsafeUserId.Text);
        }
    }
}
