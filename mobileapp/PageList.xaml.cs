using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageList : ContentPage
    {
        HttpClient client;
        private classapp.c_http c_Http;

        ObservableCollection<UserData> userDatas = new ObservableCollection<UserData>();
        //public ObservableCollection<UserData> UserDatas { get { return userDatas; } }

        private class UserData
        {
            public string Username { get; set; }
            public string Nama { get; set; }
        }

        private class DataUser
        {
            public string username { get; set; }
            public string nama { get; set; }
            public string notelepon { get; set; }
        }

        private class DataPaging
        {
            public string totalpage { get; set; }
            public string totaldata { get; set; }
        }

        public  PageList()
        {
            client = new HttpClient();
            c_Http = new classapp.c_http();

            InitializeComponent();
            initData();
        }

        protected override void OnAppearing()
        {
        }

        public async void initData()
        {
            await ReadData();
        }

        private async Task ReadData()
        {
            var katakunci = txtKataKunci.Text;

            c_Http.NewPostField();
            c_Http.AddPostField("katakunci", katakunci);
            c_Http.AddPostField("limit", "10");
            c_Http.AddPostField("page", "1");
            var postData = c_Http.GetPostField();

            var sendData = await c_Http.sendData(c_Http.url, "user", "user_list", postData);

            JObject jsonResponse = JObject.Parse(sendData);
            //Console.WriteLine(jsonResponse);

            var resDataPaging = jsonResponse["DataPaging"];
            var resDataUser = jsonResponse["DataUser"];
            //Console.WriteLine(resDataPaging["totaldata"]);

            UserView.ItemsSource = userDatas;
            userDatas.Clear();

            foreach (var row in resDataUser)
            {
                var dataTemp = new UserData();
                dataTemp.Username = (string)row["username"];
                dataTemp.Nama = (string)row["nama"];
                userDatas.Add(dataTemp);
            }

        }

        private async void btncari_Clicked(object sender, EventArgs e)
        {
            await ReadData();
        }

        private async void btnadd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageForm(""));
        }

        private async void listview_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var row = e.SelectedItem as UserData;
                string action = await DisplayActionSheet("Popup Aksi : " + row.Nama, "Cancel", null, "Edit", "Delete");
                Console.WriteLine("Action: " + action);
                   
                if(action == "Edit")
                {
                    if (e.SelectedItem != null)
                    {
                        string selectedItem = e.SelectedItem.ToString();

                        // clear selected item
                        UserView.SelectedItem = null;
                        await Navigation.PushAsync(new PageForm(row.Username));
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Gagal", ex.Message, "OK");
            }

        }

    }
}