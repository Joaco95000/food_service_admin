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
            this.Title += " ["+ventanas.Login.sesion.Login + " - " + ventanas.Login.sesion.Nombre + " " + ventanas.Login.sesion.Paterno + " " + ventanas.Login.sesion.Materno + "]";
            ListUsuarios = new List<Usuario>();
            ListarUsusarios();
            //LLenarListView();
            //dg.Columns[5].DisplayIndex = 1;
        }

        private void ListarUsusarios()
        {
            usuarioImpl = new UsuarioImpl();
            DataTable dt = usuarioImpl.listadoUsuarios();
            dg.ItemsSource = dt.DefaultView;

        }

        private void wea()
        {

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
