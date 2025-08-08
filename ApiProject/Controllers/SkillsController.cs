using ApiProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        //global variable
        private readonly ISkillRepository _skillRepository;

        //when this app runs, the skillRepository parameter will get the object of the SkillRepository class
        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        //get all, get by id, create, edit, and delete
        [HttpGet]
        public IActionResult GetSkills()
        {
            var skills = _skillRepository.GetSkills();
            return Ok(skills);
        }
        [HttpGet("{id}")]
        public IActionResult GetSkillById(int id)
        {
            var skill = _skillRepository.GetSkillById(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }
        [HttpPost]
        public IActionResult CreateSkill(Skill skill) //model binding
        {
            //server-side validation 
            if (ModelState.IsValid)
            {
                _skillRepository.Add(skill);
                _skillRepository.Save();

                return Created("", new { Message = "Skill added successfully" });
            }
            return BadRequest(new { Error = "Error while creating new skill" }); //400
        }
        [HttpPut("{id}")]
        public IActionResult EditSkill(int id, Skill skill)
        {
            if (id != skill.Id)
                return BadRequest(new { Error = "Id in the url does not match the Id of the skill in the request body" });
            if (ModelState.IsValid)
            {
                _skillRepository.Update(skill);
                _skillRepository.Save();

                return Ok(new { Message = "Skill updated successfully" });
            }
            return BadRequest(new { Error = "Error while updating skill" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSkill(int id)
        {
            _skillRepository.Delete(id);
            _skillRepository.Save();

            return Ok(new { Message = "Skill deleted successfully" });
        }
    }
}
