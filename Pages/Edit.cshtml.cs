using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages
{
    public class EditModel : PageModel
    {
        private readonly Home_Repair _homeRepair;

        [BindProperty] public Guid Id { get; set; }
        [BindProperty] public string Description { get; set; }
        [BindProperty] public string Location { get; set; }
        [BindProperty] public bool IsCompleted { get; set; }

        public EditModel(Home_Repair homeRepair)
        {
            _homeRepair = homeRepair;
        }

        public IActionResult OnGet(Guid id)
        {
            var task = _homeRepair.GetById(id);
            if (task == null) return RedirectToPage("/Index");

            Id = id;
            Description = task.Fullname;
            Location = task.Job;
            IsCompleted = task.Fired;
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var update = new HomeRep
            {
                Id = Id,
                Fullname = Description,
                Job = Location,
                Fired = IsCompleted
            };
            _homeRepair.Update(update);
            return RedirectToPage("/Index");
        }
    }
}