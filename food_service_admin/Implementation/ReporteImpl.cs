using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Interface;
using Model;

namespace Implementation
{
    public class ReporteImpl : IReporte
    {
        #region Ventas
        public DataTable ReporteVentas()
        {
            string query = @"SELECT TOP(2000) '' AS Num,(CAST(S.fecha AS nvarchar(50))+' '+CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
	                        FROM snack S
	                        INNER JOIN cliente AS C
	                        ON S.cliente = C.id
	                        INNER JOIN item AS I
	                        ON S.item = I.id
	                        WHERE I.nombre NOT LIKE '%lonche'
                            ORDER BY S.fecha desc";
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
            string query = @"SELECT TOP(2000) '' AS Num,(CAST(S.fecha AS nvarchar(50))+' '+CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
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
            string query = @"SELECT TOP(2000) '' AS Num,(CAST(S.fecha AS nvarchar(50))+' '+CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
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
        
        #endregion

        #region Asistencia
        public DataTable ReporteAsistencia()
        {
            string query = @"SELECT TOP (1000) '' AS Num, ISNULL(U.nombre,'')+' '+ISNULL(U.paterno,'')+' '+ISNULL(U.materno,'') AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, ISNULL((CAST((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1) as nvarchar(20)) )+':'+(CAST((DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) AS nvarchar(20))),'0:0') AS tiempo, A.estado, A.id AS Id, '' AS cambio
                            FROM asistencia A
                            inner JOIN usuario AS U
                            ON a.usuario = U.id
                            ORDER BY Ingreso desc";
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
        public DataTable BuscarAsistenciaNombre(string nombre, string paterno, string materno)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT TOP (1000) '' AS Num, ISNULL(U.nombre,'')+' '+ISNULL(U.paterno,'')+' '+ISNULL(U.materno,'') AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, ISNULL((CAST((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1) as nvarchar(20)) )+':'+(CAST((DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) AS nvarchar(20))),'0:0') AS tiempo, A.estado, A.id AS Id, '' AS cambio
                            FROM asistencia A
                            inner JOIN usuario AS U
                            ON a.usuario = U.id AND U.nombre = @nombre or U.paterno = @paterno or U.materno = @materno
                            ORDER BY Ingreso";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@paterno", paterno);
                cmd.Parameters.AddWithValue("@materno", materno);
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
        public DataTable BuscarAsistenciaPorFecha(string fecha_inicio, string fecha_fin)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT TOP (1000) '' AS Num, ISNULL(U.nombre,'')+' '+ISNULL(U.paterno,'')+' '+ISNULL(U.materno,'') AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, ISNULL((CAST((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1) as nvarchar(20)) )+':'+(CAST((DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) AS nvarchar(20))),'0:0') AS tiempo, A.estado, A.id AS Id, '' AS cambio
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

        #region General
        public List<string> armarConsultaCantidadLonches()
        {
            List<string> list = new List<string>();
            var res = "";
            var res2 = "";
            string query = @"SELECT id, nombre 
                            FROM item
                            WHERE nombre LIKE '%LONCHE';";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                var lonches = DBImplementation.ExecuteDataTableCommand(cmd);
                //var x = 1;

                foreach (DataRow item in lonches.Rows)
                {

                    res = res + "ISNULL((SELECT SUM(sna.cantidad) FROM snack  sna WHERE sna.item= " + item[0].ToString() + " AND sna.cliente=R.cliente AND sna.estado='ACTIVO'),0) AS '" + item[1] + "', \n";
                }
                list.Add(res);
                foreach (DataRow item in lonches.Rows)
                {
                    res2 = res2 + "ISNULL((SELECT SUM(sna.total) FROM snack  sna WHERE sna.item= " + item[0].ToString() + " AND sna.cliente=R.cliente AND sna.estado='ACTIVO'),0) AS '" + item[1] + " Total', \n";
                }
                list.Add(res2);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public DataTable mostrarDatosGeneral(string cantidadLonches, string totalLonches)
        {
            string query = @"SELECT '' AS '№' ,ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Nombre Completo',
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Almuerzo,
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Cena,"
                            + cantidadLonches +
                            @"((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*12) AS 'Total Almuerzo', 
                            ((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*10) AS 'Total Cena',"
                            + totalLonches +
                            @"ISNULL((select sum(S.total) from snack AS S INNER JOIN item as I ON S.item=I.id where S.cliente=R.cliente AND I.nombre NOT LIKE '%lonche' AND S.estado='ACTIVO'),0) AS 'Total Snack', '' AS 'Valor total'
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            GROUP BY r.cliente, C.nombre,C.paterno,C.materno
                            ORDER BY C.nombre,C.paterno,C.materno";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                //cmd.Parameters.AddWithValue("@cantidadLonches", cantidadLonches);
                // cmd.Parameters.AddWithValue("@naAun", naAun);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable mostrarDatosGeneralPorFecha(string cantidadLonches, string totalLonches, string fecha_inicio, string fecha_fin)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS '№' ,ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Nombre Completo',
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Almuerzo,
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Cena,"
                            + cantidadLonches +
                            @"((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*12) AS 'Total Almuerzo', 
                            ((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*10) AS 'Total Cena',"
                            + totalLonches +
                            @"ISNULL((select sum(S.total) from snack AS S INNER JOIN item as I ON S.item=I.id where S.cliente=R.cliente AND I.nombre NOT LIKE '%lonche' AND S.estado='ACTIVO'),0) AS 'Total Snack', '' AS 'Valor total'
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            WHERE R.fecha >= @fechaInicio AND R.fecha <= @fechaFinal
                            GROUP BY r.cliente, C.nombre,C.paterno,C.materno
                            ORDER BY C.nombre,C.paterno,C.materno";
            try 
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
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
