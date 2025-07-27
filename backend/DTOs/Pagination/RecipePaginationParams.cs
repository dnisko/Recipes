namespace DTOs.Pagination
{
    public class RecipePaginationParams : BasePaginationParams
    {
        public int? CategoryId { get; set; }
        public int? Difficulty { get; set; }
        public string? CreatedBy { get; set; }
    }
}
