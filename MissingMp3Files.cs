using BatchGanjoorLinkApprover.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RMuseum.Models.GanjoorIntegration.ViewModels;
using RMuseum.Models.GanjoorAudio.ViewModels;

namespace BatchGanjoorLinkApprover
{
    public partial class MissingMp3Files : Form
    {
        public MissingMp3Files()
        {
            InitializeComponent();
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

        private async void btnStart_Click(object sender, EventArgs e)
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

                int pageNumber = 1;


                while (true)
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"https://api.ganjoor.net/api/audio/published?PageNumber={pageNumber}&PageSize=10&searchTerm=");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    var recitations = JsonConvert.DeserializeObject<PublicRecitationViewModel[]>(await response.Content.ReadAsStringAsync());
                    if (recitations.Length == 0)
                    {
                        lblStatus.Text = "پایان";
                        return;
                    }

                    lblStatus.Text = "بررسی ...";

                    Application.DoEvents();

                    foreach (PublicRecitationViewModel recitation in recitations)
                    {
                        string filePath = txtDir.Text + "\\" + recitation.Mp3Url.Replace("https://i.ganjoor.net/", "").Replace("/", "\\");
                        if (!System.IO.File.Exists(filePath))
                        {
                            RecitationErrorReportViewModel model = new RecitationErrorReportViewModel()
                            {
                                RecitationId = recitation.Id,
                                ReasonText = "ضمن عذرخواهی عدم وجود فایل mp3 به دلیل مشکلات فنی گنجور",
                                NumberOfLinesAffected = 0,
                                CoupletIndex = 0,
                                DateTime = DateTime.Now,
                            };
                            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                            HttpResponseMessage resCat = await httpClient.PostAsync($"https://api.ganjoor.net/api/audio/errors/report", stringContent);
                            if (resCat.StatusCode != HttpStatusCode.OK)
                            {
                                Cursor = Cursors.Default;
                                MessageBox.Show(resCat.ToString());
                                return;
                            }
                        }
                    }
                    pageNumber++;
                }

                

            }
        }
    }
}
