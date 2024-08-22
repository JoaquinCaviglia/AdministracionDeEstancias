namespace Estancia
{
    public class Tarea : IValidable
    {
        private int _id;
        private static int s_ultimoId = 1;
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value;}
        }

        public int Id
        { 
            get { return _id; } 
            set { _id = value; }
        }

        public Tarea(string descripcion)
        {
            _id = s_ultimoId++;
            _descripcion = descripcion;
        }

        public Tarea() { }

        public void Validar()
        {
            if(string.IsNullOrEmpty(_descripcion))
            {
                throw new Exception("La tarea debe contener una descripción");
            }
        }
    }
}
