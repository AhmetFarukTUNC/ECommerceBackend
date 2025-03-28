using Application.Abstraction;
using Application.Repositories;
using Application.RequestParameters;
using Application.ViewModels.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        public IWebHostEnvironment _webHostEnvironment { get; }

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IProductService productService,IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpGet]

        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            await Task.Delay(5000);
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            });
            return Ok(new
            {
                totalCount,
                products
            });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product vM_Create_Product)
        {
            if (ModelState.IsValid)
            {
                
            }


            await _productWriteRepository.AddAsync(new()
            {
                Name = vM_Create_Product.Name,
                Price = vM_Create_Product.Price,
                Stock = vM_Create_Product.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
          
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok(

                new
                {
                    message = "Silme işlemi başarılı!"
                }

                );


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in Request.Form.Files)
            {
                if (file.Length == 0)
                {
                    continue;
                }

                // Göreli yolu elde edin
                var relativePath = file.Name; // veya başka bir yöntemle elde edin

                // Dosya adını ve göreli yolu kullanarak tam yolu oluşturun
                var fullPath = Path.Combine(uploadPath, relativePath);

                var directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return Ok();
        }

    }



}

