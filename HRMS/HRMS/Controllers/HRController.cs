using Microsoft.AspNetCore.Mvc;
using HRMS.Models; // Adjust based on your namespace
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class HRController : Controller
{
    private readonly HRMSContext _context;

    public HRController(HRMSContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(HRLoginViewModel model)
    {
        // Validate HR credentials with the provided username and password
        if (model.Username == "PriyankaNikam" && model.Password == "HRPriyanka@123")
        {
            HttpContext.Session.SetString("IsHR", "true");
            return RedirectToAction("Dashboard");
        }
        ViewData["ErrorMessage"] = "Invalid username or password";
        return View(model);
    }

    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("IsHR") != "true")
        {
            return RedirectToAction("Login");
        }

        var leaveRequests = _context.LeaveRequests.Include(lr => lr.Employee)
                           .Where(lr => lr.Status == "Pending").ToList();
        return View(leaveRequests);
    }

    [HttpPost]
    public IActionResult Approve(int id)
    {
        var leaveRequest = _context.LeaveRequests.Find(id);
        if (leaveRequest != null)
        {
            leaveRequest.Status = "Approved"; // Update status to Approved
            _context.SaveChanges(); // Save changes to the database
        }
        return RedirectToAction(nameof(Dashboard)); // Redirect back to Dashboard
    }

    [HttpPost]
    public IActionResult Reject(int id)
    {
        var leaveRequest = _context.LeaveRequests.Find(id);
        if (leaveRequest != null)
        {
            leaveRequest.Status = "Rejected"; // Update status to Rejected
            _context.SaveChanges(); // Save changes to the database
        }
        return RedirectToAction(nameof(Dashboard)); // Redirect back to Dashboard
    }

    [HttpPost]
    public IActionResult UpdateStatus(int id, string status)
    {
        var leaveRequest = _context.LeaveRequests.Find(id);
        if (leaveRequest != null && (status == "Approved" || status == "Rejected"))
        {
            leaveRequest.Status = status; // Update status based on dropdown selection
            _context.SaveChanges(); // Save changes to the database
        }
        return RedirectToAction(nameof(Dashboard)); // Redirect back to Dashboard
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Remove("IsHR");
        return RedirectToAction("Login");
    }
}
