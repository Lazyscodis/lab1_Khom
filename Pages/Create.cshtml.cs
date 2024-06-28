using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class CreateModel : PageModel
{
    private readonly Home_Repair _homeRepair;

    [BindProperty] public string Description { get; set; }
    [BindProperty] public string Location { get; set; }
    [BindProperty] public bool IsCompleted { get; set; }

    public CreateModel(Home_Repair homeRepair) => _homeRepair = homeRepair;

    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var task = new HomeRep
        {
            Fullname = Description,
            Job = Location,
            Fired = IsCompleted
        };
        _homeRepair.Add(task);
        return RedirectToPage("/Index");
    }
}