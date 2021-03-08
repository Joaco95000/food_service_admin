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
        List<Usuario> ListUsuarios;
        public MainWindow()
        {
            InitializeComponent();
            //this.Title += " ["+ventanas.Login.sesion.Login + " - " + ventanas.Login.sesion.Nombre + " " + ventanas.Login.sesion.Paterno + " " + ventanas.Login.sesion.Materno + "]";
            ListUsuarios = new List<Usuario>();
            ListarUsusarios();
            lbl_mensajes.Content = "Login con exito, se cargaron los datos correctamente";
            //LLenarListView();
            //dg.Columns[5].DisplayIndex = 1;
        }

        private void ListarUsusarios()
        {
            usuarioImpl = new UsuarioImpl();
            DataTable dt = usuarioImpl.listadoUsuarios();
            dg.ItemsSource = dt.DefaultView;
            lbl_mensajes.Content = "Listo";

        }

        private void wea()
        {

        }

        private void txt_nombre_buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre = txt_nombre_buscar.Text;
            usuarioImpl = new UsuarioImpl();
            DataTable dt = usuarioImpl.BuscarPorNombre(nombre);
            if(dt!=null)
            {
                dg.ItemsSource = dt.DefaultView;
                lbl_mensajes.Content = "Usaruios encontrados";
            }
            else
            {
                limpiarDataGrid();
                lbl_mensajes.Content = "Usaruios no encontrados";

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
                dg.ItemsSource = dt.DefaultView;
                lbl_mensajes.Content = "Usaruios encontrados";
            }
            else
            {
                limpiarDataGrid();
                lbl_mensajes.Content = "Usaruios no encontrados";

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
            foreach (DataRowView item in dg.SelectedItems)
            {
                    id = item.Row.ItemArray[0].ToString();
                    estadoActual = item.Row.ItemArray[5].ToString();
            }
            usuarioImpl = new UsuarioImpl();
            lbl_mensajes.Content = usuarioImpl.CambiarEstado(estadoActual,id);
            limpiarDataGrid();
            ListarUsusarios();
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

        //private void LLenarListView()
        //{
        //    dg.ItemsSource = ListUsuarios;
        //}

        //private void ListarUsusarios()
        //{
        //    usuarioImpl = new UsuarioImpl();
        //    string foto;
        //    DataTable dt = usuarioImpl.listadoUsuarios();
        //    foreach (DataRow dataRow in dt.Rows)
        //    {
        //        if (dataRow["fotografia"] == null || dataRow["fotografia"].ToString().Length == 0)
        //        {
        //            foto = "NO";
        //        }
        //        else
        //        {
        //            foto = "SI";
        //        }
        //        ListUsuarios.Add(new Usuario()
        //        {
        //            Id = int.Parse(dataRow["id"].ToString()),
        //            Nombre = dataRow["nombre"].ToString(),
        //            Apellidos = dataRow["paterno"].ToString()+" "+ dataRow["materno"].ToString(),
        //            Documento = dataRow["documento"].ToString(),
        //            FotoBool = foto,
        //            Estado = dataRow["estado"].ToString()
        //        }); 
        //    }



        //}
    }
}
