using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System.IO;

namespace MyLeasing.Web.Helpers
{
    public class Converterhelper : IConverterHelper
    {
        public Owner ToOwner(OwnerViewModel model, string path, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                OwnerName = model.OwnerName,
                Address = model.Address,
                CellPhone = model.CellPhone,
                Document = model.Document,
                FixedPhone = model.FixedPhone,
                User = model.User,
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                OwnerName = owner.OwnerName,
                Address = owner.Address,
                Document = owner.Document,
                CellPhone = owner.CellPhone,
                FixedPhone = owner.FixedPhone,
                ImageUrl = owner.ImageUrl,
                User = owner.User,
            };
        }


        public Lessee ToLessee(LesseeViewModel model, string path, bool isNew)
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Document=model.Document,
                FixedPhone=model.FixedPhone,
                CellPhone=model.CellPhone,
                User=model.User,
            };
        }

        public LesseeViewModel ToLesseeViewModel(Lessee lessee)
        {
            return new LesseeViewModel
            {
                Id =lessee.Id,
                ImageUrl = lessee.ImageUrl,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                Address = lessee.Address,
                Document = lessee.Document,
                FixedPhone = lessee.FixedPhone,
                CellPhone = lessee.CellPhone,
                User = lessee.User,
            };
        }
    }
}
