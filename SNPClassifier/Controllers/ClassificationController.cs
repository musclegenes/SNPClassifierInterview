using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using SNPClassifier.Models;
using SNPClassifier.Utilities;

namespace SNPClassifier.Controllers
{
    public class ClassificationController : Controller
    {
        public ClassificationController()
        {
            
        }


        public IActionResult Index()
        {
            return View();
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
        
    }
}