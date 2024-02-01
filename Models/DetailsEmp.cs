using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulk_Email_Sending_Groupwise.Models
{
    public class DetailsEmp
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int Emp_ID { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }
    }
}
