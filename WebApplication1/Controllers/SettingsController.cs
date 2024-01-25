using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SettingsController: Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var settings = await _context.Settings.ToListAsync();
            return View(settings);
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return PartialView("_Create");
        }
        // POST: Settings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Value,Value2,Description")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                setting.Created = DateTime.Now;
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_Create", setting);
        }
        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", setting);
        }
        // POST: Settings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Key,Value,Value2,Description,LastModified")] Setting setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    setting.LastModified = DateTime.Now;
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
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
            return PartialView("_Edit", setting);
        }

        // ... (other actions)

        private bool SettingExists(long id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }
    }
}
