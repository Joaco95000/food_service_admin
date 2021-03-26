using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class Sesion
    {
        public static int id;
        public static string nombre;
        public static string paterno;
        public static string materno;
        public static string login;

        public static string verInfo()
        {
            return "Usuario: " + login + " - " + nombre + " " + paterno + " " + materno;
        }

    }
}
