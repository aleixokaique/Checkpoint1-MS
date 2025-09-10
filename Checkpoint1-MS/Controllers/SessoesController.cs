using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using Checkpoint1_MS.Model;
using StackExchange.Redis;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace perfomance_cache.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            String key = "get-users";
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyExpireAsync(key, TimeSpan.FromMinutes(15));
            string userValue = await db.StringGetAsync(key);

            if (!string.IsNullOrEmpty(userValue))
            {
                return Ok(userValue);
            }

            using var connection = new MySqlConnection("Server=localhost;database=fiap;User=root;Password=123");
            await connection.OpenAsync();

            string sql = "select id, nome, email ultimo_acesso from users; ";
            var users = await connection.QueryAsync<Usuario>(sql);
            var userJson = JsonConvert.SerializeObject(users);
            await db.StringSetAsync(key, userJson);
            Thread.Sleep(3000);
            return Ok(users);
        }
    }
}