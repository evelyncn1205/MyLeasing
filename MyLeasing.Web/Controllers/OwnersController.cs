using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;


        public OwnersController(IOwnerRepository ownerRepository, IUserHelper userHelper, IBlobHelper blobHelper, IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
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

        [Authorize]
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
                Guid imageId = Guid.Empty;
                if(model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    
                   imageId = await _blobHelper.UploadBlobAsync(model.ImageFile,"owner");
                }
                
                var owner = _converterHelper.ToOwner(model,imageId, true);

                owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _ownerRepository.CreatAsync(owner);
                return RedirectToAction(nameof(Index));
                                
            }
            return View(model);
        }


        [Authorize]
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
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length>0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile,"owner");
                    }

                    var owner = _converterHelper.ToOwner(model, imageId,false);
                    
                    owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

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
        [Authorize]
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
