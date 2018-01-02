using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1.UI_2.Models
{
    public class LoginInfo
    {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(10, MinimumLength = 4)]
            public string Password { get; set; }

      

    }
}