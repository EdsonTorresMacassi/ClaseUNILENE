using prueba.ejemplo.model;
using System.Threading.Tasks;

namespace prueba.ejemplo.dao.intf
{
    public interface IEspecialidadesDAO
    {
        Task<int> CrearEspecialidad(Especialidades request);
        Task<Especialidades> RetornarEspecialidad(int ESP_ID);
    }
}
