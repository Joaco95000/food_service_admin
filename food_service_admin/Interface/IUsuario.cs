using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;


namespace Interface
{
    public interface IUsuario:IDao<Usuario>
    {
        DataTable listadoUsuarios();

        DataTable Login(string user, string password);

        DataTable BuscarPorNombre(string nombre);
        DataTable BuscarPorCodigo(string codigo);
        string CambiarEstado(string estadoActual, string id);

    }
}
