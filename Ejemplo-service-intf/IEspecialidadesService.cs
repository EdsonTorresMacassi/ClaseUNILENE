using prueba.ejemplo.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace prueba.ejemplo.service.intf
{
    public interface IEspecialidadesService
    {
        Task<int> CrearEspecialidad(Especialidades request);
        Task<List<Especialidades>> ListaEspecialidad();
        Task<Especialidades> RetornarEspecialidad(int ESP_ID);
    }
}
