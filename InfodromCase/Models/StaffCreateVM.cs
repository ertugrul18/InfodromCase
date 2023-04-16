using Microsoft.AspNetCore.Mvc.Rendering;

namespace InfodromCase.Models
{
    public class StaffCreateVM
    {
        public int RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<SelectListItem>? departmentDropodowns  { get; set; }
        public int DepartmentId { get; set; }

    }
}
