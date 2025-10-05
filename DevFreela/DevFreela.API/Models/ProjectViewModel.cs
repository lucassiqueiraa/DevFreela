using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, string description, int idClient, int idFreelance, string clientName, string freelancerName, decimal totalCost, List<ProjectComment> comments)
        {
            Id = id;
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelance = idFreelance;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
            Comments = comments.Select(c => c.Content).ToList(); // Extracting only the content of comments
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelance { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<string> Comments { get; private set; }

        // Static method to create a ProjectViewModel from a Project entity
        public static ProjectViewModel FromEntity(Project entity)

            => new(
                entity.Id,
                entity.Title,
                entity.Description,
                entity.IdClient,
                entity.IdFreelancer,
                entity.Client.FullName,
                entity.Freelancer.FullName,
                entity.TotalCost,
                entity.Comments
            );

    }
}
