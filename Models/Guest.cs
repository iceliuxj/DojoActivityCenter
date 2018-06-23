using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DojoActivityCenter.Models{    
    public class Guest{
        [Key]
        public int GuestId {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int ActivityId {get;set;}
        public Activity Activity {get;set;}
    }
}