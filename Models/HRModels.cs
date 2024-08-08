using System.ComponentModel.DataAnnotations;

namespace LearningLINQWithSQL.Models
{
    //public class Employee
    //{
    //    public int EmployeeID { get; set; }
    //    [Required]
    //    public string? FirstName { get; set; }
    //    public string? LastName { get; set; }
    //    public int? Age { get; set; } // Nullable
    //    public int? SalaryID { get; set; } // Nullable foreign key

    //    public virtual Salary? Salary { get; set; } // Navigation property
    //}

    //public class Salary
    //{
    //    public int SalaryID { get; set; }
    //    [Required]
    //    public string? SalaryGroup { get; set; }

    //    [Required]
    //    public int SalaryAmount { get; set; }

    //    public virtual ICollection<Employee>? Employees { get; set; } // Navigation property
    //}

    public class EmployeeSalaryViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? SalaryGroup { get; set; }

        public string? Salary { get; set; }
    }


}
