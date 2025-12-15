using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects.InsertComment
{
    public class InsertCommentCommand : IRequest<ResultViewModel>
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }

        public ProjectComment ToEntity()
            => new (Content, IdProject, IdUser);

    }
}   
