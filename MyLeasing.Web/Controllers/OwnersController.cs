using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;


        public OwnersController(IOwnerRepository ownerRepository, IUserHelper userHelper, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll().OrderBy(p => p.OwnerName));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
               
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model )
        {
            
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                if(model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    
                    path = await _imageHelper.UploadImageAsync(model.ImageFile,"owner");
                }
                
                var owner = _converterHelper.ToOwner(model, path, true);

                owner.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                await _ownerRepository.CreatAsync(owner);
                return RedirectToAction(nameof(Index));
                                
            }
            return View(model);
        }

        //private Owner ToOwner(OwnerViewModel model, string path)
        //{
        //    return new Owner
        //    {
        //        Id =  model.Id,
        //        ImageUrl = path,
        //        OwnerName = model.OwnerName,
        //        Address = model.Address,
        //        CellPhone = model.CellPhone,
        //        Document = model.Document,
        //        FixedPhone = model.FixedPhone,
        //        User = model.User,
        //    };
        //}

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToOwnerViewModel(owner);
            return View(model);
        }

        //private OwnerViewModel ToOwnerViewModel(Owner owner)
        //{
        //    return new OwnerViewModel
        //    {
        //        Id = owner.Id,
        //        OwnerName = owner.OwnerName,
        //        Address = owner.Address,
        //        Document = owner.Document,
        //        CellPhone = owner.CellPhone,
        //        FixedPhone = owner.FixedPhone,
        //        ImageUrl = owner.ImageUrl,
        //        User = owner.User,
        //    };
        //}

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;
                    if(model.ImageFile != null && model.ImageFile.Length>0)
                    {
                        
                        path = await _imageHelper.UploadImageAsync(model.ImageFile,"owner");
                    }

                    var owner = _converterHelper.ToOwner(model, path,false);
                    
                    owner.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");

                    await _ownerRepository.UpdateAsync(owner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _ownerRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
                                
            }
            return View(model);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);                                 
            await _ownerRepository.DeleteAsync(owner);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
