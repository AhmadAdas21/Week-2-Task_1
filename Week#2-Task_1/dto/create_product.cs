using System.ComponentModel.DataAnnotations;

namespace Week_2_Task_1.dto
{
    public class create_product
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        [StringLength(100)]
        public string sku { get; set; }
        [Required]
        public float price { get; set; }
        [Required]
        public int stock { get; set; }

    }
}
