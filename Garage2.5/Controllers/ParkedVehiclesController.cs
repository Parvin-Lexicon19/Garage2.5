using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._5.Data;
using Garage2._5.Models;
using AutoMapper;
using Garage2._5.ViewModel;

namespace Garage2._5.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage2_5Context _context;
        private readonly IMapper mapper;

        public ParkedVehiclesController(Garage2_5Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index()
        {
        

            // View Model to Have Owner name,CheckIn Time,Regno,Type.
            var parkedVehicles = _context.ParkedVehicle.Where(p => (p.CheckOutTime) == default(DateTime));
            var model3 = await mapper.ProjectTo<VehicleListDetails>(parkedVehicles).ToListAsync();

            return View(model3);
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
                .Include(p => p.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/
        public IActionResult Park()

        // To Indentify Member name Unique, Included Email also to appear in the Dropdown list.
        {
            var memberList = _context.Set<Member>()
                     .Select(x => new SelectListItem
                     {
                         Value = x.Id.ToString(),
                         Text = x.FullName + "( " + x.Email + ")"
                     }).ToList();
            ViewData["MemberId"] = memberList;

         // Display the Vehicle Type in the Dropdown List.

            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Type");
            return View();
        }

        // POST: ParkedVehicles/Park
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Id,RegNo,Color,Brand,Model,NoOfWheels,CheckInTime,CheckOutTime,MemberId,VehicleTypeId")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                // Populate the current date and Time for checkIN field. 

                parkedVehicle.CheckInTime = DateTime.Now;

                parkedVehicle.CheckOutTime = default(DateTime);

                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Id", parkedVehicle.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", parkedVehicle.VehicleTypeId);
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
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Id", parkedVehicle.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", parkedVehicle.VehicleTypeId);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNo,Color,Brand,Model,NoOfWheels,CheckInTime,CheckOutTime,MemberId,VehicleTypeId")] ParkedVehicle parkedVehicle)
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
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Id", parkedVehicle.MemberId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", parkedVehicle.VehicleTypeId);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Unpark(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .Include(p => p.Member)
                .Include(p => p.VehicleType)
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
            // Check out will just populate Checkout Time but will not delete any record from DB.

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            parkedVehicle.CheckOutTime = DateTime.Now;
           // _context.ParkedVehicle.Remove(parkedVehicle);
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
