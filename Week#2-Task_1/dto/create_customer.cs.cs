using System.ComponentModel.DataAnnotations;

namespace Week_2_Task_1.dto
{
    public class create_customer
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; } 

        [Required]
        [EmailAddress]
        [StringLength(120)]
        public string email { get; set; } 

    }
}
