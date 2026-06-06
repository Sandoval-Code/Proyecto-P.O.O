using System;
using System.Collections.Generic;
using System.Linq;
using TurismoSostenible.Models;
using TurismoSostenible.Servicios;

namespace TurismoSostenible
{
    class Program
    {
        private static SistemaTurismo sistema;

        static void Main(string[] args)
        {
            sistema = new SistemaTurismo();
            InicializarDatos();

            bool salir = false;
            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine().Trim();

                switch (opcion)
                {
                    case "1":
                        GestionarDestinos();
                        break;
                    case "2":
                        GestionarVisitantes();
                        break;
                    case "3":
                        CrearExperiencias();
                        break;
                    case "4":
                        RealizarReserva();
                        break;
                    case "5":
                        MostrarReportes();
                        break;
                    case "6":
                        Console.WriteLine("\n¡Gracias por usar el Sistema de Turismo Sostenible!");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }

        static void MostrarMenuPrincipal()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("SISTEMA DE TURISMO SOSTENIBLE".PadLeft(45));
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("1. Gestionar Destinos");
            Console.WriteLine("2. Gestionar Visitantes");
            Console.WriteLine("3. Crear Experiencias");
            Console.WriteLine("4. Realizar Reserva");
            Console.WriteLine("5. Ver Reportes");
            Console.WriteLine("6. Salir");
            Console.WriteLine(new string('=', 60));
            Console.Write("Seleccione una opción: ");
        }

        static void InicializarDatos()
        {
            // Crear comunidades
            var comunidad1 = new Comunidad("COM001", "León - Poneloya", 
                "Habitantes dedicados a turismo comunitario", 150, 45);
            var comunidad2 = new Comunidad("COM002", "Centro de la Ciudad Universitaria", 
                "Preservación de tradiciones culturales", 200, 60);

            sistema.RegistrarComunidad(comunidad1);
            sistema.RegistrarComunidad(comunidad2);

            // Crear destinos
            var destino1 = new Destino("D001", "Playas de Poneloya y Las Peñitas", 
                "Destino natural en zona rural", 100, comunidad1);
            var destino2 = new Destino("D002", "Centro de Arte Ortíz Gurdián", 
                "Experiencia cultural auténtica", 80, comunidad2);

            sistema.RegistrarDestino(destino1);
            sistema.RegistrarDestino(destino2);

            // Crear experiencias
            var exp1 = new Experiencia("EXP001", "Senderismo Sostenible", 
                "Recorrido guiado por naturalista", "Naturaleza", 50, destino1, 0.15);
            var exp2 = new Experiencia("EXP002", "Taller de Artesanía Local", 
                "Aprenda técnicas ancestrales", "Comunitaria", 40, destino2, 0.20);

            sistema.RegistrarExperiencia(exp1);
            sistema.RegistrarExperiencia(exp2);

            Console.WriteLine("\n✓ Datos de ejemplo cargados correctamente");
        }

