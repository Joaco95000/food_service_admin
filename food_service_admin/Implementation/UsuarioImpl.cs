using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Interface;
using System.Data;
using System.Data.SqlClient;

namespace Implementation
{
    public class UsuarioImpl : IUsuario
    {
        public DataTable BuscarPorCodigo(string codigo)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT id As Codigo, nombre AS Nombre, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, documento AS Documento, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Fotografia, estado AS Estado, '' AS Cambiar FROM usuario WHERE id like @codigo+'_%'";
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

        public DataTable BuscarPorNombre(string nombre)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT id As Codigo, nombre AS Nombre, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, documento AS Documento, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Fotografia, estado AS Estado,'' AS Cambiar FROM usuario WHERE nombre like @nombre+'%' OR paterno like @paterno+'%' OR materno like @materno+'%'";
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

        public string CambiarEstado(string estadoActual, string id)
        {
            string estado="";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE usuario SET estado=@ESTADO WHERE id=@ID";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@ESTADO", estado);
                cmd.Parameters.AddWithValue("@ID", id);
                DBImplementation.ExecuteDataTableCommand(cmd);
                return "Cambio de estado de "+estadoActual+" a "+estado+" realizado con exito";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //
        }

        public int Delete(Usuario t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Usuario t)
        {
            throw new NotImplementedException();
        }

        public bool InsertUsuario(Usuario t)
        {
            string query = @"INSERT INTO usuario(nombre,paterno,materno,login,password,documento,estado)
				                           VALUES(@nombre,@paterno,@materno,@login,@password,@documento,@estado)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                //idAux = DBImplementation.GetIdentityFromTable("registro");
                //t.Fecha = DateTime.Parse(DBImplementation.fechaHoraServidor().ToString("yyy-MM-dd"));
                //t.Hora = DateTime.Parse(DBImplementation.fechaHoraServidor().ToString("H:m:ss"));


                cmd.Parameters.AddWithValue("@nombre", t.Nombre);
                cmd.Parameters.AddWithValue("@paterno", t.Paterno);
                cmd.Parameters.AddWithValue("@materno", t.Materno);
                cmd.Parameters.AddWithValue("@login", t.Login);
                cmd.Parameters.AddWithValue("@password", t.Password);
                cmd.Parameters.AddWithValue("@documento", t.Documento);
                //cmd.Parameters.AddWithValue("@fotografia", t.Fotografia);
                cmd.Parameters.AddWithValue("@estado", t.Estado);
                //cmd.Parameters.AddWithValue("@fecha_ingreso", null);
                //cmd.Parameters.AddWithValue("@fecha_nacimiento", null);
                //cmd.Parameters.AddWithValue("@cargo", t.Cargo);
                DBImplementation.ExecuteBasicCommand(cmd);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            };
        }

        public DataTable listadoUsuarios()
        {
            string query = @"SELECT id As Codigo, nombre AS Nombre, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, documento AS Documento, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Fotografia, estado AS Estado,'' AS Cambiar FROM usuario";
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

        public Usuario Login(string user, string password)
        {
            DataTable dt = new DataTable();
            Usuario usuario = null;
            string query = @"SELECT nombre, paterno, materno, estado, id, documento, login
                              FROM usuario
                              WHERE login=@login AND password=@password";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@login", user);
                cmd.Parameters.AddWithValue("@password", password);

                dt = DBImplementation.ExecuteDataTableCommand(cmd);

                if (dt.Rows.Count > 0)
                {
                    usuario = new Usuario();
                    usuario.Nombre = dt.Rows[0][0].ToString();
                    usuario.Paterno = dt.Rows[0][1].ToString();
                    usuario.Materno = dt.Rows[0][2].ToString();
                    usuario.Estado = dt.Rows[0][3].ToString().Trim();
                    usuario.Id = int.Parse(dt.Rows[0][4].ToString());
                    usuario.Documento = dt.Rows[0][5].ToString();
                    usuario.Login = dt.Rows[0][6].ToString();

                    return usuario;
                }

                return null;
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Usuario t)
        {
            throw new NotImplementedException();
        }

       
    }
}
