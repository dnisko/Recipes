namespace DTOs.Pagination
{
    public class BasePaginationParams : IPaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SearchKeyword { get; set; }
        public int? Id { get; set; }

        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc";
    }
}
