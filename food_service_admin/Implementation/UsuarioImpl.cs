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
            string query = @"SELECT id, nombre, paterno, materno, documento, fotografia, estado FROM usuario";
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
