using System;
using System.Collections.Generic;
using System.Text;


namespace PizzaBox.Domain
{
    #region: Old Code
    //Dictionary should be made dynamically from database, any additions or subtractions happen through database

    //public class Pizza
    //{
    //        1 Pizza has:
    //        1 crust type, cost
    //        1 pan size, cost
    //        up to 5 toppings, cost
    //        total cost

    //        public string crustType { get; set; }
    //        public string panSize { get; set; }

    //        public KeyValuePair<string, decimal> crustType;
    //        public KeyValuePair<string, decimal> panSize;
    //        public int toppingAmount { get; set; }
    //        public decimal totalToppingCost;
    //        private const int toppingLimit = 5;

    //        public List<string> toppings;
    //        public decimal totalCost;

    //        example GetItemCost(crustType, crustDictionary)
    //        returns price of that crust
    //        public decimal GetItemCost(string itemName, Dictionary<string, decimal> itemDictionary)
    //        {
    //            return itemDictionary[itemName];
    //        }

    //        public void test()
    //        {
    //            panSize = new KeyValuePair<string, decimal>("", 1);
    //            string thing = panSize.Key;
    //        }

    //        public decimal CalculateToppingsCost()
    //        {
    //            return 1;
    //        }

    //        public decimal CalculateTotalCost()
    //        {
    //            decimal crustCost, panSizeCost, toppingsCost;
    //            crustCost = GetItemCost(crustType, crustDictionary);
    //            panCost = GetItemCost(crustType, panSizeDictionary);
    //            toppingsCost = GetItemCost();

    //            return crustCost + panCost + toppingsCost;

    //            return 1;
    //        }
    //        public object[] crust = {
    //            crustType,
    //        };
    //        public decimal crustCost { get; set; }
    //        public decimal[] crustCost[] 
    //        {
    //            get { return crustCost; }
    //}
    //public Dictionary<string, decimal> crustType { get; set; }

    //concurrentdictionary maybe? for thread safety
    //        public Dictionary<string, decimal> crustType =
    //        dictionary keys have to be unique, as long as string matches key,
    //        the value should be accessible

    //        public void SetCrustType(string crustType)
    //{

    //}

    //public decimal GetCrustCost(string crustName)
    //{
    //    return 1;
    //}

    //public decimal CalculateTotalCost()
    //{
    //    return 1;
    //}
    //}
    #endregion

    public class BYOPizza
    {
        //1 Pizza has:
        //1 crust type, cost
        //1 pan size, cost
        //up to 3 toppings, cost
        //total cost

        //dictionary will contain topping name , and topping price 
        public Dictionary<string,decimal> toppingsOnPizza = new Dictionary<string,decimal>();
        //consider using k,v data types for crust and size
        //public KeyValuePair<string, decimal> crustTypeKV;

        //console key will move to UI afterward
        public ConsoleKeyInfo keyInfo;
        public string crustType { get; set; }
        public decimal crustCost { get; set; }
        public string panSize { get; set; }
        public decimal panCost { get; set; }
        //public  int  toppingCount;
        
        public decimal totalPrice { get; set; }

        public BYOPizza()
        {
        }

        public BYOPizza(string crustType, string panSize)
        {
            this.crustType = crustType;
            this.panSize = panSize;
        }

        //select pan size 
        public void SelectPanSize(KeyValuePair<string,decimal> panSize)
        {
            //spit out all pan sizes from db
        }
        //select crust type
        public void SelectCrustType()
        {
            //spit out all crust types from db
        }

        public bool isMaxTopping() {
            if (toppingsOnPizza.Count >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddTopping()
        {
            if (isMaxTopping())
            {
                Console.WriteLine("Reached Topping Limit");
            }
            else
            {
                do
                {
                    Console.Write("Would you like to add a topping(y/n)? ");

                    keyInfo = Console.ReadKey();

                    Console.WriteLine();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Y:
                            toppingsOnPizza.Add("", 1);
                            Console.WriteLine("topping added");
                            break;
                        case ConsoleKey.N:
                            Console.WriteLine("topping NOT added");
                            break;
                        default:
                            Console.WriteLine("Invalid input, try again");
                            break;
                    }

                    Console.WriteLine($"you have {toppingsOnPizza.Count} toppings on your pizza");
                    
                } while (!isMaxTopping());
                 
            }
        }

        public void CalculatePizzaTotal()
        {
            totalPrice  = crustCost + panCost;
        }
        
    }
}
