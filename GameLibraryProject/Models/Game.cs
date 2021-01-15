using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GameLibraryProject.Models
{

    public class Game
    {

        public int GameId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Title cannot be less than 2"),
            MaxLength(200, ErrorMessage = "Title cannot be more than 200")]
        public string Title { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Producer cannot be less than 2!"),
            MaxLength(40, ErrorMessage = "Producer cannot be more than 40!")]
        public string Producer { get; set; }
        [Required]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage ="Price must be numeric and not be 0!")]
        public int Price { get; set; }
        
        [MaxLength(3000, ErrorMessage = "Summary cannot be more than 3000!")
            ,BadWordsValidation]
        public string Summary { get; set; }
        
        // one to many

        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        
        // one to many
        [ForeignKey("GameType")]
        public int GameTypeId { get; set; }
        public virtual GameType GameType { get; set; }
        public virtual Store Store { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public IEnumerable<SelectListItem> PublishersList { get; set; }
        public IEnumerable<SelectListItem> GameTypesList { get; set; }
        
        //many to many
        [NotMapped]
        public List<CheckBoxViewModel> GenresList { get; set; }

    }

    

}


