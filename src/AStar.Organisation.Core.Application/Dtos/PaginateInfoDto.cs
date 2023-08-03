namespace AStar.Organisation.Core.Application.Dtos
{
    public class PaginateInfoDto<T>
    {
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}