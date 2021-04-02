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

        public DataTable ReporteVentasUltimos30Dias()
        {
            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY S.fecha  DESC) AS Num,(CAST(S.fecha AS nvarchar(50))+' '+CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado
                            FROM snack S
                            INNER JOIN cliente AS C
                            ON S.cliente = C.id
                            INNER JOIN item AS I
                            ON S.item = I.id
                            WHERE DATEDIFF(day, S.fecha, getdate()) between 0 and 30 
                            ORDER BY S.fecha desc;";
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
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando buscar una orden en Ventas con codigo: {2} ", DateTime.Now, Sesion.verInfo(), orden));
            DataTable dt = new DataTable();
            string query = @"SELECT TOP(2000) '' AS Num,(CAST(S.fecha AS nvarchar(50))+' '+CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
	                        FROM snack S
	                        INNER JOIN cliente AS C
	                        ON S.cliente = C.id
	                        INNER JOIN item AS I
	                        ON S.item = I.id
	                        WHERE S.orden = @ORDEN";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@orden", orden);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se busco una orden en Ventas con codigo: {2} ", DateTime.Now, Sesion.verInfo(), orden));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Ventas Buscar por orden {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public string CambiarEstadoVentas(string estadoActual, string id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo CambiarEstado para Ventas", DateTime.Now, Sesion.verInfo()));
            string estado = "";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE snack SET estado=@ESTADO WHERE orden=@ID";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@ESTADO", estado);
                cmd.Parameters.AddWithValue("@ID", id);
                DBImplementation.ExecuteDataTableCommand(cmd);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Cambio de estado en Ventas a {2} realizado con exito", DateTime.Now, Sesion.verInfo(), id));
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Ventas ChangeStatus {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public DataTable BuscarPorFecha(string fecha_inicio, string fecha_fin)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando sacar reporte de ventas del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio,fecha_fin));
            DataTable dt = new DataTable();
            string query = @"SELECT TOP(2000) '' AS Num,(CAST(S.fecha AS nvarchar(50))+' '+CAST(s.hora AS NVARCHAR(8))) as Fecha, C.nombre+' '+C.paterno+' '+C.materno AS Cliente, C.codigo AS Codigo, I.nombre AS Item, S.cantidad AS cantidad, S.precio as Precio,S.total AS Total, S.orden AS Orden, S.tipo AS Tipo, S.estado AS Estado, '' AS Cambio, S.id
	                        FROM snack S
	                        INNER JOIN cliente AS C
	                        ON S.cliente = C.id
	                        INNER JOIN item AS I
	                        ON S.item = I.id
	                        WHERE S.fecha >= @fechaInicio AND S.fecha <= @fechaFinal
                            ORDER BY S.fecha asc";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@fechaInicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fechaFinal", fecha_fin);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se saco un reporte de ventas del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Ventas Buscar por Fecha {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
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
        public DataTable ReporteAsistenciaUltimos30Dias()
        {
            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY A.fecha_ingreso  DESC) AS Num, ISNULL(U.nombre,'')+' '+ISNULL(U.paterno,'')+' '+ISNULL(U.materno,'') AS 'Nombre Completo', CAST(A.fecha_ingreso AS nvarchar(50)) AS Ingreso, CAST(DATEPART(WEEKDAY,A.fecha_ingreso) AS nvarchar(15)) AS Dia, ISNULL(CAST(A.fecha_salida AS nvarchar(50)),'--') AS Salida, ISNULL((CAST((DATEDIFF(HOUR,A.hora_ingreso,A.hora_salida)-1) as nvarchar(20)) )+':'+(CAST((DATEDIFF(minute,A.hora_ingreso,A.hora_salida)%60) AS nvarchar(20))),'0:0') AS tiempo, A.estado
                            FROM asistencia A
                            inner JOIN usuario AS U
                            ON a.usuario = U.id
                            WHERE DATEDIFF(day, A.fecha_ingreso, getdate()) between 0 and 30 
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
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando buscar la asistencia de un usuario con nombre: {2} ", DateTime.Now, Sesion.verInfo(), nombre));
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
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se busco la asistencia de un usuario con nombre: {2} ", DateTime.Now, Sesion.verInfo(), nombre));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Asistencia Buscar por nombre {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public string CambiarEstadoAsistencia(string estadoActual, string id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo CambiarEstado para la Asistencia de un Usuario", DateTime.Now, Sesion.verInfo()));
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
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Cambio de estado de Asistencia a {2} realizado con exito", DateTime.Now, Sesion.verInfo(), id));
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Asistencia ChangeStatus {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }
        public DataTable BuscarAsistenciaPorFecha(string fecha_inicio, string fecha_fin)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando sacar reporte de Asistencia del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
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
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se saco un reporte de Asistencia del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Asistencia Buscar por Fecha {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
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
                            WHERE nombre LIKE '%LONCHE%';";
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

        //fecha
        public List<string> armarConsultaCantidadLonchesFecha()
        {
            List<string> list = new List<string>();
            var res = "";
            var res2 = "";
            string query = @"SELECT id, nombre 
                            FROM item
                            WHERE nombre LIKE '%LONCHE%';";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                var lonches = DBImplementation.ExecuteDataTableCommand(cmd);
                //var x = 1;

                foreach (DataRow item in lonches.Rows)
                {

                    res = res + "ISNULL((SELECT SUM(sna.cantidad) FROM snack  sna WHERE sna.item= " + item[0].ToString() + " AND sna.cliente=R.cliente AND sna.estado='ACTIVO' AND sna.fecha >= @fechaInicio AND sna.fecha <= @fechaFinal),0) AS '" + item[1] + "', \n";
                }
                list.Add(res);
                foreach (DataRow item in lonches.Rows)
                {
                    res2 = res2 + "ISNULL((SELECT SUM(sna.total) FROM snack  sna WHERE sna.item= " + item[0].ToString() + " AND sna.cliente=R.cliente AND sna.estado='ACTIVO' AND sna.fecha >= @fechaInicio AND sna.fecha <= @fechaFinal),0) AS '" + item[1] + " Total', \n";
                }
                list.Add(res2);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        //fin fecha

        public DataTable mostrarDatosGeneral(string cantidadLonches, string totalLonches)
        {
            string query = @"SELECT '' AS '№', C.codigo AS Codigo ,ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Nombre Completo',
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Almuerzo,
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Cena,"
                            + cantidadLonches +
                            @"((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*12) AS 'Total Almuerzo', 
                            ((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*10) AS 'Total Cena',"
                            + totalLonches +
                            @"ISNULL((select sum(S.total) from snack AS S INNER JOIN item as I ON S.item=I.id where S.cliente=R.cliente AND I.nombre NOT LIKE '%lonche%' AND S.estado='ACTIVO'),0) AS 'Total Snack', '' AS 'Valor total'
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            GROUP BY C.codigo, r.cliente, C.nombre,C.paterno,C.materno
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

        public DataTable mostrarDatosGeneralParaExcel(string cantidadLonches, string totalLonches)
        {
            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY C.codigo) AS '№', C.codigo AS Codigo ,ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Nombre Completo',
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Almuerzo,
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO') AS Cena,"
                            + cantidadLonches +
                            @"((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*12) AS 'Total Almuerzo', 
                            ((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO')*10) AS 'Total Cena',"
                            + totalLonches +
                            @"ISNULL((select sum(S.total) from snack AS S INNER JOIN item as I ON S.item=I.id where S.cliente=R.cliente AND I.nombre NOT LIKE '%lonche%' AND S.estado='ACTIVO'),0) AS 'Total Snack', '' AS 'Valor total'
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            GROUP BY C.codigo, r.cliente, C.nombre,C.paterno,C.materno
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

        public DataTable mostrarDatosGeneralPorFechaParaExcel(string cantidadLonches, string totalLonches, string fecha_inicio, string fecha_fin)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando sacar reporte General del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
            DataTable dt = new DataTable();
            string query = @"SELECT C.codigo AS Codigo ,ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Nombre Completo',
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal) AS Almuerzo,
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal) AS Cena,"
                            + cantidadLonches +
                            @"((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal)*12) AS 'Total Almuerzo', 
                            ((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal)*10) AS 'Total Cena',"
                            + totalLonches +
                            @"ISNULL((select sum(S.total) from snack AS S INNER JOIN item as I ON S.item=I.id where S.cliente=R.cliente AND I.nombre NOT LIKE '%lonche%' AND S.estado='ACTIVO' AND S.fecha >= @fechaInicio AND S.fecha <= @fechaFinal),0) AS 'Total Snack', '' AS 'Valor total'
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            INNER JOIN snack S
							ON S.cliente = R.cliente
                            WHERE R.fecha >= @fechaInicio AND R.fecha <= @fechaFinal OR S.fecha >= @fechaInicio AND S.fecha <= @fechaFinal
                            GROUP BY C.id, C.codigo, r.cliente, C.nombre,C.paterno,C.materno
                            ORDER BY C.id";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@fechaInicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fechaFinal", fecha_fin);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se saco un reporte General del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: General Buscar por Fecha {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }


        public DataTable mostrarDatosGeneralPorFecha(string cantidadLonches, string totalLonches, string fecha_inicio, string fecha_fin)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando sacar reporte General del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS '№', C.codigo AS Codigo ,ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Nombre Completo',
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal) AS Almuerzo,
                            (SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal) AS Cena,"
                            + cantidadLonches +
                            @"((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='ALMUERZO' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal)*12) AS 'Total Almuerzo', 
                            ((SELECT COUNT(RE.turno) FROM registro AS RE WHERE RE.turno='CENA' AND RE.cliente=R.cliente AND RE.estado='ACTIVO' AND RE.fecha >= @fechaInicio AND RE.fecha <= @fechaFinal)*10) AS 'Total Cena',"
                            + totalLonches +
                            @"ISNULL((select sum(S.total) from snack AS S INNER JOIN item as I ON S.item=I.id where S.cliente=R.cliente AND I.nombre NOT LIKE '%lonche%' AND S.estado='ACTIVO' AND S.fecha >= @fechaInicio AND S.fecha <= @fechaFinal),0) AS 'Total Snack', '' AS 'Valor total'
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            INNER JOIN snack S
							ON S.cliente = R.cliente
                            WHERE R.fecha >= @fechaInicio AND R.fecha <= @fechaFinal OR S.fecha >= @fechaInicio AND S.fecha <= @fechaFinal
                            GROUP BY C.id, C.codigo, r.cliente, C.nombre,C.paterno,C.materno
                            ORDER BY C.id";
            try 
            {
                SqlCommand cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@fechaInicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fechaFinal", fecha_fin);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se saco un reporte General del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fecha_inicio, fecha_fin));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: General Buscar por Fecha {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        #endregion

        #region Comedor
        public DataTable ReporteComedor()
        {
            string query = @"SELECT TOP (2000) '' AS '№', (CAST(R.fecha AS nvarchar(50))+' '+CAST(R.hora AS NVARCHAR(8))) as Fecha, ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Cliente', C.codigo AS Codigo,R.id AS Ticket ,R.turno AS 'Turno', R.cuenta AS 'Cuenta', R.cantidad AS 'Cantidad', R.tipo AS Tipo, R.estado AS Estado, '' AS Cambio
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            WHERE R.turno = 'ALMUERZO' OR R.turno = 'CENA'
                            ORDER BY r.id desc";
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

        public DataTable ReporteComedorUltimos30Dias()
        {
            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY r.id  DESC) AS '№', (CAST(R.fecha AS nvarchar(50))+' '+CAST(R.hora AS NVARCHAR(8))) as Fecha, ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Cliente', C.codigo AS Codigo, R.id AS Ticket ,R.turno AS 'Turno', R.cuenta AS 'Cuenta', R.cantidad AS 'Cantidad', R.tipo AS Tipo, R.estado AS Estado
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            WHERE R.fecha >= CURRENT_TIMESTAMP -30 AND R.turno = 'ALMUERZO' OR R.turno = 'CENA' AND R.fecha >= CURRENT_TIMESTAMP -30
                            ORDER BY r.id desc";
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

        public DataTable BuscarPorTicket(string ticket)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando buscar en Comedor el ticket: {2} ", DateTime.Now, Sesion.verInfo(), ticket));
            DataTable dt = new DataTable();
            string query = @"SELECT '' AS '№', (CAST(R.fecha AS nvarchar(50))+' '+CAST(R.hora AS NVARCHAR(8))) as Fecha, ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Cliente', C.codigo AS Codigo,R.id AS Ticket ,R.turno AS 'Turno', R.cuenta AS 'Cuenta', R.cantidad AS 'Cantidad', R.tipo AS Tipo, R.estado AS Estado, '' AS Cambio
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            WHERE (R.turno = 'ALMUERZO' OR R.turno = 'CENA') AND R.id = @ticket
                            ORDER BY r.id desc";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@ticket", ticket);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se busco en Comedor el ticket: {2} ", DateTime.Now, Sesion.verInfo(), ticket));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Comedor Buscar por Ticket {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }

        public DataTable BuscarPorFechaComedor(string fechaInicioComedor, string fechaFinComedor)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Intentando sacar reporte de Comedor del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fechaInicioComedor, fechaFinComedor));
            DataTable dt = new DataTable();
            string query = @"SELECT TOP (2000) '' AS '№', (CAST(R.fecha AS nvarchar(50))+' '+CAST(R.hora AS NVARCHAR(8))) as Fecha, ISNULL(C.nombre,'')+' '+ISNULL(C.paterno,'')+' '+ISNULL(C.materno,'') AS 'Cliente', C.codigo AS Codigo,R.id AS Ticket ,R.turno AS 'Turno', R.cuenta AS 'Cuenta', R.cantidad AS 'Cantidad', R.tipo AS Tipo, R.estado AS Estado, '' AS Cambio
                            FROM registro AS  R
                            INNER JOIN cliente C
                            ON R.cliente = C.id
                            WHERE (R.turno = 'ALMUERZO' OR R.turno = 'CENA') AND R.fecha >= @fechaInicio AND R.fecha <= @fechaFinal
                            ORDER BY r.id desc";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicioComedor);
                cmd.Parameters.AddWithValue("@fechaFinal", fechaFinComedor);
                dt = DBImplementation.ExecuteDataTableCommand(cmd);
                if (dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Se saco un reporte de Comedor del: {2} al {3}", DateTime.Now, Sesion.verInfo(), fechaInicioComedor, fechaFinComedor));
                    return dt;
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Comedor Buscar por Fecha {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
                throw ex;
            }
        }
        public string CambiarEstadoComedor(string estadoActual, string id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Inicio del metodo CambiarEstado para Comedor", DateTime.Now, Sesion.verInfo()));
            string estado = "";
            if (estadoActual == "ACTIVO") { estado = "INACTIVO"; }
            if (estadoActual == "INACTIVO") { estado = "ACTIVO"; }

            string query = @"UPDATE registro SET estado=@ESTADO WHERE id=@ID";
            SqlCommand cmd;
            try
            {
                cmd = DBImplementation.CreateBasicCommand(query);
                cmd.Parameters.AddWithValue("@ESTADO", estado);
                cmd.Parameters.AddWithValue("@ID", id);
                DBImplementation.ExecuteDataTableCommand(cmd);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} |-| Info: Cambio de estado al ticket {2} en Comedor realizado con exito", DateTime.Now, Sesion.verInfo(), id));
                return "Cambio de estado de " + estadoActual + " a " + estado + " realizado con exito";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | {2} |-| Error: Comedor ChangeStatus {1}", DateTime.Now, ex.Message, Sesion.verInfo()));
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