        static void GestionarDestinos()
        {
            bool volver = false;
            while (!volver)
            {
                Console.WriteLine("\n--- GESTIÓN DE DESTINOS ---");
                Console.WriteLine("1. Ver destinos");
                Console.WriteLine("2. Agregar nuevo destino");
                Console.WriteLine("3. Consultar capacidad");
                Console.WriteLine("4. Volver");
                Console.Write("Seleccione: ");
                string opcion = Console.ReadLine().Trim();

                switch (opcion)
                {
                    case "1":
                        {
                            var destinos = sistema.ObtenerDestinos();
                            if (destinos.Count > 0)
                            {
                                foreach (var d in destinos)
                                {
                                    Console.WriteLine($"\nID: {d.IdDestino}");
                                    Console.WriteLine($"Nombre: {d.Nombre}");
                                    Console.WriteLine($"Descripción: {d.Descripcion}");
                                    Console.WriteLine($"Capacidad diaria: {d.CapacidadDiaria}");
                                    Console.WriteLine($"Comunidad: {d.Comunidad.Nombre}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay destinos registrados");
                            }
                        }
                        break;

                    case "2":
                        {
                            Console.Write("ID del destino: ");
                            string idDest = Console.ReadLine();
                            Console.Write("Nombre: ");
                            string nombre = Console.ReadLine();
                            Console.Write("Descripción: ");
                            string desc = Console.ReadLine();
                            Console.Write("Capacidad diaria: ");
                            int cap = int.Parse(Console.ReadLine());

                            Console.WriteLine("Comunidades disponibles:");
                            foreach (var c in sistema.ObtenerComunidades())
                            {
                                Console.WriteLine($"- {c.IdComunidad}: {c.Nombre}");
                            }
                            Console.Write("ID de comunidad: ");
                            string idCom = Console.ReadLine();

                            var comunidad = sistema.ObtenerComunidad(idCom);
                            if (comunidad != null)
                            {
                                var destino = new Destino(idDest, nombre, desc, cap, comunidad);
                                sistema.RegistrarDestino(destino);
                                Console.WriteLine("✓ Destino registrado");
                            }
                        }
                        break;

                    case "3":
                        {
                            Console.Write("ID del destino a consultar: ");
                            string id = Console.ReadLine();
                            var destino = sistema.ObtenerDestino(id);
                            if (destino != null)
                            {
                                Console.WriteLine($"Destino: {destino.Nombre}");
                                Console.WriteLine($"Capacidad: {destino.CapacidadDiaria}");
                            }
                            else
                            {
                                Console.WriteLine("Destino no encontrado");
                            }
                        }
                        break;

                    case "4":
                        volver = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }

        static void GestionarVisitantes()
        {
            Console.WriteLine("\n--- GESTIÓN DE VISITANTES ---");
            Console.WriteLine("1. Registrar visitante");
            Console.WriteLine("2. Ver visitantes");
            Console.Write("Seleccione: ");
            string opcion = Console.ReadLine().Trim();

            if (opcion == "1")
            {
                Console.Write("ID visitante: ");
                string idVis = Console.ReadLine();
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("País de origen: ");
                string origen = Console.ReadLine();

                var visitante = new Visitante(idVis, nombre, origen);
                sistema.RegistrarVisitante(visitante);
                Console.WriteLine("✓ Visitante registrado");
            }
            else if (opcion == "2")
            {
                var visitantes = sistema.ObtenerVisitantes();
                if (visitantes.Count > 0)
                {
                    foreach (var v in visitantes)
                    {
                        Console.WriteLine($"\n- {v.Nombre} ({v.IdVisitante}) - Origen: {v.PaisOrigen}");
                    }
                }
                else
                {
                    Console.WriteLine("No hay visitantes registrados");
                }
            }
        }

        static void CrearExperiencias()
        {
            Console.WriteLine("\n--- CREAR EXPERIENCIAS ---");
            var destinos = sistema.ObtenerDestinos();
            if (destinos.Count == 0)
            {
                Console.WriteLine("No hay destinos disponibles");
                return;
            }

            Console.WriteLine("Destinos disponibles:");
            foreach (var d in destinos)
            {
                Console.WriteLine($"- {d.IdDestino}: {d.Nombre}");
            }

            Console.Write("ID experiencia: ");
            string idExp = Console.ReadLine();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            string desc = Console.ReadLine();
            Console.WriteLine("Tipos: Naturaleza, Cultural, Aventura, Educativa, Comunitaria");
            Console.Write("Tipo: ");
            string tipo = Console.ReadLine();
            Console.Write("Precio: ");
            double precio = double.Parse(Console.ReadLine());
            Console.Write("ID destino: ");
            string idDest = Console.ReadLine();
            Console.Write("Porcentaje para comunidad (ej: 0.20): ");
            double impacto = double.Parse(Console.ReadLine());

            var destino = sistema.ObtenerDestino(idDest);
            if (destino != null)
            {
                var exp = new Experiencia(idExp, nombre, desc, tipo, precio, destino, impacto);
                sistema.RegistrarExperiencia(exp);
                Console.WriteLine("✓ Experiencia registrada");
            }
        }

        static void RealizarReserva()
        {
            Console.WriteLine("\n--- REALIZAR RESERVA ---");

            var visitantes = sistema.ObtenerVisitantes();
            var experiencias = sistema.ObtenerExperiencias();

            if (visitantes.Count == 0 || experiencias.Count == 0)
            {
                Console.WriteLine("Se requieren visitantes y experiencias registradas");
                return;
            }

            Console.WriteLine("Visitantes:");
            foreach (var v in visitantes)
            {
                Console.WriteLine($"- {v.IdVisitante}: {v.Nombre}");
            }
            Console.Write("ID visitante: ");
            string idVis = Console.ReadLine();

            Console.WriteLine("\nExperiencias:");
            foreach (var e in experiencias)
            {
                Console.WriteLine($"- {e.IdExperiencia}: {e.Nombre} (${e.Precio})");
            }
            Console.Write("ID experiencia: ");
            string idExp = Console.ReadLine();

            Console.Write("Fecha (YYYY-MM-DD): ");
            string fecha = Console.ReadLine();
            Console.Write("Número de personas: ");
            int numPersonas = int.Parse(Console.ReadLine());

            var reserva = sistema.CrearReserva(idVis, idExp, fecha, numPersonas);
            if (reserva != null)
            {
                Console.WriteLine($"✓ Reserva creada: {reserva.IdReserva}");
                Console.WriteLine($"Total: ${reserva.MontoTotal}");
                Console.WriteLine($"Aporte a comunidad: ${reserva.MontoComunitario}");
            }
        }

        static void MostrarReportes()
        {
            Console.WriteLine("\n--- REPORTES ---");

            Console.WriteLine("\n1. Resumen de Sostenibilidad");
            var resumen = sistema.GenerarResumenSostenibilidad();
            Console.WriteLine($"Total visitantes: {resumen["total_visitantes"]}");
            Console.WriteLine($"Ingresos totales: ${resumen["ingresos_totales"]}");
            Console.WriteLine($"Aporte a comunidades: ${resumen["aporte_comunitario"]}");
            Console.WriteLine($"Destinos activos: {resumen["destinos_activos"]}");
            Console.WriteLine($"Porcentaje aporte: {resumen["porcentaje_aporte"]}%");

            Console.WriteLine("\n2. Impacto por Comunidad");
            foreach (var comunidad in sistema.ObtenerComunidades())
            {
                var impacto = sistema.CalcularImpactoComunidad(comunidad.IdComunidad);
                Console.WriteLine($"\n{impacto["comunidad"]}:");
                Console.WriteLine($"  - Ingresos generados: ${impacto["ingresos"]}");
                Console.WriteLine($"  - Visitantes: {impacto["visitantes"]}");
                Console.WriteLine($"  - Empleos: {impacto["empleos"]}");
            }

            Console.WriteLine("\n3. Ocupación de Destinos");
            foreach (var destino in sistema.ObtenerDestinos())
            {
                double ocupacion = sistema.CalcularOcupacionDestino(destino.IdDestino);
                Console.WriteLine($"{destino.Nombre}: {ocupacion:F1}% de capacidad");
            }
        }
    }
}
