using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutomationPracticeDemo.Tests.Models;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class DataHelper
    {
        public static IEnumerable<UserData> GetUsers()
        {
            // Ruta relativa dentro del bin
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Resource", "DataTest", "DataAccountInfo.json");

            var jsonData = File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<List<UserData>>(jsonData);
        }
    }
}





