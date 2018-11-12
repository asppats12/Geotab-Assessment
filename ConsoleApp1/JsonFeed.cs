using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url;
        static HttpClient client;

        public JsonFeed() {
            client = new HttpClient();
            _url = "";
        }

        public JsonFeed(string endpoint) : this()
        {
            _url = endpoint;
        }
        
		public static string GetRandomJokes(string firstname, string lastname, string category)
		{
			client.BaseAddress = new Uri(_url);
			string url = "jokes/";

			if (firstname != null)
			{
                url += url.Contains('?') ? "&" : "?"; 
				url += ("firstName=" + firstname.ToString());
			}
			if (lastname != null)
			{
                url += url.Contains('?') ? "&" : "?";
				url += ("lastName=" + lastname.ToString());
			}
			if (category != null)
			{
                url += url.Contains('?') ? "&" : "?";
				url += "limitTo=[" + category + "]";
                
			}

            return Task.FromResult(client.GetStringAsync(url).Result).Result.ToString() ;
		}
 
		public static dynamic Getnames()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			return JsonConvert.DeserializeObject<dynamic>(client.GetStringAsync("").Result);
		}

		public static string GetCategories()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			return Task.FromResult(client.GetStringAsync("categories").Result).Result.ToString() ;
		}
    }
}
