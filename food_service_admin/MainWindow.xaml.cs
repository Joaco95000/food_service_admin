using Implementation;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace food_service_admin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        UsuarioImpl usuarioImpl;
        ReporteImpl reporteImpl;
        int totalUsuario;
        int usuariosInactivos;
        int usuariosActivos;
        int contadorFilas;
        int contadorFilasAsistencia;
        int contadorFilasComedor;
        int contadorFilasAux;
        ClienteImpl clienteImpl;
        int totalCliente;
        int clientesInactivos;
        int clientesActivos;

        ItemImpl itemImpl;
        public MainWindow()
        {
            InitializeComponent();
            this.Title += " [" + Sesion.login + " - " + Sesion.nombre + " " + Sesion.paterno + " " + Sesion.materno + "]";
            ListarUsusarios();
            lbl_mensajes.Content = "Login con exito, se cargaron los datos correctamente";
            lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            totalUsuario = 0;
            usuariosInactivos = 0;
            usuariosActivos = 0;

        }

        #region codigo Usuario
        public void refrescar()
        {
            try
            {
                ListarUsusarios();
                lbl_mensajes.Content = "Datos actualizados...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Refrescar: " + ex);
            }

        }

        private void ListarUsusarios()
        {
            try
            {
                usuarioImpl = new UsuarioImpl();
                System.Data.DataTable dt = usuarioImpl.listadoUsuarios();
                DataRow dr;
                totalUsuario = 0;
                usuariosInactivos = 0;
                usuariosActivos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    DataGridRow rowDatos = new DataGridRow();
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[i] = row.ItemArray[i].ToString();

                        if (row.ItemArray[5].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr[6] += "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] += "sources/equis.png";
                        }

                    }

                    if (dr[5].ToString() == "ACTIVO") { usuariosActivos = usuariosActivos + 1; }
                    if (dr[5].ToString() == "INACTIVO") { usuariosInactivos = usuariosInactivos + 1; }
                    dg.Items.Add(dr);
                    totalUsuario = totalUsuario + 1;


                }
                lb_cantidad_total.Content = totalUsuario.ToString();
                lb_cantidad_activos.Content = usuariosActivos.ToString();
                lb_cantidad_inactivos.Content = usuariosInactivos.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ListarUsuarios: " + ex);
            }

        }

        private void txt_nombre_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txt_codigo_buscar.Text = "";
                string nombre = txt_nombre_buscar.Text;
                usuarioImpl = new UsuarioImpl();
                System.Data.DataTable dt = usuarioImpl.BuscarPorNombre(nombre);
                if (dt != null)
                {
                    limpiarDataGrid();
                    DataRow dr;
                    totalUsuario = 0;
                    usuariosInactivos = 0;
                    usuariosActivos = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        DataGridRow rowDatos = new DataGridRow();
                        dr = dt.NewRow();
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            dr[i] = row.ItemArray[i].ToString();

                            if (row.ItemArray[5].ToString() == "ACTIVO")
                            {
                                //MessageBox.Show(row.ItemArray[5].ToString());
                                dr[6] += "sources/check.png";
                            }
                            if (row.ItemArray[5].ToString() == "INACTIVO")
                            {
                                dr[6] += "sources/equis.png";
                            }

                        }
                        if (dr[5].ToString() == "ACTIVO") { usuariosActivos = usuariosActivos + 1; }
                        if (dr[5].ToString() == "INACTIVO") { usuariosInactivos = usuariosInactivos + 1; }
                        dg.Items.Add(dr);
                        totalUsuario = totalUsuario + 1;
                    }
                    lbl_mensajes.Content = "Usuarios encontrados con esta combinación";
                    lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                    lb_cantidad_total.Content = totalUsuario.ToString();
                    lb_cantidad_activos.Content = usuariosActivos.ToString();
                    lb_cantidad_inactivos.Content = usuariosInactivos.ToString();
                }
                else
                {
                    limpiarDataGrid();
                    lbl_mensajes.Content = "Usuarios no encontrados";
                    lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("nombreB: " + ex);

            }
        }

        private void txt_codigo_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_codigo_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string codigo = txt_codigo_buscar.Text;
            usuarioImpl = new UsuarioImpl();
            System.Data.DataTable dt = usuarioImpl.BuscarPorCodigo(codigo);
            if (dt != null)
            {
                limpiarDataGrid();
                DataRow dr;
                totalUsuario = 0;
                usuariosInactivos = 0;
                usuariosActivos = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DataGridRow rowDatos = new DataGridRow();
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[i] = row.ItemArray[i].ToString();

                        if (row.ItemArray[5].ToString() == "ACTIVO")
                        {
                            dr[6] += "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] += "sources/equis.png";
                        }

                    }
                    if (dr[5].ToString() == "ACTIVO") { usuariosActivos = usuariosActivos + 1; }
                    if (dr[5].ToString() == "INACTIVO") { usuariosInactivos = usuariosInactivos + 1; }
                    dg.Items.Add(dr);
                    totalUsuario = totalUsuario + 1;
                }
                lbl_mensajes.Content = "Usuarios encontrados con este codigo";
                lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                lb_cantidad_total.Content = totalUsuario.ToString();
                lb_cantidad_activos.Content = usuariosActivos.ToString();
                lb_cantidad_inactivos.Content = usuariosInactivos.ToString();
                txt_nombre_buscar.Text = "";
            }
            else
            {
                limpiarDataGrid();
                lbl_mensajes.Content = "Usuarios no encontrados";
                lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));

            }
        }

        private void btn_limpiar_codigo_Click(object sender, RoutedEventArgs e)
        {
            txt_codigo_buscar.Text = "";
        }

        private void imgEstado_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string id = "";
                string estadoActual = "";
                foreach (DataRow item in dg.SelectedItems)
                {
                    id = item.ItemArray[0].ToString();
                    estadoActual = item.ItemArray[5].ToString();
                }
                usuarioImpl = new UsuarioImpl();
                lbl_mensajes.Content = usuarioImpl.CambiarEstado(estadoActual, id);
                MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
                lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
                limpiarDataGrid();
                ListarUsusarios();
                txt_nombre_buscar.Text = "";
                txt_codigo_buscar.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cambioestado: " + ex);

            }

        }


        private void limpiarDataGrid()
        {
            try
            {
                dg.ItemsSource = null;
                dg.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cambioestado: " + ex);

            }

        }

        private void btn_nuevo_usuario_Click(object sender, RoutedEventArgs e)
        {
            ventanas.AddUsuario ventanaUsuario = new ventanas.AddUsuario();
            ventanaUsuario.Show();
        }

        private void btn_refrescar_Click(object sender, RoutedEventArgs e)
        {
            limpiarDataGrid();
            refrescar();
        }
        #endregion

        #region codigo Comensal
        public void refrescarComensales()
        {
            try
            {
                ListarComensales();
                lbl_mensajes_comensal.Content = "Datos actualizados...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("RefrescarCOm: " + ex);

            }

        }
        private void ListarComensales()
        {
            try
            {
                limpiarDataGridComesales();
                clienteImpl = new ClienteImpl();
                System.Data.DataTable dt = clienteImpl.listadoClientes();
                DataRow dr;
                totalCliente = 0;
                clientesInactivos = 0;
                clientesActivos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    DataGridRow rowDatos = new DataGridRow();
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[i] = row.ItemArray[i].ToString();

                        if (row.ItemArray[5].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr[6] += "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] += "sources/equis.png";
                        }

                    }

                    if (dr[5].ToString() == "ACTIVO") { clientesActivos = clientesActivos + 1; }
                    if (dr[5].ToString() == "INACTIVO") { clientesInactivos = clientesInactivos + 1; }
                    dg_comensal.Items.Add(dr);
                    totalCliente = totalCliente + 1;
                }
                lb_cantidad_total_comensal.Content = totalCliente.ToString();
                lb_cantidad_activos_comensal.Content = clientesActivos.ToString();
                lb_cantidad_inactivos_comensal.Content = clientesInactivos.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ListarCOm: " + ex);

            }

        }

        private void btn_nuevo_comensal_Click(object sender, RoutedEventArgs e)
        {
            ventanas.AddCliente ventanaCliente = new ventanas.AddCliente();
            ventanaCliente.Show();
        }

        private void txt_nombre_buscar_comensal_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_codigo_buscar_comensal.Text = "";
            string nombre = txt_nombre_buscar_comensal.Text;
            clienteImpl = new ClienteImpl();
            System.Data.DataTable dt = clienteImpl.BuscarComensalPorNombre(nombre);
            if (dt != null)
            {
                limpiarDataGridComesales();
                DataRow dr;
                totalCliente = 0;
                clientesInactivos = 0;
                clientesActivos = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DataGridRow rowDatos = new DataGridRow();
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[i] = row.ItemArray[i].ToString();

                        if (row.ItemArray[5].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr[6] += "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] += "sources/equis.png";
                        }

                    }
                    if (dr[5].ToString() == "ACTIVO") { clientesActivos = clientesActivos + 1; }
                    if (dr[5].ToString() == "INACTIVO") { clientesInactivos = clientesInactivos + 1; }
                    dg_comensal.Items.Add(dr);
                    totalCliente = totalCliente + 1;
                }
                lbl_mensajes_comensal.Content = "Comensales encontrados con esta combinación";
                lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                lb_cantidad_total_comensal.Content = totalCliente.ToString();
                lb_cantidad_activos_comensal.Content = clientesActivos.ToString();
                lb_cantidad_inactivos_comensal.Content = clientesInactivos.ToString();
            }
            else
            {
                limpiarDataGridComesales();
                lbl_mensajes_comensal.Content = "Comensales no encontrados";
                lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
            }
        }

        private void btn_limpiar_codigo_comensal_Click(object sender, RoutedEventArgs e)
        {
            txt_codigo_buscar_comensal.Text = "";
        }

        private void btn_refrescar_comensal_Click(object sender, RoutedEventArgs e)
        {
            limpiarDataGridComesales();
            ListarComensales();
            lbl_mensajes_comensal.Content = "Datos actualizados en comensales...";
        }

        private void imgEstadoComesales_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string codigo = "";
            string estadoActual = "";
            foreach (DataRow item in dg_comensal.SelectedItems)
            {
                codigo = item.ItemArray[0].ToString();
                estadoActual = item.ItemArray[5].ToString();
            }
            clienteImpl = new ClienteImpl();
            lbl_mensajes_comensal.Content = clienteImpl.CambiarEstadoComesal(estadoActual, codigo);
            MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
            lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
            limpiarDataGridComesales();
            ListarComensales();
            txt_codigo_buscar_comensal.Text = "";
            txt_nombre_buscar_comensal.Text = "";
        }

        private void limpiarDataGridComesales()
        {
            dg_comensal.ItemsSource = null;
            dg_comensal.Items.Clear();
        }

        private void txt_codigo_buscar_comensal2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txt_codigo_buscar_comensal2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string codigo = txt_codigo_buscar_comensal.Text;
            clienteImpl = new ClienteImpl();
            System.Data.DataTable dt = clienteImpl.BuscarComensalPorCodigo(codigo);
            if (dt != null)
            {
                limpiarDataGridComesales();
                DataRow dr;
                totalCliente = 0;
                clientesInactivos = 0;
                clientesActivos = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DataGridRow rowDatos = new DataGridRow();
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[i] = row.ItemArray[i].ToString();

                        if (row.ItemArray[5].ToString() == "ACTIVO")
                        {
                            dr[6] += "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] += "sources/equis.png";
                        }

                    }
                    if (dr[5].ToString() == "ACTIVO") { clientesActivos = clientesActivos + 1; }
                    if (dr[5].ToString() == "INACTIVO") { clientesInactivos = clientesInactivos + 1; }
                    dg_comensal.Items.Add(dr);
                    totalCliente = totalCliente + 1;
                }
                lbl_mensajes_comensal.Content = "Comensales encontrados con este codigo";
                lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                lb_cantidad_total_comensal.Content = totalCliente.ToString();
                lb_cantidad_activos_comensal.Content = clientesActivos.ToString();
                lb_cantidad_inactivos_comensal.Content = clientesInactivos.ToString();
                txt_nombre_buscar_comensal.Text = "";
            }
            else
            {
                limpiarDataGridComesales();
                lbl_mensajes_comensal.Content = "Comensales no encontrados";
                lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
            }
        }

        private void btn_refrescar_comensal_Loaded(object sender, RoutedEventArgs e)
        {
            cargarComesales();
        }

        #endregion

        #region codigo Snack

        private void ListarProductos()
        {
            limpiarDataGridSnack();
            itemImpl = new ItemImpl();
            System.Data.DataTable dt = itemImpl.listadoProductos();
            DataRow dr;

            foreach (DataRow row in dt.Rows)
            {
                DataGridRow rowDatos = new DataGridRow();
                dr = dt.NewRow();
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    dr[i] = row.ItemArray[i].ToString();

                    if (row.ItemArray[6].ToString() == "ACTIVO")
                    {
                        //MessageBox.Show(row.ItemArray[5].ToString());
                        dr[7] += "sources/check.png";
                    }
                    if (row.ItemArray[6].ToString() == "INACTIVO")
                    {
                        dr[7] += "sources/equis.png";
                    }
                }
                dg_snack.Items.Add(dr);
            }
        }

        private void btn_nuevo_producto_Click(object sender, RoutedEventArgs e)
        {
            ventanas.AddProducto ventanaProducto = new ventanas.AddProducto();
            ventanaProducto.Show();
        }

        private void imgEstadoSnack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string codigo = "";
            string estadoActual = "";
            foreach (DataRow item in dg_snack.SelectedItems)
            {
                codigo = item.ItemArray[0].ToString();
                estadoActual = item.ItemArray[6].ToString();
            }
            itemImpl = new ItemImpl();
            lbl_mensajes_snack.Content = itemImpl.CambiarEstadoSnack(estadoActual, codigo);
            MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
            lbl_mensajes_snack.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
            limpiarDataGridSnack();
            ListarProductos();
        }

        private void limpiarDataGridSnack()
        {
            dg_snack.ItemsSource = null;
            dg_snack.Items.Clear();
        }
        public void refrescarSnack()
        {
            limpiarDataGridSnack();
            ListarProductos();
            lbl_mensajes_snack.Content = "Datos actualizados...";
        }
        private void btn_refrescar_snack_Click(object sender, RoutedEventArgs e)
        {
            limpiarDataGridSnack();
            refrescarSnack();
        }

        private void btn_refrescar_snack_Loaded(object sender, RoutedEventArgs e)
        {
            cargarSnack();
        }

        #endregion


        #region botones Sacar reporte
        private void btn_exportar_excel_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "Usuario", Sesion.verInfo()));
            UsuarioImpl usuarioImpl = new UsuarioImpl();
            var dt = usuarioImpl.listadoUsuariosParaExcel();
            
            BusyIndicadorUsuario.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte Usuarios");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorUsuario.IsBusy = false;
            worker.RunWorkerAsync();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "Usuario", Sesion.verInfo()));
        }

        private void btn_exportar_excel_comensal_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "Comensal", Sesion.verInfo()));
            ClienteImpl clienteImpl = new ClienteImpl();
            var dt = clienteImpl.listadoClientesParaExcel();
            
            BusyIndicadorComensal.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte Comensales");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorComensal.IsBusy = false;
            worker.RunWorkerAsync();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "Comensal", Sesion.verInfo()));
        }

        private void btn_exportar_excel_snack_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "Snack", Sesion.verInfo()));
            ItemImpl itemImpl = new ItemImpl();
            var dt = itemImpl.listadoProductosParaExcel();
            
            BusyIndicadorSnack.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte Snack");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorSnack.IsBusy = false;
            worker.RunWorkerAsync();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "Snack", Sesion.verInfo()));
        }

        private void btn_exportar_excel_general_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "General", Sesion.verInfo()));
            ReporteImpl reporteImpl = new ReporteImpl();
            System.Data.DataTable dt;

            if (dp_fecha_inicio_general.SelectedDate.ToString() != "" && dp_fecha_fin_general.SelectedDate.ToString() != "")
            {
                var fechaInicioGeneral = dp_fecha_inicio_general.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                var fechaFinGeneral = dp_fecha_fin_general.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                var totalesloncjes = reporteImpl.armarConsultaCantidadLonchesFecha();
                dt = reporteImpl.mostrarDatosGeneralPorFechaParaExcel(totalesloncjes[0], totalesloncjes[1], fechaInicioGeneral, fechaFinGeneral);    
                //ARREGLAR TIEMPO DE RESPUESTA
            }
            else
            {
                var totalesloncjes = reporteImpl.armarConsultaCantidadLonches();
                dt = reporteImpl.mostrarDatosGeneralParaExcel(totalesloncjes[0], totalesloncjes[1]);
            }
           
            List<string> names = new List<string>();
            double total = 0;
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "Valor total" && column.ColumnName.Contains("total") || column.ColumnName.Contains("Total"))
                {
                    names.Add(column.ColumnName);
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                foreach (var name in names)
                {
                    total += double.Parse(row[name].ToString());
                }
                row["Valor total"] = total.ToString();
                total = 0;
            }

            BusyIndicadorGeneral.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte General");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorGeneral.IsBusy = false;
            worker.RunWorkerAsync();  
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "General", Sesion.verInfo()));
        }

        private void btn_exportar_excel_comedor_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "Comedor", Sesion.verInfo()));
            ReporteImpl reporteImpl = new ReporteImpl();
            var dt = reporteImpl.ReporteComedorUltimos30Dias();

            BusyIndicadorComedor.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte Comedor");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorComedor.IsBusy = false;
            worker.RunWorkerAsync();  
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "Comedor", Sesion.verInfo()));
        }
        private void btn_exportar_excel1_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "Ventas", Sesion.verInfo()));
            ReporteImpl reporteImpl = new ReporteImpl();
            var dt = reporteImpl.ReporteVentasUltimos30Dias();            
            BusyIndicador.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte Ventas");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicador.IsBusy = false;
            worker.RunWorkerAsync();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "Ventas", Sesion.verInfo()));
        }

        private void btn_exportar_excel_asistencia_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Generando Reporte: {1} Por el {2}", DateTime.Now, "Asistencia", Sesion.verInfo()));
            ReporteImpl reporteImpl = new ReporteImpl();
            var dt = reporteImpl.ReporteAsistenciaUltimos30Dias();
            
            BusyIndicadorAsistencia.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => exportarExcel(dt, "Reporte Asistencias");
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorAsistencia.IsBusy = false;
            worker.RunWorkerAsync();
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Reporte {1} Exportado Por el {2}", DateTime.Now, "Asistencia", Sesion.verInfo()));
        }
        #endregion

        #region Excel

        public void exportarExcel(System.Data.DataTable dt, string titulo)
        {
            object misValue= System.Reflection.Missing.Value;
            Excel.Application appExcel = null;
            Workbook excelWorkbook = null;
            Worksheet excelWorksheet = null;
            try
            {
                appExcel = new Excel.Application();
                excelWorkbook = appExcel.Workbooks.Add(misValue);

                excelWorksheet = appExcel.ActiveWorkbook.ActiveSheet as Worksheet;

                Range columnsNameRange;
                columnsNameRange = excelWorksheet.get_Range("A1", misValue).get_Resize(1, dt.Columns.Count);

                string[] arrColumnNames = new string[dt.Columns.Count];

                for (int i = 0; i<dt.Columns.Count; i++)
                {
                    arrColumnNames[i] = dt.Columns[i].ColumnName;
                }

                columnsNameRange.set_Value(misValue, arrColumnNames);
                columnsNameRange.Font.Bold = true;
                
                for (int Idx = 0; Idx<dt.Rows.Count; Idx++)
                {                      
                    excelWorksheet.Range["J2"].Offset[Idx].Resize[1, 10].HorizontalAlignment = XlHAlign.xlHAlignRight; 
                    excelWorksheet.Range["A2"].Offset[Idx].Resize[1, dt.Columns.Count].Value = dt.Rows[Idx].ItemArray;
                }
                
                columnsNameRange.Rows["1"].Cells.Orientation = Excel.XlOrientation.xlUpward;

                Range line = (Range)columnsNameRange.Rows[1];
                line.Insert();

                columnsNameRange.Columns.EntireColumn.AutoFit();


                var fechaDoc = DateTime.Now.ToString("ddMMyyyyHHmmss");
                var fechaTit = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                var tituloCompletoDoc = titulo + " " + fechaDoc;
                var tituloCompletoTit = titulo + " " + fechaTit;
                excelWorkbook.SaveAs(tituloCompletoDoc);
                MessageBox.Show(tituloCompletoTit + " exportado con exito en Documentos", "Exportacion de Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                appExcel.Visible = true;
            }
               catch { MessageBox.Show("Error Al Exportar el Reporte"); }
        }
        #endregion

        #region Style

        private void ti_reportes_GotFocus(object sender, RoutedEventArgs e)
        {
            
            lbl_usuarios_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_usuarios_header.FontSize = 15;
            lbl_usuarios_header.FontStyle = FontStyles.Normal;
            lbl_usuarios_header.FontWeight = FontWeights.Normal;
            lbl_reportes_header.Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 27));
            lbl_reportes_header.FontSize = 18;
            lbl_reportes_header.FontStyle = FontStyles.Italic;
            lbl_reportes_header.FontWeight = FontWeights.Bold;
        }

        private void ti_reportes_LostFocus(object sender, RoutedEventArgs e)
        {
            lbl_reportes_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_reportes_header.FontSize = 15;
            lbl_reportes_header.FontStyle = FontStyles.Normal;
            lbl_reportes_header.FontWeight = FontWeights.Normal;

        }

        private void ti_usuarios_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_usuarios_header.Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 27));
            lbl_usuarios_header.FontSize = 18;
            lbl_usuarios_header.FontStyle = FontStyles.Italic;
            lbl_usuarios_header.FontWeight = FontWeights.Bold;
        }

        private void ti_usuarios_LostFocus(object sender, RoutedEventArgs e)
        {
            lbl_usuarios_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_usuarios_header.FontSize = 15;
            lbl_usuarios_header.FontStyle = FontStyles.Normal;
            lbl_usuarios_header.FontWeight = FontWeights.Normal;
        }



        private void ti_comensales_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_usuarios_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_usuarios_header.FontSize = 15;
            lbl_usuarios_header.FontStyle = FontStyles.Normal;
            lbl_usuarios_header.FontWeight = FontWeights.Normal;
            lbl_comensales_header.Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 27));
            lbl_comensales_header.FontSize = 18;
            lbl_comensales_header.FontStyle = FontStyles.Italic;
            lbl_comensales_header.FontWeight = FontWeights.Bold;
            
        }

        private void ti_comensales_LostFocus(object sender, RoutedEventArgs e)
        {
            lbl_comensales_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_comensales_header.FontSize = 15;
            lbl_comensales_header.FontStyle = FontStyles.Normal;
            lbl_comensales_header.FontWeight = FontWeights.Normal;
        }

        private void ti_snack_LostFocus(object sender, RoutedEventArgs e)
        {
            lbl_snack_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_snack_header.FontSize = 15;
            lbl_snack_header.FontStyle = FontStyles.Normal;
            lbl_snack_header.FontWeight = FontWeights.Normal;
        }

        private void ti_snack_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_usuarios_header.Foreground = new SolidColorBrush(Color.FromRgb(148, 148, 153));
            lbl_usuarios_header.FontSize = 15;
            lbl_usuarios_header.FontStyle = FontStyles.Normal;
            lbl_usuarios_header.FontWeight = FontWeights.Normal;
            lbl_snack_header.Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 27));
            lbl_snack_header.FontSize = 18;
            lbl_snack_header.FontStyle = FontStyles.Italic;
            lbl_snack_header.FontWeight = FontWeights.Bold;
            
        }

        private void cargarUsuarios()
        {
            //cargarUsuarios
            ListarUsusarios();
            lbl_mensajes.Content = "Datos Cargados en Usuarios";
            lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            //limpiar dg comensales
            limpiarDataGridComesales();
            //limpiar dg snack
            limpiarDataGridSnack();
            //limpiar dg reportes
            limpiarDataGridReportes("general");
            limpiarDataGridReportes("ventas");
            limpiarDataGridReportes("asistencia");
        }

        private void cargarComesales()
        {
            //limpiar dg usuarios
            //limpiarDataGrid();
            //cargar Comensales
            ListarComensales();
            lbl_mensajes_comensal.Content = "Datos Cargados en Comensales";
            lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            //limpiar dg snack
            limpiarDataGridSnack();
            //limpiar dg reportes
            limpiarDataGridReportes("general");
            limpiarDataGridReportes("ventas");
            limpiarDataGridReportes("asistencia");
        }

        private void cargarSnack()
        {
            
            //limpiar dg comensales
            limpiarDataGridComesales();
            //cargar dg snack
            ListarProductos();
            lbl_mensajes_snack.Content = "Datos Cargados en Productos";
            lbl_mensajes_snack.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            //limpiar dg reportes
            limpiarDataGridReportes("general");
            limpiarDataGridReportes("ventas");
            limpiarDataGridReportes("asistencia");
        }

        private void cargarReportes()
        {
            //limpiar dg usuarios
            
            //limpiar dg comensales
            limpiarDataGridComesales();
            //limpiar dg snack
            limpiarDataGridSnack();
            //vargar dg reportes
            ListarReporteGeneral();
            lbl_mensajes_general.Content = "Datos Cargados en Reporte General";
            lbl_mensajes_general.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }

        #endregion

        #region Reportes

        private void limpiarDataGridReportes(string data)
        {
            switch (data)
            {
                case "gColumnas":
                    this.Dispatcher.Invoke(() =>
                    {
                        dgGeneral.ItemsSource = null;
                        dgGeneral.Columns.Clear();
                    });
                    break;
                case "general":
                    this.Dispatcher.Invoke(() =>
                    {
                        dgGeneral.ItemsSource = null;
                        dgGeneral.Items.Clear();
                    });
                    break;
                case "ventas":
                    this.Dispatcher.Invoke(() =>
                    {
                        dg1.ItemsSource = null;
                        dg1.Items.Clear();
                    });
                    break;
                case "asistencia":
                    this.Dispatcher.Invoke(() =>
                    {
                        dgAsistencia.ItemsSource = null;
                        dgAsistencia.Items.Clear();
                    });
                    break;
                case "comedor":
                    this.Dispatcher.Invoke(() =>
                    {
                        dgComedor.ItemsSource = null;
                        dgComedor.Items.Clear();
                    });
                    break;
            }

        }


        #region ReporteVentas
        private void ListarReporteVentas()
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt2 = reporteImpl.ReporteVentas();
            DataRow dr2;
            contadorFilas = 1;
            foreach (DataRow row2 in dt2.Rows)
            {
                dr2 = dt2.NewRow();
                for (int i = 0; i < row2.ItemArray.Length; i++)
                {
                    dr2[0] = contadorFilas;
                    dr2[i] = row2.ItemArray[i].ToString();
                    //MessageBox.Show(row2.ItemArray[i].ToString());
                    //if (row2.ItemArray[9].ToString() == "0")
                    //{
                    //    dr2[9] = "TOUCH";
                    //}
                    if (row2.ItemArray[10].ToString() == "ACTIVO")
                    {
                        //MessageBox.Show(row.ItemArray[5].ToString());
                        dr2[11] = "sources/arrow_green.png";
                    }
                    if (row2.ItemArray[10].ToString() == "INACTIVO")
                    {
                        dr2[11] = "sources/arrow_red.png";
                    }

                }
                this.Dispatcher.Invoke(() =>
                {
                    dg1.Items.Add(dr2);
                });

                contadorFilas += 1;
            }
            //dg1.ItemsSource = null;
            //dg1.Items.Clear();
        }
        private void btn_refrescar_ventas_Loaded(object sender, RoutedEventArgs e)
        {
            BusyIndicador.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteVentas();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicador.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes1.Content = "Datos Cargados";
        }

        private void txt_orden_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }


        private void ListarReportesVentasOrden(string orden)
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt = reporteImpl.BuscarPorOrden(orden);
            contadorFilas = 1;
            if (dt != null)
            {
                limpiarDataGridReportes("ventas");
                DataRow dr;
                foreach (DataRow row in dt.Rows)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DataGridRow rowDatos = new DataGridRow();
                    });
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[0] = contadorFilas;
                        dr[i] = row.ItemArray[i].ToString();
                        //if (row.ItemArray[9].ToString() == "0")
                        //{
                        //    dr[9] = "TOUCH";
                        //}
                        if (row.ItemArray[10].ToString() == "ACTIVO")
                        {
                            dr[11] = "sources/arrow_green.png";
                        }
                        if (row.ItemArray[10].ToString() == "INACTIVO")
                        {
                            dr[11] = "sources/arrow_red.png";
                        }

                    }
                    contadorFilas += 1;
                    this.Dispatcher.Invoke(() =>
                    {
                        dg1.Items.Add(dr);
                    });
                }
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes1.Content = "Ordenes encontradas con este codigo";
                    lbl_mensajes1.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });

            }
            else
            {
                limpiarDataGridReportes("ventas");
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes1.Content = "Orden no encontrada";
                    lbl_mensajes1.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });


            }
        }

        private void ListarReportesVentasFecha(string fecha_inicio, string fecha_final)
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt = reporteImpl.BuscarPorFecha(fecha_inicio, fecha_final);
            contadorFilas = 1;
            if (dt != null)
            {
                limpiarDataGridReportes("ventas");
                DataRow dr;
                foreach (DataRow row in dt.Rows)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DataGridRow rowDatos = new DataGridRow();
                    });
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[0] = contadorFilas;
                        dr[i] = row.ItemArray[i].ToString();
                        //if (row.ItemArray[9].ToString() == "0")
                        //{
                        //    dr[9] = "TOUCH";
                        //}
                        if (row.ItemArray[10].ToString() == "ACTIVO")
                        {
                            dr[11] = "sources/arrow_green.png";
                        }
                        if (row.ItemArray[10].ToString() == "INACTIVO")
                        {
                            dr[11] = "sources/arrow_red.png";
                        }

                    }
                    contadorFilas += 1;
                    this.Dispatcher.Invoke(() =>
                    {
                        dg1.Items.Add(dr);
                    });
                }
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes1.Content = "Encontrado en esta fecha";
                    lbl_mensajes1.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });

            }
            else
            {
                limpiarDataGridReportes("ventas");
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes1.Content = "No hay datos registrados durante este peridodo";
                    lbl_mensajes1.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });


            }
        }

        private void btn_buscar_orden_Click(object sender, RoutedEventArgs e)
        {
            string orden = txt_orden_buscar.Text;
            BusyIndicador.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReportesVentasOrden(orden);
            worker.RunWorkerCompleted += (s, ev) => BusyIndicador.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes1.Content = "Datos Cargados";
        }

        private void btn_buscar_fecha_ventas_Click(object sender, RoutedEventArgs e)
        {
            if (dp_fecha_inicio.SelectedDate.ToString() == "")
            {
                MessageBox.Show("Seleccione una fecha de inicio primero", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string fechaInicio = "";
                string fechaFin = "";
                this.Dispatcher.Invoke(() =>
                {
                    fechaInicio = dp_fecha_inicio.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                    fechaFin = dp_fecha_fin.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                });
                BusyIndicador.IsBusy = true;
                var worker = new BackgroundWorker();
                worker.DoWork += (s, ev) => ListarReportesVentasFecha(fechaInicio, fechaFin);
                worker.RunWorkerCompleted += (s, ev) => BusyIndicador.IsBusy = false;
                worker.RunWorkerAsync();
                lbl_mensajes1.Content = "Datos Actualizados";
            }
        }

        private void imgEstado2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string id = "";
            string estadoActual = "";
            foreach (DataRow item in dg1.SelectedItems)
            {
                id = item.ItemArray[8].ToString();
                estadoActual = item.ItemArray[10].ToString();
            }
            reporteImpl = new ReporteImpl();
            lbl_mensajes1.Content = reporteImpl.CambiarEstadoVentas(estadoActual, id);
            MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
            lbl_mensajes1.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
            limpiarDataGridReportes("ventas");
            ListarReporteVentas();

        }

        private void btn_refrescar_ventas_Click(object sender, RoutedEventArgs e)
        {
            BusyIndicador.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteVentas();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicador.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes1.Content = "Datos Actualizados";
        }

        #endregion

        #region ReporteAsistencia
        private void ListarReporteAsistencia()
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt2 = reporteImpl.ReporteAsistencia();
            DataRow dr2;
            contadorFilasAsistencia = 1;
            limpiarDataGridReportes("asistencia");
            foreach (DataRow row2 in dt2.Rows)
            {
                dr2 = dt2.NewRow();
                for (int i = 0; i < row2.ItemArray.Length; i++)
                {
                    dr2[0] = contadorFilasAsistencia;
                    dr2[i] = row2.ItemArray[i].ToString();
                    //MessageBox.Show(row2.ItemArray[i].ToString());
                    switch (row2.ItemArray[3].ToString())
                    {
                        case "1":
                            dr2[3] = "Domingo";
                            break;
                        case "2":
                            dr2[3] = "Lunes";
                            break;
                        case "3":
                            dr2[3] = "Martes";
                            break;
                        case "4":
                            dr2[3] = "Miercoles";
                            break;
                        case "5":
                            dr2[3] = "Jueves";
                            break;
                        case "6":
                            dr2[3] = "Viernes";
                            break;
                        case "7":
                            dr2[3] = "Sabado";
                            break;

                    }
                    if (row2.ItemArray[5].ToString().Contains('-'))
                    {
                        dr2[5] = '0' + row2.ItemArray[5].ToString().Substring(2);
                    }
                    if (row2.ItemArray[6].ToString() == "ACTIVO")
                    {
                        //MessageBox.Show(row.ItemArray[5].ToString());
                        dr2[8] = "sources/check.png";
                    }
                    if (row2.ItemArray[6].ToString() == "INACTIVO")
                    {
                        dr2[8] = "sources/equis.png";
                    }

                }
                this.Dispatcher.Invoke(() =>
                {
                    dgAsistencia.Items.Add(dr2);
                });

                contadorFilasAsistencia += 1;
            }
        }

        private void ListarAsistenciaNombre(string nombre, string paterno, string materno)
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt = reporteImpl.BuscarAsistenciaNombre(nombre, paterno, materno);
            contadorFilasAsistencia = 1;
            if (dt != null)
            {
                limpiarDataGridReportes("asistencia");
                DataRow dr;
                foreach (DataRow row in dt.Rows)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DataGridRow rowDatos = new DataGridRow();
                    });

                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[0] = contadorFilasAsistencia;
                        dr[i] = row.ItemArray[i].ToString();
                        switch (row.ItemArray[3].ToString())
                        {
                            case "1":
                                dr[3] = "Domingo";
                                break;
                            case "2":
                                dr[3] = "Lunes";
                                break;
                            case "3":
                                dr[3] = "Martes";
                                break;
                            case "4":
                                dr[3] = "Miercoles";
                                break;
                            case "5":
                                dr[3] = "Jueves";
                                break;
                            case "6":
                                dr[3] = "Viernes";
                                break;
                            case "7":
                                dr[3] = "Sabado";
                                break;

                        }
                        if (row.ItemArray[5].ToString().Contains('-'))
                        {
                            dr[5] = '0' + row.ItemArray[5].ToString().Substring(2);
                        }
                        if (row.ItemArray[6].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr[8] = "sources/check.png";
                        }
                        if (row.ItemArray[6].ToString() == "INACTIVO")
                        {
                            dr[8] = "sources/equis.png";
                        }

                    }
                    contadorFilasAsistencia += 1;
                    this.Dispatcher.Invoke(() =>
                    {
                        dgAsistencia.Items.Add(dr);
                    });
                }
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_asistencia.Content = "Usuarios encontrados";
                    lbl_mensajes_asistencia.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });

            }
            else
            {
                limpiarDataGridReportes("ventas");
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_asistencia.Content = "Usuarios no encontrados";
                    lbl_mensajes_asistencia.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });


            }
        }


        private void btn_refrescar_asistencia_Loaded(object sender, RoutedEventArgs e)
        {
            limpiarDataGridReportes("asistencia");
            BusyIndicadorAsistencia.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteAsistencia();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorAsistencia.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_asistencia.Content = "Datos Cargados";
        }
        private void btn_refrescar_asistencia_Click(object sender, RoutedEventArgs e)
        {
            limpiarDataGridReportes("asistencia");
            BusyIndicadorAsistencia.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteAsistencia();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorAsistencia.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_asistencia.Content = "Datos Actualizados";
        }

        private void btn_buscar_asistenica_personal_Click(object sender, RoutedEventArgs e)
        {
            if (txt_buscar_asistencia_nombre.Text == String.Empty || txt_buscar_asistencia_paterno.Text == String.Empty|| txt_buscar_asistencia_materno.Text == String.Empty)
            {
                MessageBox.Show("Debe completar todos los campos para realizar una busqueda.","Campos incompletos",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                string nombre = txt_buscar_asistencia_nombre.Text;
                string paterno = txt_buscar_asistencia_paterno.Text;
                string materno = txt_buscar_asistencia_materno.Text;
                //if (txt_buscar_asistencia_nombre.Text == "null") { nombre = null; }
                //if (txt_buscar_asistencia_paterno.Text == "null") { paterno = null; }
                //if (txt_buscar_asistencia_materno.Text == "null") { materno = null; }

                limpiarDataGridReportes("asistencia");
                BusyIndicadorAsistencia.IsBusy = true;
                var worker = new BackgroundWorker();
                worker.DoWork += (s, ev) => ListarAsistenciaNombre(nombre,paterno,materno);
                worker.RunWorkerCompleted += (s, ev) => BusyIndicadorAsistencia.IsBusy = false;
                worker.RunWorkerAsync();
            }
            
        }

        private void btn_buscar_fecha_asistencia_Click(object sender, RoutedEventArgs e)
        {
            if (dp_fecha_inicio_asistencia.SelectedDate.ToString() == "")
            {
                MessageBox.Show("Seleccione una fecha de inicio primero", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string fechaInicioAsistencia = "";
                string fechaFinAsistencia = "";
                this.Dispatcher.Invoke(() =>
                {
                    fechaInicioAsistencia = dp_fecha_inicio_asistencia.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                    fechaFinAsistencia = dp_fecha_fin_asistencia.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                });
                BusyIndicador.IsBusy = true;
                var worker = new BackgroundWorker();
                worker.DoWork += (s, ev) => ListarReportesAsistenciaFecha(fechaInicioAsistencia, fechaFinAsistencia);
                worker.RunWorkerCompleted += (s, ev) => BusyIndicador.IsBusy = false;
                worker.RunWorkerAsync();
                lbl_mensajes1.Content = "Datos Actualizados";
            }
        }
        private void ListarReportesAsistenciaFecha(string fechaInicio, string fechaFin)
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt = reporteImpl.BuscarAsistenciaPorFecha(fechaInicio, fechaFin);
            contadorFilasAsistencia = 1;
            if (dt != null)
            {
                limpiarDataGridReportes("asistencia");
                DataRow dr;
                foreach (DataRow row in dt.Rows)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DataGridRow rowDatos = new DataGridRow();
                    });
                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[0] = contadorFilasAsistencia;
                        dr[i] = row.ItemArray[i].ToString();
                        switch (row.ItemArray[3].ToString())
                        {
                            case "1":
                                dr[3] = "Domingo";
                                break;
                            case "2":
                                dr[3] = "Lunes";
                                break;
                            case "3":
                                dr[3] = "Martes";
                                break;
                            case "4":
                                dr[3] = "Miercoles";
                                break;
                            case "5":
                                dr[3] = "Jueves";
                                break;
                            case "6":
                                dr[3] = "Viernes";
                                break;
                            case "7":
                                dr[3] = "Sabado";
                                break;

                        }
                        if (row.ItemArray[5].ToString().Contains('-'))
                        {
                            dr[5] = '0' + row.ItemArray[5].ToString().Substring(2);
                        }
                        if (row.ItemArray[6].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr[8] = "sources/check.png";
                        }
                        if (row.ItemArray[6].ToString() == "INACTIVO")
                        {
                            dr[8] = "sources/equis.png";
                        }

                    }
                    contadorFilasAsistencia += 1;
                    this.Dispatcher.Invoke(() =>
                    {
                        dgAsistencia.Items.Add(dr);
                    });
                }
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_asistencia.Content = "Encontrado en esta fecha";
                    lbl_mensajes_asistencia.Content = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });

            }
            else
            {
                limpiarDataGridReportes("asistencia");
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_asistencia.Content = "No hay datos registrados durante este peridodo";
                    lbl_mensajes_asistencia.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });


            }
        }

        private void imgEstado_asistencia_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string id = "";
            string estadoActual = "";
            foreach (DataRow item in dgAsistencia.SelectedItems)
            {
                id = item.ItemArray[7].ToString();
                estadoActual = item.ItemArray[6].ToString();
            }
            reporteImpl = new ReporteImpl();
            lbl_mensajes1.Content = reporteImpl.CambiarEstadoAsistencia(estadoActual, id);
            MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
            lbl_mensajes_asistencia.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
            limpiarDataGridReportes("asistencia");
            ListarReporteAsistencia();
        }

        #endregion

        #region codigo Comedor
        private void ListarReporteComedor()
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt2 = reporteImpl.ReporteComedor();
            DataRow dr2;
            contadorFilasComedor = 1;
            limpiarDataGridReportes("comedor");
            foreach (DataRow row2 in dt2.Rows)
            {
                dr2 = dt2.NewRow();
                for (int i = 0; i < row2.ItemArray.Length; i++)
                {
                    dr2[0] = contadorFilasComedor;
                    dr2[i] = row2.ItemArray[i].ToString();
                    //MessageBox.Show(row2.ItemArray[i].ToString());
                    if (row2.ItemArray[9].ToString() == "ACTIVO")
                    {
                        //MessageBox.Show(row.ItemArray[5].ToString());
                        dr2[10] = "sources/check.png";
                    }
                    if (row2.ItemArray[9].ToString() == "INACTIVO")
                    {
                        dr2[10] = "sources/equis.png";
                    }

                }
                this.Dispatcher.Invoke(() =>
                {
                    dgComedor.Items.Add(dr2);
                });

                contadorFilasComedor += 1;
            }

        }
        private void ListarReportesComedorTicket(string ticket)
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt2 = reporteImpl.BuscarPorTicket(ticket);
            DataRow dr2;
            contadorFilasComedor = 1;
            limpiarDataGridReportes("comedor");
            if (dt2 != null)
            {
                foreach (DataRow row2 in dt2.Rows)
                {
                    dr2 = dt2.NewRow();
                    for (int i = 0; i < row2.ItemArray.Length; i++)
                    {
                        dr2[0] = contadorFilasComedor;
                        dr2[i] = row2.ItemArray[i].ToString();
                        //MessageBox.Show(row2.ItemArray[i].ToString());
                        if (row2.ItemArray[9].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr2[10] = "sources/check.png";
                        }
                        if (row2.ItemArray[9].ToString() == "INACTIVO")
                        {
                            dr2[10] = "sources/equis.png";
                        }

                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        dgComedor.Items.Add(dr2);
                    });

                    contadorFilasComedor += 1;
                }
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_comedor.Content = "Tickets encotrados";
                    lbl_mensajes_comedor.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });
            }
            else
            {
                limpiarDataGridReportes("comedor");
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_comedor.Content = "Ticket no encontrado";
                    lbl_mensajes_comedor.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });


            }
        }
        private void ListarReportesComedorFecha(string fechaInicioComedor, string fechaFinComedor)
        {
            reporteImpl = new ReporteImpl();
            System.Data.DataTable dt2 = reporteImpl.BuscarPorFechaComedor(fechaInicioComedor, fechaFinComedor);
            DataRow dr2;
            contadorFilasComedor = 1;
            limpiarDataGridReportes("comedor");
            if (dt2 != null)
            {
                foreach (DataRow row2 in dt2.Rows)
                {
                    dr2 = dt2.NewRow();
                    for (int i = 0; i < row2.ItemArray.Length; i++)
                    {
                        dr2[0] = contadorFilasComedor;
                        dr2[i] = row2.ItemArray[i].ToString();
                        //MessageBox.Show(row2.ItemArray[i].ToString());
                        if (row2.ItemArray[9].ToString() == "ACTIVO")
                        {
                            //MessageBox.Show(row.ItemArray[5].ToString());
                            dr2[10] = "sources/check.png";
                        }
                        if (row2.ItemArray[9].ToString() == "INACTIVO")
                        {
                            dr2[10] = "sources/equis.png";
                        }

                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        dgComedor.Items.Add(dr2);
                    });

                    contadorFilasComedor += 1;
                }
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_comedor.Content = "Tickets encotrados en esta fecha";
                    lbl_mensajes_comedor.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });
            }
            else
            {
                limpiarDataGridReportes("comedor");
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_comedor.Content = "Ticket no encontrados en estas fechas";
                    lbl_mensajes_comedor.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });


            }
        }

        private void btn_refrescar_comedor_Loaded(object sender, RoutedEventArgs e)
        {
            BusyIndicadorComedor.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteComedor();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorComedor.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_comedor.Content = "Datos Cargados";
        }

        private void btn_refrescar_comedor_Click(object sender, RoutedEventArgs e)
        {
            limpiarDataGridReportes("comedor");
            BusyIndicadorComedor.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteComedor();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorComedor.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_comedor.Content = "Datos Actualizados";
        }

        private void txt_ticket_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn_buscar_ticket_Click(object sender, RoutedEventArgs e)
        {
            string ticket = txt_ticket_buscar.Text;
            BusyIndicadorComedor.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReportesComedorTicket(ticket);
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorComedor.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes1.Content = "Datos Cargados";
        }

        private void btn_buscar_fecha_comedor_Click(object sender, RoutedEventArgs e)
        {
            if (dp_fecha_inicio_comedor.SelectedDate.ToString() == "")
            {
                MessageBox.Show("Seleccione una fecha de inicio primero", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string fechaInicioComedor = "";
                string fechaFinComedor = "";
                this.Dispatcher.Invoke(() =>
                {
                    fechaInicioComedor = dp_fecha_inicio_comedor.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                    fechaFinComedor = dp_fecha_fin_comedor.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                });
                BusyIndicadorComedor.IsBusy = true;
                var worker = new BackgroundWorker();
                worker.DoWork += (s, ev) => ListarReportesComedorFecha(fechaInicioComedor, fechaFinComedor);
                worker.RunWorkerCompleted += (s, ev) => BusyIndicadorComedor.IsBusy = false;
                worker.RunWorkerAsync();
                lbl_mensajes_comedor.Content = "Datos Actualizados";
            }
        }

        private void imgEstadoComedor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string id = "";
            string estadoActual = "";
            foreach (DataRow item in dgComedor.SelectedItems)
            {
                id = item.ItemArray[4].ToString();
                estadoActual = item.ItemArray[9].ToString();
            }
            reporteImpl = new ReporteImpl();
            lbl_mensajes_comedor.Content = reporteImpl.CambiarEstadoComedor(estadoActual, id);
            MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
            lbl_mensajes_comedor.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
            limpiarDataGridReportes("comedor");
            ListarReporteComedor();
        }
        #endregion

        #region ReporteGeneral
        private void ListarReporteGeneral()
        {
            //inicio
            reporteImpl = new ReporteImpl();
            var totalesloncjes = reporteImpl.armarConsultaCantidadLonches();
            System.Data.DataTable dt = reporteImpl.mostrarDatosGeneral(totalesloncjes[0], totalesloncjes[1]);
            int binding = 0;
            double totalSumatoria = 0;
            List<int> listaValoresSumar = new List<int>();

            //limpieza
            limpiarDataGridReportes("gColumnas");
            //inicio CargaColumnas
            foreach (DataColumn columna in dt.Columns)
            {
                this.Dispatcher.Invoke(() =>
                {
                    DataGridTextColumn textColumn = new DataGridTextColumn();
                    textColumn.Header = columna.ColumnName;
                    textColumn.Binding = new Binding("[" + binding.ToString() + "]");
                    dgGeneral.Columns.Add(textColumn);
                    binding += 1;
                    //MessageBox.Show(binding.ToString());

                    if (columna.ColumnName.Contains("Total"))
                    {
                        listaValoresSumar.Add(binding-1);
                        //MessageBox.Show(binding.ToString());
                    }
                });
            }
            //Fin CargaColumnas
            limpiarDataGridReportes("general");
            //inicio cargar datos
 
            DataRow dr;
            contadorFilasAux = 1;
            foreach (DataRow row in dt.Rows)
            {
                this.Dispatcher.Invoke(() =>
                {
                    DataGridRow rowDatos = new DataGridRow();
                });

                dr = dt.NewRow();
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    dr[0] = contadorFilasAux;
                    dr[i] = row.ItemArray[i];

                }

                totalSumatoria = 0;
                foreach (var item in listaValoresSumar)
                {
                    totalSumatoria = totalSumatoria+double.Parse(dr[item].ToString());
                    //MessageBox.Show("Sumatotal: "+ double.Parse(dr[item].ToString()));
                }
                int fincol = dt.Columns.Count;

                dr[fincol-1] = totalSumatoria;

                this.Dispatcher.Invoke(() =>
                {
                    dgGeneral.Items.Add(dr);
                });
                contadorFilasAux += 1;

            }

            //fin cargar datos
        }

        private void btn_refrescar_general_Loaded(object sender, RoutedEventArgs e)
        {
            BusyIndicadorGeneral.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteGeneral();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorGeneral.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_general.Content = "Datos Cargados";
        }

        private void ListarReporteGeneralFecha(string fechaInicioGeneral, string fechaFinGeneral)
        {
            //inicio
            reporteImpl = new ReporteImpl();
            var totalesloncjes = reporteImpl.armarConsultaCantidadLonchesFecha();
            System.Data.DataTable dt = reporteImpl.mostrarDatosGeneralPorFecha(totalesloncjes[0], totalesloncjes[1], fechaInicioGeneral, fechaFinGeneral);
            int binding = 0;
            double totalSumatoria = 0;
            List<int> listaValoresSumar = new List<int>();
            if (dt != null)
            {
                //limpieza
                limpiarDataGridReportes("gColumnas");

                foreach (DataColumn columna in dt.Columns)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        textColumn.Header = columna.ColumnName;
                        textColumn.Binding = new Binding("[" + binding.ToString() + "]");
                        dgGeneral.Columns.Add(textColumn);
                        binding += 1;
                        //MessageBox.Show(binding.ToString());

                        if (columna.ColumnName.Contains("Total"))
                        {
                            listaValoresSumar.Add(binding - 1);
                            //MessageBox.Show(binding.ToString());
                        }
                    });
                }


                //Fin CargaColumnas
                limpiarDataGridReportes("general");
                //inicio cargar datos

                DataRow dr;
                contadorFilasAux = 1;
                foreach (DataRow row in dt.Rows)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DataGridRow rowDatos = new DataGridRow();
                    });

                    dr = dt.NewRow();
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        dr[0] = contadorFilasAux;
                        dr[i] = row.ItemArray[i];

                    }

                    totalSumatoria = 0;
                    foreach (var item in listaValoresSumar)
                    {
                        totalSumatoria = totalSumatoria + double.Parse(dr[item].ToString());
                        //MessageBox.Show("Sumatotal: "+ double.Parse(dr[item].ToString()));
                    }
                    int fincol = dt.Columns.Count;

                    dr[fincol - 1] = totalSumatoria;

                    if (totalSumatoria == 0)
                    {
                        //no agrega porque tiene 0 consumo
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            dgGeneral.Items.Add(dr);
                        });
                        contadorFilasAux += 1;
                    }


                }

                //aqui
                this.Dispatcher.Invoke(() =>
                {
                    lbl_mensajes_general.Content = "Encontrado en esta fecha";
                    lbl_mensajes_general.Content = new SolidColorBrush(Color.FromRgb(76, 175, 80));
                });
            }
            else
            {

                this.Dispatcher.Invoke(() =>
                {
                    dgGeneral.ItemsSource = null;
                    dgGeneral.Items.Clear();
                    lbl_mensajes_general.Content = "No hay datos registrados durante este peridodo";
                    lbl_mensajes_general.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54));
                });
            }
        }
        private void dp_fecha_fin_general_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        private void btn_refrescar_general_Click(object sender, RoutedEventArgs e)
        {
            limpiarDataGridReportes("gColumnas");
            limpiarDataGridReportes("general");
            BusyIndicadorGeneral.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteGeneral();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorGeneral.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_general.Content = "Datos Cargados";
        }

        private void btn_buscar_fecha_Click(object sender, RoutedEventArgs e)
        {
            if (dp_fecha_inicio_general.SelectedDate.ToString() == "")
            {
                MessageBox.Show("Seleccione una fecha de inicio primero", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string fechaInicioGeneral = "";
                string fechaFinGeneral = "";
                this.Dispatcher.Invoke(() =>
                {
                    fechaInicioGeneral = dp_fecha_inicio_general.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                    fechaFinGeneral = dp_fecha_fin_general.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                    //MessageBox.Show(fechaInicioGeneral+" "+fechaFinGeneral);
                });
                BusyIndicadorGeneral.IsBusy = true;
                var worker = new BackgroundWorker();
                worker.DoWork += (s, ev) => ListarReporteGeneralFecha(fechaInicioGeneral, fechaFinGeneral);
                worker.RunWorkerCompleted += (s, ev) => BusyIndicadorGeneral.IsBusy = false;
                worker.RunWorkerAsync();
                lbl_mensajes_general.Content = "Datos Actualizados";
            }
        }


        #endregion

        #endregion

        private void vntPrincipal_Closing(object sender, CancelEventArgs e)
        {
            AutoClosingMessageBox.Show("Cerrando el programa, espere... \nNO CIERRE ESTA VENTANA", "CERRANDO MOBIUS FOOD SERVICE...", 3000);
            System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Final y cierre de sesion del {1}", DateTime.Now, Sesion.verInfo()));
            //MessageBox.Show("Cerrando el programa, espere...","Cerrando...",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void btn_limpiar_codigo_Loaded(object sender, RoutedEventArgs e)
        {
            ListarUsusarios();
        }

        
    }
}
