
namespace Estancia
{
    public class Vacuna : IValidable
    {
        private string _nombre;
        private string _descripcion;
        private string _patogeno;

        public string Nombre
        {
            get { return _nombre; }
        }

        public Vacuna(string nombre, string descripcion, string patogeno)
        {
            _nombre = nombre;
            _descripcion = descripcion;
            _patogeno = patogeno;
        }

        public Vacuna()
        {
            _nombre = "SN";
            _descripcion = "SD";
            _patogeno = "SP";
        }


        // Revisar letra si necesitamos algún otro control
        public void Validar()
        {
            if(string.IsNullOrEmpty(_nombre))            
            {
                throw new Exception("El nombre no puede ser vacio");
            }
        }
    }
}
