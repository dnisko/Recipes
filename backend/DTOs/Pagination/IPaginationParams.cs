namespace DTOs.Pagination
{
    public interface IPaginationParams
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // Optional filters
        public string? SearchKeyword { get; set; }
        public int? Id { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
    }
}
