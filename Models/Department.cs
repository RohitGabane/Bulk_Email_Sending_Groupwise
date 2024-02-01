using System.ComponentModel.DataAnnotations;

namespace Bulk_Email_Sending_Groupwise.Models
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }

        [Required(ErrorMessage = "Dept_Name is required")]
        [StringLength(15, ErrorMessage = "Dept_Name cannot exceed 15 characters.")]
        public string Dept_Name { get; set; }

        public ICollection<DetailsEmp> DetailsEmp { get; set; } = new List<DetailsEmp>();
    }
}
