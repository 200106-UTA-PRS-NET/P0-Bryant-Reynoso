using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using PizzaBox.Domain;
using PizzaBox.Domain.Models;
using System.Linq;

namespace PizzaBox.Client
{
    public class OrderPizza
    {
        PizzaDbContext db = new PizzaDbContext();
        public static ConsoleKeyInfo keyInfo;
        public string str_signIn = "Sign In";
        public string str_signUp = "Sign up";
        public string username, password;

        //1
        //first methods that get called
        #region: Signin and Register Methods

        public void ChooseSigning()
        {
            bool thisPage = true;

            while (thisPage)
            {
                Console.Write("Type '1' for Sign in or '2' for Sign up: ");
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        thisPage = false;
                        GoToSignPage(str_signIn);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        thisPage = false;
                        GoToSignPage(str_signUp);
                        break;
                    default:
                        Console.Clear();
                        thisPage = true;
                        Console.WriteLine("Invalid Input, try again");
                        Thread.Sleep(1700);
                        Console.Clear();
                        break;
                }
            }

        }

        public void GoToSignPage(string str_sign)
        {
            Console.Clear();
            bool thisPage = true;

            while (thisPage)
            {
                Console.Write($"Press 'Enter' to {str_sign} or 'Backspace' to go back: ");

                keyInfo = Console.ReadKey();

                Console.Clear();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        thisPage = false;
                        
                        if (str_sign == str_signIn)
                        { 
                            SignInUser( );
                        }
                        else
                        { 
                            RegisterNewUser( );
                        }
                        break;
                    case ConsoleKey.Backspace:
                        thisPage = false;
                        ChooseSigning();
                        break;
                    default:
                        Console.Clear();
                        thisPage = true;
                        Console.WriteLine("Invalid Input, try again");
                        Thread.Sleep(1700);
                        Console.Clear();
                        break;
                }
            }
        }
        
        public void RegisterNewUser()
        {
            Register:
            Login register = new Login();
            Console.WriteLine($"Sign Up below");

            Console.WriteLine();
             
            Console.Write("username: ");
            username = Console.ReadLine();

            Console.Write("password: ");
            password = Console.ReadLine();
             
            //if username already exists
            if (register.CheckIfUserNameExists(username))
            {
                Console.WriteLine();
                Console.WriteLine("user already exists, press 'Backspace' to go back or press any other key to try again");
                keyInfo = Console.ReadKey();

                Console.Clear();
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    ChooseSigning();
                }
                else {
                    //goto statement
                    goto Register;
                }
            }
            else //if username does not exist
            {
                //register
                Users userToRegister = new Users()
                {
                    Username = username,
                    Pass = password,
                    IsEmployee = false
                };

                register.RegisterNewUser(userToRegister);

                Console.WriteLine("Registration Complete");
                Thread.Sleep(1200);
                //pass registered user id to userprompts
                UserPrompts(register.GetUserIdByName(userToRegister.Username));
            } 
             
        }

        public void SignInUser()
        {
            SignIn:
            Login login = new Login();

            Console.WriteLine($"Sign In below");

            Console.WriteLine();

            Console.Write("username: ");
            username = Console.ReadLine();

            Console.Write("password: ");
            password = Console.ReadLine();
             
            if (login.CheckIfUserExists(username, password))
            {
                Console.WriteLine($"Login successful, welcome '{username}'");

                Console.WriteLine($"user Id is {login.userId}");
                //check if isEmployee, else give user prompts
                //pass userId
                if (login.CheckIfEmployee(login.userId))
                {
                    Console.WriteLine("is employee");
                    
                    EmployeePrompts();
                }
                else {
                    Console.WriteLine("is customer");
                    UserPrompts(login.userId);
                }
                
                
                
            }
            else //if not try again, or go back
            {
                Console.WriteLine();
                Console.WriteLine("either username or password is incorrect, press 'Backspace' to go back or any other key to try again");
                keyInfo = Console.ReadKey();

                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    ChooseSigning();
                }
                else
                {
                    //goto statement 
                    goto SignIn;
                }
            }
            
        }

        public void Logout()
        {
            Console.Clear();

            username = "";
            password = "";

            ChooseSigning();
        }

        #endregion
        
        //2(user)
        //comes from user/employee signin or register
        #region: User Prompts
        public void UserPrompts(int userId)
        {
            
            Login login = new Login();

            Console.Clear();
            Console.WriteLine($"Welcome '{login.GetUserNameById(userId)}'");
            Console.WriteLine();

            Choose:

            //select between order history, orderpizza, and logout
            Console.WriteLine("Press '1' to view order history");
            Console.WriteLine("Press '2' to order pizza");
            Console.WriteLine("Press '3' to logout");
            keyInfo = Console.ReadKey();

            Console.Clear();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.WriteLine("selected view Order History");
                    ViewUserOrders(userId);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.WriteLine("selected Order Pizza");
                    ChooseLocation(userId);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.WriteLine("selected Logout");
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    Console.Clear();
                    goto Choose;
            }
        }
        #endregion

        //2(employee)
        #region: Employee Prompts

        public void EmployeePrompts()
        {
            EmployeeTry:
            Console.Clear();

            //select between order history, orderpizza, and logout
            Console.WriteLine("Press '1' to view order history");
            //Console.WriteLine("Press '2' to view inventory");
            Console.WriteLine("Press '2' to logout");
            keyInfo = Console.ReadKey();

            Console.Clear();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1: 
                    ViewStoreOrders();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.WriteLine("selected Logout");
                    Logout();
                    break;
                default:
                    Console.WriteLine("invalid input, try again");
                    goto EmployeeTry; 
            }
        }

        #endregion

        //3(employee and user)
        //comes from user,employee prompts selection
         
        #region: Location,Order History

        public void ChooseLocation(int userId)
        {
            SelectStore: //JUMP LABEL
            
            Console.Clear();
             
            //tryAgain:
            Console.Write($"Press 'Backspace' to go back or any other key to continue: ");
        
            keyInfo = Console.ReadKey();

            Console.Clear();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Backspace:
                    UserPrompts(userId);
                    break;
                default:
                    Dictionary<int, string> stores = new Dictionary<int, string>();
                    List<int> realIds = new List<int>(); //stores real Id's to be passed 
                    int input = 0;
                    int index = 0;
                    stores.Clear(); //clears so that when it'd added to there are no exceptions
                    realIds.Clear();


                    Console.WriteLine("Select from one of the stores below");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();

                    var query = from s in db.Stores select s;

                    foreach (var store in query)
                    { 
                        index++;

                        Console.WriteLine($"Press '{index}' for {store.StoreAddress}");
                        stores.Add(index, store.StoreAddress); 

                        //add id from database 
                        realIds.Add(store.Id);
                    }
                    //=========================================

                    Console.WriteLine(); 
                    //use try catch for empty values
                    try
                    {
                        Console.Write("Which store? ");
                        input = Convert.ToInt32(Console.ReadLine());

                        if (stores.ContainsKey(input))
                        {
                            Console.WriteLine($"selected store '{stores[input]}'");
                            //pass the original store id to order pizza
                            //call a method that startsOrder and pass it store id from realIds at index of 'index' -1?

                            var selectedStore = db.Stores.FirstOrDefault(s => s.Id == realIds[input-1]);
                            Console.WriteLine(selectedStore.StoreAddress);

                            //Move to Order
                            StartOrder(realIds[input-1],userId); 
                        }
                        else
                        {
                            //try again
                            Console.WriteLine("not valid store, try again");
                            Thread.Sleep(1700);
                            Console.Clear();

                            //goto statement
                            goto SelectStore;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("type a corresponding number, try again");
                        Thread.Sleep(1500);
                        Console.Clear();

                        goto SelectStore; //JUMP TO STATEMENT
                    }
                    break;
            }
        }

        public void ViewStoreOrders()
        {
            Console.Clear();

            Console.Write($"Press 'Backspace' to go back home or any other key to continue: ");

            keyInfo = Console.ReadKey();

            Console.Clear();

            switch (keyInfo.Key)
            {

                case ConsoleKey.Backspace:
                    EmployeePrompts();
                    break;
                default:
                    Console.WriteLine("Store Order History");
                    //fetch the data from order history where id matches
                    Console.WriteLine();

                    if (db.Orders.Any())
                    {
                        var query = db.Orders;
                        foreach (var order in query)
                        { 
                            Console.WriteLine($" User: {order.Userid}, Store: {order.Storeid}, Time: {order.TimeOrdered},  Pizzas: {order.Pizzas}   Total: ${order.Total}");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();

                        EmployeePrompts();
                    }
                    else
                    {
                        Console.WriteLine("no orders to show");

                        Console.WriteLine();
                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();
                    }
                    break;
            }
            //fetch all data from order history
        }

        public void ViewUserOrders(int userId)
        {
            Console.Clear();
            
            Console.Write($"Press 'Backspace' to go back home or any other key to continue: ");

            keyInfo = Console.ReadKey();

            Console.Clear();

            switch (keyInfo.Key)
            {
               
                case ConsoleKey.Backspace:
                    UserPrompts(userId);
                    break;
                default:
                    Console.WriteLine("Order History");
                    Console.WriteLine();
                    //fetch the data from order history where id matches

                    if (db.Orders.Any(u => u.Userid == userId))
                    {
                        var query = db.Orders.Where(u => u.Userid == userId);
                        foreach (var order in query)
                        {
                            Console.WriteLine($"Store: {order.Storeid}, Time: {order.TimeOrdered},  Pizzas: {order.Pizzas}   Total: ${order.Total}");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();

                        UserPrompts(userId);
                    }
                    else
                    {
                        Console.WriteLine("no orders to show");

                        Console.WriteLine();
                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();

                        UserPrompts(userId);
                    }
                   
                    break;
            }
           
        }
        #endregion

         
        //4
        //comes from choose store
        #region: Pizza Ordering Methods
 
        public void StartOrder(int storeId, int userId)
        {
            Order order = new Order(storeId, userId);

            StartPizza(order);
            
        }

        private void StartPizza(Order order)
        {
            Pizza pizza = new Pizza();

            pizza.panSize = ChooseSize(order);
            Console.Clear();

            pizza.crustType = ChooseCrust(order);

            Console.Clear();

            //ChooseCustomOrMade(order, pizza);

            OrderPremade(order, pizza);
        }

        public KeyValuePair<string,decimal> ChooseSize(Order order)
        {
            SelectSize:
            Console.Clear();
            Pizza pizza = new Pizza();
            Dictionary<int, string> panSizes = new Dictionary<int, string>();
            List<int> realIds = new List<int>();
            int input = 0;
            int index = 0;
            panSizes.Clear(); //clears so that when it'd added to there are no exceptions
            realIds.Clear();


            Console.WriteLine("Select from one of the sizes below");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine();

            var sizes = from row in db.PanSizes
                        select row;

            foreach (var pan in sizes)
            {
                index++;

                Console.WriteLine($"Press '{index}' for a {pan.Size} pizza");
                panSizes.Add(index, pan.Size);

                //add id from database 
                realIds.Add(pan.Id);

            }

            Console.WriteLine();
            //use try catch for empty values
            try
            {
                Console.Write("Which size? ");
                input = Convert.ToInt32(Console.ReadLine());

                if (panSizes.ContainsKey(input))
                {
                    Console.WriteLine($"selected size '{panSizes[input]}'");

                    var selectedSize = db.PanSizes.FirstOrDefault(s => s.Id == realIds[input - 1]);
                    
                    //return keyvalue pair for size
                    return new KeyValuePair<string, decimal>(selectedSize.Size, selectedSize.Price);
                    
                }
                else
                {
                    //try again
                    Console.WriteLine("not valid selection, try again");
                    Thread.Sleep(1700);
                    Console.Clear();

                    //goto statement
                    goto SelectSize;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                Console.WriteLine("type a corresponding number, try again");
                Thread.Sleep(1500);
                Console.Clear();

                goto SelectSize; //JUMP TO STATEMENT
            }


            //pizza.setPanSize();
        }

        public KeyValuePair<string, decimal> ChooseCrust(Order order)
        {
            SelectCrust:
            Pizza pizza = new Pizza();
            Dictionary<int, string> crustTypes = new Dictionary<int, string>();
            List<int> realIds = new List<int>();
            int input = 0;
            int index = 0;
            crustTypes.Clear(); //clears so that when it'd added to there are no exceptions
            realIds.Clear();


            Console.WriteLine("Select from one of the crust types below");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine();

            var crust = from row in db.CrustTypes
                        select row;


            foreach (var type in crust)
            {
                index++;

                Console.WriteLine($"Press '{index}' for {type.CrustName} Crust");
                crustTypes.Add(index, type.CrustName);

                //add id from database 
                realIds.Add(type.Id);

            }


            Console.WriteLine();
            //use try catch for empty values
            try
            {
                Console.Write("Which crust? ");
                input = Convert.ToInt32(Console.ReadLine());

                if (crustTypes.ContainsKey(input))
                {
                    Console.WriteLine($"selected crust '{crustTypes[input]}'");

                    var selectedCrust = db.CrustTypes.FirstOrDefault(s => s.Id == realIds[input - 1]);
                    Console.WriteLine();

                    
                    return new KeyValuePair<string, decimal>(selectedCrust.CrustName, selectedCrust.Price);
                }
                else
                {
                    //try again
                    Console.WriteLine("not valid selection, try again");
                    Thread.Sleep(1700);
                    Console.Clear();

                    //goto statement
                    goto SelectCrust;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("type a corresponding number, try again");
                Thread.Sleep(1500);
                Console.Clear();

                goto SelectCrust; //JUMP TO STATEMENT
            }
        }

        //pizza object has to be passed to all pizza order methods
        //public void ChooseCustomOrMade(Order order, Pizza pizza)
        //{
        //    pizzaChoice:
        //    Console.WriteLine("Press '1' to choose from our premade pizzas");
        //    Console.WriteLine("Press '2' to build your own pizza");

        //    keyInfo = Console.ReadKey();

        //    Console.Clear();

        //    switch (keyInfo.Key)
        //    {
        //        case ConsoleKey.D1:
        //        case ConsoleKey.NumPad1:
        //            OrderPremade(order,pizza);
        //            break;
        //        case ConsoleKey.D2:
        //        case ConsoleKey.NumPad2:
        //            OrderCustom(order,pizza);
        //            break;
        //        default:
        //            Console.WriteLine("invalid input, try again");
        //            Thread.Sleep(1300);
        //            Console.Clear();
        //            goto pizzaChoice;
        //    }
        //}
         
        private void OrderPremade(Order order, Pizza pizza)
        {
            SelectMade: 
            Dictionary<int, string> ourPizzas = new Dictionary<int, string>();
            List<int> realIds = new List<int>();
            int input = 0;
            int index = 0;
            ourPizzas.Clear(); //clears so that when it'd added to there are no exceptions
            realIds.Clear();

            var premade = from row in db.OurPizzas
                          select row;

            Console.WriteLine("Select from one of the pizzas below");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine();

            foreach (var p in premade)
            { 
                index++;

                Console.WriteLine($"Press '{index}' for {p.PizzaName} ");
                ourPizzas.Add(index, p.PizzaName);

                //add id from database 
                realIds.Add(p.Id);
            }

            Console.WriteLine();
            //use try catch for empty values
            try
            {
                Console.Write("Which pizza? ");
                input = Convert.ToInt32(Console.ReadLine());

                if (ourPizzas.ContainsKey(input))
                {
                    Console.WriteLine($"selected pizza '{ourPizzas[input]}'"); 

                    var selectedPizza = db.OurPizzas.FirstOrDefault(s => s.Id == realIds[input - 1]);
                    Console.WriteLine();

                    pizza.pizzaName = selectedPizza.PizzaName;
                    pizza.pizzaNameCost = selectedPizza.Price;
                    //finalize pizza
                    FinishPizza(order,pizza);
                     
                }
                else
                {
                    //try again
                    Console.WriteLine("not valid selection, try again");
                    Thread.Sleep(1700);
                    Console.Clear();

                    //goto statement
                    goto SelectMade;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("type a corresponding number, try again");
                Thread.Sleep(1500);
                Console.Clear();

                goto SelectMade; //JUMP TO STATEMENT
            }
        }

        //private void OrderCustom(Order order, Pizza pizza)
        //{
        //    //ChooseSize(order);

        //    //Console.Clear();

        //    //ChooseCrust(order);

        //    //ChooseToppings(order);

        //    Console.ReadKey();
        //}

        private void FinishPizza(Order order, Pizza pizza)
        {
            question: 
            Console.Write("Would you like to add this pizza to your order(y/n)? ");

            keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Y:
                    order.AddPizza(pizza); 
                    Console.Clear();
                    Console.WriteLine($"Pizzas in your order: ");
                    Console.WriteLine();
                    foreach (var p in order.pizzasInOrder)
                    { 
                        Console.WriteLine($"-{p.panSize.Key} {p.crustType.Key} Crust, {p.pizzaName}: ${p.CalculateTotal()}");
                    }

                    Console.WriteLine();
                    PromptForMore(order,pizza);
                    break;
                case ConsoleKey.N:
                    Console.Clear();
                    StartPizza(order);
                    break;
                default:
                    Console.WriteLine("Try again");
                    goto question;
            }
        }

        private void PromptForMore(Order order, Pizza pizza)
        {
            Prompt:
            Console.WriteLine("Press '1' to add another pizza or '2' to Finish Order");

            keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear(); 
                    StartPizza(order);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    FinishOrder(order,pizza);
                    break;
                default:
                    goto Prompt;
            } 
        }
         
        public void FinishOrder(Order order, Pizza pizza)
        {
            Orders saveOrder = new Orders(); 
            List<string> pizzaNames = new List<string>();
            string combinedString;

            foreach (var p in order.pizzasInOrder)
            {
                 pizzaNames.Add(p.pizzaName);
            }
            combinedString =  string.Join(",",pizzaNames);
            

            saveOrder.Pizzas = combinedString;
            saveOrder.Userid = order.userId;
            saveOrder.Storeid = order.storeId;
            saveOrder.TimeOrdered = DateTime.Now;
            saveOrder.Total = order.CalculateTotal();

            Console.WriteLine($"pizzas: {saveOrder.Pizzas}");
            Console.WriteLine($"userId: {saveOrder.Userid}");
            Console.WriteLine($"storeId: {saveOrder.Storeid}");
            Console.WriteLine($"Time Ordered: {saveOrder.TimeOrdered}"); 
            Console.WriteLine($"Order Total: ${(decimal)saveOrder.Total}");

            
            db.Orders.Add(saveOrder);
            db.SaveChanges();

            UserPrompts(order.userId);


            Console.ReadKey();
        }


        //private void ChooseToppings(Order order)
        //{
        //   // throw new NotImplementedException();
        //}
        #endregion

    }
}