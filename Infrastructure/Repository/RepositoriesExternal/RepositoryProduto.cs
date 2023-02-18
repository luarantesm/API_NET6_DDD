using Domain.InterfacesExternal;
using Entites.EntitiesExernal;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Repository.RepositoriesExternal
{
    public class RepositoryProduto : IProduto
    {
        private readonly string urlApi = "http://localhost:4200/";

        public Produto GetOne(int codigo)
        {
            var produtoCriado = new Produto();
            produtoCriado.Codigo = codigo;

            try
            {
                using (var cliente = new HttpClient())
                {
                    string jsonObj = JsonConvert.SerializeObject(produtoCriado);
                    var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                    var resposta = cliente.PostAsync(urlApi + "GetOne", content);
                    resposta.Wait();

                    if (resposta.Result.IsSuccessStatusCode)
                    {
                        var retorno = resposta.Result.Content.ReadAsStringAsync();
                        produtoCriado = JsonConvert.DeserializeObject<Produto>(retorno.Result);
                    }
                }

                return produtoCriado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Produto> List()
        {
            var retorno = new List<Produto>();

            try
            {
                using (var cliente = new HttpClient())
                {
                    var resposta = cliente.GetStringAsync(urlApi + "List");
                    resposta.Wait();

                    retorno = JsonConvert.DeserializeObject<Produto[]>(resposta.Result).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }

        public Produto Create(Produto produto)
        {
            var produtoCriado = new Produto();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string jsonObj = JsonConvert.SerializeObject(produto);
                    var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                    var resposta = cliente.PostAsync(urlApi + "Create", content);
                    resposta.Wait();

                    if (resposta.Result.IsSuccessStatusCode)
                    {
                        var retorno = resposta.Result.Content.ReadAsStringAsync();
                        produtoCriado = JsonConvert.DeserializeObject<Produto>(retorno.Result);
                    }
                }

                return produtoCriado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Produto Update(Produto produto)
        {
            var produtoCriado = new Produto();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string jsonObj = JsonConvert.SerializeObject(produto);
                    var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                    var resposta = cliente.PostAsync(urlApi + "Update", content);
                    resposta.Wait();

                    if (resposta.Result.IsSuccessStatusCode)
                    {
                        var retorno = resposta.Result.Content.ReadAsStringAsync();
                        produtoCriado = JsonConvert.DeserializeObject<Produto>(retorno.Result);
                    }
                }

                return produtoCriado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Produto Delete(int codigo)
        {
            var produtoCriado = new Produto();
            produtoCriado.Codigo = codigo;

            try
            {
                using (var cliente = new HttpClient())
                {
                    string jsonObj = JsonConvert.SerializeObject(produtoCriado);
                    var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                    var resposta = cliente.PostAsync(urlApi + "Delete", content);
                    resposta.Wait();

                    if (resposta.Result.IsSuccessStatusCode)
                    {
                        var retorno = resposta.Result.Content.ReadAsStringAsync();
                        produtoCriado = JsonConvert.DeserializeObject<Produto>(retorno.Result);
                    }
                }

                return produtoCriado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}