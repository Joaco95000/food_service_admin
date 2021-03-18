using Implementation;
using Model;
using System;
using System.Collections.Generic;
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
        int totalUsuario;
        int usuariosInactivos;
        int usuariosActivos;

        ClienteImpl clienteImpl;
        int totalCliente;
        int clientesInactivos;
        int clientesActivos;

        ItemImpl itemImpl;
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
        #region codigo Usuario
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
                        dr[6] += "sources/check.png";
                    }
                    if (row.ItemArray[5].ToString() == "INACTIVO")
                    {
                        dr[6] += "sources/equis.png";
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

        private void btn_nuevo_usuario_Click(object sender, RoutedEventArgs e)
        {
            ventanas.AddUsuario ventanaUsuario = new ventanas.AddUsuario();
            ventanaUsuario.Show();
        }

        private void btn_refrescar_Click(object sender, RoutedEventArgs e)
        {
            refrescar();
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
            cargarUsuarios();
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
            cargarComesales();
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
            cargarSnack();
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
        }

        private void cargarComesales()
        {
            //limpiar dg usuarios
            limpiarDataGrid();
            //cargar Comensales
            ListarComensales();
            lbl_mensajes_comensal.Content = "Datos Cargados en Comensales";
            lbl_mensajes_comensal.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            //limpiar dg snack
            limpiarDataGridSnack();
        }

        private void cargarSnack()
        {
            //limpiar dg usuarios
            limpiarDataGrid();
            //limpiar dg comensales
            limpiarDataGridComesales();
            //cargar dg snack
            ListarProductos();
            lbl_mensajes_snack.Content = "Datos Cargados en Productos";
            lbl_mensajes_snack.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }

        #endregion

        #region codigo Comensal
        public void refrescarComensales()
        {
            ListarComensales();
            lbl_mensajes_comensal.Content = "Datos actualizados...";
        }
        private void ListarComensales()
        {
            clienteImpl = new ClienteImpl();
            DataTable dt = clienteImpl.listadoClientes();
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
            DataTable dt = clienteImpl.BuscarComensalPorNombre(nombre);
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
            DataTable dt = clienteImpl.BuscarComensalPorCodigo(codigo);
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
        #endregion

        #region codigo Snack

        private void ListarProductos()
        {
            itemImpl = new ItemImpl();
            DataTable dt = itemImpl.listadoProductos();
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
            ListarProductos();
            lbl_mensajes_snack.Content = "Datos actualizados...";
        }
        private void btn_refrescar_snack_Click(object sender, RoutedEventArgs e)
        {
            refrescarSnack();
        }

        #endregion
    }
}
