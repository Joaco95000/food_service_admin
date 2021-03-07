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
    }
}
