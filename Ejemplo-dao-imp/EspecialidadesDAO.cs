using minedu.tecnologia.util.lib;
using prueba.ejemplo.dao.intf;
using prueba.ejemplo.model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace prueba.ejemplo.dao.imp
{
    public class EspecialidadesDAO: DAOBase,  IEspecialidadesDAO
    {
        public EspecialidadesDAO(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<int> CrearEspecialidad(Especialidades request)
        {
            int response = 0;
            string sql = @"
                    insert into dbo.TCOM_ESPECIALIDADES (ESP_NOMENCLATURA, ESP_DESCRIPCION, ESP_ESTADO, ESP_TIPO)
                    output inserted.ESP_ID
                    values (
                    @ESP_NOMENCLATURA,
                    @ESP_DESCRIPCION,
                    @ESP_ESTADO,
                    @ESP_TIPO
                    )";

            SqlParameter[] parametro = new SqlParameter[4];
            parametro[0] = new SqlParameter("@ESP_NOMENCLATURA", SqlDbType.VarChar, 10);
            parametro[0].Value = request.ESP_NOMENCLATURA;
            parametro[1] = new SqlParameter("@ESP_DESCRIPCION", SqlDbType.VarChar, 200);
            parametro[1].Value = request.ESP_DESCRIPCION;
            parametro[2] = new SqlParameter("@ESP_ESTADO", SqlDbType.Int);
            parametro[2].Value = request.ESP_ESTADO;
            parametro[3] = new SqlParameter("@ESP_TIPO", SqlDbType.Int);
            parametro[3].Value = request.ESP_TIPO;

            TransactionBase tran = null;

            try
            {
                tran = await SqlHelper.BeginTransaction(txtConnectionString);
                var insertId = await SqlHelper.ExecuteScalarAsync(tran.connection, tran.transaction, CommandType.Text, sql, parametro);
                response = Convert.ToInt32(insertId);

                if (response < 1)
                {
                    await tran.transaction.RollbackAsync();
                    return response;
                }

                await tran.transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await SqlHelper.RollbackTransactionAsync(tran);
                throw ex;
            }
            finally
            {
                await SqlHelper.DisposeTransactionAsync(tran);
            }

            return response;
        }
    }
}
