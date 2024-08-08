using System;
using System.Collections.Generic;

namespace LearningLINQWithSQL.Models;

public class Salary
{
    public int SalaryId { get; set; }

    public string SalaryGroup { get; set; } = null!;

    public IEnumerable<Employee>? EmployeesWithSalary { get; set; }
}
