using System;
using System.Collections.Generic;
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
        public static Usuario sesion;
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
                var userSelected = usuarioImpl.Login(user, password);
                if (userSelected != null)
                {
                    sesion = userSelected;
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
