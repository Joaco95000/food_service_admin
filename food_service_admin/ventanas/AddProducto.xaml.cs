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
    /// Lógica de interacción para AddProducto.xaml
    /// </summary>
    public partial class AddProducto : Window
    {
        ItemImpl itemImpl;
        CategoriaImpl categoriaImpl;
        int stock = 0;
        double precio = 0.10;


        public AddProducto()
        {
            InitializeComponent();
            cargarDatosCB();
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Nombre_Producto.Text != String.Empty && txt_Descripcion.Text != String.Empty)
            {
                itemImpl = new ItemImpl();
                if (itemImpl.InsertProducto(new Item(txt_Nombre_Producto.Text, txt_Descripcion.Text, precio, int.Parse(cb_Categoria.SelectedValue.ToString()), stock)))
                {
                    MessageBoxResult result = MessageBox.Show("Producto Agregado con Exito", "Agregación de producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        MainWindow obj = new MainWindow();
                        obj.refrescarSnack();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Error al insertar producto consulte con su desarrollador", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        private void cargarDatosCB()
        {
            try
            {
                categoriaImpl = new CategoriaImpl();

                cb_Categoria.DisplayMemberPath = "nombre";
                cb_Categoria.SelectedValuePath = "id";
                cb_Categoria.ItemsSource = categoriaImpl.SelectCategorias().DefaultView;
                cb_Categoria.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ups! ocurrio un error, contactese con su encargado de sistemas.  \n error: " + ex.Message);
            }
        }

        #region precio

        private void btnReducirPrecio_Click(object sender, RoutedEventArgs e)
        {
            if (precio <= 0.1)
            {
                precio = 0.1;
            }
            else
            {
                precio = precio - 0.1;
            }
            txtPrecio.Text = precio.ToString("F2");
        }

        private void btnAmentarPrecio_Click(object sender, RoutedEventArgs e)
        {
            precio = precio + 0.10;
            txtPrecio.Text = precio.ToString("F2");
        }

        private void btnAmentarPreciox10_Click(object sender, RoutedEventArgs e)
        {
            precio = precio + 1;
            txtPrecio.Text = precio.ToString("F2");
        }

        private void btnReducirPreciox10_Click(object sender, RoutedEventArgs e)
        {
            if (precio >= 1.1)
            {
                precio = precio - 1;
            }
            else
            {
                precio = 0.1;
            }
            txtPrecio.Text = precio.ToString("F2");
        }

        #endregion


        #region stock
        private void cbHabilitarStock_Checked(object sender, RoutedEventArgs e)
        {
            btnAmentarStock.IsEnabled = true;
            btnReducirStock.IsEnabled = true;
        }
        private void cbHabilitarStock_Unchecked(object sender, RoutedEventArgs e)
        {
            btnAmentarStock.IsEnabled = false;
            btnReducirStock.IsEnabled = false;
            stock = 0;
            txtCantidadStock.Text = stock.ToString();
        }

        private void btnAmentarStock_Click(object sender, RoutedEventArgs e)
        {
            stock++;
            txtCantidadStock.Text = stock.ToString();
        }

        private void btnReducirStock_Click(object sender, RoutedEventArgs e)
        {
            if (stock <= 0)
            {
                stock = 0;
            }
            else
            {
                stock--;
            }
            txtCantidadStock.Text = stock.ToString();
        }
        #endregion


    }
}

