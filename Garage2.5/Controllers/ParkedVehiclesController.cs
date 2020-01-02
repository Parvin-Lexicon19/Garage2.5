using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._5.Models;
using Garage2._5.Data;

namespace Garage2._5.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage2_5Context _context;

        public ParkedVehiclesController(Garage2_5Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index()
        {
            var garage2_5Context = _context.ParkedVehicle.Include(p => p.Member);
            return View(await garage2_5Context.ToListAsync());
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNo,Color,Brand,Model,NoOfWheels,MemberId,CheckInTime,CheckOutTime")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", parkedVehicle.MemberId);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", parkedVehicle.MemberId);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNo,Color,Brand,Model,NoOfWheels,MemberId,CheckInTime,CheckOutTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", parkedVehicle.MemberId);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(m => m.Id == id);
            

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var model = new Receipt();
            model.RegNo = parkedVehicle.RegNo;

            var parkedMemberId = parkedVehicle.MemberId;
            var parkedvehicletypeId = parkedVehicle.VehicleTypeId;
            var memberdetails = await _context.Member.FirstOrDefaultAsync(m => m.Id == parkedMemberId);
            var vehicletypedetails = await _context.VehicleType.FirstOrDefaultAsync(m => m.Id == parkedvehicletypeId);

            model.FullName = memberdetails.FullName;
            model.Type = vehicletypedetails.Type;
            model.CheckInTime = parkedVehicle.CheckInTime;
            model.CheckOutTime = DateTime.Now;
            var totaltime = model.CheckOutTime - model.CheckInTime;
            var min = (totaltime.Minutes > 0) ? 1 : 0;


            if (totaltime.Days == 0)
            {
                model.Totalparkingtime = totaltime.Hours + " Hrs " + totaltime.Minutes + " Mins";
                model.Totalprice = ((totaltime.Hours + min) * 5) + "Kr";
            }
            else
            {
                model.Totalparkingtime = totaltime.Days + "Days" + " " + totaltime.Hours + "hrs" + " " + totaltime.Minutes + "mins";
                model.Totalprice = (totaltime.Days * 100) + ((totaltime.Hours + min) * 5) + "Kr";
            }

            parkedVehicle.CheckOutTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return View(model);
        }
    }
}
