using Implementation;
using Model;
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

namespace food_service_admin.ventanas
{
    /// <summary>
    /// Lógica de interacción para AddUsuario.xaml
    /// </summary>
    public partial class AddUsuario : Window
    {
        UsuarioImpl usuarioImpl;
        Usuario usuario;
        public AddUsuario()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text != String.Empty && txt_Paterno.Text != String.Empty && txt_Materno.Text != String.Empty && txt_NombreUsuario.Text != String.Empty && pb_contraseña.Password != String.Empty && txt_carnet.Text != String.Empty) {
                usuarioImpl = new UsuarioImpl();
                usuario = new Usuario();
                usuario.Nombre = txt_Nombre.Text;
                usuario.Paterno = txt_Paterno.Text;
                usuario.Materno = txt_Materno.Text;
                usuario.Login = txt_NombreUsuario.Text;
                usuario.Password = Encrypt.GetMD5(pb_contraseña.Password);
                usuario.Documento = txt_carnet.Text;
                usuario.Fotografia = null;
                usuario.Estado = "ACTIVO";
                usuario.Cargo = null;
                if (usuarioImpl.InsertUsuario(usuario))
                {
                    MessageBoxResult result = MessageBox.Show("Usuario Agregado con Exito", "Agregación de usuario", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        MainWindow obj = new MainWindow();
                        obj.refrescar();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya esta en uso, por favor cambielo o añada un número","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
               
                
            }
            else{
                MessageBox.Show("No puede dejar campos vacios.", "Error al llenar el formulario", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        #region validaciones

        private void txt_Nombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z-ñÑ]"))
            {
                e.Handled = true;
            }
        }

        private void txt_Paterno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z-ñÑ]"))
            {
                e.Handled = true;
            }
        }

        private void txt_Materno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z-ñÑ]"))
            {
                e.Handled = true;
            }
        }

        private void txt_carnet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn_limpiar_codigo_Click(object sender, RoutedEventArgs e)
        {
            txt_carnet.Text = "";
        }

        #endregion


    }
}
