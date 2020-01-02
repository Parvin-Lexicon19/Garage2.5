using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._5.Models;
using Garage2._5.ViewModels;
using Bogus;
using AutoMapper;

namespace Garage2._5.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage2_5Context _context;
        private readonly IMapper mapper;
        private Faker faker;
        public MembersController(Garage2_5Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
            faker = new Faker("sv");
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Member.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var member = await _context.Member
            //.Include(s => s.ParkedVehicles)
            //.FirstOrDefaultAsync(m => m.Id == id);
            var member = await mapper.ProjectTo<MemberDetailsViewModel>(_context.Member)                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Members/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Avatar,FirstName,LastName,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.Avatar = faker.Internet.Avatar();
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Avatar,FirstName,LastName,Email")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }


        /*Searches based on First name and Last name of member*/
        public async Task<IActionResult> Filter(string firstname, string? lastname)
        {
            var model = await _context.Member.ToListAsync();
            model = string.IsNullOrWhiteSpace(firstname) ?
                model :
                model.Where(m => m.FirstName.ToLower().Contains(firstname.ToLower())).ToList();

            model = lastname == null ?
                model :
                model.Where(m => m.LastName.ToLower().Contains(lastname.ToLower())).ToList();

            return View(nameof(Index), model);
        }
    }
}
