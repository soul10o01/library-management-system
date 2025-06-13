using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    public class MembersController : Controller
    {
        private readonly LibraryDbContext _context;

        public MembersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PhoneNumber,Address,MembershipDate,IsActive,MembershipType")] Member member)
        {
            try
            {
                // Log the incoming member data
                System.Diagnostics.Debug.WriteLine("Attempting to create member with data:");
                System.Diagnostics.Debug.WriteLine($"FirstName: {member.FirstName}");
                System.Diagnostics.Debug.WriteLine($"LastName: {member.LastName}");
                System.Diagnostics.Debug.WriteLine($"Email: {member.Email}");
                System.Diagnostics.Debug.WriteLine($"PhoneNumber: {member.PhoneNumber}");
                System.Diagnostics.Debug.WriteLine($"Address: {member.Address}");
                System.Diagnostics.Debug.WriteLine($"MembershipDate: {member.MembershipDate}");
                System.Diagnostics.Debug.WriteLine($"IsActive: {member.IsActive}");
                System.Diagnostics.Debug.WriteLine($"MembershipType: {member.MembershipType}");

                if (ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("ModelState is valid, attempting to save...");
                    try
                    {
                        _context.Add(member);
                        var result = await _context.SaveChangesAsync();
                        System.Diagnostics.Debug.WriteLine($"SaveChangesAsync result: {result}");
                        System.Diagnostics.Debug.WriteLine("Member saved successfully!");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
                        ModelState.AddModelError("", "Error saving to database: " + ex.Message);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ModelState is invalid. Validation errors:");
                    foreach (var modelStateEntry in ModelState.Values)
                    {
                        foreach (var error in modelStateEntry.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                            ModelState.AddModelError("", error.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception occurred: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
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

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Address,MembershipDate,IsActive,MembershipType")] Member member)
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

            var member = await _context.Members
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
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
} 