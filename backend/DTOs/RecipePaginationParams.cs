namespace DTOs
{
    public class RecipePaginationParams : PaginationParams
    {
        //public string? SearchKeyword { get; set; }
        public int? CategoryId { get; set; }
        public int? Difficulty { get; set; }
    }
}
