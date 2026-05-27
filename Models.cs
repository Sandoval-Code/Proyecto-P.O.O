using System;
using System.Collections.Generic;
using System.Linq;

namespace TurismoSostenible.Models
{
    /// <summary>
    /// Representa una comunidad local que se beneficia del turismo
    /// </summary>
    public class Comunidad
    {
        public string IdComunidad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Poblacion { get; set; }
        public int HogaresParticipantes { get; set; }
        public double IngresosAcumulados { get; set; }
        public int EmpleosGenerados { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Comunidad(string idComunidad, string nombre, string descripcion, 
                        int poblacion, int hogaresParticipantes)
        {
            IdComunidad = idComunidad;
            Nombre = nombre;
            Descripcion = descripcion;
            Poblacion = poblacion;
            HogaresParticipantes = hogaresParticipantes;
            IngresosAcumulados = 0.0;
            EmpleosGenerados = 0;
            FechaRegistro = DateTime.Now;
        }

        public void AgregarIngreso(double monto)
        {
            IngresosAcumulados += monto;
        }

        public void IncrementarEmpleos(int cantidad = 1)
        {
            EmpleosGenerados += cantidad;
        }

        public double ObtenerPorcentajeParticipacion()
        {
            if (Poblacion == 0) return 0;
            return (double)HogaresParticipantes / Poblacion * 100;
        }

        public override string ToString()
        {
            return $"Comunidad: {Nombre} ({IdComunidad})";
        }
    }

