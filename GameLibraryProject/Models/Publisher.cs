using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameLibraryProject.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; } 
        // pentru ca o sa uit: aici nu avem required la name deoarece cand vrem sa updatam publisherul vom avea optiunea de a updata doar magazinul.
        [MinLength(2, ErrorMessage = "Publisher name cannot be less than 2!"),
            MaxLength(60, ErrorMessage = "Publisher name cannot be more than 60!")]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        // one-to many-relationship
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public IEnumerable<SelectListItem> StoreNamesList { get; set; }
    }

}