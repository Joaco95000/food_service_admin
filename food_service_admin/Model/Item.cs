using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Item
    {
        public Item()
        {

        }

        public Item(string nombre, string descripcion, double precio, int categoria, int stock)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Categoria = categoria;
            Stock = stock;
        }


        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Categoria { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; }
    }
}
