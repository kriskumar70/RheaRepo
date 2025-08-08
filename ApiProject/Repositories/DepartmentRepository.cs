using ApiProject.Models;

namespace ApiProject.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApiProjectDatabaseContext _context;
        public DepartmentRepository(ApiProjectDatabaseContext context)
        {
            _context = context;
        }
        public void Add(Department department)
        {
            _context.Departments.Add(department);
        }

        public void Delete(int id)
        {
           var departmentToDelete = GetDepartmentById(id);
            _context.Departments.Remove(departmentToDelete);
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.Find(id);
        }

        public List<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }
    }
}
