using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageForm : ContentPage
    {
        HttpClient client;
        private classapp.c_http c_Http;
        private string act;
        private string ID;
        //ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };

        public PageForm(string ids)
        {
            client = new HttpClient();
            c_Http = new classapp.c_http();
            InitializeComponent();
            
            if(ids == "")
            {
                act = "**new";
                initData();
            }
            else
            {
                ID = ids;
                act = "**upd";
                readData();
            }

            initPageData();

            //NavigationPage.SetHasNavigationBar(this, false);

        }

        public void initPageData()
        {
            navTitle.Text = "Daftar User";

            if (act == "**new")
                navSubTitle.Text = "tambah data";
            else
                navSubTitle.Text = "edit data";
        }

        public void initData()
        {
            txtUsername.Text = "iwan";
            txtPassword.Text = "12345";
            txtNama.Text = "Wayan Styawan Saputra";
            txtNotelepon.Text = "08563735581";
        }

        private void showIndicator()
        {
            //activity.IsRunning = true;
            //activity.IsEnabled = true;
            //activity.IsVisible = true;
        }

        private void hideIndicator()
        {
            //activity.IsRunning = false;
            //activity.IsEnabled = false;
            //activity.IsVisible = false;
        }

        private async void readData()
        {
            try
            {
                c_Http.NewPostField();
                c_Http.AddPostField("id", ID);
                var postData = c_Http.GetPostField();

                var sendData = await c_Http.sendData(c_Http.url, "user", "user_show", postData);
                var jsonResponse = JObject.Parse(sendData);
                var status = (bool)jsonResponse["status"];
                var pesan = jsonResponse["pesan"];

                if (!status)
                {
                    await DisplayAlert("Gagal", pesan.ToString(), "OK");
                    return;
                }

                var row = jsonResponse["DataUser"][0];
                txtUsername.Text = (string)row["username"];
                txtPassword.Text = (string)row["password"];
                txtNama.Text = (string)row["nama"];
                txtNotelepon.Text = (string)row["notelepon"];

            }
            catch (Exception ex)
            {
                await DisplayAlert("Gagal", ex.Message, "OK");
            }
        }

        private async void BtnSimpan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text;
                var password = txtPassword.Text;
                var nama = txtNama.Text;
                var notelepon = txtNotelepon.Text;

                c_Http.NewPostField();
                c_Http.AddPostField("username", username);
                c_Http.AddPostField("password", password);
                c_Http.AddPostField("nama", nama);
                c_Http.AddPostField("notelepon", notelepon);
                var postData = c_Http.GetPostField();

                var sendData = await c_Http.sendData(c_Http.url, "user", "user_ae", postData);
                var jsonResponse = JObject.Parse(sendData);
                var status = (bool)jsonResponse["status"];
                var pesan = jsonResponse["pesan"];

                if (status)
                {
                    await DisplayAlert("Berhasil", pesan.ToString(), "OK");
                }
                else
                {
                    await DisplayAlert("Gagal", pesan.ToString(), "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Gagal", ex.Message, "OK");
            }

            
            

            //Console.WriteLine(sendData);

            //var values = new Dictionary<string, string>
            //{
            //    { "username", username },
            //    { "password", password },
            //    { "nama", nama },
            //    { "notelepon", notelepon },
            //};

            //await DisplayAlert("Alert", c_Http.url, "OK");

            //var content = new FormUrlEncodedContent(values);
            //var response = await client.PostAsync(c_Http.url + "?mod_route=user&mod_name=user_ae", content);
            //var responseString = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(responseString);

            //c_Http.InitializeData();

            //if (username == "")
            //{
            //    await DisplayAlert("Alert", "[username] masih kosong", "OK");
            //    return;
            //}

        }
    }
}