using Application.Abstraction;
using Application.Repositories;
using Domain.Entities;
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

        public async Task Get()
        {

            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new(){ Id = Guid.NewGuid(),Name = "Product 1" ,Price=100,CreatedDate = DateTime.Now,Stock = 10 },
            //    new(){ Id = Guid.NewGuid(),Name = "Product 2" ,Price=300,CreatedDate = DateTime.Now,Stock = 100 },
            //    new(){ Id = Guid.NewGuid(),Name = "Product 3" ,Price=500,CreatedDate = DateTime.Now,Stock = 170 },
            //});

            //await _productWriteRepository.SaveAsync();

            Product product =  await _productReadRepository.GetByIdAsync("634005e3-3d66-477a-82d6-9aeabb221da8",false);

            product.Name = "Mehmet";

            await _productWriteRepository.SaveAsync();

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(string id) {
        
        Product product =   await _productReadRepository.GetByIdAsync(id);

         return Ok(product);
        
        }
    }
}
