using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string Search, int Page, int Size);
        Task<Project?> GetDetailsByIdAsync(int id);
        Task<Project?> GetByIdAsync(int id);
        Task<int> Add(Project project);
        Task Update(Project project);
        Task AddComment(ProjectComment comment);
        Task<bool> Exists(int id);


    }
}
