namespace Presentation.DTOs
{
    public class ProductUpdateDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public int? CategoryId { get; set; }

        public ProductUpdateDto(ProductFrontDto selected)
        {
            if (selected != null)
            {
                Name = selected.Name;
                Description = selected.Description;
                Price = selected.Price;
                Status = selected.Status;
                CategoryId = selected.CategoryId;
            }
        }

        public ProductUpdateDto() { }
    }
}
