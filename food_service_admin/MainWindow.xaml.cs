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

namespace food_service_admin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsuarioImpl usuarioImpl;
        ReporteImpl reporteImpl;
        int totalUsuario;
        int usuariosInactivos;
        int usuariosActivos;
        int contadorFilas;
        public MainWindow()
        {
            InitializeComponent();
            this.Title += " ["+ventanas.Login.sesion.Login + " - " + ventanas.Login.sesion.Nombre + " " + ventanas.Login.sesion.Paterno + " " + ventanas.Login.sesion.Materno + "]";
            ListarUsusarios();
            lbl_mensajes.Content = "Login con exito, se cargaron los datos correctamente";
            lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(76,175,80));
            totalUsuario = 0;
            usuariosInactivos = 0;
            usuariosActivos = 0;

        }

        #region Usuarios

        public void refrescar()
        {
            ListarUsusarios();
            lbl_mensajes.Content = "Datos actualizados...";
        }

        private void ListarUsusarios()
        {
            usuarioImpl = new UsuarioImpl();
            DataTable dt = usuarioImpl.listadoUsuarios();
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
                        dr[6] = "sources/check.png";
                    }
                    if (row.ItemArray[5].ToString() == "INACTIVO")
                    {
                        dr[6] = "sources/equis.png";
                    }
                    
                }
                        
                if(dr[5].ToString() == "ACTIVO") { usuariosActivos = usuariosActivos + 1; }
                if(dr[5].ToString() == "INACTIVO") { usuariosInactivos = usuariosInactivos + 1; }
                dg.Items.Add(dr);
                totalUsuario = totalUsuario+1;
                

            }
            lb_cantidad_total.Content = totalUsuario.ToString();
            lb_cantidad_activos.Content = usuariosActivos.ToString();
            lb_cantidad_inactivos.Content = usuariosInactivos.ToString();
        }

        private void wea()
        {

        }

        private void txt_nombre_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_codigo_buscar.Text = "";
            string nombre = txt_nombre_buscar.Text;
            usuarioImpl = new UsuarioImpl();
            DataTable dt = usuarioImpl.BuscarPorNombre(nombre);
            if(dt!=null)
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
                            dr[6] = "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] = "sources/equis.png";
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
            DataTable dt = usuarioImpl.BuscarPorCodigo(codigo);
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
                            dr[6] = "sources/check.png";
                        }
                        if (row.ItemArray[5].ToString() == "INACTIVO")
                        {
                            dr[6] = "sources/equis.png";
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
            string id="";
            string estadoActual="";
            foreach (DataRow item in dg.SelectedItems)
            {
                id = item.ItemArray[0].ToString();
                estadoActual = item.ItemArray[5].ToString();
            }
            usuarioImpl = new UsuarioImpl();
            lbl_mensajes.Content = usuarioImpl.CambiarEstado(estadoActual, id);
            lbl_mensajes.Background = new SolidColorBrush(Color.FromRgb(63, 81, 181));
            MessageBox.Show("Estado cambiado con exito.", "Acción realizada", MessageBoxButton.OK, MessageBoxImage.Information);
            limpiarDataGrid();
            ListarUsusarios();
            txt_nombre_buscar.Text = "";
            txt_codigo_buscar.Text = "";
        }


        private void limpiarDataGrid()
        {
            dg.ItemsSource = null;
            dg.Items.Clear();
        }

        private void limpiarDataGridReportes(string data)
        {
            switch (data)
            {
                case "general":
                    dg1.ItemsSource = null;
                    dg1.Items.Clear();
                    break;
                case "comedor":
                    dg1.ItemsSource = null;
                    dg1.Items.Clear();
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
            }
            
        }

        private void btn_nuevo_usuario_Click(object sender, RoutedEventArgs e)
        {
            ventanas.AddUsuario ventanaUsuario = new ventanas.AddUsuario();
            ventanaUsuario.Show();
        }

        private void btn_refrescar_Click(object sender, RoutedEventArgs e)
        {
            refrescar();
        }

        private void btn_cambiar_password_Loaded(object sender, RoutedEventArgs e)
        {
            limpiarDataGrid();
            ListarUsusarios();
        }

        #endregion Usuarios

        #region Style

        private void ti_reportes_GotFocus(object sender, RoutedEventArgs e)
        {
            limpiarDataGrid();
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
            //limpiarDataGrid();
            //ListarUsusarios();
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

        #endregion

        #region Reportes

        private void ListarReporteVentas()
        {
            reporteImpl = new ReporteImpl();
            DataTable dt2 = reporteImpl.ReporteVentas();
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
                    if (row2.ItemArray[9].ToString() == "0")
                    {
                        dr2[9] = "TOUCH";
                    }
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

        #endregion


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
            DataTable dt = reporteImpl.BuscarPorOrden(orden);
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
                        if (row.ItemArray[9].ToString() == "0")
                        {
                            dr[9] = "TOUCH";
                        }
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
            DataTable dt = reporteImpl.BuscarPorFecha(fecha_inicio, fecha_final);
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
                        if (row.ItemArray[9].ToString() == "0")
                        {
                            dr[9] = "TOUCH";
                        }
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

        private void dp_fecha_fin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp_fecha_inicio.SelectedDate.ToString() == "")
            {
                MessageBox.Show("Seleccione una fecha de inicio primero", "Atención",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            { 
                string fechaInicio="";
                string fechaFin="";
                this.Dispatcher.Invoke(() =>
                {
                    fechaInicio = dp_fecha_inicio.SelectedDate.Value.Date.ToShortDateString();
                    fechaFin = dp_fecha_fin.SelectedDate.Value.Date.ToShortDateString();
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
                id = item.ItemArray[12].ToString();
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


        private void ListarReporteAsistencia()
        {
            reporteImpl = new ReporteImpl();
            DataTable dt2 = reporteImpl.ReporteAsistencia();
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

                contadorFilas += 1;
            }
        }

        private void ListarAsistenciaNombre(string nombre)
        {
            reporteImpl = new ReporteImpl();
            DataTable dt = reporteImpl.BuscarAsistenciaNombre(nombre);
            contadorFilas = 1;
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
                        dr[0] = contadorFilas;
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
                    contadorFilas += 1;
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
            BusyIndicadorAsistencia.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarReporteAsistencia();
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorAsistencia.IsBusy = false;
            worker.RunWorkerAsync();
            lbl_mensajes_asistencia.Content = "Datos Cargados";
        }

        private void txt_buscar_asistencia_nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre = txt_buscar_asistencia_nombre.Text;
            limpiarDataGridReportes("asistencia");
            BusyIndicadorAsistencia.IsBusy = true;
            var worker = new BackgroundWorker();
            worker.DoWork += (s, ev) => ListarAsistenciaNombre(nombre);
            worker.RunWorkerCompleted += (s, ev) => BusyIndicadorAsistencia.IsBusy = false;
            worker.RunWorkerAsync();
            
        }

        private void dp_fecha_fin_asistencia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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
                    fechaInicioAsistencia = dp_fecha_inicio_asistencia.SelectedDate.Value.Date.ToShortDateString();
                    fechaFinAsistencia = dp_fecha_fin_asistencia.SelectedDate.Value.Date.ToShortDateString();
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
            DataTable dt = reporteImpl.BuscarAsistenciaPorFecha(fechaInicio, fechaFin);
            contadorFilas = 1;
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
                        dr[0] = contadorFilas;
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
                    contadorFilas += 1;
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
                limpiarDataGridReportes("ventas");
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
    }




}
