using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.BusinessLogic.Conductor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DGT.Models.Base;
using DGT.Models.Conductor;

namespace DGTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorController : ControllerBase
    {

        private readonly IConductorBL ConductorBL;

        public ConductorController(IConductorBL ConductorBL)
        {
            this.ConductorBL = ConductorBL;
        }


        [Route("GetInfracciones")]
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)] 
        public IActionResult GetInfracciones(string DNI)
        {
            try
            {
                return Ok(this.ConductorBL.GetInfracciones(DNI));
            }
            catch (Exception e)
            { 
                Error item = new Error { msnError = e.Message, statusCode = 50001  };
                return BadRequest(item);
            } 
        }

        [Route("GetTopConductores")]
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        public IActionResult GetTopConductores(int Cantidad)
        {
            try
            {
                return Ok(this.ConductorBL.GetTopConductores(Cantidad));
            }
            catch (Exception e)
            {
                Error item = new Error { msnError = e.Message, statusCode = 50001 };
                return BadRequest(item);
            }
        }


        [Route("SetConductor")]
        [HttpPut]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        public IActionResult SetConductor([FromBody]DtoConductor conductor)
        {
            try
            {
                return Ok(this.ConductorBL.SetConductor(conductor));
            }
            catch (Exception e)
            {
                Error item = new Error { msnError = e.Message, statusCode = 50001 };
                return BadRequest(item);
            }
        } 
          
    }
}
