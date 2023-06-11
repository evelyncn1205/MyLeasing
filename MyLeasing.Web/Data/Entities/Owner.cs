using System;
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

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty ?
            $"https://myleasingwebtpsi.azurewebsites.net/images/noimage.png"
            : $"https://myleasing76.blob.core.windows.net/owner/{ImageId}";


    }
}
