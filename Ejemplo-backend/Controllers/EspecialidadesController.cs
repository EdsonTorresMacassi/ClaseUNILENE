using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using minedu.tecnologia.util.lib;
using prueba.ejemplo.backend.DTO;
using prueba.ejemplo.backend.Mappers;
using prueba.ejemplo.model;
using prueba.ejemplo.service.imp;
using prueba.ejemplo.service.intf;

namespace prueba.ejemplo.backend.Controllers
{
    [Route("[controller]")]
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

        [HttpGet]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListaEspecialidad()
        {
            try
            {
                IEspecialidadesService especialidadesService = new EspecialidadesService(config.GetConnectionString("DefaultConnection"));
                var response = await especialidadesService.ListaEspecialidad();
                List<EspecialidadesDTO> lista = new List<EspecialidadesDTO>();
                foreach (var item in response)
                {
                    lista.Add(EspecialidadesMappers.Map(item));
                }

                return Ok(new StatusResponse(lista));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{ESP_ID}", Name = "RetornarEspecialidad")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetornarEspecialidad(int ESP_ID)
        {
            try
            {
                IEspecialidadesService especialidadesService = new EspecialidadesService(config.GetConnectionString("DefaultConnection"));
                var response = await especialidadesService.RetornarEspecialidad(ESP_ID);
                EspecialidadesDTO registro = new EspecialidadesDTO();
                registro = EspecialidadesMappers.Map(response);
                return Ok(new StatusResponse(registro));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
