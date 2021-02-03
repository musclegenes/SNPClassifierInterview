using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SNPClassifier.Models;

namespace SNPClassifier.Utilities
{
    public static class SnpClassificationGenerator
    {
        public static List<SnpClassificationViewModel> GenerateRandomProfile()
        {
            var profile = new List<SnpClassificationViewModel>();
            var random = new Random();
            var profileId = random.Next(0, 100).ToString();
            for (var i = 0; i < 3; i++)
            {
                var model = new SnpClassificationViewModel
                {
                    ProfileId = profileId,
                    Result = GetRandomResultWordFromList(),
                    Name = GetRandomNameWordFromList(),
                    PercentageShare = random.Next(0, 100)
                };
                profile.Add(model);
            }
            return profile;
        }


        private static string GetRandomResultWordFromList()
        {
            var list = new List<string>()
            {
                "Neutral",
                "Good",
                "Bad",
                "Intolerant",
                "Positive",
                "Negative"
            };
            var random = new Random();
            return list[random.Next(6)];
        }

        private static string GetRandomNameWordFromList()
        {
            var list = new List<string>()
            {
                "Lactose",
                "Blood Glucose",
                "LERP",
                "BCAA Rate",
                "Inflammation Risk",
                "NAT 1 Risk"
            };
            var random = new Random();
            return list[random.Next(6)];
        }
    }
}