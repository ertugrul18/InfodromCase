namespace InfodromCase.Entities
{
    public class Staff : BaseEntity
    {
        public int RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }

    }
}
