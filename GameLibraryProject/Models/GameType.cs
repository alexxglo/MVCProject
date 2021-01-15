using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameLibraryProject.Models
{
    public class GameType
    {
        public int GameTypeId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Game type name cannot be less than 2!"),
            MaxLength(20, ErrorMessage = "Game type name cannot be more than 20!"),
                RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers are not allowed.")]
        public string Name { get; set; }
        // many to one
        public virtual ICollection<Game> Games { get; set; }
    }
}