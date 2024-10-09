using System.ComponentModel.DataAnnotations;

namespace SpendSmartV2.Models
{
    public class Expenses
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Discerption { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Value { get; set; }
    }
}
