namespace Estancia
{
    public class Ovino : Res
    {
        private double _pesoEstimadoLana;
        private static decimal s_precioPorKgLana = 50;
        private static decimal s_precioPorKgEnPie = 30;
        private static decimal s_costoAlimentacion = 100;

        public decimal PrecioKiloLana
        {
            get { return s_precioPorKgLana; }
            set { s_precioPorKgLana = value; }
        }

        public Ovino(double pesoEstimadoLana, string id, Sexo sexo, string raza, DateTime fechaNacimiento, decimal costoAdquisicion, double pesoActual, bool esHibrido, List<Inyeccion> vacunas) : base(id, sexo, raza, fechaNacimiento, costoAdquisicion, pesoActual, esHibrido, vacunas)
        {
            _pesoEstimadoLana = pesoEstimadoLana;
        }

        public override decimal CostoCrianza()
        {
            return s_costoAlimentacion + CostoAdquisicion;
        }
        public static decimal GetPrecioActualKgLana()
        {
            return s_precioPorKgLana;
        }
        public static void SetNewPrecioKgLana(decimal newPrecio)
        {
            s_precioPorKgLana = newPrecio;
        }

        public override string TipoRes()
        {
            return "Ovino";
        }

        public override decimal GananciaPotencial()
        {
            //Precio base
            decimal ganancia = ((decimal)_pesoEstimadoLana * s_precioPorKgLana) + (s_precioPorKgEnPie * (decimal)PesoActual);

            //Si es hibrido le resto el 5%
            if (EsHibrido)
                ganancia *= 0.95M;

            return ganancia;
        }
    }
}
