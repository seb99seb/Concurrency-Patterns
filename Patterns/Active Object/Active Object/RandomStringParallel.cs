using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Active_Object
{
    public class RandomStringsParallel
    {
        public string State { get; set; }
        public string Type { get; set; }
        private BlockingCollection<Thread> dispatchQueue = new BlockingCollection<Thread>();
        public RandomStringsParallel()
        {
            State = "no string";
            Type = "none";
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    dispatchQueue.Take().Start();
                    //Console.WriteLine(dispatchQueue.Count);
                }
            });
            t.Start();
        }
        public async void CatFactAsync()
        {
            dispatchQueue.Add(new Thread(() =>
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://catfact.ninja/fact").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(responseBody);
                var catFact = parsedObject["fact"].ToString();
                State = catFact;
                Type = "Cat Fact";
            }));
        }
        public async void BoredAsync()
        {
            var task = new Thread(() =>
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://www.boredapi.com/api/activity").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(responseBody);
                var Bored = parsedObject["activity"].ToString();
                State = Bored;
                Type = "What to do";
            });
            dispatchQueue.Add(task);
        }
        public async void IpAsync()
        {
            dispatchQueue.Add(new Thread(() =>
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync("https://api.ipify.org/?format=json").Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var parsedObject = JObject.Parse(responseBody);
                var Bored = parsedObject["ip"].ToString();
                State = Bored;
                Type = "IP";
            }));
        }
        public override string ToString()
        {
            return $"{Type}:\n{State}";
        }
    }
}
