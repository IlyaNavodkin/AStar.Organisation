namespace AStar.Organisation.Core.Domain.Entities.old
{
    public class Position : EntityBase
    {
        public string Name { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}