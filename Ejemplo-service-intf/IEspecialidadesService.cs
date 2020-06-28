using prueba.ejemplo.model;
using System.Threading.Tasks;

namespace prueba.ejemplo.service.intf
{
    public interface IEspecialidadesService
    {
        Task<int> CrearEspecialidad(Especialidades request);
    }
}
