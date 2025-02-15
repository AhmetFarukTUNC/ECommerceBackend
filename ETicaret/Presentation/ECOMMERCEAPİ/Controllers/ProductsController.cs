using Application.Abstraction;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCEAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        

        readonly private IProductWriteRepository _productWriteRepository;

        readonly private IProductReadRepository _productReadRepository;



        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IProductService productService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            
        }

        

        [HttpGet]

        public async void Get()
        {

            await _productWriteRepository.AddRangeAsync(new()
            {
                new(){ Id = Guid.NewGuid(),Name = "Product 1" ,Price=100,CreatedDate = DateTime.Now,Stock = 10 },
                new(){ Id = Guid.NewGuid(),Name = "Product 2" ,Price=300,CreatedDate = DateTime.Now,Stock = 100 },
                new(){ Id = Guid.NewGuid(),Name = "Product 3" ,Price=500,CreatedDate = DateTime.Now,Stock = 170 },
            });

            await _productWriteRepository.SaveAsync();

        }
    }
}
