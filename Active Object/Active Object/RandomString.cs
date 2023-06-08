using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Reflection.Metadata;

namespace Active_Object
{
    public class RandomStrings
    {
        public string TheString { get; set; }
        public string Type { get; set; }
        private BlockingCollection<Action> dispatchQueue;
        public RandomStrings()
        {
            TheString = "no string";
            Type = "none";
            dispatchQueue = new BlockingCollection<Action>(5);
            Thread t = new Thread(() =>
            {
                while (true)
                {
                if (dispatchQueue.Count > 0)
                {
                    Console.WriteLine($"{dispatchQueue.Count} actions queued");
                    }
                    dispatchQueue.Take().Invoke();
                }
            });
            t.Start();
        }
        public void CatFactAsync()
        {
            var task = new Action(() => 
            {
                //Console.WriteLine("cat");
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://catfact.ninja/fact").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(responseBody);
                var catFact = parsedObject["fact"].ToString();
                TheString = catFact;
                Type = "Cat Fact";
            });
            dispatchQueue.Add(task);
        }
        public void BoredAsync()
        {
            var task = new Action(() => 
            {
                //Console.WriteLine("bored");
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://www.boredapi.com/api/activity").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(responseBody);
                var Bored = parsedObject["activity"].ToString();
                TheString = Bored;
                Type = "What to do";
            });
            dispatchQueue.Add(task);
        }
        public void IpAsync()
        {
            var task = new Action(() => 
            {
                //Console.WriteLine("ip");
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://api.ipify.org/?format=json").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(responseBody);
                var Bored = parsedObject["ip"].ToString();
                TheString = Bored;
                Type = "IP";
            });
            dispatchQueue.Add(task);
        }
        public override string ToString()
        {
            return $"{Type}:\n{TheString}";
        }
    }
}