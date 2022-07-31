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
    public class UserController : ControllerBase
    {
        private readonly IElasticClient elasticClient;
        public UserController(IElasticClient _elasticClient) 
        {
            elasticClient = _elasticClient;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpPost]
        public async Task<My_index> Get([FromBody] string name)
        {
            var response = await elasticClient.SearchAsync<My_index>
                (s => s.Query
                (q => q.Fuzzy
                (f => f.Field
                (k => k.Name)
                .Value(name)
                .Fuzziness(Fuzziness.EditDistance(2)))));

            return response?.Documents?.FirstOrDefault();
        }
    }
}
