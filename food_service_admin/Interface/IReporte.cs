using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Interface
{
    public interface IReporte : IDao<Reporte>
    {
        DataTable ReporteVentas();

        DataTable BuscarPorOrden(string orden);
        DataTable BuscarPorFecha(string fecha_inicio, string fecha_fin);

        string CambiarEstadoVentas(string estadoActual, string id);

        DataTable ReporteAsistencia();
        DataTable ListarUsuariosComboBox();
        DataTable BuscarAsistenciaNombre(string nombre);
        DataTable BuscarAsistenciaPorFecha(string fecha_inicio, string fecha_fin);
        string CambiarEstadoAsistencia(string estadoActual, string id);


    }
}
