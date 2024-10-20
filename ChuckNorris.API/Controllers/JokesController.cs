using ChuckNorris.Tools;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChuckNorris.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {

        Db _db;

        Call _call;



        public JokesController()
        {
            string connectionString
              = "Data Source=.;Initial Catalog=Db2022ChuckNorris;User ID=Db2022ChuckNorrisUser;Password=Db2022ChuckNorrisUser";
            // = @"Data Source=M15R2\SQL20219;Initial Catalog=Db2022ChuckNorris;User ID=Db2022ChuckNorrisUser;Password=Db2022ChuckNorrisUser";
            _db = new Db(connectionString);

            _call = new Call();
          

        }
        // GET: api/<JokesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [EnableCors("MyPolicy")]
        [HttpGet]
        public ViewModels.Joke GetJoke()
        {
            var url = "https://api.chucknorris.io/jokes/random";

            var joke = _call.GetJoke(url);

            while (_db.AddJoke(joke) == "Already is the database" )
            {
                joke = _call.GetJoke(url);

            }

            return  new ViewModels.Joke {  Id = joke.id , Content = joke.value};
        }

        // GET api/<JokesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JokesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JokesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JokesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
