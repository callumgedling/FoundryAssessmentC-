
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using foundry_assessment_RAZOR.API;
using foundry_assessment_RAZOR.Model;

namespace foundry_assessment_RAZOR.Pages
{
    public class ClientsModel : PageModel
    {
        [BindProperty]
        public string NewClientName { get; set; }
        public ClientName clientName = new ClientName();
        public ClientAPI clientAPI = new ClientAPI();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientName.name = NewClientName;
            clientAPI.CreateClient(clientName);
        }
    }
}



