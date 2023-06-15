using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        
        private readonly ILesseeRepository _lesseeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public LesseesController(ILesseeRepository lesseeRepository, IUserHelper userHelper, IBlobHelper blobHelper, IConverterHelper converterHelper)
        {
           _lesseeRepository = lesseeRepository;
           _userHelper = userHelper;
           _blobHelper = blobHelper;
           _converterHelper = converterHelper;
        }

        // GET: Lessees
        public IActionResult Index()
        {
            return View(_lesseeRepository.GetAll().OrderBy(l => l.FirstName));
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        [Authorize(Roles = "Admin")]
        // GET: Lessees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                 Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length >0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "lessee");
                }

                var lessee = _converterHelper.ToLessee(model, imageId, true);

                lessee.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                await _lesseeRepository.CreatAsync(lessee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            var model =_converterHelper.ToLesseeViewModel(lessee);
            return View(model);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                       imageId= await _blobHelper.UploadBlobAsync(model.ImageFile, "lessee");
                    }

                    var lessee = _converterHelper.ToLessee(model, imageId, false);

                    lessee.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                    await _lesseeRepository.UpdateAsync(lessee); ;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _lesseeRepository.ExistAsync(model.Id))
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

        // GET: Lessees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);
                
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessee = await _lesseeRepository.GetByIdAsync(id);
            await _lesseeRepository.DeleteAsync(lessee);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
