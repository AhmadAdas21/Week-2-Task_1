using System.ComponentModel.DataAnnotations;

namespace Week_2_Task_1.dto
{
    public class update_customer
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(130)]
        public string email { get; set; } 

    }
}
