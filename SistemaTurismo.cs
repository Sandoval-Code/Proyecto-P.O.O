using System;
using System.Collections.Generic;
using System.Linq;
using TurismoSostenible.Models;

namespace TurismoSostenible.Servicios
{
    /// <summary>
    /// Sistema de Turismo Sostenible - Orquestador central
    /// Gestiona todas las operaciones del sistema de turismo
    /// </summary>
    public class SistemaTurismo
    {
        private Dictionary<string, Comunidad> comunidades;
        private Dictionary<string, Destino> destinos;
        private Dictionary<string, Visitante> visitantes;
        private Dictionary<string, Experiencia> experiencias;
        private Dictionary<string, Reserva> reservas;
        private int contadorReservas;
        private DateTime fechaCreacion;

        public SistemaTurismo()
        {
            comunidades = new Dictionary<string, Comunidad>();
            destinos = new Dictionary<string, Destino>();
            visitantes = new Dictionary<string, Visitante>();
            experiencias = new Dictionary<string, Experiencia>();
            reservas = new Dictionary<string, Reserva>();
            contadorReservas = 0;
            fechaCreacion = DateTime.Now;
        }

        #region Métodos de Comunidades

        public bool RegistrarComunidad(Comunidad comunidad)
        {
            if (!comunidades.ContainsKey(comunidad.IdComunidad))
            {
                comunidades.Add(comunidad.IdComunidad, comunidad);
                return true;
            }
            return false;
        }

        public Comunidad ObtenerComunidad(string idComunidad)
        {
            if (comunidades.TryGetValue(idComunidad, out var comunidad))
                return comunidad;
            return null;
        }

        public List<Comunidad> ObtenerComunidades()
        {
            return comunidades.Values.ToList();
        }

        #endregion

        #region Métodos de Destinos

        public bool RegistrarDestino(Destino destino)
        {
            if (!destinos.ContainsKey(destino.IdDestino))
            {
                destinos.Add(destino.IdDestino, destino);
                return true;
            }
            return false;
        }

        public Destino ObtenerDestino(string idDestino)
        {
            if (destinos.TryGetValue(idDestino, out var destino))
                return destino;
            return null;
        }

        public List<Destino> ObtenerDestinos()
        {
            return destinos.Values.ToList();
        }

        public double CalcularOcupacionDestino(string idDestino)
        {
            var destino = ObtenerDestino(idDestino);
            if (destino != null)
                return destino.CalcularOcupacionPorcentual();
            return 0.0;
        }

        #endregion

        #region Métodos de Experiencias

        public bool RegistrarExperiencia(Experiencia experiencia)
        {
            if (!experiencias.ContainsKey(experiencia.IdExperiencia))
            {
                experiencias.Add(experiencia.IdExperiencia, experiencia);
                return true;
            }
            return false;
        }

        public Experiencia ObtenerExperiencia(string idExperiencia)
        {
            if (experiencias.TryGetValue(idExperiencia, out var experiencia))
                return experiencia;
            return null;
        }

        public List<Experiencia> ObtenerExperiencias()
        {
            return experiencias.Values.ToList();
        }

        public List<Experiencia> ObtenerExperienciasPorDestino(string idDestino)
        {
            return experiencias.Values.Where(e => e.Destino.IdDestino == idDestino).ToList();
        }

        public List<Experiencia> ObtenerExperienciasPorTipo(string tipo)
        {
            return experiencias.Values.Where(e => e.Tipo == tipo).ToList();
        }

        #endregion

        #region Métodos de Visitantes

        public bool RegistrarVisitante(Visitante visitante)
        {
            if (!visitantes.ContainsKey(visitante.IdVisitante))
            {
                visitantes.Add(visitante.IdVisitante, visitante);
                return true;
            }
            return false;
        }

        public Visitante ObtenerVisitante(string idVisitante)
        {
            if (visitantes.TryGetValue(idVisitante, out var visitante))
                return visitante;
            return null;
        }

