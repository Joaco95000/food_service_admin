using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string paterno;
        private string materno;
        private string login;
        private string password;
        private string documento;
        private byte[] fotografia;
        private string fotoBool;
        private string estado;
        private DateTime fechaIngreso;
        private DateTime fechaNacimiento;
        private string cargo;
        private string apellidos;


        public Usuario()
        {

        }

        public Usuario(string nombre, string paterno, string materno, string login, string password, string documento, byte[] fotografia, string fotoBool, string estado, DateTime fechaIngreso, DateTime fechaNacimiento, string cargo, string apellidos)
        {
            this.nombre = nombre;
            this.paterno = paterno;
            this.materno = materno;
            this.login = login;
            this.password = password;
            this.documento = documento;
            this.fotografia = fotografia;
            this.fotoBool = fotoBool;
            this.estado = estado;
            this.fechaIngreso = fechaIngreso;
            this.fechaNacimiento = fechaNacimiento;
            this.cargo = cargo;
            this.apellidos = apellidos;
        }

        #region get set

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        public string Paterno
        {
            get { return paterno; }
            set { paterno = value; }
        }

        public string Materno
        {
            get { return materno; }
            set { materno = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }
        public byte[] Fotografia
        {
            get { return fotografia; }
            set { fotografia = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public DateTime FechaIngreso
        {
            get { return fechaIngreso; }
            set { fechaIngreso = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }

        public string Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }


        public string FotoBool
        {
            get { return fotoBool; }
            set { fotoBool = value; }
        }


        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }


        #endregion

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
