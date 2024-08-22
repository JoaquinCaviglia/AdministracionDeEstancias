namespace Estancia
{
    public class Capataz : Empleado
    {
        List<Peon> _personasACargo = new List<Peon>();

        public Capataz() { }
        public Capataz(List<Peon> personasACargo, string email, string contrasenia, string nombre, DateTime fechaIngreso) : base(email, contrasenia, nombre, fechaIngreso)
        {
            _personasACargo = personasACargo;
        }

        public override string Rol()
        {
            return "capataz";
        }
    }
}
