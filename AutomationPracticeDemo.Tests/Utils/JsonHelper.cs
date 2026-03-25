using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Models;
using Newtonsoft.Json;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class JsonHelper
    {
        public static List<UserData> LoadTestData(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<UserData>>(json);
        }
    }
}

    