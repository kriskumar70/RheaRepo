using ApiProject.Models;

namespace ApiProject.Repositories
{
    public interface ISkillRepository
    {
        //empty CRUD operations methods
        List<Skill> GetSkills();
        Skill GetSkillById(int id);
        void Add(Skill skill);
        void Update(Skill skill);
        void Delete(int id);
        void Save();
    }
}
