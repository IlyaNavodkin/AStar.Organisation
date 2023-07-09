namespace AStar.Organisation.Core.Domain.Entities
{
    public class Department : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}