using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using foundry_assessment_RAZOR.Model;
using foundry_assessment_RAZOR.API;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace foundry_assessment_RAZOR.Pages.Client
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ClientClass client { get; set; }
        public ClientAPI clientAPI = new ClientAPI();
        public ActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            client = clientAPI.FindClientByID(id);

            if (client == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                clientAPI.UpdateClient(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return RedirectToPage("../Clients");

        }
    }
}



