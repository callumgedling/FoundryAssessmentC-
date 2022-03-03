
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using foundry_assessment_RAZOR.API;
using foundry_assessment_RAZOR.Model;

namespace foundry_assessment_RAZOR.Pages
{
    public class EmployeesModel : PageModel
    {
        public string NewEmployeeName { get; set; }
        public EmployeeName employeeName = new EmployeeName();
        public EmployeeAPI employeeAPI = new EmployeeAPI();
        public void OnGet()
        {
        }

        public void onPost()
        {
            employeeName.name = NewEmployeeName;
            employeeAPI.CreateEmployee(employeeName);
        }
    }
}



