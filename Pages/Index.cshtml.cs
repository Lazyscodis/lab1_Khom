using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class IndexModel : PageModel
{
    private EmployeeRepository employeeRepository;
    public IList<Employee> Employees;

    public IndexModel(EmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public void OnGet()
    {
        Employees = employeeRepository.List();
    }
    
    public IActionResult OnPostDelete(Guid id)
    {
        employeeRepository.Remove(id);
        return RedirectToPage();
    }
}