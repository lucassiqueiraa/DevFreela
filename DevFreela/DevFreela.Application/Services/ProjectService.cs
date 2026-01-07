using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectService(DevFreelaDbContext context)
        {
            _dbContext = context;
        }
        public Result Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //project trackeado

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            project.Complete();
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return Result.Success();
        }

        public Result Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //project trackeado

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            project.SetAsDeleted();
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return Result.Success();
        }

        public Result<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects
                .Select(ProjectItemViewModel.FromEntity)
                .ToList();

            return Result<List<ProjectItemViewModel>>.Success(model);
        }

        public Result<ProjectViewModel> GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            if(project is null)
            {
                return Result<ProjectViewModel>.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            var model = ProjectViewModel.FromEntity(project);

            return Result<ProjectViewModel>.Success(model);
        }

        public Result<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return Result<int>.Success(project.Id);
        }

        public Result InsertComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            var comment = model.ToEntity();

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return Result.Success();   
        }

        public Result Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //project trackeado

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound, "Projeto não encontrado.");
            }

            project.Start();
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return Result.Success();
        }

        public Result Update(UpdateProjectInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == model.IdProject); //project trackeado

            if (project is null)
            {
                return Result.Failure(ErrorType.NotFound ,"Projeto não encontrado.");
            }

            project.Update(model.Title, model.Description, model.TotalCost); //talvez desnecessário pq ja está trackeado

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return Result.Success();
        }
    }

}
