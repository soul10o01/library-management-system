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
    public class LoansController : Controller
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var libraryDbContext = _context.Loans.Include(l => l.Book).Include(l => l.Member);
            return View(await libraryDbContext.ToListAsync());
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FirstName");
            return View();
        }

        // POST: Loans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,MemberId,BorrowDate,DueDate,ReturnDate,IsReturned,Notes,FineAmount,IsFineCollected")] Loan loan)
        {
            try
            {
                // Log the incoming loan data
                System.Diagnostics.Debug.WriteLine("Attempting to create loan with data:");
                System.Diagnostics.Debug.WriteLine($"BookId: {loan.BookId}");
                System.Diagnostics.Debug.WriteLine($"MemberId: {loan.MemberId}");
                System.Diagnostics.Debug.WriteLine($"BorrowDate: {loan.BorrowDate}");
                System.Diagnostics.Debug.WriteLine($"DueDate: {loan.DueDate}");
                System.Diagnostics.Debug.WriteLine($"ReturnDate: {loan.ReturnDate}");
                System.Diagnostics.Debug.WriteLine($"IsReturned: {loan.IsReturned}");
                System.Diagnostics.Debug.WriteLine($"Notes: {loan.Notes}");
                System.Diagnostics.Debug.WriteLine($"FineAmount: {loan.FineAmount}");
                System.Diagnostics.Debug.WriteLine($"IsFineCollected: {loan.IsFineCollected}");

                if (ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("ModelState is valid, attempting to save...");
                    try
                    {
                        _context.Add(loan);
                        var result = await _context.SaveChangesAsync();
                        System.Diagnostics.Debug.WriteLine($"SaveChangesAsync result: {result}");
                        System.Diagnostics.Debug.WriteLine("Loan saved successfully!");
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

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FirstName", loan.MemberId);
            return View(loan);
        }
    }
} 