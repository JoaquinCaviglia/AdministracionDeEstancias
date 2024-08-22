using Estancia;
using System;

namespace Obligatorio
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Sistema miSistema = Sistema.Instance;
            bool sigue = true;

            Utilidades.MostrarTitulo("OBLIGATORIO 1");
            Console.WriteLine("Participantes:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("___ Joaquin Caviglia (328187) : Emmanuel Duffaut (177427)");
            Console.ResetColor();
            Console.WriteLine("Materia:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("___ Programación 2");
            Console.ResetColor();
            Console.WriteLine("Profesor: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("___ Mauricio Quintero\n");
            Utilidades.PresionarCualquierTeclaParaBorrar();
            do
            {
                
                
                bool exito;
                int opcionElegida;
                do
                {
                    Utilidades.MostrarMenu();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nElige una opción: ");
                    Console.ResetColor();
                    exito = int.TryParse(Console.ReadLine(), out opcionElegida);
                    if (!exito || opcionElegida < 0 || opcionElegida > 4)
                    {
                        Utilidades.MostrarError("\nDebes ingresar una opcion valida");
                    }
                } while (!exito);

                switch( opcionElegida)
                {
                    case 0:
                        sigue = false;
                        break;
                    case 1:
                        ListReses();
                        break;
                    case 2:
                        ListFreePotreros();
                        break;
                    case 3:
                        DefinirPrecioLana();
                        break;
                    case 4:
                        AltaBovino();
                        break;
                }
             } while (sigue);
        }

        private static void DefinirPrecioLana()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Utilidades.MostrarTitulo("Definir precio por kg de Lana");
            Console.ResetColor();
            // Muestro precio actual
            decimal precioKgLanaActual = Sistema.Instance.GetPrecioKgLana();
            Utilidades.MostrarExito($"El precio actual por kg de lana es {precioKgLanaActual}\n");

            // Pido nuevo precio
            bool exito;
            do
            {
                Console.WriteLine("Ingrese precio por kilo de lana:");
                exito = decimal.TryParse(Console.ReadLine(), out precioKgLanaActual);
            } while (!exito);

            // Seteo nuevo precio
            Sistema.Instance.SetPrecioLana(precioKgLanaActual);
            Console.Clear();
            Utilidades.MostrarTitulo("Definir precio por kg de Lana");
            Utilidades.MostrarExito($"El precio de lana se a cambiado correctamente, Valor: {precioKgLanaActual}\n");

            Utilidades.PresionarCualquierTeclaParaBorrar();
        }

        private static void ListFreePotreros()
        {
            Console.Clear();
            // Pido al usuario que ingrese cant de hectareas y un valor
            Utilidades.MostrarTitulo("Potreros disponibles");
            int hectareas = Utilidades.AskInformation("Ingrese cantidad de hectáreas");
            int valor = Utilidades.AskInformation("Ingrese un valor");
            Console.Clear();
            Utilidades.MostrarTitulo("Potreros disponibles");

            // Busco potreros disponibles.
            Sistema.Instance.PotrerosLibres(hectareas, valor);

            Utilidades.PresionarCualquierTeclaParaBorrar();
        }

        private static void ListReses()
        {
            Utilidades.MostrarTitulo("Reses");


            // Me traigo una copia de todas las reses
            List<Res> listaReses = Sistema.Instance.GetAnimales();
            listaReses.Sort();

            if(listaReses == null || listaReses.Count == 0)
            {
                Utilidades.MostrarError("No existen animales ingresados en el sistema");
            }
            else
            {
                foreach (Res _unaRes in listaReses)
                {
                    Console.WriteLine(_unaRes.ToString());
                }
            }
            Utilidades.PresionarCualquierTeclaParaBorrar();
        }

        private static void AltaBovino()
        {
            Utilidades.MostrarTitulo("Alta Bovino");
            ConsoleColor userInput = ConsoleColor.Cyan;

            string raza;
            do
            {
                Console.WriteLine("Ingrese la Raza del bovino: ");
                Console.ForegroundColor = userInput;
                raza = Console.ReadLine();
                Console.ResetColor();
            } while (raza == null);

            string idCaravana;
            do
            {
                Console.WriteLine("Ingrese número de caravana(8 caracteres): ");
                Console.ForegroundColor = userInput;
                idCaravana = Console.ReadLine();
                Console.ResetColor();
            } while (idCaravana == null);

            int peso = Utilidades.AskInformation("Ingrese el peso actual: ");
            
            int dia;
            int mes;
            int anio;
            //tomar fecha
            Console.WriteLine("Ingrese Fecha de Nacimiento del Bovino: ");
            
            do
            {
                dia = Utilidades.AskInformation("Ingrese el dia");
            } while (dia<=0 || dia > 31);

            do
            {
                mes = Utilidades.AskInformation("Ingrese el mes");
            }while(mes<=0 || mes >= 13);

            do
            {
                anio = Utilidades.AskInformation("Ingrese el año");
            } while (anio > DateTime.Now.Year);

            DateTime fechaNacimiento = new DateTime(anio, mes, dia);

            int numSexo = Utilidades.AskInformationTwoValues("Que sexo es?(1-Macho 2-Hembra): ");
            int numAlimento = Utilidades.AskInformationTwoValues("Cual es su alimentación? (1-Grano 2-Pastura) : ");
            int costoAdquisicion = Utilidades.AskInformation("Cual es el costo de adquisición?");
            int hibrido = Utilidades.AskInformationTwoValues("Es hibrido? (1-SI 2-NO) : ");
            bool esHibrido = hibrido == 1 ? true : false;
            List<Inyeccion> inyeccions = new List<Inyeccion>();

            Bovino nuevoBovino = new Bovino((Alimento)numAlimento, idCaravana, (Sexo)numSexo, raza, fechaNacimiento, costoAdquisicion, peso, esHibrido, inyeccions);

            try
            {
                Sistema.Instance.AltaBovino(nuevoBovino);
                Console.Clear();
                Utilidades.MostrarExito($"El bovino se ingresó correctamente\n");
                Console.WriteLine(nuevoBovino.ToString());
            }
            catch (Exception ex)
            {
                Utilidades.MostrarError(ex.Message);
            }

            Utilidades.PresionarCualquierTeclaParaBorrar();
        }
    }
}
