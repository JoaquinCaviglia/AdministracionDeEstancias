namespace Estancia
{
    public class TareaAsignada : IValidable, IComparable<TareaAsignada>
    {
        private string _comentario;
        private DateTime _fechaPactada;
        private DateTime _fechaCierre;
        private Tarea _tarea;

        public string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        public DateTime FechaCierre
        {
            get { return _fechaCierre; }
            set { _fechaCierre = value; }
        }

        public DateTime FechaPactada
        {
            get { return _fechaPactada; }
            set { _fechaPactada = value; }
        }

        public Tarea Tarea
        { 
            get { return _tarea; }
            set { _tarea = value; }
        }


        public TareaAsignada(string comentario, DateTime fechaPactada, DateTime fechaCierre, Tarea tarea)
        {
            _comentario = comentario;
            _fechaPactada = fechaPactada;
            _fechaCierre = fechaCierre;
            _tarea = tarea;
        }
        public TareaAsignada(DateTime fechaPactada, Tarea tarea)
        {
            _fechaPactada = fechaPactada;
            _tarea = tarea;
        }

        public TareaAsignada() { }

        public void Validar()
        {
            //Valida casos INCORRECTOS a la hora de asignar una tarea. (No puede existir comentario sin fecha de cierre y viceversa.)
            if (_fechaCierre == DateTime.MinValue && _comentario != null)
            {
                throw new Exception("No puede existir una asignación de tarea con un comentario y sin fecha de cierre.");
            }
            else if (_fechaCierre != DateTime.MinValue && _comentario == null) 
            {
                throw new Exception("La tarea finalizada debe tener un comentario.");
            }     
        }

        public int CompareTo(TareaAsignada otraTa)
        {
            return _fechaPactada.CompareTo(otraTa._fechaPactada);
        }
    }
}
