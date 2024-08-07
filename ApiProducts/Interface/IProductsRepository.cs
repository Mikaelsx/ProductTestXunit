using ApiProducts.Domains;
using System.Collections.Generic;

namespace ApiProducts.Interface
{
    public interface IProductsRepository
    {
        Task CadastrarAsync(Products products);
        Task<List<Products>> ListarAsync();
        Task<Products> BuscarPorIdAsync(Guid id);
        Task<Products> AtualizarPorIdAsync(Guid id, Products products);
        Task<Products> DeletarPorIdAsync(Guid id, Products products);

    }
}
