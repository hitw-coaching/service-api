using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Service.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private const string FileName = "Grille_protocole_points_fixes.csv";

        public ValuesController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var points = from line in System.IO.File.ReadAllLines($"./Datas/{FileName}")
                            .Skip(1)
                         let columns = line.Split(';')
                         select new
                         {
                             NPlace = columns[0],
                             Code = float.Parse(columns[1]),
                             X = columns[2],
                             Y = columns[3],
                             D = columns[4],
                             Service = columns[5],
                             Carte = columns[6],
                             Type = columns[7],
                             TypeDescrIGN = columns[8]
                         };

            return Ok(JsonConvert.SerializeObject(points));
        }
    }
}
