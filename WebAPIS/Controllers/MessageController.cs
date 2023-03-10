using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfacesServices;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMessage;
        private readonly IServiceMessage _serviceMessage;

        public MessageController(IMapper IMapper, IMessage IMessage, IServiceMessage serviceMessage)
        {
            _IMapper = IMapper;
            _IMessage = IMessage;
            _serviceMessage = serviceMessage;
    }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await RetornarIdUsuarioLogado()
                ;
            var messageMap = _IMapper.Map<Message>(message);
            //await _IMessage.Add(messageMap);
            await _serviceMessage.Adicionar(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            //await _IMessage.Update(messageMap);
            await _serviceMessage.Atualizar(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            await _IMessage.Delete(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _IMessage.GetEntityById(message.Id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);

            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var mensagens = await _IMessage.List();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);

            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListarAtivos")]
        public async Task<List<MessageViewModel>> ListarAtivos()
        {
            //var mensagens = await _IMessage.List();
            var mensagens = await _serviceMessage.ListarMessageAtivas();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);

            return messageMap;
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }

            return string.Empty;
        }
    }
}