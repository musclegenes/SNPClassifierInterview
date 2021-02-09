using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SNPClassifier.Models;
using SNPClassifier.Services;
using SNPClassifier.Utilities;

namespace SNPClassifier.Controllers
{
    public class ClassificationController : Controller
    {
        public ClassificationController()
        {
            
        }


        public async Task<IActionResult> Index()
        {
            var model = new List<SnpClassificationViewModel>();
            
            for (var i = 1; i <= 20; i++)
            {
                var snpData = await GetSnpData(i);
                var lifeStyleData = await GetLifeStyleData(i);

                var profileService = new ProfileService(i, snpData, lifeStyleData);
                model.AddRange(profileService.GetClassifications());
            }

            model = ProfileService.CalculatePercentageShare(model);

            return View(model);
        }


        public IActionResult Example()
        {
            var model = new List<SnpClassificationViewModel>();
            for (var i = 0; i < 5; i++)
            {
                model.AddRange(SnpClassificationGenerator.GenerateRandomProfile());
            }
            
            return View("Index",model);
        }


        public async Task<List<SnpDataLine>> GetSnpData(int profileId)
        {
            var client = new RestClient("https://interview.fitnessgenes.com/api/");
            var request = new RestRequest("snp/profiles/{profileId}");
            request.AddUrlSegment("profileId", profileId);
            var data = await client.GetAsync<List<SnpDataLine>>(request);

            return data;
        }
        
        
        public async Task<List<LifestyleDataLine>> GetLifeStyleData(int profileId)
        {
            var client = new RestClient("https://interview.fitnessgenes.com/api/");
            var request = new RestRequest("lifestyle/profiles/{profileId}");
            request.AddUrlSegment("profileId", profileId);
            var data = await client.GetAsync<List<LifestyleDataLine>>(request);

            return data;
        }
            
        
    }
    
    public class SnpDataLine    
    {
        public string Id { get; set; } 
        public string Snp { get; set; } 
        public string SnpValue { get; set; }
    }
    
    public class LifestyleDataLine    
    {
        public string Id { get; set; } 
        public string Attribute { get; set; } 
        public string AttributeValue { get; set; } 
    }
}