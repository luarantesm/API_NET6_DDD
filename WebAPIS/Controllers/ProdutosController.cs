using Domain.InterfacesExternal;
using Entites.EntitiesExernal;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProduto _produto;

        public ProdutosController(IProduto produto)
        {
            _produto = produto;
        }

        [Produces("application/json")]
        [HttpPost("/api/GetOne")]
        public Produto GetOne(int codigo)
        {
            return _produto.GetOne(codigo);
        }

        [Produces("application/json")]
        [HttpPost("/api/ListProduto")]
        public List<Produto> ListProduto()
        {
            return _produto.List();
        }

        [Produces("application/json")]
        [HttpPost("/api/CreateProduto")]
        public bool CreateProduto(Produto produto)
        {
            try
            {
                _produto.Create(produto);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Produces("application/json")]
        [HttpPost("/api/UpdateProduto")]
        public bool UpdateProduto(Produto produto)
        {
            try
            {
                _produto.Update(produto);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Produces("application/json")]
        [HttpPost("/api/DeleteProduto")]
        public bool DeleteProduto(int codigo)
        {
            try
            {
                _produto.Delete(codigo);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}