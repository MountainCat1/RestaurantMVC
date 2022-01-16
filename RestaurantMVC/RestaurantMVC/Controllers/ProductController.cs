using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Models;
using RestaurantMVC.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: ProductController
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDto productDto)
        {
            await productService.ProductCreate(productDto, User);
            return View();
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ProductDto productDto)
        {
            await productService.ProductEdit(productDto, User);
            return View();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.ProductDelete(id, User);
            return View();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            ProductDto productDto =  await productService.ProductGet(id);
            return View(productDto);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            List<ProductDto> producDtos = await productService.ProductGet();
            return View(producDtos);
        }

        [HttpGet("view")]
        public async Task<IActionResult> ViewUwU([FromRoute] int id)
        {
            ProductDto productDto = await productService.ProductGet(id);
            return View(productDto);
        }
    }
}
