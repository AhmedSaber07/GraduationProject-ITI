
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.DTOs.ReviewDto
{
    public class CreateOrUpdateDto
    {
        public string nickName { get; set; }
        public string summary { get; set; }
        public string reviewText { get; set; }
        [Range(0, 5, ErrorMessage = "Quality rating must be between 0 and 5.")]
        public decimal qualityRating { get; set; }
        [Range(0, 5, ErrorMessage = "Value rating must be between 0 and 5.")]
        public decimal valueRating { get; set; }
        [Range(0, 5, ErrorMessage = "Price rating must be between 0 and 5.")]

        public decimal priceRating { get; set; }
        public Guid ProductId { get; set; }
    }
}
