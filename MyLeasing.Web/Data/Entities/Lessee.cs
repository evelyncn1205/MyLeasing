using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }

        public string FirstName { get; set; }   

        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }


        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name ="Photo")]
        public Guid ImageId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public string ImageFullPath => ImageId == Guid.Empty ?
            $"https://myleasingwebtpsi.azurewebsites.net/images/noimage.png"
            : $"https://myleasing76.blob.core.windows.net/lessee/{ImageId}";
    }
}
