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

        readonly private IOrderReadRepository _orderReadRepository;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IProductService productService, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
        }



        [HttpGet]

        public async Task Get()
        {

            Order order = await _orderReadRepository.GetByIdAsync("99c37bdb-c4b1-447d-eaaa-08dd502f4681");
            Order order2 = await _orderReadRepository.GetByIdAsync("b9be3ea3-fdfb-4ee6-eaab-08dd502f4681");

            order.Address = "İstanbul";

            order2.Address = "Çorum";

            await _orderWriteRepository.SaveAsync();

        }

        
    }
}
