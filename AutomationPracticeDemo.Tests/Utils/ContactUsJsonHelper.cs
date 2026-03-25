using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using AutomationPracticeDemo.Tests.Models;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class ContactUsJsonHelper
    {
        // Método específico para ContactUsModel
        public static List<ContactUsModel> LoadContactUsData(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ContactUsModel>>(json);
        }
    }
}