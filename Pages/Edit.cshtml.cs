using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages
{
    public class EditModel : PageModel
    {
        private readonly EmployeeRepository employeeRepository;

        [BindProperty] public Guid Id { get; set; }

        [BindProperty] public string FullName { get; set; }

        [BindProperty] public string Job { get; set; }

        [BindProperty] public bool Fired { get; set; }

        [BindProperty] public decimal Salary { get; set; }

        public EditModel(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IActionResult OnGet(Guid id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee == null) return RedirectToPage("/Index");

            Id = id;
            FullName = employee.Fullname;
            Job = employee.Job;
            Fired = employee.Fired;
            Salary = employee.Salary;
            
            return Page();
        }

        public IActionResult OnPost()
        {
            var update = new Employee
            {
                Id = Id,
                Fullname = FullName,
                Job = Job,
                Fired = Fired,
                Salary = Salary
            };
            employeeRepository.Update(update);
            return RedirectToPage("/Index");
        }
    }
}