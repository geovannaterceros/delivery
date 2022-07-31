using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDelivery.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IElasticClient elasticClient;
        public DeliveryController(IElasticClient _elasticClient)
        {
            elasticClient = _elasticClient;
        }
        // GET: api/<DeliveryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        public async Task<Delivery> Post([FromBody] string nombreProducto)
        {
            var response = await elasticClient.SearchAsync<Delivery>
                (s => s.Query
                (q => q.Fuzzy
                (f => f.Field
                (k => k.Products.First().Name)
                .Value(nombreProducto)
                .Fuzziness(Fuzziness.EditDistance(2)))));


            return response?.Documents?.FirstOrDefault();
        }

    }
}
