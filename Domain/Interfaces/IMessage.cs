using Domain.Interfaces.Generics;
using Entites.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IMessage : IGeneric<Message>
    {
        Task<List<Message>> ListarMessage(Expression<Func<Message, bool>> exMessage);
    }
}