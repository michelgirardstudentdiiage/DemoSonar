using Newtonsoft.Json;

namespace ChuckNorris.Tools
{
    public class Call
    {
        public string GetDataFromApi(string url)

        {
            string result = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            result = client.GetStringAsync(client.BaseAddress).Result;
            return result;
        }
        public List<string> GetChuckNorrisJokeCategories(string url)
        {
            List<string> results = new List<string>();

            results = JsonConvert.DeserializeObject<List<string>>(
                GetDataFromApi(url));

            return results;

        }



        public JokeChuckNorris GetJoke(string url)
        {
            JokeChuckNorris result;

            result = JsonConvert.DeserializeObject<JokeChuckNorris>(
                GetDataFromApi(url));

            return result;


        }
    }
}