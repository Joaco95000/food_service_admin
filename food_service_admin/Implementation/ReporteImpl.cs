using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Model;

namespace Implementation
{
    public class ReporteImpl : IReporte
    {
        #region Ventas
        public DataTable ReporteVentas()
        {
            string query = @"SELECT '' AS Num, CONCAT(S.fecha,' ',CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
	                        FROM snack S
	                        INNER JOIN cliente AS C
	                        ON S.cliente = C.id
	                        INNER JOIN item AS I
	                        ON S.item = I.id
	                        WHERE I.nombre NOT LIKE '%lonche'";
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

        public DataTable BuscarPorOrden(string orden)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS Num, CONCAT(S.fecha,' ',CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
	                        FROM snack S
	                        INNER JOIN cliente AS C
	                        ON S.cliente = C.id
	                        INNER JOIN item AS I
	                        ON S.item = I.id
	                        WHERE I.nombre NOT LIKE '%lonche' AND S.orden = @ORDEN";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@orden", orden);
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

        public string CambiarEstadoVentas(string estadoActual, string id)
        {
            string estado = "";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE snack SET estado=@ESTADO WHERE id=@ID";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@ESTADO", estado);
                cmd.Parameters.AddWithValue("@ID", id);
                DBImplementation.ExecuteDataTableCommand(cmd);
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable BuscarPorFecha(string fecha_inicio, string fecha_fin)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS Num, CONCAT(S.fecha,' ',CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
	                        FROM snack S
	                        INNER JOIN cliente AS C
	                        ON S.cliente = C.id
	                        INNER JOIN item AS I
	                        ON S.item = I.id
	                        WHERE I.nombre NOT LIKE '%lonche' AND S.fecha >= @fechaInicio AND S.fecha <= @fechaFinal";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@fechaInicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fechaFinal", fecha_fin);
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
        public DataTable BuscarAsistenciaPorFecha(string fecha_inicio, string fecha_fin)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS Num, concat(U.nombre,' ',U.paterno,' ',U.materno) AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, CONCAT((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1),':',DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) as tiempo, A.estado, A.id AS Id, '' AS cambio
                            FROM asistencia A
                            inner JOIN usuario AS U
                            ON a.usuario = U.id AND A.fecha_ingreso >= @fechaInicio AND A.fecha_ingreso <= @fechaFinal
                            ORDER BY Ingreso";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@fechaInicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fechaFinal", fecha_fin);
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
        #endregion

        public DataTable ReporteAsistencia()
        {
            string query = @"SELECT '' AS Num, concat(U.nombre,' ',U.paterno,' ',U.materno) AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, CONCAT((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1),':',DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) as tiempo, A.estado, A.id AS Id, '' AS cambio
                            FROM asistencia A
                            inner JOIN usuario AS U
                            ON a.usuario = U.id
                            ORDER BY Ingreso";
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

        public DataTable ListarUsuariosComboBox()
        {
            string query = @"SELECT U.id AS Id, ISNULL((U.nombre+' '+U.paterno+' '+U.materno),'--') AS 'Nombre Completo'
                            FROM asistencia A
                            INNER JOIN usuario AS U
                            ON A.usuario = U.id";
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
        public DataTable BuscarAsistenciaNombre(string nombre)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS Num, concat(U.nombre,' ',U.paterno,' ',U.materno) AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, CONCAT((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1),':',DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) as tiempo, A.estado, A.id AS Id, '' AS cambio
            FROM asistencia A
            inner JOIN usuario AS U
            ON a.usuario = U.id AND U.nombre LIKE @nombre+'%' OR U.paterno like @paterno+'%' OR U.materno like @materno+'%'
            ORDER BY Ingreso";
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

        public string CambiarEstadoAsistencia(string estadoActual, string id)
        {
            string estado = "";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE asistencia SET estado=@ESTADO WHERE id=@ID";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@ESTADO", estado);
                cmd.Parameters.AddWithValue("@ID", id);
                DBImplementation.ExecuteDataTableCommand(cmd);
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #region DefaultFunctions
        public int Delete(Reporte t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Reporte t)
        {
            throw new NotImplementedException();
        }

        
        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Reporte t)
        {
            throw new NotImplementedException();
        }

        



        #endregion
    }
}
