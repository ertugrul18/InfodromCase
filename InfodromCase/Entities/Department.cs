namespace InfodromCase.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
    }
}
