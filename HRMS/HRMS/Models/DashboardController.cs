using Microsoft.AspNetCore.Mvc;
using HRMS.Models;
using System.Linq;

namespace HRMS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HRMSContext _context;

        public DashboardController(HRMSContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var model = new DashboardViewModel
            {
                TotalEmployees = _context.Employees.Count(),
                ActiveEmployees = _context.Employees.Count(e => e.IsActive == true),
                InactiveEmployees = _context.Employees.Count(e => e.IsActive == false),
                PendingLeaveRequests = _context.LeaveRequests.Count(lr => lr.Status == "Pending"),
                ApprovedLeaveRequests = _context.LeaveRequests.Count(lr => lr.Status == "Approved"),
                TotalAttendanceRecords = _context.Attendances.Count(),
                TotalSalariesProcessed = _context.Payrolls.Sum(p =>
                    (p.BasicSalary ?? 0) + (p.Bonus ?? 0) - (p.Deductions ?? 0)),
                TotalLeaveRequests = _context.LeaveRequests.Count(), // Set the total leave requests
                PendingApprovals = _context.LeaveRequests.Count(lr => lr.Status == "Pending") // Set pending approvals
            };

            return View(model);
        }
    }
}