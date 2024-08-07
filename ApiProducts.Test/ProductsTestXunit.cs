using ApiProducts.Domains;
using ApiProducts.Interface;
using ApiProducts.Repositories;
using Moq;

namespace ApiProducts.Test
{
    public class ProductsTestXunit
    {
        // Teste Para A Funcionalidade de Listar Todos os Produtos
        [Fact]
        public async Task Get()
        {
            // Arrange
            List<Products> productList = new List<Products>
    {
                new Products {Id = Guid.NewGuid(), Name = "Produto1", Price = 70},
                new Products {Id = Guid.NewGuid(), Name = "Produto2", Price = 80},
                new Products {Id = Guid.NewGuid(), Name = "Produto3", Price = 90},
    };

            var mockRepository = new Mock<IProductsRepository>(); // Assuming IProductsRepository has ListarAsync()

            mockRepository.Setup(x => x.ListarAsync()).ReturnsAsync(productList); // Note the use of ReturnsAsync

            // Act
            var result = await mockRepository.Object.ListarAsync(); // Properly await the asynchronous call

            // Assert
            Assert.Equal(3, result.Count);
        }

        // Teste Para A Funcionalidade de Cadastrar Novos Produtos
        [Fact]
        public async Task Post()
        {
            // Arrange
            // Criar Objeto
            Products product = new Products { Id = Guid.NewGuid(), Name = "Rolex", Price = 99 };

            // Criar Lista
            List<Products> productList = new List<Products>();
            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.CadastrarAsync(product)).Callback<Products>(x => productList.Add(product));

            // Act
            mockRepository.Object.CadastrarAsync(product);

            // Assert
            Assert.Contains(product, productList);
        }

        // Teste Para A Funcionalidade de Deletar Produtos

        // Teste Para A Funcionalidade de Buscar Produtos Por Id

        // Teste Para A Funcionalidade de Atualizar os Produtos

    }
}