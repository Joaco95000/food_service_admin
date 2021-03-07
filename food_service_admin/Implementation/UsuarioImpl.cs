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
        public int Delete(Usuario t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Usuario t)
        {
            throw new NotImplementedException();
        }

        public DataTable listadoUsuarios()
        {
            string query = @"SELECT id As Codigo, nombre AS Nombre, (paterno+' '+ISNULL(materno,' ')) AS Apellidos, documento AS Documento, (CASE WHEN fotografia IS NULL THEN'NO' ELSE 'SI' END)  AS Fotografia, estado AS Estado FROM usuario";
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
