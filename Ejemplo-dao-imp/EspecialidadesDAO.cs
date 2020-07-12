using minedu.tecnologia.util.lib;
using prueba.ejemplo.dao.intf;
using prueba.ejemplo.model;
using System;
using System.Collections.Generic;
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

        private Especialidades cargarEspecialidades(SqlDataReader dr)
        {
            int index = 0;
            Especialidades model = new Especialidades();
            model.ESP_ID = SqlHelper.GetInt32(dr, index);
            index = index + 1;
            model.ESP_NOMENCLATURA = SqlHelper.GetNullableString(dr, index);
            index = index + 1;
            model.ESP_DESCRIPCION = SqlHelper.GetNullableString(dr, index);

            return model;
        }

        public async Task<List<Especialidades>> ListaEspecialidad()
        {
            string sql = @"
                    SELECT 
                    ESP_ID, 
                    ESP_NOMENCLATURA, 
                    ESP_DESCRIPCION 
                    FROM dbo.TCOM_ESPECIALIDADES
                    WHERE ESP_ESTADO = 1
                    ";

            SqlParameter[] parametro = new SqlParameter[0];
            SqlDataReader dr = null;
            SqlConnection cn = null;

            try
            {
                List<Especialidades> lista = new List<Especialidades>();
                cn = new SqlConnection(txtConnectionString);
                await cn.OpenAsync();
                dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                if (!dr.HasRows) return lista;
                while (dr.ReadAsync().Result) 
                {
                    Especialidades model = cargarEspecialidades(dr);
                    lista.Add(model);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                SqlHelper.CloseDataReader(dr);
                SqlHelper.CloseConnection(cn);
            }
        }

        public async Task<Especialidades> RetornarEspecialidad(int ESP_ID)
        {
            string sql = @"
                    SELECT 
                    ESP_ID, 
                    ESP_NOMENCLATURA, 
                    ESP_DESCRIPCION 
                    FROM dbo.TCOM_ESPECIALIDADES
                    WHERE ESP_ESTADO = 1 
                    and ESP_ID = @ESP_ID
                    ";

            SqlParameter[] parametro = new SqlParameter[1];
            parametro[0] = new SqlParameter("@ESP_ID", SqlDbType.Int);
            parametro[0].Value = ESP_ID;

            SqlDataReader dr = null;
            SqlConnection cn = null;
            Especialidades response = null;

            try
            {                
                cn = new SqlConnection(txtConnectionString);
                await cn.OpenAsync();
                dr = await SqlHelper.ExecuteReaderAsync(cn, CommandType.Text, sql, parametro);
                if (!dr.HasRows) return response;
                while (dr.ReadAsync().Result)
                {
                    response = cargarEspecialidades(dr);                    
                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                SqlHelper.CloseDataReader(dr);
                SqlHelper.CloseConnection(cn);
            }
        }

        
    }
}
