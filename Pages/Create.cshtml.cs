using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class CreateModel : PageModel
{
    private readonly EmployeeRepository employeeRepository;

    [BindProperty] public string FullName { get; set; }

    [BindProperty] public string Job { get; set; }


    [BindProperty] public decimal Salary { get; set; }

    public CreateModel(EmployeeRepository employeeRepository) => this.employeeRepository = employeeRepository;
    
    public IActionResult OnPostAdd()
    {
        var employee = new Employee
        {
            Fullname = FullName,
            Job = Job,
            Fired = false,
            Salary = Salary
        };
        employeeRepository.Add(employee);
        return RedirectToPage("/Index");
    }
}