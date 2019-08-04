using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Api._2._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery]string contactName)
        {
            int countForRank = 0;
            string rank = "Newbie";

            if (ValuesController._dynamicPoints != null)
            {
                countForRank = ValuesController._dynamicPoints.Count(c => !string.IsNullOrEmpty(c.Operator) && c.Operator.Contains(contactName, StringComparison.InvariantCultureIgnoreCase));

                if (countForRank >= 5)
                    rank = "Explorer";

                if (countForRank >= 15)
                    rank = "Expert";

                if (countForRank >= 50)
                    rank = "Indi";

                if (countForRank >= 100)
                    rank = "McGyver";
            }

            return Ok(new { Rank = $"{rank} ({countForRank})" });
        }
    }
}
