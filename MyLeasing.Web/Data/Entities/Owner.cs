using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [Required]
        [Display(Name ="Owner Name")]
        public string OwnerName { get; set; }

        
        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }


        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Required]
        public string Address { get; set; } 

    }
}
