using Microsoft.AspNetCore.Mvc.ModelBinding;
using ApiProject.Models;

namespace ApiProject.Repositories
{
    public interface IDepartmentRepository
    {
        //empty methods
        //crud methods
        //get, add, update, delete, and save
        List<Department> GetDepartments();
        Department GetDepartmentById(int id);
        void Add(Department department);
        void Update(Department department);
        void Delete(int id);
        void Save();//SaveChanges of ef core
    }
}
