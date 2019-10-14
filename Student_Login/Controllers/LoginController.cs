using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Student_Login.Models;
using System.Text;

namespace Student_Login.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult NewUser(string FirstName,string LastName,string Email,string UserName,string Password)
        {

            

            Stud_log login = new Stud_log();

            

            login.FirstName = FirstName;
            login.LastName = LastName;
            login.Email = Email;
            login.Username = UserName;
            login.Password = Password;

            if ((login.FirstName).Length != 0)
            {
                login.saveToDB(login.FirstName, login.LastName, login.Email, login.Username, login.Password);
            }


             return View();

            
        }

       

        
    }


    

}