using InfodromCase.Entities;

namespace InfodromCase.Models
{
    public class DepartmentIndexVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public IList<DepartmentIndexVM> Children { get; set; }
    }
}
