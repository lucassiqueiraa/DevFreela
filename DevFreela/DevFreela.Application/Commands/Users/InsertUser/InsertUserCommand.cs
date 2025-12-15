using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users.InsertUser
{
    public class InsertUserCommand : IRequest<ResultViewModel<int>>
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        public User ToEntity()
            => new User(FullName, Email, BirthDate, true);

    }
}
