using Domain.Interfaces;
using Domain.Interfaces.InterfacesServices;
using Entites.Entities;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _message;

        public ServiceMessage(IMessage message)
        {
            _message = message;
        }

        public async Task Adicionar(Message message)
        {
            var validaTitulo = message.ValidarPropriedadeString(message.Titulo, "Titulo");

            if (validaTitulo)
            {
                message.Ativo = true;
                message.DataCadastro = DateTime.Now;
                message.DataAlteracao = DateTime.Now;

                await _message.Add(message);
            }
        }

        public async Task Atualizar(Message message)
        {
            var validaTitulo = message.ValidarPropriedadeString(message.Titulo, "Titulo");

            if (validaTitulo)
            {
                message.DataAlteracao = DateTime.Now;

                await _message.Update(message);
            }
        }

        public async Task<List<Message>> ListarMessageAtivas()
        {
            return await _message.ListarMessage(n => n.Ativo);
        }
    }
}