using Entites.EntitiesExernal;

namespace Domain.InterfacesExternal
{
    public interface IProduto
    {
        Produto GetOne(int codigo);

        Produto Create(Produto produto);

        Produto Update(Produto produto);

        Produto Delete(int codigo);

        List<Produto> List();
    }
}