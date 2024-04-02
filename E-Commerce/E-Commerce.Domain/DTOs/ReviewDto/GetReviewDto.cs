using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.ReviewDto
{
    public class GetReviewDto
    {
        public string nickName { get; set; }
        public string summary { get; set; }
        public string reviewText { get; set; }
        public decimal qualityRating { get; set; }
        public decimal valueRating { get; set; }
        public decimal priceRating { get; set; }
        //public Guid ProductId { get; set; }
    }
}
