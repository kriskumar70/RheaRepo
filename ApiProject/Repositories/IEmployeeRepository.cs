using ApiProject.Models;

namespace ApiProject.Repositories
{
    public interface IEmployeeRepository
    {
        //loading employee + department + skills + profile
        //GetAllEmployeesWithDetails()
        List<Employee> GetAllEmployeesWithDetails();

        //GetEmployeeByIdWithDetails(id)
        Employee GetEmployeeByIdWithDetails(int id);

        //Add(employee, profil, skillIds)
        void Add(Employee employee, Profile profile, List<int> skillIds);

        //Update
        void Update(Employee employee, Profile profile, List<int> skillIds);

        //Delete
        void Delete(int id);

        //Save
        void Save();
    }
}
