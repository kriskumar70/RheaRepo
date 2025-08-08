using ApiProject.Models;

namespace ApiProject.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        //in this repository, we want to do data access for the Skills table
        //we can do the data access, only when the ApiProjectyDatabaseContext object is injected in the class constructor

        //global variable
        private readonly ApiProjectDatabaseContext _context;
        public SkillRepository(ApiProjectDatabaseContext context)
        {
            _context = context;
        }

        //crud operations
        public void Add(Skill skill)
        {
            _context.Skills.Add(skill);
        }

        public void Delete(int id)
        {
            //delete from table where Id = 23
            var skillToDelete = GetSkillById(id);
            _context.Skills.Remove(skillToDelete);
        }

        public Skill GetSkillById(int id)
        {
            return _context.Skills.Find(id);
        }

        public List<Skill> GetSkills()
        {
           return _context.Skills.ToList(); //LINQ immeidate execution
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Skill skill)
        {
            _context.Skills.Update(skill);
        }
    }
}
