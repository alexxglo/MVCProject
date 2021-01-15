using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameLibraryProject.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Genre name cannot be less than 3!"),
            MaxLength(20, ErrorMessage = "Genre name cannot be more than 20!"),
             RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$",ErrorMessage = "Can only use letters, no special characters or numbers!")]
        public string Name { get; set; }
        
        // many-to-many relationship
        public virtual ICollection<Game> games { get; set; }
    }
}