using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.Models;
using System.Linq;

namespace HRMS.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly HRMSContext _context;

        public LeaveRequestController(HRMSContext context)
        {
            _context = context;
        }

        // GET: LeaveRequest
        public IActionResult Index()
        {
            var leaveRequests = _context.LeaveRequests.Include(l => l.Employee).ToList();
            return View(leaveRequests);
        }

        // GET: LeaveRequest/Details/5
        public IActionResult Details(int id)
        {
            var leaveRequest = _context.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefault(l => l.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Create
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList(); // Pass employees for dropdown
            ViewBag.LeaveTypes = _context.LeaveTypes.ToList(); // Fetching leave types for dropdown
            return View();
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                leaveRequest.Status = "Pending"; // Set the default status to Pending
                _context.LeaveRequests.Add(leaveRequest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.LeaveTypes = _context.LeaveTypes.ToList();
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Edit/5
        public IActionResult Edit(int id)
        {
            var leaveRequest = _context.LeaveRequests.Find(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewBag.Employees = _context.Employees.ToList(); // Pass employees for dropdown
            ViewBag.LeaveTypes = _context.LeaveTypes.ToList(); // Fetching leave types for dropdown
            return View(leaveRequest);
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LeaveRequests.Any(e => e.Id == leaveRequest.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.LeaveTypes = _context.LeaveTypes.ToList();
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Delete/5
        public IActionResult Delete(int id)
        {
            var leaveRequest = _context.LeaveRequests.Find(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var leaveRequest = _context.LeaveRequests.Find(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
