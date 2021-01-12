using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.BusinessLogic.Vehiculo;
using DGT.Models.Base;
using DGT.Models.Vehiculo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DGTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {

        private readonly IVehiculoBL VehiculoBL;

        public VehiculoController(IVehiculoBL VehiculoBL)
        {
            this.VehiculoBL = VehiculoBL;
        } 

        [Route("GetTopInfracciones")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DtoInfracciones>), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        public IActionResult GetTopInfracciones()
        {
            try
            {
                return Ok(this.VehiculoBL.GetTopInfracciones());
            }
            catch (Exception e)
            {
                Error item = new Error { msnError = e.Message, statusCode = 50001 };
                return BadRequest(item);
            }
        }



        [Route("SetInfracciones")]
        [HttpPut]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        public IActionResult SetInfracciones(DtoInfracciones infracciones)
        {
            try
            {
                return Ok(this.VehiculoBL.SetInfracciones(infracciones));
            }
            catch (Exception e)
            {
                Error item = new Error { msnError = e.Message, statusCode = 50001 };
                return BadRequest(item);
            }
        }



        [Route("SetVehiculo")]
        [HttpPut]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        public IActionResult SetVehiculo(DtoVehiculo vehiculo)
        {
            try
            {
                return Ok(this.VehiculoBL.SetVehiculo(vehiculo));
            }
            catch (Exception e)
            {
                Error item = new Error { msnError = e.Message, statusCode = 50001 };
                return BadRequest(item);
            }
        }
   
    }
}
