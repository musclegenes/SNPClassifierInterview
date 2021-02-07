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


        /// <summary>
        /// The result of the task should be returned here.
        /// The result model should be placed inside the View constructor
        /// </summary>
        /// <returns>List<SnpClassificationViewModel></returns>
        public IActionResult Index()
        {
            //Model return intentionally left blank.
            //This action will nto return a view without error until a model has been passed.
            return View();
        }


        /// <summary>
        /// Example action which uses a randomly generated set of profiles and classifications to show the
        /// resulting output of the classification view 
        /// </summary>
        /// <returns>List<SnpClassificationViewModel></returns>
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