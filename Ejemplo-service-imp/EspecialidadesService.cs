using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using prueba.ejemplo.common;
using prueba.ejemplo.dao.imp;
using prueba.ejemplo.dao.intf;
using prueba.ejemplo.model;
using prueba.ejemplo.service.intf;
using System.Collections.Generic;
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
            request.ESP_ESTADO = (int)Constante.ListadoEstados.Activo;

            int response = await especialidadesDAO.CrearEspecialidad(request);
            if (response == 0) throw new NotFoundCustomException(Constante.EX_ESPECIALIDADES_NOT_FOUND);
            return response;
        }

        public async Task<List<Especialidades>> ListaEspecialidad()
        {
            IEspecialidadesDAO especialidadesDAO = new EspecialidadesDAO(txtConnectionString);
            var response = await especialidadesDAO.ListaEspecialidad();
            return response;
        }

        public async Task<Especialidades> RetornarEspecialidad(int ESP_ID)
        {
            IEspecialidadesDAO especialidadesDAO = new EspecialidadesDAO(txtConnectionString);
            var response = await especialidadesDAO.RetornarEspecialidad(ESP_ID);
            if (response == null) throw new NotFoundCustomException(Constante.EX_ESPECIALIDADES_NO_EXISTE);
            
            return response;
        }
    }
}
