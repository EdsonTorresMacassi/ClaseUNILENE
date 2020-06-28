using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using minedu.tecnologia.util.lib;
using prueba.ejemplo.model;
using prueba.ejemplo.service.imp;
using prueba.ejemplo.service.intf;

namespace prueba.ejemplo.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly string environment;

        public EspecialidadesController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearEspecialidad([FromBody] Especialidades request)
        {            
            try
            {
                IEspecialidadesService especialidadesService = new EspecialidadesService(config.GetConnectionString("DefaultConnection"));
                int response = await especialidadesService.CrearEspecialidad(request);
                return Created("", new StatusResponse(response));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
