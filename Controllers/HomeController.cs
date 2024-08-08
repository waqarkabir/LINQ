using LearningLINQWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LearningLINQWithSQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public TestDatabaseContext Context { get; }

        public HomeController(ILogger<HomeController> logger, TestDatabaseContext Context)
        {
            _logger = logger;
            this.Context = Context;
        }
        public IActionResult Index()
        { 
            var employees= Context
                .Employees
                .Include(e => e.SalaryNav)
                .ToList();
            var model = employees.Select(e => new EmployeeSalaryViewModel
            { 
                Age = e.Age,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Salary = e.Salary.ToString(),
                SalaryGroup = e.SalaryNav?.SalaryGroup
            }).ToList();


            return View(model);
        }
        public IActionResult InnerJoin()
        {
            var model = from emp in Context.Employees
                        join sal in Context.Salaries
                        on emp.Salary equals sal.SalaryId
                        select new EmployeeSalaryViewModel
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Age = emp.Age,
                            SalaryGroup = sal.SalaryGroup,
                            Salary = $"{sal.SalaryId:C}",
                        };

            return View(model.ToList());
        }

        public IActionResult LeftJoin()
        {
            var model = from emp in Context.Employees
                        join sal in Context.Salaries
                        on emp.Salary equals sal.SalaryId into empSalary
                        from sal in empSalary.DefaultIfEmpty()
                        select new EmployeeSalaryViewModel
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Age = emp.Age,
                            SalaryGroup = sal.SalaryGroup,
                            Salary = $"{sal.SalaryId:C}",
                        };

            return View(model.ToList());
        }

        public IActionResult RightJoin()
        {
            var model = from sal in Context.Salaries
                        join emp in Context.Employees
                        on sal.SalaryId equals emp.Salary into empSalary
                        from emp in empSalary.DefaultIfEmpty()
                        select new EmployeeSalaryViewModel
                        {
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Age = emp.Age,
                            SalaryGroup = sal.SalaryGroup,
                            Salary = $"{sal.SalaryId:C}",
                        };

            return View(model.ToList());
        }

        public IActionResult Union()
        {
            // Select data from both tables
            var emp = Context.Employees.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.Age,
                e.Salary
            }).ToList();

            var emp2 = Context.Employees2.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.Age,
                e.Salary
            }).ToList();

            // Perform the union operation
            var empUnion = emp.Union(emp2).ToList();

            // Project the union result into EmployeeSalaryViewModel
            var employeeSalaryViewModels = from empu in empUnion
                                           join sal in Context.Salaries
                                           on empu.Salary equals sal.SalaryId
                                           select new EmployeeSalaryViewModel
                                           {
                                               FirstName = empu.FirstName,
                                               LastName = empu.LastName,
                                               Age = empu.Age,
                                               SalaryGroup = sal.SalaryGroup,
                                               Salary = $"{sal.SalaryId:C}",
                                           };

            // Pass the view model to the view
            return View(employeeSalaryViewModels);
        }

        public IActionResult UnionAllByConcat()
        {
            // Select data from both tables
            var emp = Context.Employees.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.Age,
                e.Salary
            }).ToList();

            var emp2 = Context.Employees2.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.Age,
                e.Salary
            }).ToList();

            // Perform the union operation
            var empUnion = emp.Concat(emp2).ToList();

            // Project the union result into EmployeeSalaryViewModel
            var employeeSalaryViewModels = from empu in empUnion
                                           join sal in Context.Salaries
                                           on empu.Salary equals sal.SalaryId
                                           select new EmployeeSalaryViewModel
                                           {
                                               FirstName = empu.FirstName,
                                               LastName = empu.LastName,
                                               Age = empu.Age,
                                               SalaryGroup = sal.SalaryGroup,
                                               Salary = $"{sal.SalaryId:C}",
                                           };

            // Pass the view model to the view
            return View(employeeSalaryViewModels);
        }

        public IActionResult Count()
        {
            int totalEmployees = Context.Employees.Count();
            int employeesWithTaxableSalary = Context.Employees.Count(e => e.Salary > 50000);
            var employeeNamesWithTaxableSalary = Context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new 
                {
                    e.FirstName,
                    e.Salary
                })
                .ToList();
            var allEmployeesWithSalaries = Context.Employees
                .Select(x => new
                { 
                    x.FirstName,
                    x.Salary
                }).ToList();
            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.AllEmployeesWithSalaries = allEmployeesWithSalaries;
            ViewBag.EmployeeNamesWithTaxableSalary = employeeNamesWithTaxableSalary;
            ViewBag.EmployeesWithTaxableSalary = employeesWithTaxableSalary;

           return View();
        }

        public IActionResult Sum()
        {
            var model = Context.Employees
                .GroupBy(e => e.Age)
                .Select(g => new
                {
                   Age = g.Key,
                   TotalSalary = g.Sum(e => e.Salary)
                }).ToList();

            var allEmployeesAgewise= Context.Employees
             .Select(x => new
             {
                 x.FirstName,
                 x.Age
             }).ToList();
            ViewBag.AllEmployeesAgewise = allEmployeesAgewise;

            ViewBag.SalaryGroupedByAge = model;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
