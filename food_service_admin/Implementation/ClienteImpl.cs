using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class ClienteImpl : ICliente
    {
        public int Delete(Cliente t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Cliente t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Cliente t)
        {
            throw new NotImplementedException();
        }

        public DataTable listadoClientes()
        {
            string query = @"SELECT codigo As Ficha, nombre AS Nombres, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, (CASE WHEN tipo = 0 THEN'NORMAL' ELSE 'EMPRESA' END)  AS Tipo, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Imagen, estado AS Estado,'' AS Cambiar 
                            FROM cliente;";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);

                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string CambiarEstadoComesal(string estadoActual, string codigo)
        {
            string estado = "";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE cliente SET estado=@estado WHERE codigo =@codigo";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                DBImplementation.ExecuteDataTableCommand(cmd);
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable BuscarComensalPorCodigo(string codigo)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT codigo As Ficha, nombre AS Nombres, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, (CASE WHEN tipo = 0 THEN'NORMAL' ELSE 'EMPRESA' END)  AS Tipo, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Imagen, estado AS Estado,'' AS Cambiar 
                            FROM cliente
                            WHERE codigo like @codigo+'%'";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable BuscarComensalPorNombre(string nombre)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT codigo As Ficha, nombre AS Nombres, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, (CASE WHEN tipo = 0 THEN'NORMAL' ELSE 'EMPRESA' END)  AS Tipo, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Imagen, estado AS Estado,'' AS Cambiar 
                            FROM cliente
                            WHERE nombre like @nombre+'%' OR paterno like @paterno+'%' OR materno like @materno+'%'";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@paterno", nombre);
                cmd.Parameters.AddWithValue("@materno", nombre);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool InsertCliente(Cliente c)
        {
            string query = @"INSERT INTO cliente(nombre, paterno, materno, documento, fecha_ingreso, codigo, tipo, estado)
			                VALUES(@nombre, @paterno, @materno, @documento, @fecha_ingreso, @codigo, @tipo, @estado);";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);

                cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@paterno", c.Paterno);
                cmd.Parameters.AddWithValue("@materno", c.Materno);
                cmd.Parameters.AddWithValue("@documento", c.Documento);
                cmd.Parameters.AddWithValue("@fecha_ingreso", c.FechaIngreso);
                cmd.Parameters.AddWithValue("@codigo", c.Codigo);
                cmd.Parameters.AddWithValue("@tipo", c.Tipo);
                cmd.Parameters.AddWithValue("@estado", "ACTIVO");

                DBImplementation.ExecuteBasicCommand(cmd);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            };
        }
        public bool BuscarCodigo(string codigo)
        {
            string query = @"SELECT id
                            FROM cliente 
                            WHERE codigo = @codigo";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                var res = DBImplementation.ExecuteDataTableCommand(cmd).Rows[0][0];
                if (int.Parse(res.ToString()) > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return true;
            };
        }
    }
}
