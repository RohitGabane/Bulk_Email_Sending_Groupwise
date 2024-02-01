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
    public class DetailsEmpController : Controller
    {
        private readonly BulkDbContext _context;

        public DetailsEmpController(BulkDbContext context)
        {
            _context = context;
        }

        // GET: DetailsEmp
        public async Task<IActionResult> Index()
        {
            var bulkDbContext = _context.DetailsEmp.Include(d => d.Department).Include(d => d.Employee);
            return View(await bulkDbContext.ToListAsync());
        }

        // GET: DetailsEmp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailsEmp = await _context.DetailsEmp
                .Include(d => d.Department)
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailsEmp == null)
            {
                return NotFound();
            }

            return View(detailsEmp);
        }


        private bool DetailsEmpExists(int id)
        {
            return _context.DetailsEmp.Any(e => e.Id == id);
        }
        public async Task<IActionResult> ShowAllEmployees(int Dept_Id)
        {
            ViewData["Dept_Id"] = Dept_Id;

            var allEmployees = await _context.Employee.ToListAsync();

            return View(allEmployees);
        }


        // Inside DetailsEmpController.cs
        [HttpPost]
        public async Task<IActionResult> AddEmployee(int Dept_Id, List<int> selectedEmployees)
        {
            var department = await _context.Department.Include(d => d.DetailsEmp).FirstOrDefaultAsync(m => m.Dept_Id == Dept_Id);

            if (department != null && selectedEmployees != null && selectedEmployees.Any())
            {
                foreach (var empId in selectedEmployees)
                {
                    var detailsEmp = new DetailsEmp { Emp_ID = empId, Dept_Id = Dept_Id };
                    _context.DetailsEmp.Add(detailsEmp);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ViewEmployees(int Dept_Id)
        {
            var department = await _context.Department
                .Include(d => d.DetailsEmp)
                .ThenInclude(de => de.Employee)
                .FirstOrDefaultAsync(m => m.Dept_Id == Dept_Id);

            if (department == null)
            {
                return NotFound();
            }

            var employeesInDepartment = department.DetailsEmp
                .Select(de => de.Employee)
                .OrderBy(emp => emp.FirstName)
                .ThenBy(emp => emp.LastName)
                .ToList();

            return View("ViewEmployee", employeesInDepartment);
        }




        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var detailsEmp = await _context.DetailsEmp
        //        .Include(de => de.Employee)
        //        .Include(de => de.Department)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (detailsEmp == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(detailsEmp);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var detailsEmp = await _context.DetailsEmp.FindAsync(id);

        //    if (detailsEmp == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.DetailsEmp.Remove(detailsEmp);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

    }
}
