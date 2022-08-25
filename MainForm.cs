﻿using Newtonsoft.Json;
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

                    
                    if (correction.UserId != Guid.Parse("5f5fec7e-91db-4155-ea7a-08d95ee3730c") && correction.UserId != Guid.Parse("1bb8e457-a922-4764-039e-08da771bc8c7"))
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
    }
}
