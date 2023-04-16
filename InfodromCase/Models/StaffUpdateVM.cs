using Microsoft.AspNetCore.Mvc.Rendering;

namespace InfodromCase.Models
{
    public class StaffUpdateVM
    {
        public int StaffId { get; set; }
        public int StaffRegistrationNumber { get; set; }
        public string StaffName { get; set; }
        public string StaffSurname { get; set; }
        public IList<SelectListItem>? departmentDropodowns { get; set; }
        public int StaffDepartmentId { get; set; }
    }
}
