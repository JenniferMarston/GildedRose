using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GildedRoseData.Entities
{
   public class AuthenticationModel
    {
        [Required (ErrorMessage = "A Username is required")]
        public string Username { get; set; }
        [Required (ErrorMessage = "A Password is required")]
        public string Password { get; set; }

    }
}
