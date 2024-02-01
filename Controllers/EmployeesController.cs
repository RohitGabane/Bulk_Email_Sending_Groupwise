using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bulk_Email_Sending_Groupwise.Models;

namespace Bulk_Email_Sending_Groupwise.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly BulkDbContext _context;

        public EmployeesController(BulkDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Emp_ID,FirstName,LastName,BirthDate,Email_ID")] Employee employee)
        {
            // Check if the email already exists
            if (_context.Employee.Any(e => e.Email_ID == employee.Email_ID))
            {
                ModelState.AddModelError("Email_ID", "Email already exists");
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Emp_ID,FirstName,LastName,BirthDate,Email_ID")] Employee employee)
        {
            if (id != employee.Emp_ID)
            {
                return NotFound();
            }

            // Check if the new email ID is unique
            if (!IsEmailUnique(employee.Email_ID, employee.Emp_ID))
            {
                ModelState.AddModelError("Email_ID", "The provided email address is already in use.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Emp_ID))
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
            return View(employee);
        }

        // Custom method to check if the email ID is unique
        private bool IsEmailUnique(string email, int empId)
        {
            return !_context.Employee.Any(e => e.Email_ID == email && e.Emp_ID != empId);
        }


        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Emp_ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Emp_ID == id);
        }
    }
}
