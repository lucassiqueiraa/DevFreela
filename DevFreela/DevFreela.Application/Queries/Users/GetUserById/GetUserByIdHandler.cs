using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Users.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserViewModel>>
    {
        private readonly IUserRepository _repository;
        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return Result<UserViewModel>.Failure(ErrorType.NotFound, "User not found");
            }

            var model = UserViewModel.FromEntity(user);

            return Result<UserViewModel>.Success(model);

        }


    }
}
