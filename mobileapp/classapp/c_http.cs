using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace mobileapp.classapp
{
    // https://carldesouza.com/httpclient-getasync-postasync-sendasync-c/

    class c_http
    {
        public string url = "http://192.168.102.50/ws_xamarin/ws/index.php";

        private HttpClient client = new HttpClient();
        Dictionary<string, string> myPostData = new Dictionary<string, string>();

        public void InitializeData()
        {
        }

        public void NewPostField()
        {
            myPostData.Clear();
        }

        public void AddPostField(string keyname, string keyvalue)
        {
            myPostData.Add(keyname, keyvalue);
        }

        public Dictionary<string, string> GetPostField()
        {
            return myPostData;
        }

        public async Task<string> sendData(string urls, string mod_route, string mod_name, Dictionary<string, string> postData)
        {
            //var client = new HttpClient();

            var content = new FormUrlEncodedContent(postData);
            var response = await client.PostAsync(urls + "?mod_route=" + mod_route + "&mod_name=" + mod_name, content);
            var responseString = await response.Content.ReadAsStringAsync();

            //var content = new FormUrlEncodedContent(postData);
            //response = await client.PostAsync(urls + "?mod_route=" + mod_route + "&mod_name=" + mod_name, content);
            //var responseString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseString);
            return responseString;
        }
    }
}
