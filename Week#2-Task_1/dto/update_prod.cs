using System.ComponentModel.DataAnnotations;

namespace Week_2_Task_1.dto
{
    public class update_prod
    {
        [Required]
        [StringLength(100)]

        public string name { get; set; }
        [Required]
       public float price { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
