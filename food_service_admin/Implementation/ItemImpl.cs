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
    public class ItemImpl: IItem
    {
        
        public DataTable listadoProductos()
        {
            string query = @"SELECT it.id As 'Numero', it.nombre AS 'Nombre', cat.nombre AS 'Categoria', it.precio AS 'Precio', ISNULL('ND', it.stock)  AS 'Stock', (CASE WHEN it.imagen IS NULL THEN'NO' ELSE 'SI' END)  AS 'Imagen', it.estado AS 'Estado','' AS 'Cambiar' 
                            FROM item AS it
                            INNER JOIN categoria AS cat ON cat.id = it.categoria;";
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

        public string CambiarEstadoSnack(string estadoActual, string id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo CambiarEstado para Item", DateTime.Now, Sesion.verInfo()));
            string estado = "";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE item SET estado = @estado WHERE id = @id";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@id", id);
                DBImplementation.ExecuteDataTableCommand(cmd);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Cambio de estado a {2} realizado con exito", DateTime.Now, Sesion.verInfo(), id));
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Item ChangeStatus {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public bool InsertProducto(Item i)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo Insert para Producto", DateTime.Now, Sesion.verInfo()));
            string query = @"INSERT INTO item(nombre, descripcion, precio, categoria, stock, estado)
			                VALUES(@nombre, @descripcion, @precio, @categoria, @stock, @estado);";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);

                cmd.Parameters.AddWithValue("@nombre", i.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", i.Descripcion);
                cmd.Parameters.AddWithValue("@precio", i.Precio);
                cmd.Parameters.AddWithValue("@categoria", i.Categoria);
                cmd.Parameters.AddWithValue("@stock", i.Stock);
                cmd.Parameters.AddWithValue("@estado", "ACTIVO");

                DBImplementation.ExecuteBasicCommand(cmd);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Registro de Producto insertado con exito: {2}", DateTime.Now, Sesion.verInfo(), i.Nombre));
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Productos Insert {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                return false;
            };
        }

        public int Insert(Item t)
        {
            throw new NotImplementedException();
        }

        public int Update(Item t)
        {
            throw new NotImplementedException();
        }

        public int Delete(Item t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }
    }
}
