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


        readonly private IOrderWriteRepository _orderWriteRepository;

        readonly private ICustomerWriteRepository _customerWriteRepository;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IProductService productService, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }



        [HttpGet]

        public async Task Get()
        {

            var customerId = Guid.NewGuid();

            _customerWriteRepository.AddAsync(new() { Id = customerId,Name = "Muiddin"});

            await _orderWriteRepository.AddAsync(new() {Description = "bla bla bla",Address = "Ankara,Çankaya",CustomerId=customerId});

            await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla 2", Address = "Ankara,Pursaklar",CustomerId = customerId });

            await _orderWriteRepository.SaveAsync();

        }

        
    }
}
