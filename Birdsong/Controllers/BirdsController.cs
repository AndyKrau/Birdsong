using Birdsong.Models;
using Birdsong.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Birdsong.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        public BirdsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Bird> Get()
        {
            return ProductService.GetBirds();
        }
    }
}
