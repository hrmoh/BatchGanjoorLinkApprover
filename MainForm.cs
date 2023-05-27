using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMuseum.Models.Ganjoor;
using RMuseum.Models.Ganjoor.ViewModels;
using RMuseum.Models.GanjoorIntegration.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

            txtEmail.Text = Properties.Settings.Default.Email;
            txtPassword.Text = Properties.Settings.Default.Password;

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
                if (string.IsNullOrEmpty(Properties.Settings.Default.MuseumToken))
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
            if(!await TokenIsValid())
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

                foreach(GanjoorLinkViewModel link in links)
                {
                    if(link.SuggestedBy.Id == Properties.Settings.Default.UserId)
                    {
                        int nRetry = 0;
                        while(true)
                        {
                            nRetry++;
                            lblStatus.Text = $"{nRetry} : {link.GanjoorUrl}" ;
                            Application.DoEvents();

                            HttpResponseMessage approveResponse = await httpClient.PutAsync($"https://api.ganjoor.net/api/artifacts/ganjoor/review/{link.Id}/1", null);
                            if(approveResponse.StatusCode == HttpStatusCode.OK)
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
                do
                {
                    lblStatus.Text = $"بعدی ...";
                    Application.DoEvents();

                    
                    HttpResponseMessage response = await httpClient.GetAsync($"https://api.ganjoor.net/api/ganjoor/poem/correction/next?skip={skip}"); 
                    if(response.StatusCode == HttpStatusCode.NoContent)
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
                    if(correction == null)
                    {
                        break;
                    }

                    
                    if (correction.UserId != Guid.Parse("5f5fec7e-91db-4155-ea7a-08d95ee3730c") //کژدم
                        && correction.UserId != Guid.Parse("1bb8e457-a922-4764-039e-08da771bc8c7")//خانزادی
                        && correction.UserId != Guid.Parse("80da71b5-a682-4a81-4956-08da5ec3845b")//یاسین مهدیان
                        && correction.UserId != Guid.Parse("615de499-fab9-4d4e-5c82-08db50ff5d57")//Dr. ortegoli
                        && correction.UserId != Guid.Parse("542a5920-9928-4f45-28a1-08d91e912ac2")//مجید گزیان
                        && correction.UserId != Guid.Parse("28d87825-824f-4e1b-cec3-08d9dd717ff9")//شهاب عمرانی
                        && correction.UserId != Guid.Parse("f749edce-f937-433f-4d98-08d98c4e7811")//حافظ سعدی
                        && correction.UserId != Guid.Parse("654e4047-acdf-4f23-ca11-08d919575382")//آرام نوبری نیا
                        && correction.UserId != Guid.Parse("f53b5699-434f-42f4-491b-08da8818a70a")//مسعود
                        && correction.UserId != Guid.Parse("90714860-6bb3-4b7c-1e6b-08d9aeb170b9")//ادیب
                        && correction.UserId != Guid.Parse("efe21afa-ee9f-4b84-f18e-08d94833c888")//آروین یوسفی
                        && correction.UserId != Guid.Parse("5f921847-2ad7-4fb2-4c11-08d957209196")//ابوالفضل
                        )
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

                    if (correction.Title != null)
                    {
                        correction.Result = CorrectionReviewResult.Approved;
                    }
                    
                    if(correction.Rhythm != null)
                    {
                        correction.RhythmResult = CorrectionReviewResult.Approved;
                    }

                    if(correction.RhymeLetters != null)
                    {
                        correction.RhymeLettersReviewResult = CorrectionReviewResult.Approved;
                    }

                    foreach (var v in correction.VerseOrderText)
                    {
                        v.Result = CorrectionReviewResult.Approved;
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
    }
}
