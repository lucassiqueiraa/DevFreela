namespace DevFreela.API.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, bool active)
            : base()
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = active;

            Skills = [];
            OwnedProjects = [];
            FreelanceProjects = [];
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Active  { get; private set; }

        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
    }
}
