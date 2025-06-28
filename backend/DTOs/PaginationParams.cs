namespace DTOs
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Optional filters
        //public string? SearchKeyword { get; set; }
        //public int? CategoryId { get; set; }
        //public int? Difficulty { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc";
    }
}
