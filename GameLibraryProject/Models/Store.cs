using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameLibraryProject.Models
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name cannot be less than 2!"),
            MaxLength(20, ErrorMessage = "Name cannot be more than 20!")]
        public string Name { get; set; }
        // one-to one-relationship
        [Required]
        public virtual Manager Manager { get; set; }
        public virtual ICollection<Game> games { get; set; }
    }

}