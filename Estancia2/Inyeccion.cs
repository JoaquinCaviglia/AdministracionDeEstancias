namespace Estancia
{
    public class Inyeccion : IValidable
    {
        private Vacuna _tipoVacuna;
        private DateTime _fechaDeVacunacion;
        private DateTime _vencimiento; //se vence luego de un anio
        private bool _estaPrecargada;

        public bool EstaPrecargada
        {
            get { return _estaPrecargada; } 
            set { _estaPrecargada = value;}
        }
        public Inyeccion(Vacuna tipoVacuna, DateTime fechaDeVacunacion, DateTime vencimiento)
        {
            _tipoVacuna = tipoVacuna;
            _fechaDeVacunacion = fechaDeVacunacion;
            _vencimiento = vencimiento;
            _estaPrecargada = false;

        }

        public Inyeccion()
        {
            _tipoVacuna = new Vacuna();
            _fechaDeVacunacion = new DateTime();
            _vencimiento = new DateTime();
            _estaPrecargada = false;
        }

        // Revisar letra si necesitamos algún otro control
        public void Validar()
        {
            if (_tipoVacuna == null)
                throw new Exception("No existe vacuna");
        }
    }
}
