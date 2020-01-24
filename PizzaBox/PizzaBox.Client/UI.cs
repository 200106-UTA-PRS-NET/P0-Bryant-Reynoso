using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Client
{
    public static class UI
    {
        private static void PageBreak()
        {
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void WelcomeMessage() {
            Console.WriteLine("    Welcome to Pizzeria");
            Console.WriteLine("---------------------------");

            PageBreak();
        }

        public static void ChooseSigningPrompt()
        {
            WelcomeMessage();

            //Console.WriteLine("          TYPE:");
            Console.WriteLine("    '1'            '2'");
            Console.WriteLine(" ---------	---------");
            Console.WriteLine("| Sign In | or | Sign Up |");
            Console.WriteLine(" ---------	---------");

            PageBreak();

            Console.Write("Type '1' or '2': ");
        }

        //public static void SignPrompt()
        //{
        //    Console.Write("username: ");
        //    Console.Write("password: ");
        //}
        
        //which key press
        //public static string KeyPress(string keyPress)
        //{
            
        //    return "";
        //} 
    }
}
