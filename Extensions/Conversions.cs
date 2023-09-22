using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Entities;
using SalesManagementApp.Models;

namespace SalesManagementApp.Extensions
{
    public static class Conversions
    {
        public static async Task<List<EmployeeModel>> Convert(this IQueryable<Employee> employees)
        {
            return await (from e in employees
                          select new EmployeeModel
                          {
                              Id = e.Id,
                              FirstName = e.FirstName,
                              LastName = e.LastName,
                              DateOfBirth = e.DateOfBirth,
                              EmployeeJobTitleId = e.EmployeeJobTitleId,
                              Email = e.Email,
                              Gender = e.Gender,
                              ReportToEmpId = e.ReportToEmpId,
                              ImagePath = e.ImagePath,
                          }
                          ).ToListAsync();
        }

        public static async Task<List<EmployeeJobTitleModel>> Convert(this IQueryable<EmployeeJobTitle> jobTitles)
        {
            return await (from e in jobTitles
                          select new EmployeeJobTitleModel
                          {
                             EmployeeJobTitleId = e.EmployeeJobTitleId,
                             Name = e.Name,
                             Description = e.Description,
                          }
                          ).ToListAsync();
        }

        public static Employee Convert(this EmployeeModel employeeModel)
        {
            return new Employee
            {
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                DateOfBirth = employeeModel.DateOfBirth,
                Gender = employeeModel.Gender,
                EmployeeJobTitleId = employeeModel.EmployeeJobTitleId,
                Email = employeeModel.Email,
                ReportToEmpId = employeeModel.ReportToEmpId,
                ImagePath = employeeModel.Gender.ToUpper() == "MALE" ? "Images/Profile/MaleDefault.jpg" : "Images/Profile/FemaleDefault.jpg"
            };

        }
    }
}
