using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class JsonDataProvider
    {
        private static string GetDataFolder()
        {
            var projectPath = ScreenshotHelper.GetPathFromProject();
            // now use Resource/Data as requested by user
            return Path.Combine(projectPath, "Resource", "Data");
        }

        public static IEnumerable<TestCaseData> GetContactUsData()
        {
            var dataPath = Path.Combine(GetDataFolder(), "contactData.json");
            if (!File.Exists(dataPath)) yield break;
            var json = File.ReadAllText(dataPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<ContactData[]>(json, options);
            if (items == null) yield break;
            foreach (var item in items) yield return new TestCaseData(item).SetName($"ContactUs_{item.name}");
        }

        public static IEnumerable<TestCaseData> GetRegisterData()
        {
            var dataPath = Path.Combine(GetDataFolder(), "registerData.json");
            if (!File.Exists(dataPath)) yield break;
            var json = File.ReadAllText(dataPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<RegisterData[]>(json, options);
            if (items == null) yield break;
            foreach (var item in items) yield return new TestCaseData(item).SetName($"Register_{item.email}");
        }

        public static IEnumerable<TestCaseData> GetNewsletterData()
        {
            var dataPath = Path.Combine(GetDataFolder(), "newsletterData.json");
            if (!File.Exists(dataPath)) yield break;
            var json = File.ReadAllText(dataPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<NewsletterData[]>(json, options);
            if (items == null) yield break;
            foreach (var item in items) yield return new TestCaseData(item).SetName($"Subscribe_{item.email}");
        }
    }
}
