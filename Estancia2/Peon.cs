namespace Estancia
{
    public class Peon : Empleado 
    {
        private bool _esResidente;
        private List<TareaAsignada> _tareas;
        private bool _tieneCapataz;

        public bool TieneCapataz
        {
            get { return _tieneCapataz; }
            set { _tieneCapataz = value;}
        }
        public bool EsResidente
        { 
            set { _esResidente = value;}
            get { return _esResidente;}
        }

        public List<TareaAsignada> TareasAsignadas
        {
            set { _tareas = value;}
            get { return _tareas; }
        }

        public Peon() { }

        public Peon(bool esResidente, List<TareaAsignada> tareas, bool tieneCapataz, string email, string contrasenia, string nombre, DateTime fechaIngreso) : base(email, contrasenia, nombre, fechaIngreso)
        {
            _esResidente = esResidente;
            _tareas = tareas;
            _tieneCapataz = tieneCapataz;
        }

        public Peon(bool esResidente, bool tieneCapataz, string email, string contrasenia, string nombre, DateTime fechaIngreso) : base(email, contrasenia, nombre, fechaIngreso)
        {
            _esResidente = esResidente;
            _tieneCapataz = tieneCapataz;
            _tareas = new List<TareaAsignada>();
        }

        internal void AsignarTarea(Tarea tarea, DateTime fecha)
        {
            TareaAsignada tareaAsignada = new TareaAsignada(fecha, tarea);
            _tareas.Add(tareaAsignada);
        }

        public override string Rol()
        {
            return "peon";    
        }

        public TareaAsignada BuscarTareaAsignada(int id)
        {
            TareaAsignada buscada = null;
            int i = 0;
            while(buscada == null && i < _tareas.Count)
            {
                if (_tareas[i].Tarea.Id == id)
                {
                    buscada = _tareas[i];
                }
                i++;
            }
            if (buscada == null)
                throw new Exception("Tarea no encontrada");

            return buscada;
        }

        public void actualizarTareaAsignada(TareaAsignada tareaA)
        {
            tareaA.Validar();
            TareaAsignada buscada = BuscarTareaAsignada(tareaA.Tarea.Id);
            this.TareasAsignadas.Remove(buscada);
            this.TareasAsignadas.Add(tareaA);
        }
    }
}
