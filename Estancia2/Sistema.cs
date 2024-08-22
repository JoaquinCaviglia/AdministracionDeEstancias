using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Estancia
{
    public class Sistema
    {
        #region singleton
        // Atributo privado
        private static Sistema s_instance;

        // Listas
        private List<Empleado> _empleados;
        private List<Potrero> _potreros;
        private List<Res> _reses;
        private List<Vacuna> _vacunas;
        private List<Inyeccion> _inyeccionesToPreLoad;
        private List<Tarea> _tareas;
        private List<Peon> _registrados;
        // Propiedad
        public static Sistema Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new Sistema();
                return s_instance;
            }
        }
        #endregion

        // Constructor
        private Sistema()
        {
            _empleados = new List<Empleado>();
            _potreros = new List<Potrero>();
            _reses = new List<Res>();
            _vacunas = new List<Vacuna>();
            _tareas = new List<Tarea>();
            PrecargarDatos();
        }

        private void PrecargarDatos()
        {
            PreLoadVaccine();
            PreLoadTask();
            PreLoadPeon();
            PreLoadTaskToPeon();
            PreLoadCapataz();
            PreLoadOvino();
            PreLoadBovino();
            PreLoadPotrero();
        }


        private void PreLoadTaskToPeon()
        {
            foreach (Empleado unEmpleado in _empleados)
                if (unEmpleado is Peon peon)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Tarea tareaRandom = RandomTaskToPeon();
                        DateTime fechaAleatoria = FechaRandom();
                        peon.AsignarTarea(tareaRandom, fechaAleatoria);
                    }
                }
        }

        private DateTime FechaRandom()
        {
            DateTime fechaAleatoria;
            Random random = new Random();
            //Creo fechas aleatorias, excepción para el mes 2(febrero).
            int mes = random.Next(1, 13);
            if (mes == 2)
            {
                fechaAleatoria = new DateTime(2024, mes, random.Next(1, 29));
            }
            else
            {
                fechaAleatoria = new DateTime(2024, mes, random.Next(1, 31));
            }
            return fechaAleatoria;

        }

        private List<Inyeccion> LoadInjectionTo()
        {
            List<Inyeccion> injectionTo = new List<Inyeccion>();
            List<Inyeccion> injection = new List<Inyeccion>();
            Random random = new Random();
            DateTime fechaAleatoria;

            for (int i = 0; i < 8; i++)
            {

                fechaAleatoria = new DateTime(DateTime.Now.Year, random.Next(1, 13), random.Next(1, 29));
                Vacuna vacunaGenerada = _vacunas[random.Next(0, _vacunas.Count)];
                Inyeccion inyeccion = new Inyeccion(vacunaGenerada, fechaAleatoria, fechaAleatoria.AddYears(1));
                ValidarInyeccion(inyeccion);
                injection.Add(inyeccion);
            }

            for (int i = 0; i < random.Next(0, 9); i++)
            {
                injectionTo.Add(injection[i]);
            }
            return injectionTo;
        }

        private void ValidarInyeccion(Inyeccion inyeccion)
        {
            inyeccion.Validar();
        }
        private void PreLoadCapataz()
        {
            InstanceCapataz(new Capataz(LoadPeonACargo(), "jose.perez@estancia.com", "CapEstancia123", "Jose Perez", new DateTime(2015, 01, 01)));
            InstanceCapataz(new Capataz(LoadPeonACargo(), "juan.rodriguez@estancia.com", "CapEstancia456", "Juan Rodriguez", new DateTime(2016, 06, 01)));
        }
        private void InstanceCapataz(Capataz capataz)
        {
            ValidarEmpleado(capataz);
            _empleados.Add(capataz);
        }
        private void PreLoadOvino()
        {
            // Creo fechas randoms en una lista _fechas para utilizar en la precarga
            List<DateTime> _fechas = new List<DateTime>();
            DateTime fechaNacimiento;
            Random random = new Random();
            for (int i = 0; i < 30; i++)
            {
                fechaNacimiento = new DateTime(2024, random.Next(1, 12), random.Next(1, 28));
                if (fechaNacimiento.Month > DateTime.Now.Month)
                    i--;
                else
                {
                    _fechas.Add(fechaNacimiento);
                }
            }

            InstanceRes(new Ovino(30, "7654321A", Sexo.MALE, "Corriedale", _fechas[0], 50, 80, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.5, "A2345678", Sexo.FEMALE, "Merino", _fechas[1], 150, 45, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "B9876543", Sexo.MALE, "Corriedale", _fechas[2], 220, 62, true, LoadInjectionTo()));
            InstanceRes(new Ovino(1.8, "C1234567", Sexo.FEMALE, "Hampshire Down", _fechas[3], 120, 38, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.7, "D8765432", Sexo.MALE, "Suffolk", _fechas[4], 180, 55, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.4, "E2345678", Sexo.FEMALE, "Texel", _fechas[5], 165, 42, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.1, "F9876543", Sexo.MALE, "Île-de-France", _fechas[6], 210, 60, true, LoadInjectionTo()));
            InstanceRes(new Ovino(1.9, "G1234567", Sexo.FEMALE, "Romney Marsh", _fechas[7], 130, 36, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.8, "H8765432", Sexo.MALE, "Lincoln", _fechas[8], 195, 58, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.5, "I2345678", Sexo.FEMALE, "Border Leicester", _fechas[9], 170, 44, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "J9876543", Sexo.MALE, "Cheviot", _fechas[10], 230, 65, true, LoadInjectionTo()));
            InstanceRes(new Ovino(1.8, "K1234567", Sexo.FEMALE, "Bluefaced Leicester", _fechas[11], 125, 35, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.7, "L8765432", Sexo.MALE, "Swaledale", _fechas[12], 185, 57, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.4, "M2345678", Sexo.FEMALE, "Dalesbred", _fechas[13], 160, 43, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.1, "N9876543", Sexo.MALE, "Exmoor Horn", _fechas[14], 215, 61, true, LoadInjectionTo()));
            InstanceRes(new Ovino(1.9, "O1234567", Sexo.FEMALE, "Scottish Blackface", _fechas[15], 135, 37, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.8, "P8765432", Sexo.MALE, "Clun Forest", _fechas[16], 190, 59, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.5, "Q2345678", Sexo.FEMALE, "Soay", _fechas[17], 175, 45, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "R9876543", Sexo.MALE, "Hebridean", _fechas[18], 235, 66, true, LoadInjectionTo()));
            InstanceRes(new Ovino(1.8, "S1234567", Sexo.FEMALE, "Shetland", _fechas[19], 120, 34, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.7, "T8765432", Sexo.MALE, "Arapawa", _fechas[20], 180, 56, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.4, "U2345678", Sexo.FEMALE, "Columbia", _fechas[21], 165, 43, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.1, "V9876543", Sexo.MALE, "Rambouillet", _fechas[22], 220, 64, true, LoadInjectionTo()));
            InstanceRes(new Ovino(1.9, "W1234567", Sexo.FEMALE, "Merino Argentino", _fechas[23], 130, 36, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.8, "X8765432", Sexo.MALE, "Corriedale Australiano", _fechas[24], 195, 58, true, LoadInjectionTo()));
            InstanceRes(new Ovino(2.5, "Y2345678", Sexo.FEMALE, "Romney Marsh Neozelandés", _fechas[25], 170, 45, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "Z9876543", Sexo.MALE, "Tex", _fechas[26], 175, 40, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "Z9876544", Sexo.MALE, "Merilin", _fechas[27], 155, 40, false, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "Z9876545", Sexo.FEMALE, "Merino Australiano", _fechas[28], 145, 40, true, LoadInjectionTo()));
            InstanceRes(new Ovino(3.2, "Z9876546", Sexo.MALE, "Romney Marsh", _fechas[29], 145, 40, false, LoadInjectionTo()));

        }
        public void InstanceRes(Res unRes)
        {
            //Controlo que no exista otro animal con misma caravana (id)
            if (!_reses.Contains(unRes))
            {
                ValidarRes(unRes);
                _reses.Add(unRes);
            }
            else
            {
                throw new Exception("La caravana ingresada ya existe en el sistema");
            }

        }
        private void ValidarRes(Res unRes)
        {

            unRes.Validar();
        }

        private void PreLoadBovino()
        {
            // Creo fechas randoms en una lista _fechas para utilizar en la precarga
            List<DateTime> _fechas = new List<DateTime>();
            DateTime fechaNacimiento;
            Random random = new Random();
            for (int i = 0; i < 30; i++)
            {
                fechaNacimiento = new DateTime(2024, random.Next(1, 12), random.Next(1, 28));
                if(fechaNacimiento.Month > DateTime.Now.Month)
                    i--;
                else
                {
                    _fechas.Add(fechaNacimiento);
                }
            }

            InstanceRes(new Bovino(Alimento.GRANO, "1234567A", Sexo.FEMALE, "Angus", _fechas[0], 1500, 150, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123B", Sexo.FEMALE, "Hereford", _fechas[1], 1200, 180, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "4567890C", Sexo.MALE, "Holstein", _fechas[2], 1800, 120, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "1234567D", Sexo.FEMALE, "Shorthorn", _fechas[3], 1000, 100, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123A", Sexo.MALE, "Limousin", _fechas[4], 1600, 130, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "7890123E", Sexo.MALE, "Limousin", _fechas[5], 1600, 130, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "4567890F", Sexo.FEMALE, "Charolais", _fechas[6], 1300, 160, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "1234567G", Sexo.MALE, "Gelbvieh", _fechas[7], 1700, 180, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123H", Sexo.FEMALE, "Maine", _fechas[8], 1100, 120, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "4567890I", Sexo.MALE, "South Devon", _fechas[9], 1550, 120, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "1234567J", Sexo.FEMALE, "Red Angus", _fechas[10], 1250, 150, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123K", Sexo.MALE, "Wagyu", _fechas[11], 2000, 155, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "4567890L", Sexo.FEMALE, "Belted Galloway", _fechas[12], 1150, 135, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "1234567M", Sexo.MALE, "Texas Longhorn", _fechas[13], 1650, 170, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123J", Sexo.FEMALE, "Brahman", _fechas[14], 1350, 170, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "4567890O", Sexo.MALE, "Murray Grey", _fechas[15], 1850, 130, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "1234567P", Sexo.FEMALE, "Highland", _fechas[16], 1200, 140, false, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "7890123Q", Sexo.MALE, "Chianina", _fechas[17], 1700, 190, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "4567890R", Sexo.FEMALE, "Simmental", _fechas[18], 1400, 190, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "1234567S", Sexo.FEMALE, "Sussex", _fechas[19], 1900, 190, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "7890123T", Sexo.MALE, "Devon", _fechas[20], 1050, 190, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "4567890U", Sexo.FEMALE, "Galloway", _fechas[21], 1500, 165, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "1234567V", Sexo.MALE, "Brangus", _fechas[22], 1200, 195, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "7890123W", Sexo.FEMALE, "Beefmaster", _fechas[23], 1300, 185, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123X", Sexo.MALE, "Angus", _fechas[24], 1300, 140, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123Y", Sexo.FEMALE, "Aberdeen angus", _fechas[25], 1300, 190, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "7890123Z", Sexo.MALE, "Charolais", _fechas[26], 1300, 1550, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123M", Sexo.FEMALE, "Normando", _fechas[27], 1300, 166, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.GRANO, "7890133J", Sexo.MALE, "Holando", _fechas[28], 1300, 180, true, LoadInjectionTo()));
            InstanceRes(new Bovino(Alimento.PASTURA, "7890123L", Sexo.FEMALE, "Jersey", _fechas[29], 1300, 185, true, LoadInjectionTo()));

        }

        private List<Peon> LoadPeonACargo()
        {
            List<Peon> _peonesToCapataz = new List<Peon>();
            for (int i = 0; i < 10; i++)
                if (_empleados[i].GetType() == typeof(Peon))
                {
                    Peon unPeon = (Peon)_empleados[i];
                    if (!(unPeon.TieneCapataz) && _peonesToCapataz.Count < 5)
                    {
                        _peonesToCapataz.Add(unPeon);
                        unPeon.TieneCapataz = true;
                    }
                }
            return _peonesToCapataz;
        }

        private void PreLoadPeon()
        {
            InstanceEmpleados(new Peon(true, false, "john.smith@estancia.com", "password123", "John Smith", new DateTime(2020, 10, 15)));
            InstanceEmpleados(new Peon(true, false, "jane.doe@estancia.com", "password456", "Jane Doe", new DateTime(2019, 12, 01)));
            InstanceEmpleados(new Peon(true, false, "peter.jones@estancia.com", "password789", "Peter Jones", new DateTime(2024, 01, 04)));
            InstanceEmpleados(new Peon(true, false, "mary.williams@estancia.com", "password012", "Mary Williams", new DateTime(2023, 03, 22)));
            InstanceEmpleados(new Peon(true, false, "david.miller@estancia.com", "password345", "David Miller", new DateTime(2022, 05, 18)));
            InstanceEmpleados(new Peon(true, false, "sarah.taylor@estancia.com", "password678", "Sarah Taylor", new DateTime(2024, 02, 11)));
            InstanceEmpleados(new Peon(true, false, "michael.brown@estancia.com", "password901", "Michael Brown", new DateTime(2023, 04, 07)));
            InstanceEmpleados(new Peon(true, false, "laura.davis@estancia.com", "password234", "Laura Davis", new DateTime(2022, 06, 24)));
            InstanceEmpleados(new Peon(true, false, "richard.thomas@estancia.com", "password567", "Richard Thomas", new DateTime(2018, 03, 09)));
            InstanceEmpleados(new Peon(true, false, "elizabeth.anderson@estancia.com", "password890", "Elizabeth Anderson", new DateTime(2021, 05, 13)));
        }

        public void InstanceEmpleados(Empleado unEmpleado)
        {
            if (_empleados.Contains(unEmpleado))
            {
                throw new Exception("El empleado ya se encuentra registrado en el sistema");
            }
            else
            {
                ValidarEmpleado(unEmpleado);
                _empleados.Add(unEmpleado);

            }

        }
        private void ValidarEmpleado(Empleado empleado)
        {
            empleado.Validar();
        }

        private Tarea RandomTaskToPeon()
        {
            Random random = new Random();
            Tarea tarea = _tareas[random.Next(0, 14)];
            return tarea;
        }

        private void PreLoadTask()
        {
            InstanceTask(new Tarea("Inspeccionar el sistema de alarma contra incendios en el edificio principal."));
            InstanceTask(new Tarea("Verificar el funcionamiento de las luces de emergencia en todas las salidas."));
            InstanceTask(new Tarea("Realizar pruebas de fugas de gas en todas las áreas de almacenamiento. "));
            InstanceTask(new Tarea("Calibrar los manómetros de presión en las calderas"));
            InstanceTask(new Tarea("Inspeccionar el estado de las barandas y pasamanos en las escaleras"));
            InstanceTask(new Tarea("Revisar los extintores de incendios para garantizar su funcionamiento adecuado."));
            InstanceTask(new Tarea("Verificar que las señales de seguridad y emergencia estén visibles y en buen estado."));
            InstanceTask(new Tarea("Realizar una inspección general de las instalaciones en busca de posibles peligros."));
            InstanceTask(new Tarea("Documentar los hallazgos de las inspecciones y tomar las medidas correctivas necesarias."));
            InstanceTask(new Tarea("Capacitar al personal sobre los procedimientos de seguridad en caso de emergencias."));
            InstanceTask(new Tarea("Realizar simulacros de incendio y evacuación para evaluar la preparación del personal."));
            InstanceTask(new Tarea("Mantener registros actualizados de las tareas de mantenimiento y seguridad realizadas."));
            InstanceTask(new Tarea("Comunicar los riesgos de seguridad y los procedimientos de emergencia a todo el personal."));
            InstanceTask(new Tarea("Investigar los incidentes de seguridad y tomar medidas para prevenir su recurrencia."));
            InstanceTask(new Tarea("Implementar programas de mejora continua para garantizar la seguridad en el lugar de trabajo."));
        }
        public void InstanceTask(Tarea unaTarea)
        {
            ValidarTarea(unaTarea);
            _tareas.Add(unaTarea);
        }
        private void ValidarTarea(Tarea tarea)
        {
            tarea.Validar();
        }


        private void PreLoadPotrero()
        {
            Random random = new Random();

            InstancePotrero(new Potrero("Potrero 1", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 2", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 3", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 4", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 5", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 6", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 7", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 8", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 9", random.Next(1, 10)));
            InstancePotrero(new Potrero("Potrero 10", random.Next(1, 10)));

            AsignarAnimalesEnPoteros();
        }
        private void AsignarAnimalesEnPoteros()
        {
            // Armo lista de animales libres
            List<Res> freeAnimals = new List<Res>();
            foreach (Res unaRes in _reses)
            {
                if (unaRes.Libre)
                    freeAnimals.Add(unaRes);
            }

            // Asigno animales libres a cada potrero
            foreach (Potrero unPotrero in _potreros)
            {
                unPotrero.AssignAnimals(freeAnimals);
            }
        }
        private void InstancePotrero(Potrero potrero)
        {
            ValidarPotrero(potrero);
            potrero.AssignAnimalCapacity();
            _potreros.Add(potrero);
        }

        private void ValidarPotrero(Potrero potrero)
        {
            potrero.Validar();
        }

        private void PreLoadVaccine()
        {
            InstanceVaccine(new Vacuna("Fiebre Aftosa", "Enfermedad viral altamente contagiosa que afecta a bovinos, ovinos y porcinos. Se caracteriza por fiebre, vesículas en la boca y patas, y dificultad para caminar.", "Virus de la Fiebre Aftosa"));
            InstanceVaccine(new Vacuna("Brucelosis", "Enfermedad bacteriana que afecta el sistema reproductivo de bovinos y ovinos. Provoca abortos, infertilidad y nacimiento de terneros débiles.", "Bacteria Brucella abortus"));
            InstanceVaccine(new Vacuna("Carbón", "Enfermedad bacteriana grave que afecta a bovinos, ovinos y otros animales. Se caracteriza por muerte súbita, hinchazón y sangre en la nariz y boca.", "Bacteria Clostridium chauvoei"));
            InstanceVaccine(new Vacuna("Manquera", "Enfermedad viral que afecta las patas de bovinos y ovinos. Provoca cojera, úlceras en las patas y disminución de la producción de leche.", "Virus de la Manquera"));
            InstanceVaccine(new Vacuna("Leptospirosis", "Enfermedad bacteriana que afecta a bovinos y ovinos. Provoca abortos, infertilidad, problemas renales y hepáticos.", "Bacteria Leptospira spp"));
            InstanceVaccine(new Vacuna("Rabia", "Enfermedad viral mortal que afecta a todos los mamíferos, incluyendo bovinos y ovinos. Se caracteriza por síntomas neurológicos como agresividad, parálisis y muerte.", "Virus de la Rabia"));
            InstanceVaccine(new Vacuna("Diarrea Viral Bovina", "Enfermedad viral que afecta a bovinos de todas las edades. Provoca diarrea, neumonía, y disminución de la producción de leche.", "Virus de la Diarrea Viral Bovina"));
            InstanceVaccine(new Vacuna("Parainfluenza Bovina", "Enfermedad viral que afecta a bovinos de todas las edades. Provoca problemas respiratorios, especialmente en terneros", "Virus Parainfluenza Bovino tipo 3"));
            InstanceVaccine(new Vacuna("Rotavirus", "Enfermedad viral que afecta a terneros jóvenes. Provoca diarrea severa, deshidratación y muerte en algunos casos.", "Rotavirus bovino"));
            InstanceVaccine(new Vacuna("Rinotraqueítis Infecciosa Bovina", "Enfermedad viral que afecta a bovinos de todas las edades. Provoca problemas respiratorios, abortos y disminución de la producción de leche.", "Virus de la Rinotraqueítis Infecciosa Bovina"));
        }

        private void InstanceVaccine(Vacuna vaccine)
        {
            ValidateVaccine(vaccine);
            _vacunas.Add(vaccine);
        }

        private void ValidateVaccine(Vacuna oneVaccine)
        {
            oneVaccine.Validar();
        }

        public List<Res> GetAnimales()
        {
            List<Res> copiaAnimales = new List<Res>();
            foreach (Res unaRes in _reses)
                copiaAnimales.Add(unaRes);
            return copiaAnimales;
        }

        public List<Potrero> GetPotreros()
        {
            List<Potrero> copiaPotreros = new List<Potrero>();
            foreach (Potrero unP in _potreros)
                copiaPotreros.Add(unP);
            return copiaPotreros;
        }

        public Potrero BuscarPotrero(int id)
        {
            Potrero potrero = null;
            int i = 0;
            while (potrero == null || i < _potreros.Count)
            {
                if (_potreros[i].Id == id)
                {
                    potrero = _potreros[i];
                }
                i++;
            }
            if (potrero == null)
                throw new Exception("Potrero no encontrado");
            return potrero;
        }
        public List<Vacuna> GetVacunas()
        {
            List<Vacuna> copiaVacunas = new List<Vacuna>();
            foreach (Vacuna unaV in _vacunas)
            {
                copiaVacunas.Add(unaV);
            }
            return copiaVacunas;
        }

        public void PotrerosLibres(int hectareas, int valor)
        {
            int cont = 0;
            foreach (Potrero unPotrero in _potreros)
            {
                if (unPotrero.AreaMayorACantHectareasYCapacidadMaxSuperiorNroDado(hectareas, valor))
                {
                    Console.WriteLine(unPotrero);
                    cont++;
                }
            }
            if (cont == 0)
                Console.WriteLine("No existen potreros disponibles para los valores ingresados.");
        }

        public void SetPrecioLana(decimal precioLana)
        {
            Ovino.SetNewPrecioKgLana(precioLana);
        }

        public void AltaBovino(Bovino nuevoBovino)
        {
            ValidarRes(nuevoBovino);

            // Contain para ver si ya no existe
            if (_reses.Contains(nuevoBovino))
                throw new Exception("El bovino ya existe en el sistema.");
            _reses.Add(nuevoBovino);
        }

        public decimal GetPrecioKgLana()
        {
            decimal precioKgLana = 0;
            precioKgLana = Ovino.GetPrecioActualKgLana();
            return precioKgLana;
        }

        public Empleado AutenticarUsuario(string email, string password)
        {
            Empleado usuario = null;
            int i = 0;
            while (usuario == null && i < _empleados.Count)
            {
                if (_empleados[i].Email == email)
                {
                    usuario = _empleados[i];
                }
                i++;
            }
            if (usuario == null || usuario.Contrasenia != password)
                throw new Exception("Usuario o Contraseña incorrectas");

            return usuario;
        }

        public Peon BuscarPeon(string email)
        {
            Peon usuario = null;
            int i = 0;
            while (usuario == null && i < _empleados.Count)
            {
                if (_empleados[i].Email == email && _empleados[i] is Peon)
                {
                    usuario = (Peon)_empleados[i];
                }
                i++;
            }

            if (usuario == null)
                throw new Exception("Peón no encontrado");

            return usuario;
        }

        public Tarea BuscarTarea(int id)
        {
            Tarea tarea = null;
            int i = 0;
            while (tarea == null && i < _tareas.Count)
            {
                if (_tareas[i].Id == id)
                {
                    tarea = _tareas[i];
                }
                i++;
            }

            if (tarea == null)
                throw new Exception("Tarea no encontrada");

            return tarea;
        }

        public Res GetUnAnimal(string id)
        {
            Res buscado = null;
            int i = 0;
            while (buscado == null && i < _reses.Count)
            {
                if (_reses[i].Id == id)
                {
                    buscado = _reses[i];
                }
                i++;
            }
            if (buscado == null)
                throw new Exception("Animal no encontrado");
            return buscado;
        }

        public List<Peon> GetPeones()
        {
            List<Peon> copiaPeones = new List<Peon>();
            foreach (Empleado unE in _empleados)
            {
                if (unE is Peon)
                    copiaPeones.Add((Peon)unE);
            }
            if (copiaPeones.Count == 0)
                throw new Exception("No hay peones disponibles");
            return copiaPeones;
        }

        public List<Tarea> GetTareas()
        {
            List<Tarea> copiaTareas = new List<Tarea>();
            foreach (Tarea unE in _tareas)
            {
                copiaTareas.Add(unE);
            }
            if (copiaTareas.Count == 0)
                throw new Exception("No hay tareas creadas");
            return copiaTareas;
        }

        public List<Res> GetResesPorTipo(string tipo)
        {
            List<Res> listado = new List<Res>();

            if (tipo == "Ovino")
            {
                foreach (Res unaRes in _reses)
                    if (unaRes is Ovino)
                        listado.Add(unaRes);
            }
            else if (tipo == "Bovino")
            {
                foreach (Res unaRes in _reses)
                    if (unaRes is Bovino)
                        listado.Add(unaRes);
            }

            return listado;
        }

        public Vacuna GetUnaVacuna(string nombreVacuna)
        {
            Vacuna vacuna = null;
            int i = 0;
            while (vacuna == null && i < _vacunas.Count)
            {
                if (_vacunas[i].Nombre == nombreVacuna)
                {
                    vacuna = _vacunas[i];
                }
                i++;
            }

            if (vacuna == null)
                throw new Exception("No se encontró la vacuna");
            return vacuna;
        }
    }
}
