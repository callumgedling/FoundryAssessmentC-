using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using foundry_assessment_RAZOR.Model;
using foundry_assessment_RAZOR.API;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace foundry_assessment_RAZOR.Pages.Employee
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public EmployeeClass employee { get; set; }
        public EmployeeAPI employeeAPI = new EmployeeAPI();
        public ActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            employee = employeeAPI.FindEmployeeByID(id);

            if (employee == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                employeeAPI.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return RedirectToPage("../Employees");

        }
    }
}



