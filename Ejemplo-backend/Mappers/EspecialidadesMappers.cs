using prueba.ejemplo.backend.DTO;
using prueba.ejemplo.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.ejemplo.backend.Mappers
{
    public class EspecialidadesMappers
    {
        public static EspecialidadesDTO Map(Especialidades model)
        {
            return new EspecialidadesDTO()
            {
                ESP_ID = model.ESP_ID,
                ESP_NOMENCLATURA = model.ESP_NOMENCLATURA,
                ESP_DESCRIPCION = model.ESP_DESCRIPCION
                
            };
        }
    }
}
