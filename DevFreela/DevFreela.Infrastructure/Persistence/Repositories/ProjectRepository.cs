using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _context;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<int> AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task AddCommentAsync(ProjectComment comment)
        {
            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAllAsync(string Search, int Page, int Size)
        {
            var projects = await _context.Projects
                            .Include(p => p.Client)
                            .Include(p => p.Freelancer)
                            .Where(p => !p.IsDeleted && (Search == "" || p.Title.Contains(Search) || p.Description.Contains(Search)))
                            .Skip(Page * Size)
                            .Take(Size)
                            .ToListAsync();

            return projects;
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetDetailsByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;

        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
