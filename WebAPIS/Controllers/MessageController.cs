using AutoMapper;
using Domain.Interfaces;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIS.Models;

namespace WebAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessage _message;

        public MessageController(IMapper mapper, IMessage message)
        {
            _mapper = mapper;
            _message = message;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _message.GetEntityById(message.Id);
            var messageMap = _mapper.Map<MessageViewModel>(message);

            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var messages = await _message.List();
            var messagesMap = _mapper.Map<List<MessageViewModel>>(messages);

            return messagesMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await RetornarIdUsuarioLogado();

            var messageMap = _mapper.Map<Message>(message);
            await _message.Add(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            var messageMap = _mapper.Map<Message>(message);
            await _message.Update(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            var messageMap = _mapper.Map<Message>(message);
            await _message.Delete(messageMap);

            return messageMap.Notificacoes;
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