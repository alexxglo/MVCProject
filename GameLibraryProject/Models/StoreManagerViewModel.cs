using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameLibraryProject.Models
{
    public class StoreManagerViewModel
    {   
        
        public int StoreId { get; set; }
        [MinLength(2, ErrorMessage = "Publisher name cannot be less than 2!"),
       MaxLength(30, ErrorMessage = "Publisher name cannot be more than 30!")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
          MaxLength(20, ErrorMessage = "Name cannot be more than 20!")]
        public string NameM { get; set; }
        [Required, MinLength(2, ErrorMessage = "Manager name cannot be less than 2!"),
            MaxLength(20, ErrorMessage = "Manager name cannot be more than 20!")]
        public string Surname { get; set; }
        [Required, RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public string PhoneNumber { get; set; }
    }
}