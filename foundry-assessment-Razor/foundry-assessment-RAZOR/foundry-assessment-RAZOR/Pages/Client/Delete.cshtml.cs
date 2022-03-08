using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using foundry_assessment_RAZOR.Model;
using foundry_assessment_RAZOR.API;

namespace foundry_assessment_RAZOR.Pages.Client
{
    public class DeleteModel : PageModel
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

        public async Task<ActionResult> OnPostAsync()
        {
            try
            {
                await clientAPI.DeleteClient(client.id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("debug");
            return RedirectToPage("../Clients");

        }
    }
}