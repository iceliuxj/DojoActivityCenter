using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DojoActivityCenter.Models;

namespace DojoActivityCenter.Controllers
{
    public class HomeController : Controller
    {
        private userContext _context;
        public HomeController(userContext context){
            _context = context;
        }        
        private bool UserExists(string email)
        {
            if(_context.users.Where(data => data.Email == email).Count() > 0){
                return true;
            } 
            else{
                return false;
            }
        }
 //below are all routes methods
        public IActionResult Index()
            {
                return View();
            }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegValidator data)
        {
            if(UserExists(data.Email)){
                ModelState.AddModelError("Email", "Email is in use");
            }
            if(ModelState.IsValid){
                User user = new User {
                    Firstname = data.Firstname,
                    Lastname = data.Lastname,
                    Email = data.Email,
                    Password = data.Password,
                };
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.users.Add(user);
                _context.SaveChanges();
                User user1 = _context.users.SingleOrDefault(u => u.Email == data.Email);
                HttpContext.Session.SetInt32("ID",user1.UserId);
                return RedirectToAction("Home");
            }
            else{
                return View("Index");                
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginValidator data)
        {
            if(ModelState.IsValid){
                User thisUser = _context.users.SingleOrDefault(u => u.Email == data.LoginId);
                if(thisUser != null){
                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(thisUser, thisUser.Password, data.LoginPw)){
                        HttpContext.Session.SetInt32("ID",thisUser.UserId);
                        ViewData["User"] = thisUser;
                        return RedirectToAction("Home");
                    }
                    else{
                        ViewBag.Error = "Matching Password Failed. ";
                        return View("Index");
                    }
                }
                else{
                    ViewBag.Error = "No User Record Found.";
                    return View("Index");
                }
            }
            else{
                return View("Index");                
            }
        }
        public IActionResult Home()
        {
            if(HttpContext.Session.GetInt32("ID") == null) {
                return RedirectToAction("Index");
            }
            else {
                int? x= HttpContext.Session.GetInt32("ID");
                User user = _context.users.SingleOrDefault(u=>u.UserId == (int)x);
                List<Models.Activity> activities = _context.activities.Include(data=>data.User).Include(data=>data.Attenders).ThenInclude( data=>data.User).OrderBy(data=>data.Date).ToList();
                ViewBag.activities = activities;    
                ViewBag.user = user;
                return View("Home");
            }
        }

        [HttpGet]
        [Route("add")]
        public IActionResult ActivityForm(){
            return View("Add");
        }

        [HttpPost]
        [Route("add")]
        public IActionResult SubmitForm(Models.Activity data){
            if(ModelState.IsValid){
                if(data.Date < DateTime.Now){
                    ViewBag.Error ="Date has to be in future";
                    return View("Add");
                }
                else {
                    int? x = HttpContext.Session.GetInt32("ID");
                    data.UserId = (int)x;
                    _context.activities.Add(data);
                    _context.SaveChanges();
                    return RedirectToAction("Home");
                }
            }
            else{
                return View("Add");
            }
        }

        [HttpGet]
        [Route("info/{id}")]
        public IActionResult InfoPage(int id){
            Models.Activity thisActivity = _context.activities.Include(data => data.User).Include(data=>data.Attenders).ThenInclude(data => data.User).SingleOrDefault(data => data.ActivityId == id);
            // int? x= HttpContext.Session.GetInt32("Id");
            // User user = _context.users.SingleOrDefault(u=>u.UserId == (int)x);
            ViewBag.activities = thisActivity;
            // ViewBag.user=user;
            return View("Infopage");
        }

        [HttpGet]
        [Route("join/{id}")]
        public IActionResult Join(int id){
            int? x = HttpContext.Session.GetInt32("ID");
            Guest thisJoint = _context.guests.SingleOrDefault(data => data.UserId == (int)x && data.ActivityId == id);
            if (thisJoint == null){             
                Guest Attend = new Guest{
                    UserId = (int)x,
                    ActivityId = id,
                };
                _context.guests.Add(Attend);
                _context.SaveChanges();
                return RedirectToAction("Home");          
            }
            return RedirectToAction("Home");   
        }

        [HttpGet]
        [Route("leave/{id}")]
        public IActionResult Leave(int id){
            int? x = HttpContext.Session.GetInt32("ID");
            Guest thisJoint = _context.guests.SingleOrDefault(data => data.UserId == (int)x && data.ActivityId == id);
            _context.guests.Remove(thisJoint);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("info/{id}/delete")]
        public IActionResult Delete(int id){
            int? x = HttpContext.Session.GetInt32("ID");
            Models.Activity thisActivity = _context.activities.SingleOrDefault(data => data.UserId == (int)x && data.ActivityId == id);
            _context.activities.Remove(thisActivity);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }



        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