        public List<Visitante> ObtenerVisitantes()
        {
            return visitantes.Values.ToList();
        }

        #endregion

        #region Métodos de Reservas

        public Reserva CrearReserva(string idVisitante, string idExperiencia,
                                   string fecha, int cantidadPersonas)
        {
            var visitante = ObtenerVisitante(idVisitante);
            var experiencia = ObtenerExperiencia(idExperiencia);

            if (visitante == null || experiencia == null)
                return null;

            // Verificar disponibilidad
            if (!experiencia.VerificarDisponibilidad(fecha, cantidadPersonas))
            {
                Console.WriteLine("⚠ La experiencia no tiene disponibilidad para esa fecha");
                return null;
            }

            // Crear reserva
            contadorReservas++;
            var idReserva = $"RES{contadorReservas:D4}";
            var reserva = new Reserva(idReserva, visitante, experiencia, fecha, cantidadPersonas);

            // Registrar reserva
            reservas.Add(idReserva, reserva);
            visitante.Reservas.Add(reserva);
            experiencia.Reservas.Add(reserva);

            // Actualizar estadísticas
            visitante.AgregarGasto(reserva.MontoTotal);
            experiencia.Destino.RegistrarVisitantes(cantidadPersonas);
            experiencia.Destino.Comunidad.AgregarIngreso(reserva.MontoComunitario);

            return reserva;
        }

        public Reserva ObtenerReserva(string idReserva)
        {
            if (reservas.TryGetValue(idReserva, out var reserva))
                return reserva;
            return null;
        }

        public List<Reserva> ObtenerReservas()
        {
            return reservas.Values.ToList();
        }

        public bool CancelarReserva(string idReserva)
        {
            var reserva = ObtenerReserva(idReserva);
            if (reserva != null && reserva.Estado == "confirmada")
            {
                reserva.Cancelar();
                reserva.Experiencia.Destino.Comunidad.IngresosAcumulados -= reserva.MontoComunitario;
                return true;
            }
            return false;
        }

        #endregion

        #region Métodos de Reportes

        public Dictionary<string, object> GenerarResumenSostenibilidad()
        {
            var totalVisitantes = visitantes.Count;
            var totalReservas = reservas.Values.Count(r => r.Estado == "confirmada");
            var ingresosTotal = reservas.Values
                .Where(r => r.Estado == "confirmada")
                .Sum(r => r.MontoTotal);
            var aporteComunitario = reservas.Values
                .Where(r => r.Estado == "confirmada")
                .Sum(r => r.MontoComunitario);

            var porcentajeAporte = ingresosTotal > 0 
                ? Math.Round((aporteComunitario / ingresosTotal) * 100, 2)
                : 0;

            return new Dictionary<string, object>
            {
                { "total_visitantes", totalVisitantes },
                { "total_reservas", totalReservas },
                { "ingresos_totales", Math.Round(ingresosTotal, 2) },
                { "aporte_comunitario", Math.Round(aporteComunitario, 2) },
                { "destinos_activos", destinos.Count },
                { "comunidades", comunidades.Count },
                { "experiencias", experiencias.Count },
                { "porcentaje_aporte", porcentajeAporte }
            };
        }

