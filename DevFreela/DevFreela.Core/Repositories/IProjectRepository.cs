using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string Search, int Page, int Size);
        Task<Project?> GetDetailsByIdAsync(int id);
        Task<Project?> GetByIdAsync(int id);
        Task<int> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task AddCommentAsync(ProjectComment comment);
        Task<bool> ExistsAsync(int id);


    }
}
