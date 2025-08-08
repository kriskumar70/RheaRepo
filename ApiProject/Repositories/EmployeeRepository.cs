using ApiProject.Models;
using Microsoft.EntityFrameworkCore; //Include

namespace ApiProject.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //field or global variable
        private readonly ApiProjectDatabaseContext _context;
        public EmployeeRepository(ApiProjectDatabaseContext context)
        {
            _context = context;
        }

        public void Add(Employee employee, Profile profile, List<int> skillIds)
        {
            employee.Profile = profile;

            //skillIds: 1, 2, 3, 4

            //in order to assign skills to an employee, we need the complete skill object and not just the skillIds...

            //we have to load all the skills using the skillIds
            //10 skills: 1 2 3 4
            var skills = _context.Skills
                .Where(x => skillIds.Contains(x.Id))
                .ToList();
            foreach (var skill in skills)
            { 
                employee.Skills.Add(skill);
            }
            _context.Employees.Add(employee);
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }

        //we want to load all the employees + departments + profile + skills
        //We load employee + related data using Eager Loading
        //Employees.Include(x => x.Department).Include(x => x.Profile).Include(x => x.Skills)
        //Whenever we are fetching data, we can disable the change traker of ef core
        //by using AsNoTracking() method
        //by disabling the change tracker, we can improve the performance while fetching the data from the db
        public List<Employee> GetAllEmployeesWithDetails()
        {
            var employeesWithAllDetails = _context.Employees
                .Include(x => x.Profile)
                .Include(x => x.Department)
                .Include(x => x.Skills)
                .AsNoTracking()
                .ToList();

            return employeesWithAllDetails;
        }

        public Employee GetEmployeeByIdWithDetails(int id)
        {
            var employeesWithDetails = _context.Employees
                .Include(x => x.Profile)
                .Include(x => x.Department)
                .Include(x => x.Skills)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (employeesWithDetails == null)
            { 
             throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

                return employeesWithDetails;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Employee employee, Profile profile, List<int> skillIds)
        {
            var existingEmp = _context.Employees
                .Include(x => x.Profile)
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.Id == employee.Id);

            if (existingEmp == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employee.Id} not found.");
            }

            existingEmp.FirstName = employee.FirstName;
            existingEmp.LastName = employee.LastName;
            existingEmp.DepartmentId = employee.DepartmentId;
            existingEmp.Profile.Bio = profile.Bio;
            existingEmp.Profile.Email = profile.Email;

            existingEmp.Skills.Clear();

            var skills = _context.Skills
                .Where(x => skillIds.Contains(x.Id))
                .ToList();

            foreach (var skill in skills)
            { 
            existingEmp.Skills.Add(skill);
            }

           _context.Employees.Update(existingEmp);
        }
    }
}
