using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SNPClassifier.Controllers;
using SNPClassifier.Models;

namespace SNPClassifier.Services
{
    public class ProfileService
    {
        private readonly int _profileId;
        private readonly List<SnpDataLine> _snpDataLines;
        private readonly List<LifestyleDataLine> _lifestyleDataLines;
        private List<SnpClassificationViewModel> SnpClassificationViewModels { get; set; }

        public ProfileService(int profileId, List<SnpDataLine> snpDataLines, List<LifestyleDataLine> lifestyleDataLines)
        {
            _profileId = profileId;
            _snpDataLines = snpDataLines;
            _lifestyleDataLines = lifestyleDataLines;
            SnpClassificationViewModels = new List<SnpClassificationViewModel>();
        }


        public List<SnpClassificationViewModel> GetClassifications()
        {
            LactoseIntolerance();
            FruitColourTolerance();
            FoodRecommendation();

            return SnpClassificationViewModels;
        }


        public static List<SnpClassificationViewModel> CalculatePercentageShare(List<SnpClassificationViewModel> myModels)
        {
            var lactoseIntolerant = 0;
            var lactoseTolerant = 0;
            var lactoseNoMatch = 0;
            var fruitAll = 0;
            var fruitRedsAndYellow = 0;
            var fruitYellow = 0;
            var fruitNone = 0;
            var fruitNoMatch = 0;
            var foodMeat = 0;
            var foodVeg = 0;
            var foodFish = 0;
            var foodNoMatch = 0;

            foreach (var model in myModels)
            {
                switch (model.Name)
                {
                    case "LACTOSE_INTOLERANCE" when model.Result == "LACTOSE_INTOLERANT":
                        lactoseIntolerant += 1;
                        break;
                    case "LACTOSE_INTOLERANCE" when model.Result == "LACTOSE_TOLERANT":
                        lactoseTolerant += 1;
                        break;
                    case "LACTOSE_INTOLERANCE" when model.Result == "NO_MATCH":
                        lactoseNoMatch += 1;
                        break;
                    case "FRUIT_COLOUR_TOLERANCE" when model.Result == "ALL":
                        fruitAll += 1;
                        break;
                    case "FRUIT_COLOUR_TOLERANCE" when model.Result == "REDS_AND_YELLOW":
                        fruitRedsAndYellow += 1;
                        break;
                    case "FRUIT_COLOUR_TOLERANCE" when model.Result == "YELLOW":
                        fruitYellow += 1;
                        break;
                    case "FRUIT_COLOUR_TOLERANCE" when model.Result == "NONE":
                        fruitNone += 1;
                        break;
                    case "FRUIT_COLOUR_TOLERANCE" when model.Result == "NO_MATCH":
                        fruitNoMatch += 1;
                        break;
                    case "FOOD_RECOMMENDATION" when model.Result == "MEAT":
                        foodMeat += 1;
                        break;
                    case "FOOD_RECOMMENDATION" when model.Result == "VEGETARIAN":
                        foodVeg += 1;
                        break;
                    case "FOOD_RECOMMENDATION" when model.Result == "FISH":
                        foodFish += 1;
                        break;
                    case "FOOD_RECOMMENDATION" when model.Result == "NO_MATCH":
                        foodNoMatch += 1;
                        break;
                }
                
            }

            var lactoseIntolerantPerShare = (Convert.ToDouble(lactoseIntolerant) / 20) * 100;
            var lactoseTolerantPerShare = Convert.ToDouble(lactoseTolerant) / 20 * 100;
            var lactoseNoMatchPerShare = Convert.ToDouble(lactoseNoMatch) / 20 * 100;
            var fruitAllPerShare = Convert.ToDouble(fruitAll) / 20 * 100;
            var fruitRedsAndYellowPerShare = Convert.ToDouble(fruitRedsAndYellow) / 20 * 100;
            var fruitYellowPerShare =  Convert.ToDouble(fruitYellow) / 20 * 100;
            var fruitNoMatchPerShare = Convert.ToDouble(fruitNoMatch) / 20 * 100;
            var fruitNonePerShare = Convert.ToDouble(fruitNone) / 20 * 100;
            var foodMeatPerShare = Convert.ToDouble(foodMeat) / 20 * 100;
            var foodVegPerShare = Convert.ToDouble(foodVeg) / 20 * 100;
            var foodFishPerShare = Convert.ToDouble(foodFish) / 20 * 100;
            var foodNoMatchPerShare = Convert.ToDouble(foodNoMatch) / 20 * 100;

            foreach (var model in myModels)
            {
                model.PercentageShare = model.Name switch
                {
                    "LACTOSE_INTOLERANCE" when model.Result == "LACTOSE_INTOLERANT" => lactoseIntolerantPerShare,
                    "LACTOSE_INTOLERANCE" when model.Result == "LACTOSE_TOLERANT" => lactoseTolerantPerShare,
                    "LACTOSE_INTOLERANCE" when model.Result == "NO_MATCH" => lactoseNoMatchPerShare,
                    "FRUIT_COLOUR_TOLERANCE" when model.Result == "ALL" => fruitAllPerShare,
                    "FRUIT_COLOUR_TOLERANCE" when model.Result == "REDS_AND_YELLOW" => fruitRedsAndYellowPerShare,
                    "FRUIT_COLOUR_TOLERANCE" when model.Result == "YELLOW" => fruitYellowPerShare,
                    "FRUIT_COLOUR_TOLERANCE" when model.Result == "NONE" => fruitNonePerShare,
                    "FRUIT_COLOUR_TOLERANCE" when model.Result == "NO_MATCH" => fruitNoMatchPerShare,
                    "FOOD_RECOMMENDATION" when model.Result == "MEAT" => foodMeatPerShare,
                    "FOOD_RECOMMENDATION" when model.Result == "VEGETARIAN" => foodVegPerShare,
                    "FOOD_RECOMMENDATION" when model.Result == "FISH" => foodFishPerShare,
                    "FOOD_RECOMMENDATION" when model.Result == "NO_MATCH" => foodNoMatchPerShare,
                    _ => model.PercentageShare
                };
            }

            return myModels;
        }
        
        
        public void LactoseIntolerance()
        {
            var snpClassificationModel = new SnpClassificationViewModel
            {
                ProfileId = _profileId.ToString(),
                Name = "LACTOSE_INTOLERANCE",
                Result = "NO_MATCH"
            };
            
            if (_snpDataLines.Any(x => string.Equals(x.Snp, "rs4988235", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "CC")) snpClassificationModel.Result = "LACTOSE_INTOLERANT";
            if (_snpDataLines.Any(x => string.Equals(x.Snp, "rs4988235", StringComparison.OrdinalIgnoreCase) && (x.SnpValue == "CT" || x.SnpValue == "TT"))) snpClassificationModel.Result = "LACTOSE_TOLERANT";
            
            SnpClassificationViewModels.Add(snpClassificationModel);
        }


        public void FruitColourTolerance()
        {
            var snpClassificationModel = new SnpClassificationViewModel
            {
                ProfileId = _profileId.ToString(),
                Name = "FRUIT_COLOUR_TOLERANCE",
                Result = "NO_MATCH"
            };

            if (_snpDataLines.FindAll(x => 
                (string.Equals(x.Snp, "rs1042711", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "AA") || 
                (string.Equals(x.Snp, "rs1801725", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "CC") ||
                (string.Equals(x.Snp, "rs6003484", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "GG") ||
                (string.Equals(x.Snp, "rs9650069", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "GG")).Count == 4) snpClassificationModel.Result = "ALL";
            
            else if (_snpDataLines.FindAll(x => 
                (string.Equals(x.Snp, "RS1042711", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "AA") || 
                (string.Equals(x.Snp, "RS1801725", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "CC") ||
                (string.Equals(x.Snp, "RS6003484", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "AG") ||
                (string.Equals(x.Snp, "RS9650069", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "AG")).Count == 4) snpClassificationModel.Result = "REDS_AND_YELLOW";

            else if (_snpDataLines.FindAll(x => 
                (string.Equals(x.Snp, "RS11950646", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "AG"))
                .Count == 1) snpClassificationModel.Result = "YELLOW";
            
            else if (_snpDataLines.FindAll(x => 
                (string.Equals(x.Snp, "RS11950646", StringComparison.OrdinalIgnoreCase) && (x.SnpValue == "AA" || x.SnpValue == "GG")))
                .Count == 1) snpClassificationModel.Result = "NONE";
            
            SnpClassificationViewModels.Add(snpClassificationModel);
        }


        public void FoodRecommendation()
        {
            var snpClassificationModel = new SnpClassificationViewModel
            {
                ProfileId = _profileId.ToString(),
                Name = "FOOD_RECOMMENDATION",
                Result = "NO_MATCH"
            };

            if (_lifestyleDataLines.Any(x => x.Attribute == "RED_MEAT_INTAKE_WEEK" && int.Parse(x.AttributeValue) > 10)
                && SnpClassificationViewModels.Any(x => x.Name == "FRUIT_COLOUR_TOLERANCE" && x.Result == "NONE")
                && _snpDataLines.Any(x =>
                    string.Equals(x.Snp, "rs9939609", StringComparison.OrdinalIgnoreCase) &&
                    (x.SnpValue == "AT" || x.SnpValue == "TT"))
            )
            {
                snpClassificationModel.Result = "MEAT";
                SnpClassificationViewModels.Add(snpClassificationModel);
                return;
            }

            if (_lifestyleDataLines.Any(x => x.Attribute == "RED_MEAT_INTAKE_WEEK" && int.Parse(x.AttributeValue) < 10)
                && SnpClassificationViewModels.Any(x =>
                    x.Name == "FRUIT_COLOUR_TOLERANCE" &&
                    (x.Result == "YELLOW" || x.Result == "REDS_AND_YELLOW" || x.Result == "NONE"))
                && _snpDataLines.Any(x =>
                    string.Equals(x.Snp, "rs9939609", StringComparison.OrdinalIgnoreCase) && x.SnpValue == "AA")
            )
            {
                snpClassificationModel.Result = "VEGETARIAN";
                SnpClassificationViewModels.Add(snpClassificationModel);
                return;
            }

            if (_lifestyleDataLines.Any(x => x.Attribute == "RED_MEAT_INTAKE_WEEK" && int.Parse(x.AttributeValue) > 10)
                && SnpClassificationViewModels.Any(x =>
                    x.Name == "FRUIT_COLOUR_TOLERANCE" &&
                    (x.Result == "YELLOW" || x.Result == "REDS_AND_YELLOW" || x.Result == "NONE"))
            )
            {
                snpClassificationModel.Result = "FISH";
                SnpClassificationViewModels.Add(snpClassificationModel);
                return;
            }
            
            SnpClassificationViewModels.Add(snpClassificationModel);
        }
    }
}