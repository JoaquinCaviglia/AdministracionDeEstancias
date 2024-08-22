namespace Estancia
{
    public class Empleado : IValidable
    {
        private int _id;
        private static int s_ultimoId = 1;
        private string _email;
        private string _contrasenia;
        private string _nombre;
        private DateTime _fechaIngreso;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Contrasenia
        {
            get { return _contrasenia; }
            set { _contrasenia = value;}
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime FechaIngreso
        {
            get { return _fechaIngreso; }
        }

        public Empleado() { }
        public Empleado(string email, string contrasenia, string nombre, DateTime fechaIngreso)
        {
            _id = s_ultimoId++;
            _email = email;
            _contrasenia = contrasenia;
            _nombre = nombre;
            _fechaIngreso = fechaIngreso;
        }

        
        public void Validar()
        {
            if (string.IsNullOrEmpty(_contrasenia) || _contrasenia.Length < 8)
                throw new Exception("Contraseña no cumple con los requisitos");
        }

        public override string ToString()
        {
            return $"Empleado: {_nombre}\n" +
                $"Gmail {_email} \n" +
                $"Fecha de Ingreso: {_fechaIngreso}\n";

        }

        public virtual string Rol()
        {
            return "";
        }

        public override bool Equals(object? obj)
        {   
            return obj is Empleado otroE && _email == otroE._email;
        }
    }
}
