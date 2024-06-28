using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Home_Repair _homeRepair;
        public IList<HomeRep> Repair { get; private set; }

        public IndexModel(Home_Repair homeRepair)
        {
            _homeRepair = homeRepair;
        }

        public void OnGet()
        {
            Repair = _homeRepair.List();
        }
        
        public IActionResult OnPostDelete(Guid id)
        {
            _homeRepair.Remove(id);
            return RedirectToPage();
        }
    }
}