namespace Estancia
{
    public class Bovino : Res
    {
        private Alimento _tipoAlimento;
        private static decimal s_precioPorkgEnPie = 100;
        private static decimal s_costoAlimentacion = 200;

        public Alimento Alimento
        {
            get { return _tipoAlimento; }
        }

        public Bovino(Alimento alimento, string id, Sexo sexo, string raza, DateTime fechaNacimiento, decimal costoAdquisicion, double pesoActual, bool esHibrido, List<Inyeccion> vacunas) : base(id, sexo, raza, fechaNacimiento, costoAdquisicion, pesoActual, esHibrido, vacunas)
        {
            _tipoAlimento = alimento;
        }

        public Bovino() { }

        public override decimal CostoCrianza()
        {
            return s_costoAlimentacion + CostoAdquisicion;
        }

        public override string TipoRes()
        {
            return "Bovino";
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || !(obj is Bovino))
                return false;
            Bovino otroBovino = (Bovino)obj;

            return Id == otroBovino.Id;
        }

        public override decimal GananciaPotencial()
        {
            //Precio base
            decimal ganancia = (s_precioPorkgEnPie * (decimal)PesoActual);

            //Si se alimento a grano aumento 30%
            if (_tipoAlimento == Alimento.GRANO)
                ganancia *= 1.30M;

            //Si además es hembra aumento 10%
            if (Sexo == Sexo.FEMALE)
                ganancia *= 1.10M;

            return s_precioPorkgEnPie * (decimal)PesoActual;
        }
    }
}