using Entites.Entities;

namespace Domain.Interfaces.InterfacesServices
{
    public interface IServiceMessage
    {
        Task Adicionar(Message message);

        Task Atualizar(Message message);

        Task<List<Message>> ListarMessageAtivas();
    }
}