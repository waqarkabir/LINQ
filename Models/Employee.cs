using System;
using System.Collections.Generic;

namespace LearningLINQWithSQL.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    public int? Salary { get; set; }

    public virtual Salary SalaryNav { get; set; }
}
