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
using System.Windows.Shapes;
using Implementation;
using Model;

namespace food_service_admin.ventanas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        //public static Usuario sesion;
        UsuarioImpl usuarioImpl;

        public Login()
        {
            InitializeComponent();
            usuarioImpl = new UsuarioImpl();
        }

        private void btn_ingresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string user = txt_usuario.Text;
                string password = psb_password.Password;
                password = Encrypt.GetMD5(password);
                DataTable dt = usuarioImpl.Login(user, password);
                if (dt != null)
                {
                    Sesion.id = int.Parse(dt.Rows[0][0].ToString());
                    Sesion.nombre = dt.Rows[0][1].ToString();
                    Sesion.paterno = dt.Rows[0][2].ToString();
                    Sesion.materno = dt.Rows[0][3].ToString();
                    Sesion.login = dt.Rows[0][4].ToString();
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} |-| Info: Inicio de sesion de el {1}", DateTime.Now,Sesion.verInfo()));
                    MainWindow principal = new MainWindow();
                    principal.Show();
                    this.Close();
                }
                else
                {
                    txt_usuario.Text = "";
                    psb_password.Password = "";
                    lbl_mensaje.Content = "Usuario y/o contraseña incorrecta.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login: " + ex);
            }
            

        }
    }
}
