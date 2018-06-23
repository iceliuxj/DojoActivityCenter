using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DojoActivityCenter.Models{    
    public class Activity{
        [Key]
        public int ActivityId {get;set;}

        [Required(ErrorMessage = "Title cannot be empty.")]
        [MinLength(2,ErrorMessage="Must be more than 2 characters.")]
        public string Title {get;set;}

        [Required(ErrorMessage = "Date cannot be empty and must be in future")]
        public DateTime Date {get;set;}

        [Required(ErrorMessage = "Time cannot be empty.")]
        public String Time {get;set;}

        [Required(ErrorMessage = "Duration cannot be empty.")]
        public int Duration {get;set;}

        public string Durationtype {get;set;}

        [Required(ErrorMessage = "Description cannot be empty.")]
        [MinLength(10,ErrorMessage="Description is to short")]
        public string Description {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public List<Guest> Attenders {get;set;}
        public Activity(){
            Attenders = new List<Guest>();
        }
    }
}