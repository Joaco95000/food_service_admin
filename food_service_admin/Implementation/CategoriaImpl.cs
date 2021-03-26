using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Implementation
{
    public class CategoriaImpl : ICategoria
    {
        public int Delete(Categoria t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Categoria t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Categoria t)
        {
            throw new NotImplementedException();
        }
        public DataTable SelectCategorias()
        {
            string query = @"SELECT nombre AS 'nombre', id AS 'id'
                            FROM categoria
                            WHERE estado = 'ACTIVO';";
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
    }
}
