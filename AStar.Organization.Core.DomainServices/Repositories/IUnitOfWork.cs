namespace AStar.Organisation.Core.DomainServices.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPositionRepository PositionRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        void Commit();
    }
}