namespace Estancia
{
    public abstract class Res : IValidable, IComparable<Res>
    {
        private string _numeroCaravana;
        private Sexo _sexo;
        private string _raza;
        private DateTime _fechaNacimiento;
        private decimal _costoAdquisicion;
        private double _pesoActual;
        private bool _esHibrido;
        private List<Inyeccion> _vacunas;
        private bool _esLibre;

        public string Id
        {
            get { return _numeroCaravana; }
            set { _numeroCaravana = value; }
        }

        public string Raza
        {
            get { return _raza; }
            set { _raza = value; }
        }
        public double PesoActual
        {
            get { return _pesoActual; }
            set { _pesoActual = value; }
        }
        public Sexo Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }
        public bool Libre
        {
            get { return _esLibre; }
            set { _esLibre = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public decimal CostoAdquisicion
        {
            get { return _costoAdquisicion; }
            set { _costoAdquisicion = value; }
        }

        public bool EsHibrido
        {
            get { return _esHibrido; }
        } 

        public List<Inyeccion> Inyecciones
        {
            get { return _vacunas; }
            set { _vacunas = value; } 
        }

        public Res(string numeroCaravana, Sexo sexo, string raza, DateTime fechaNacimiento, decimal costoAdquisicion, double pesoActual, bool esHibrido, List<Inyeccion> vacunas)
        {
            _numeroCaravana = numeroCaravana;
            _sexo = sexo;
            _raza = raza;
            _fechaNacimiento = fechaNacimiento;
            _costoAdquisicion = costoAdquisicion;
            _pesoActual = pesoActual;
            _esHibrido = esHibrido;
            _vacunas = vacunas;
            _esLibre = true;
        }

        public Res() { }

        public decimal CostoTotalVacunas()
        {
            return _vacunas.Count * 200;
        }

        public virtual int CompareTo(Res animal)
        {
            if(animal.PesoActual == _pesoActual) return 0;
            else if(animal.PesoActual < _pesoActual) return 1;
            return -1;
        }
        #region validar
        public virtual void Validar()
        {
            string numeros = "0123456789";
            int contieneNumero = 0;

            if (_numeroCaravana.Length != 8 || string.IsNullOrEmpty(_numeroCaravana))
                throw new Exception("El código identificador no puede ser vacio ni de largo distinto a 8");
            for (int i = 0; i< _numeroCaravana.Length; i++)
            {
                for(int j = 0; j < numeros.Length; j++)

                    if (numeros[j] == _numeroCaravana[i])
                        contieneNumero++;
            }
            if (contieneNumero == 8 || contieneNumero == 0)
                throw new Exception("La caravana debe ser alfanumerica");

        }
        #endregion

        public bool EsMayorATresMeses()
        {
            TimeSpan resta = DateTime.Now - _fechaNacimiento;

            if (resta.Days < 90)
                throw new Exception("No se puede vacunar, es menor a 3 meses");
            return true;
        }

        public abstract decimal CostoCrianza();

        public abstract string TipoRes();

        public abstract decimal GananciaPotencial();
       
        public override bool Equals(object? obj)
        {
            if(obj == null && !(obj is Res))
                return true;
            
            Res unaRes = (Res)obj;

            if (unaRes._numeroCaravana == _numeroCaravana)
                return true;
            return false;
        }
        public override string ToString()
        {
            return $"Número de caravana: {Id}\n" +
                   $"_Raza: {Raza}\n" +
                   $"_Peso actual: {PesoActual}\n" +
                   $"_Sexo: {Sexo}\n";
        }
    }
}
