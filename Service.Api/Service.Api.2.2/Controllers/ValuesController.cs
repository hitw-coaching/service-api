using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Api._2._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const string FileName = "Grille_protocole_points_fixes.csv";
        private const string FileNameDynamic = "Donnees_acoustiques_18102018.csv";

        public static dynamic _fixPoints;
        public static List<PointDto> _dynamicPoints;


        public ValuesController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            if (_fixPoints == null)
                _fixPoints = from line in System.IO.File.ReadAllLines($"./Datas/{FileName}")
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

            return Ok(JsonConvert.SerializeObject(_fixPoints));
        }

        [HttpGet("dynamic")]
        public IActionResult GetDynamics()
        {
            if (_dynamicPoints == null)
                _dynamicPoints = (from line in System.IO.File.ReadAllLines($"./Datas/{FileNameDynamic}")
                                .Skip(1)
                                 let columns = line.Split(';')
                                 where DateTime.ParseExact(columns[1], "dd/MM/yyyy", null) > DateTime.UtcNow.AddYears(-1)
                                 select new PointDto
                                 {
                                     Site = columns[0],
                                     NightDate = DateTime.ParseExact(columns[1], "dd/MM/yyyy", null),
                                     Id = columns[2],
                                     Positive = columns[3],
                                     Validation = columns[4],
                                     Contacts = columns[5],
                                     X = columns[6],
                                     Y = columns[7],
                                     Habitat1 = columns[8],
                                     Habitat2 = columns[9],
                                     Machine = columns[10],
                                     Operator = columns[11],
                                     Project = columns[12],
                                     Start = columns[13],
                                     End = columns[14],
                                     Diff = columns[15],
                                 }).ToList();

            return Ok(JsonConvert.SerializeObject(_dynamicPoints));
        }

        [HttpPost("dynamic/create")]
        public IActionResult AddDynamic([FromBody]PointDto model)
        {
            if (_dynamicPoints == null)
                _dynamicPoints = (from line in System.IO.File.ReadAllLines($"./Datas/{FileNameDynamic}")
                                .Skip(1)
                                  let columns = line.Split(';')
                                  where DateTime.ParseExact(columns[1], "dd/MM/yyyy", null) > DateTime.UtcNow.AddYears(-1)
                                  select new PointDto
                                  {
                                      Site = columns[0],
                                      NightDate = DateTime.ParseExact(columns[1], "dd/MM/yyyy", null),
                                      Id = columns[2],
                                      Positive = columns[3],
                                      Validation = columns[4],
                                      Contacts = columns[5],
                                      X = columns[6],
                                      Y = columns[7],
                                      Habitat1 = columns[8],
                                      Habitat2 = columns[9],
                                      Machine = columns[10],
                                      Operator = columns[11],
                                      Project = columns[12],
                                      Start = columns[13],
                                      End = columns[14],
                                      Diff = columns[15],
                                  }).ToList();

            _dynamicPoints.Add(model);

            return Created("", null);
        }
    }

    public class PointDto
    {
        public string Site { get; set; }
        public DateTime NightDate { get; set; }
        public string Id { get; set; }
        public string Positive { get; set; }
        public string Validation { get; set; }
        public string Contacts { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Habitat1 { get; set; }
        public string Habitat2 { get; set; }
        public string Machine { get; set; }
        public string Operator { get; set; }
        public string Project { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Diff { get; set; }
    }
}
