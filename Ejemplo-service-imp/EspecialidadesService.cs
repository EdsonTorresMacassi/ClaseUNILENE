using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using prueba.ejemplo.common;
using prueba.ejemplo.dao.imp;
using prueba.ejemplo.dao.intf;
using prueba.ejemplo.model;
using prueba.ejemplo.service.intf;
using System.Threading.Tasks;

namespace prueba.ejemplo.service.imp
{
    public class EspecialidadesService : ServiceBase, IEspecialidadesService
    {
        public EspecialidadesService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<int> CrearEspecialidad(Especialidades request)
        {
            IEspecialidadesDAO especialidadesDAO = new EspecialidadesDAO(txtConnectionString);
            int response = await especialidadesDAO.CrearEspecialidad(request);

            if (response == 0) throw new NotFoundCustomException(Constante.EX_ESPECIALIDADES_NOT_FOUND);
            return response;
        }
    }
}
