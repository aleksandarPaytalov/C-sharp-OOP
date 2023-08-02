using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;

            IBooth booth = new Booth(boothId, capacity);
            this.booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        } // it works 100%

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            if (booths.Models.Any(b => b.CocktailMenu.Models.Any(cn => cn.Name == cocktailName && cn.Size == size)))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            ICocktail cocktail;
            if (cocktailTypeName == nameof(Hibernation))
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else
            {
                cocktail = new MulledWine(cocktailName, size);
            }

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.CocktailMenu.AddModel(cocktail);
            
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }// it works 100 %

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.Any(b => b.DelicacyMenu.Models.Any(d => d.Name == delicacyName)))  //
            {
               return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy;

            if (delicacyTypeName == nameof(Gingerbread))
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else
            {
                delicacy = new Stolen(delicacyName);
            }

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }// it works 100%

        public string BoothReport(int boothId)
        {          
            var currentBooth = booths.Models.FirstOrDefault(bi => bi.BoothId == boothId);

            return currentBooth.ToString().TrimEnd();
        } // checked work 100%;

        public string LeaveBooth(int boothId) // checked work 100%
        {
            StringBuilder sb = new StringBuilder();

            var currentBooth = this.booths.Models.FirstOrDefault(bi => bi.BoothId == boothId);
            //var currentBill = currentBooth.CurrentBill;
            sb.AppendLine($"Bill {currentBooth.CurrentBill:f2} lv");

            currentBooth.Charge();
            currentBooth.ChangeStatus();
            
            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople) // checked work 100%
        {
            var boothsOrdered = booths.Models.Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .OrderBy(bc => bc.Capacity).ThenByDescending(bi => bi.BoothId);

            var firsBoothInTheList = boothsOrdered.FirstOrDefault();
            if (firsBoothInTheList == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            else
            {
                firsBoothInTheList.ChangeStatus();            
            }
               
            return string.Format(OutputMessages.BoothReservedSuccessfully, firsBoothInTheList.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] cmd = order.Split("/", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string itemTypeName = cmd[0];
            string itemName = cmd[1];
            int countOrdered = int.Parse(cmd[2]);
            
            var currentBooth = booths.Models.FirstOrDefault(bi => bi.BoothId == boothId);
            
            if (itemTypeName != nameof(MulledWine) && itemTypeName != nameof(Hibernation)
               && itemTypeName != nameof(Gingerbread) && itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }
            
            var currentCocktail = currentBooth.CocktailMenu.Models.FirstOrDefault(i => i.Name == itemName);
            var currentDelicacy = currentBooth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName);
            
            if (currentCocktail == null && currentDelicacy == null)
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);                
            }
            
            if (itemTypeName == nameof(MulledWine) || itemTypeName == nameof(Hibernation))
            {
                string size = cmd[3];
            
                var cocktailWanted = currentBooth.CocktailMenu.Models.FirstOrDefault(c => c.GetType().Name == itemTypeName 
                && c.Name == itemName && c.Size == size); // защото валидацията за името я има вече горе
                //var currentCocktailExist = currentBooth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);
            
                if (cocktailWanted == null)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, size, itemName);
                }
                else
                {
                    double currentBillIncrease = cocktailWanted.Price * countOrdered;
                    currentBooth.UpdateCurrentBill(currentBillIncrease);
                    return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOrdered, itemName);
                }
            }
            else 
            {
                IDelicacy desiredDelicacy = currentBooth.DelicacyMenu.Models.FirstOrDefault(m => m.GetType().Name == itemTypeName && m.Name == itemName);

                if (desiredDelicacy == null)
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemName);
                }

                double currentBillIncrease = currentDelicacy.Price * countOrdered;
                currentBooth.UpdateCurrentBill(currentBillIncrease);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOrdered, itemName);
            }
            
        } // looks like it works.
    }
}
