using Domain.Interfaces;
using Entites.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {
        private readonly DbContextOptions<ContextBase> _dbContextOptions;

        public RepositoryMessage()
        {
            _dbContextOptions = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Message>> ListarMessage(Expression<Func<Message, bool>> exMessage)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                return await banco.Message.Where(exMessage).AsNoTracking().ToListAsync();
            }
        }
    }
}