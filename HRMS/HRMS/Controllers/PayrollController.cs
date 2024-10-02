using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PayrollController : Controller
{
    private readonly HRMSContext _context;

    public PayrollController(HRMSContext context)
    {
        _context = context;
    }

    // GET: Payroll
    public async Task<IActionResult> Index()
    {
        var payrolls = await _context.Payrolls.Include(p => p.Employee).ToListAsync();
        return View(payrolls);
    }

    // GET: Payroll/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var payroll = await _context.Payrolls
            .Include(p => p.Employee) // Include related Employee information
            .FirstOrDefaultAsync(m => m.Id == id);

        if (payroll == null)
        {
            return NotFound();
        }

        return View(payroll);
    }


    // GET: Payroll/Create
    public async Task<IActionResult> Create()
    {
        // Correcting the ViewBag variable name to 'Employees'
        ViewBag.Employees = new SelectList(await _context.Employees.ToListAsync(), "Id", "FullName");
        return View();
    }

    // POST: Payroll/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,EmployeeId,PayrollDate,BasicSalary,Bonus,Deductions")] Payroll payroll)
    {
        if (ModelState.IsValid)
        {
            _context.Add(payroll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Again, ensure the correct ViewBag variable name
        ViewBag.Employees = new SelectList(await _context.Employees.ToListAsync(), "Id", "FullName", payroll.EmployeeId);
        return View(payroll);
    }


    // GET: Payroll/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var payroll = await _context.Payrolls.FindAsync(id);
        if (payroll == null)
        {
            return NotFound();
        }

        var employees = await _context.Employees.ToListAsync();
        ViewBag.Employees = new SelectList(employees, "Id", "FullName", payroll.EmployeeId);

        return View(payroll);
    }


    // POST: Payroll/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,PayrollDate,BasicSalary,Bonus,Deductions")] Payroll payroll)
    {
        if (id != payroll.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(payroll);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayrollExists(payroll.Id))
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
        ViewBag.Employees = new SelectList(await _context.Employees.ToListAsync(), "Id", "FullName", payroll.EmployeeId);
        return View(payroll);
    }

    // GET: Payroll/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var payroll = await _context.Payrolls
            .Include(p => p.Employee)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (payroll == null)
        {
            return NotFound();
        }

        return View(payroll);
    }

    // POST: Payroll/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var payroll = await _context.Payrolls.FindAsync(id);
        _context.Payrolls.Remove(payroll);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PayrollExists(int id)
    {
        return _context.Payrolls.Any(e => e.Id == id);
    }
}
