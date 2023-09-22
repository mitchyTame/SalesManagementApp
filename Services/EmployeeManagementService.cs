using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Entities;
using SalesManagementApp.Extensions;
using SalesManagementApp.Models;
using SalesManagementApp.Services.Contracts;

namespace SalesManagementApp.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        public EmployeeManagementService(SalesManagementDbContext smDbContext)
        {
            this.smDbContext = smDbContext;
        }

        public SalesManagementDbContext smDbContext { get; }

        public async Task<Employee> AddEmployee(EmployeeModel employeeModel)
        {
            Employee employeeToAdd = employeeModel.Convert();

            var result = await smDbContext.Employees.AddAsync(employeeToAdd);

            await smDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteEmployee(EmployeeModel employeeModel)
        {
            try
            {
             
                var employeeToUpdate = await smDbContext.Employees.FindAsync(employeeModel.Id);

                if(employeeToUpdate != null)
                {
                    smDbContext.Remove(employeeToUpdate.Id);

                    await smDbContext.SaveChangesAsync();
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            try
            {
                return await smDbContext.Employees.Convert();
            }
            catch(Exception) {
                throw;
            }
        }

        public async Task<List<EmployeeJobTitleModel>> GetJobTitles()
        {
            try
            {
                return await smDbContext.EmployeeJobTitles.Convert();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<List<ReportToModel>> GetReportToEmployees()
        {
            
            try
            {
                var employees = await (from e in this.smDbContext.Employees
                                       join j in this.smDbContext.EmployeeJobTitles
                                       on e.EmployeeJobTitleId equals j.EmployeeJobTitleId
                                       where j.Name.ToUpper() == "TL" || j.Name.ToUpper() == "SM"
                                       select new ReportToModel
                                       {
                                           ReportToEmpId = e.Id,
                                           ReportToName = e.FirstName + " " + e.LastName.Substring(0, 1).ToUpper() + "."
                                       }).ToListAsync();
                employees.Add(new ReportToModel { ReportToEmpId = null, ReportToName = "<None>" });
                return employees.OrderBy(o=> o.ReportToEmpId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var employeeToUpdate = await smDbContext.Employees.FindAsync(employeeModel.Id);

                if (employeeToUpdate != null) {

                    employeeToUpdate.FirstName = employeeModel.FirstName;
                    employeeToUpdate.LastName = employeeModel.LastName;
                    employeeToUpdate.ReportToEmpId = employeeModel.ReportToEmpId;
                    employeeToUpdate.DateOfBirth = employeeModel.DateOfBirth;
                    employeeToUpdate.ImagePath = employeeModel.ImagePath;
                    employeeToUpdate.Gender = employeeModel.Gender;
                    employeeToUpdate.Email = employeeModel.Email;
                    employeeToUpdate.EmployeeJobTitleId = employeeModel.EmployeeJobTitleId;

                    await smDbContext.SaveChangesAsync();
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
