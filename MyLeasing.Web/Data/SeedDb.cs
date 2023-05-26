using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
           _context=context;
            _random=new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            if (!_context.Owners.Any())
            {
                AddOwner("Evelyn Nunes","Travessa de Pomardufe 140");
                AddOwner("Carla Silva", "Travessa de Pomardufe 140");
                AddOwner("Marta Sousa", "Travessa de Pomardufe 140");
                AddOwner("Bruno Campos", "Travessa de Pomardufe 140");
                AddOwner("Luis Monteiro","Travessa de Pomardufe 140");
                AddOwner("Augusto Pimentel", "Travessa de Pomardufe 140");
                AddOwner("Angela Castro", "Travessa de Pomardufe 140");
                AddOwner("Antonio Silva", "Travessa de Pomardufe 140");
                AddOwner("Nuno Pereira", "Travessa de Pomardufe 140");
                AddOwner("Cristiano Ronaldo", "Travessa de Pomardufe 140");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string name, string address)
        {
            _context.Owners.Add(new Owner
            {
                Document=_random.Next(10000).ToString("D9"),
                OwnerName = name,
                FixedPhone = "2" + _random.Next(10000000, 99999999).ToString(),
                CellPhone = "9" + _random.Next(10000000, 99999999).ToString(),
                Address = address
            });
        }
    }
}
