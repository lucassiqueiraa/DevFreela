using Azure;
using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{

    public interface IProjectService
    {
        Result<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3);
        Result<ProjectViewModel> GetById(int id);
        Result<int> Insert(CreateProjectInputModel model);
        Result Update(UpdateProjectInputModel model);
        Result Delete(int id);
        Result Start(int id);
        Result Complete(int id);
        Result InsertComment(int id, CreateProjectCommentInputModel model);
    }

}
