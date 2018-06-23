using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DojoActivityCenter.Models
{
    public class RegValidator
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage = "First Name cannot be empty.")]
        [MinLength(2,ErrorMessage="First name must be more than 2 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First name must be characters only.")]
        public string Firstname { get;set; }

        [Required(ErrorMessage = "Last Name cannot be empty.")]
        [MinLength(2,ErrorMessage="Last name must be more than 2 characters.")] 
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name must be characters only.")]
        public string Lastname  { get;set; }
        
        [Required(ErrorMessage = "Email Address cannot be empty.")]
        [EmailAddress]
        public string Email { get;set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get;set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string Confirm { get;set; }
    }
    public class User
    {
        [Key]
        public int UserId {get;set;}
        public string Firstname { get;set; }
        public string Lastname  { get;set; }
        public string Email { get;set; }
        public string Password { get;set; }
        public List<Activity> Activities {get;set;}
        public List<Guest> Attenders {get;set;}
        public User(){
            Activities = new List<Activity>();
            Attenders = new List<Guest>();
        }
    }
    public class LoginValidator
    {
        [Required(ErrorMessage="ID REQUIRED.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Wrong Email.")] 
        public string LoginId {get;set;}

        [Required(ErrorMessage="Password REQUIRED.")]
        [DataType(DataType.Password)]
        public string LoginPw {get;set;}
    }
}