﻿using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
           _context=context;
            _userHelper=userHelper;            
            _random=new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Lessee");

            var user = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Evelyn",
                    LastName = "Nunes",
                    Email = "evelynrx_rj@hotmail.com",
                    UserName = "evelynrx_rj@hotmail.com",
                    PhoneNumber = "963258741",
                    Document = "123456789",
                    Address = "Travessa de pomarfufe 140"

                };
                               
                var result= await _userHelper.AddUserAsync(user,"123456");
                if(result!= IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                //await _userHelper.AddUserToRoleAsync(user, "Owner");
                //await _userHelper.AddUserToRoleAsync(user, "Lessee");
               
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
                await _userHelper.AddUserToRoleAsync(user, "Owner");
                await _userHelper.AddUserToRoleAsync(user, "Lessee");
            }

            if (!_context.Owners.Any())
            {
                AddOwner("Evelyn Nunes","Travessa de Pomardufe 140",user);
                AddOwner("Carla Silva", "Travessa de Pomardufe 140",user);
                AddOwner("Marta Sousa", "Travessa de Pomardufe 140",user);
                AddOwner("Bruno Campos", "Travessa de Pomardufe 140",user);
                AddOwner("Luis Monteiro","Travessa de Pomardufe 140",user);
                AddOwner("Augusto Pimentel", "Travessa de Pomardufe 140",user);
                AddOwner("Angela Castro", "Travessa de Pomardufe 140",user);
                AddOwner("Antonio Silva", "Travessa de Pomardufe 140",user);
                AddOwner("Nuno Pereira", "Travessa de Pomardufe 140",user);
                AddOwner("Cristiano Ronaldo", "Travessa de Pomardufe 140",user);
                await _context.SaveChangesAsync();
            }

            if(!_context.Lessees.Any())
            {
                AddLessee("Evelyn", "Nunes", "Travessa de Pomardufe 140", user);
                AddLessee("Carla", "Silva", "Rua da Felicidade 50", user);
                AddLessee("Cristiano", "Ronaldo", "Av. da Liberdade 100", user);
                AddLessee("Nuno", "Pereira", "Travessa dos Pesadelos 78", user);
                AddLessee("Angela", "Castro", "Rua das Flores 200", user);
                await _context.SaveChangesAsync();
            }
        }

        

        private void AddOwner(string name, string address,User user)
        {
            _context.Owners.Add(new Owner
            {
                Document=_random.Next(10000).ToString("D9"),
                OwnerName = name,
                FixedPhone = "2" + _random.Next(10000000, 99999999).ToString(),
                CellPhone = "9" + _random.Next(10000000, 99999999).ToString(),
                Address = address,
                User = user
            });
        }

        private void AddLessee(string name,string lastName, string address, User user)
        {
            _context.Lessees.Add(new Lessee
            {
                Document = _random.Next(10000).ToString("D9"),
                FirstName = name,
                LastName = lastName,
                FixedPhone = "2" + _random.Next(10000000, 99999999).ToString(),
                CellPhone = "9" + _random.Next(10000000, 99999999).ToString(),
                Address = address,
                User = user
            });
        }
    }
}