    /// <summary>
    /// Representa un destino turístico sostenible
    /// </summary>
    public class Destino
    {
        public string IdDestino { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CapacidadDiaria { get; set; }
        public Comunidad Comunidad { get; set; }
        public int VisitantesHoy { get; set; }
        public List<Experiencia> Experiencias { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Destino(string idDestino, string nombre, string descripcion,
                      int capacidadDiaria, Comunidad comunidad)
        {
            IdDestino = idDestino;
            Nombre = nombre;
            Descripcion = descripcion;
            CapacidadDiaria = capacidadDiaria;
            Comunidad = comunidad;
            VisitantesHoy = 0;
            Experiencias = new List<Experiencia>();
            FechaCreacion = DateTime.Now;
        }

        public bool PuedeAceptarVisitantes(int cantidad)
        {
            return (VisitantesHoy + cantidad) <= CapacidadDiaria;
        }

        public bool RegistrarVisitantes(int cantidad)
        {
            if (PuedeAceptarVisitantes(cantidad))
            {
                VisitantesHoy += cantidad;
                return true;
            }
            return false;
        }

        public double CalcularOcupacionPorcentual()
        {
            if (CapacidadDiaria == 0) return 0;
            return (double)VisitantesHoy / CapacidadDiaria * 100;
        }

        public void ResetearVisitantesDiarios()
        {
            VisitantesHoy = 0;
        }

        public override string ToString()
        {
            return $"Destino: {Nombre} - Ocupación: {CalcularOcupacionPorcentual():F1}%";
        }
    }

    /// <summary>
    /// Representa una experiencia turística sostenible
    /// </summary>
    public class Experiencia
    {
        public string IdExperiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public double Precio { get; set; }
        public Destino Destino { get; set; }
        public double PorcentajeComunitario { get; set; }
        public List<Reserva> Reservas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int DuracionHoras { get; set; }

        public Experiencia(string idExperiencia, string nombre, string descripcion,
                          string tipo, double precio, Destino destino,
                          double porcentajeComunitario)
        {
            IdExperiencia = idExperiencia;
            Nombre = nombre;
            Descripcion = descripcion;
            Tipo = tipo;
            Precio = precio;
            Destino = destino;
            PorcentajeComunitario = porcentajeComunitario;
            Reservas = new List<Reserva>();
            FechaCreacion = DateTime.Now;
            DuracionHoras = 2;
        }

        public double CalcularAporteComunitario(double monto)
        {
            return monto * PorcentajeComunitario;
        }

        public bool VerificarDisponibilidad(string fecha, int personas)
        {
            return Destino.PuedeAceptarVisitantes(personas);
        }

        public Dictionary<string, object> ObtenerResumen()
        {
            return new Dictionary<string, object>
            {
                { "id", IdExperiencia },
                { "nombre", Nombre },
                { "tipo", Tipo },
                { "precio", Precio },
                { "destino", Destino.Nombre },
                { "aporte_comunitario", $"{PorcentajeComunitario * 100}%" }
            };
        }

        public override string ToString()
        {
            return $"Experiencia: {Nombre} (${Precio})";
        }
    }

    /// <summary>
    /// Representa un visitante al sistema
    /// </summary>
    public class Visitante
    {
        public string IdVisitante { get; set; }
        public string Nombre { get; set; }
        public string PaisOrigen { get; set; }
        public List<Reserva> Reservas { get; set; }
        public double GastoTotal { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Visitante(string idVisitante, string nombre, string paisOrigen)
        {
            IdVisitante = idVisitante;
            Nombre = nombre;
            PaisOrigen = paisOrigen;
            Reservas = new List<Reserva>();
            GastoTotal = 0.0;
            FechaRegistro = DateTime.Now;
        }

        public void AgregarGasto(double monto)
        {
            GastoTotal += monto;
        }

        public Dictionary<string, object> ObtenerEstadisticas()
        {
            return new Dictionary<string, object>
            {
                { "id", IdVisitante },
                { "nombre", Nombre },
                { "origen", PaisOrigen },
                { "reservas", Reservas.Count },
                { "gasto_total", GastoTotal }
            };
        }

        public override string ToString()
        {
            return $"Visitante: {Nombre} ({PaisOrigen})";
        }
    }

    /// <summary>
    /// Representa una reserva de experiencia
    /// </summary>
    public class Reserva
    {
        public string IdReserva { get; set; }
        public Visitante Visitante { get; set; }
        public Experiencia Experiencia { get; set; }
        public string Fecha { get; set; }
        public int CantidadPersonas { get; set; }
        public double MontoTotal { get; set; }
        public double MontoComunitario { get; set; }
        public string Estado { get; set; }
        public DateTime FechaReserva { get; set; }

        public Reserva(string idReserva, Visitante visitante, Experiencia experiencia,
                      string fecha, int cantidadPersonas)
        {
            IdReserva = idReserva;
            Visitante = visitante;
            Experiencia = experiencia;
            Fecha = fecha;
            CantidadPersonas = cantidadPersonas;
            MontoTotal = experiencia.Precio * cantidadPersonas;
            MontoComunitario = experiencia.CalcularAporteComunitario(MontoTotal);
            Estado = "confirmada";
            FechaReserva = DateTime.Now;
        }

        public void Cancelar()
        {
            Estado = "cancelada";
            MontoComunitario = 0;
        }

        public Dictionary<string, object> ObtenerResumen()
        {
            return new Dictionary<string, object>
            {
                { "id", IdReserva },
                { "visitante", Visitante.Nombre },
                { "experiencia", Experiencia.Nombre },
                { "fecha", Fecha },
                { "personas", CantidadPersonas },
                { "total", MontoTotal },
                { "aporte_comunidad", MontoComunitario },
                { "estado", Estado }
            };
        }

        public override string ToString()
        {
            return $"Reserva: {IdReserva} - {Experiencia.Nombre}";
        }
    }

    /// <summary>
    /// Representa la evaluación de una experiencia por un visitante
    /// </summary>
    public class Evaluacion
    {
        public string IdEvaluacion { get; set; }
        public Reserva Reserva { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaEvaluacion { get; set; }

        public Evaluacion(string idEvaluacion, Reserva reserva, int calificacion, string comentario)
        {
            IdEvaluacion = idEvaluacion;
            Reserva = reserva;
            Calificacion = Math.Max(1, Math.Min(5, calificacion));
            Comentario = comentario;
            FechaEvaluacion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Evaluación: {Reserva.Experiencia.Nombre} - {Calificacion}/5";
        }
    }
}
