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
public class ErrorController : Controller
{
    public IActionResult Default()
    {
        return View("~/Views/Shared/Errors/Default.cshtml");
    }
}