
namespace Estancia
{
    public class Potrero : IValidable, IComparable<Potrero>
    {
        private int _id;
        private static int s_ultimoId = 1;
        private string _descripcion;
        private int _cantHectarias;
        private int _capacidadAnimales;
        private List<Res> _cantPastando = new List<Res>();

        public int Id
        {
            get { return _id; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
        }
        public int CantidadHectareas
        {
            get { return _cantHectarias; }
        }

        public int CapacidadMaxima
        {
            get { return _capacidadAnimales; }
        }
        public int CapacidadAnimales
        {
            get { return _capacidadAnimales; }
        }

        public int CantidadAnimales
        {
            get { return _cantPastando.Count; }
        }

        public Potrero(string descripcion, int cantHectarias, int capacidadAnimales = 10)
        {
            _id = s_ultimoId++;
            _descripcion = descripcion;
            _cantHectarias = cantHectarias;
            _capacidadAnimales = capacidadAnimales;
            //_cantPastando = cantPastando;
        }
        
        public decimal CostoTotalCrianza()
        {
            decimal costoTotal = 0;
            foreach(Res res in _cantPastando)
            {
                costoTotal += res.GananciaPotencial() - (res.CostoCrianza() + res.CostoTotalVacunas());
            }
            return costoTotal;
        }

        public void AssignAnimalCapacity()
        {
            //Asigno la capacidad de los animales respecto a la cantidad de hectarias
             _capacidadAnimales = _cantHectarias * 2;
        }

        public void AddRes(Res unRes)
        {
            if (!HayEspacio())
                throw new Exception("Potrero lleno");
             _cantPastando.Add(unRes);
        }

        public bool HayEspacio()
        {
            return _capacidadAnimales > _cantPastando.Count;
        }

        public void Validar()
        {
            if (_cantHectarias > 10)
                throw new Exception("Los potreros no pueden tener más de 10 hectareas");
        }

        internal void AssignAnimals(List<Res> freeAnimals)
        {
            int aux = 0;
            while( (_capacidadAnimales > _cantPastando.Count) && (aux<60) )
            {
                if (freeAnimals[aux].Libre == true)
                {
                    _cantPastando.Add(freeAnimals[aux]);
                    freeAnimals[aux].Libre = false;
                }
                aux++;
            }
        }
        public override string ToString()
        {
            return $"Identificador: {_id}\n" +
                   $"_Descripción: {_descripcion}\n" +
                   $"_Héctareas: {_cantHectarias}\n" +
                   $"_Capacidad: {_capacidadAnimales}\n";
        }

        internal bool AreaMayorACantHectareasYCapacidadMaxSuperiorNroDado(int hectareas, int valor)
        {
            return this.CantidadHectareas > hectareas && this.CapacidadMaxima > valor;
        }

        public int CompareTo(Potrero otroP)
        {
            if (_capacidadAnimales.CompareTo(otroP._capacidadAnimales) == 0)
                return _cantPastando.Count.CompareTo(otroP._cantPastando.Count) * -1;
            return _capacidadAnimales.CompareTo(otroP._capacidadAnimales);
        }
    }
}
