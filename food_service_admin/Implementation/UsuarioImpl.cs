using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Interface;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Implementation
{
    public class UsuarioImpl : IUsuario
    {
        public DataTable BuscarPorCodigo(string codigo)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando buscar al usuario con codigo: {2} ", DateTime.Now, Sesion.verInfo(), codigo));
            DataTable dt = new DataTable();
            string query = @"SELECT id As Codigo, nombre AS Nombre, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, documento AS Documento, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Fotografia, estado AS Estado, '' AS Cambiar FROM usuario WHERE id like @codigo+'_%' OR id=@codigo";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se busco al usuario con codigo: {2} ", DateTime.Now, Sesion.verInfo(), codigo));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Usuarios Buscar por codigo {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public DataTable BuscarPorNombre(string nombre)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando buscar al usuario con nombre: {2} ", DateTime.Now, Sesion.verInfo(), nombre));
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
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se busco al usuario con nombre: {2} ", DateTime.Now, Sesion.verInfo(), nombre));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Usuarios Buscar por nombre {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public string CambiarEstado(string estadoActual, string id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo CambiarEstado para Usuario", DateTime.Now, Sesion.verInfo()));
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
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Cambio de estado a {2} realizado con exito", DateTime.Now, Sesion.verInfo(), id));
                return "Cambio de estado de "+estadoActual+" a "+estado+" realizado con exito";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Usuarios ChangeStatus {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
            
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
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo Insert para Usuario", DateTime.Now,Sesion.verInfo()));
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
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Registro de Usuario insertado con exito: {2}", DateTime.Now, Sesion.verInfo(),t.Login));
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Usuarios Insert {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
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
                System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Error: ListarUsuarios {1}", DateTime.Now, ex.Message));
                throw ex;
            }
        }

        public DataTable Login(string user, string password)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Intendando ingresar al sistema", DateTime.Now));
            DataTable dt = new DataTable();
            string query = @"SELECT id, nombre, paterno, materno, login
                              FROM usuario
                              WHERE login=@login AND password=@password AND estado='ACTIVO'";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@login", user);
                cmd.Parameters.AddWithValue("@password", password);

                dt = DBImplementation.ExecuteDataTableCommand(cmd);

                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Ingreso al sistema exitoso", DateTime.Now));
                    return dt;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Ingreso al sistema fallido - {1}", DateTime.Now,user));
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Error: Login {1}", DateTime.Now, ex.Message));
                MessageBox.Show("Login: " + ex);
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