        public Dictionary<string, object> CalcularImpactoComunidad(string idComunidad)
        {
            var comunidad = ObtenerComunidad(idComunidad);
            if (comunidad == null)
                return new Dictionary<string, object>();

            var visitantesComunidad = new HashSet<string>();
            var totalExperiencias = 0;

            foreach (var destino in destinos.Values)
            {
                if (destino.Comunidad.IdComunidad == idComunidad)
                {
                    totalExperiencias += destino.Experiencias.Count;
                    foreach (var reserva in reservas.Values)
                    {
                        if (reserva.Experiencia.Destino == destino && 
                            reserva.Estado == "confirmada")
                        {
                            visitantesComunidad.Add(reserva.Visitante.IdVisitante);
                        }
                    }
                }
            }

            return new Dictionary<string, object>
            {
                { "comunidad", comunidad.Nombre },
                { "ingresos", Math.Round(comunidad.IngresosAcumulados, 2) },
                { "visitantes", visitantesComunidad.Count },
                { "empleos", comunidad.EmpleosGenerados },
                { "experiencias", totalExperiencias },
                { "poblacion", comunidad.Poblacion },
                { "participacion", $"{comunidad.ObtenerPorcentajeParticipacion():F1}%" }
            };
        }

        public Dictionary<string, object> GenerarReporteVisitantes()
        {
            var paises = new Dictionary<string, Dictionary<string, object>>();

            foreach (var visitante in visitantes.Values)
            {
                if (!paises.ContainsKey(visitante.PaisOrigen))
                {
                    paises[visitante.PaisOrigen] = new Dictionary<string, object>
                    {
                        { "cantidad", 0 },
                        { "gasto_total", 0.0 }
                    };
                }
                paises[visitante.PaisOrigen]["cantidad"] = 
                    (int)paises[visitante.PaisOrigen]["cantidad"] + 1;
                paises[visitante.PaisOrigen]["gasto_total"] = 
                    (double)paises[visitante.PaisOrigen]["gasto_total"] + visitante.GastoTotal;
            }

            var gastoPromedio = visitantes.Count > 0
                ? Math.Round(visitantes.Values.Sum(v => v.GastoTotal) / visitantes.Count, 2)
                : 0;

            return new Dictionary<string, object>
            {
                { "total_visitantes", visitantes.Count },
                { "gasto_promedio", gastoPromedio },
                { "paises", paises }
            };
        }

        public Dictionary<string, object> GenerarReporteExperiencias()
        {
            var porTipo = new Dictionary<string, Dictionary<string, object>>();

            foreach (var exp in experiencias.Values)
            {
                if (!porTipo.ContainsKey(exp.Tipo))
                {
                    porTipo[exp.Tipo] = new Dictionary<string, object>
                    {
                        { "cantidad", 0 },
                        { "ingresos", 0.0 },
                        { "reservas", 0 }
                    };
                }
                porTipo[exp.Tipo]["cantidad"] = (int)porTipo[exp.Tipo]["cantidad"] + 1;

                foreach (var reserva in exp.Reservas)
                {
                    if (reserva.Estado == "confirmada")
                    {
                        porTipo[exp.Tipo]["ingresos"] = 
                            (double)porTipo[exp.Tipo]["ingresos"] + reserva.MontoTotal;
                        porTipo[exp.Tipo]["reservas"] = (int)porTipo[exp.Tipo]["reservas"] + 1;
                    }
                }
            }

            return new Dictionary<string, object>
            {
                { "total_experiencias", experiencias.Count },
                { "por_tipo", porTipo }
            };
        }

        public List<Dictionary<string, object>> ObtenerTopDestinos(int cantidad = 5)
        {
            var destiniosSorted = destinos.Values
                .OrderByDescending(d => d.VisitantesHoy)
                .Take(cantidad)
                .Select(d => new Dictionary<string, object>
                {
                    { "nombre", d.Nombre },
                    { "visitantes", d.VisitantesHoy }
                })
                .ToList();

            return destiniosSorted;
        }

        public Dictionary<string, object> ObtenerEstadisticasGenerales()
        {
            var resumen = GenerarResumenSostenibilidad();
            var visitantesRep = GenerarReporteVisitantes();
            var experienciasRep = GenerarReporteExperiencias();

            return new Dictionary<string, object>
            {
                { "resumen", resumen },
                { "visitantes", visitantesRep },
                { "experiencias", experienciasRep },
                { "fecha_reporte", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
            };
        }

        #endregion
    }
}
