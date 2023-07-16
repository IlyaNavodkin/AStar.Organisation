namespace AStar.Organisation.Core.Domain.Entities.old
{
    public class Department : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}