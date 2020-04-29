﻿using System.ComponentModel.DataAnnotations;

namespace LitigationPlanner.MVC.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name="User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
