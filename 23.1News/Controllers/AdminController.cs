﻿using _23._1News.Data;
using _23._1News.Models;
using _23._1News.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _23._1News.Controllers
{
    public class AdminController
    {

        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        public AdminController(ILogger<AdminController> logger, IArticleService articleService, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;

        }


        
    }
}