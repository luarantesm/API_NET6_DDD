using Domain.Interfaces;
using Entites.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {
        private readonly DbContextOptions<ContextBase> _dbContextOptions;

        public RepositoryMessage()
        {
            _dbContextOptions = new DbContextOptions<ContextBase>();
        }
    }
}