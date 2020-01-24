using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Domain;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client
{
    //finish all login and registration stuff
    public class Login
    { 
        PizzaDbContext db = new PizzaDbContext();
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        //public bool isEmployee { get; set; }
 
        public Login()
        {

        }

        public Login(string userName, string password)
        {
            if (CheckIfUserExists(userName, password))
            {
                Console.WriteLine($"Login successful, welcome '{userName}'");
                
            }
            else {
                Console.WriteLine("either username or password is incorrect, try again");
            }
        }

        public bool CheckIfUserExists(string userName, string password)
        {
            if (db.Users.Any(u => u.Username == userName && u.Pass == password))
            {
                var user = db.Users.FirstOrDefault(e => e.Username == userName);
                userId = user.Id;
                return true;
            }
            else
            {
                return false;
            }
        }
         
        public bool CheckIfEmployee(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            
            return user.IsEmployee;
            
        }
        
        public bool CheckIfUserNameExists(string userName)
         {
            if (db.Users.Any(u => u.Username == userName))
            {
                return true;
            }
            else
            {
                return false;
            }
         }
         
        public void RegisterNewUser(Users user)
        {
            
                db.Users.Add(user);// this will generate insert query
                db.SaveChanges();// this will execute the above generate insert query
            
        }

        public int GetUserIdByName(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            
            return user.Id;
        }

        public string GetUserNameById(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            return user.Username;
        }
    }
}
