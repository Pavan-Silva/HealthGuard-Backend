namespace HealthGuard.Application.DTOs
{
    public class PaginatedList<T>
    {
        public List<T> Data { get; }

        public int PageIndex { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> data, int pageIndex, int totalPages, int totalCount)
        {
            Data = data;
            PageIndex = pageIndex;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }
    }
}
