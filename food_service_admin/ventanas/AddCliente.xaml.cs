using Implementation;
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
using Model;

namespace food_service_admin.ventanas
{
    /// <summary>
    /// Lógica de interacción para AddCliente.xaml
    /// </summary>
    public partial class AddCliente : Window
    {
        ClienteImpl clienteImpl;
        public AddCliente()
        {
            InitializeComponent();
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre.Text != String.Empty && txt_Paterno.Text != String.Empty && txt_Materno.Text != String.Empty && txt_carnet.Text != String.Empty && txt_ficha.Text != String.Empty)
            {
                clienteImpl = new ClienteImpl();
                if (clienteImpl.BuscarCodigo(txt_ficha.Text))
                {
                    if (clienteImpl.InsertCliente(new Cliente(txt_Nombre.Text, txt_Paterno.Text, txt_Materno.Text, txt_carnet.Text, DateTime.Now.ToString("yyyy-MM-dd"), txt_ficha.Text, int.Parse(cbTipo.SelectedIndex.ToString()))))
                    {
                        MessageBoxResult result = MessageBox.Show("Comensal Agregado con Exito", "Agregación de comensal", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (result == MessageBoxResult.OK)
                        {
                            MainWindow obj = new MainWindow();
                            obj.refrescarComensales();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al insertar comensal consulte con su desarrollador", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("El codigo ya existe use otro.", "Error al ingresar codigo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("No puede dejar campos vacios.", "Error al llenar el formulario", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void txt_ficha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn_limpiar_codigo_ficha_Click(object sender, RoutedEventArgs e)
        {
            txt_ficha.Text = "";
        }

        #endregion
    }
}
