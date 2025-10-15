using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class CreateSkillInputModel
    {
        public string Description { get; set; }

        public Skill ToEntity() => new Skill(Description);
    }
}
