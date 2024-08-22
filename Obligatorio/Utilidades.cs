using System;

namespace Obligatorio
{
    public class Utilidades
    {
        static ConsoleColor s_userInput = ConsoleColor.Cyan;
        public static void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        public static void MostrarExito(string exito)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(exito);
            Console.ResetColor();
        }
        
        public static void MostrarMenu()
        {
           // Console.Clear();
            

            // Opciones del menú
            string[] opciones = { "Salir", "Listar animales", "Listar potreros disponibles", "Precio de Lana", "Alta de Bovino" };
            
                // Muestro opciones del menú
                MostrarTitulo("Menú");
                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.WriteLine($"{i}- {opciones[i]}");
                }            
        }

        internal static int AskInformation(string message)
        {

            bool exito;
            int hectareas;
            do
            {
                Console.WriteLine(message + ":");
                Console.ForegroundColor = s_userInput;
                exito = int.TryParse(Console.ReadLine(), out hectareas);
                Console.ResetColor();
            } while (!exito);
            return hectareas;
        }
        internal static int AskInformationTwoValues(string message)
        {

            bool exito;
            int value;
            do
            {
                Console.WriteLine(message);
                Console.ForegroundColor = s_userInput;
                exito = int.TryParse(Console.ReadLine(), out value);
                Console.ResetColor();
            } while (!exito || (value!=1 && value!=2));
            return value;
        }
        public static void MostrarTitulo(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"*** {title} ***\n");
            Console.ResetColor();
            //Console.WriteLine("Ingrese 0 para salir\n");
        }
        public static void PresionarCualquierTeclaParaBorrar()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPresione una tecla para continuar");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
    }
}   
