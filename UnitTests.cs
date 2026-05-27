using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TurismoSostenible.Models;
using TurismoSostenible.Servicios;

namespace TurismoSostenible
{
    public class ComunidadTests
    {
        [Fact]
        public void CreacionComunidad_DebeCrearCorrectamente()
        {
            // Arrange & Act
            var comunidad = new Comunidad("COM001", "Comunidad Test", "Descripción", 200, 50);

            // Assert
            Assert.Equal("COM001", comunidad.IdComunidad);
            Assert.Equal("Comunidad Test", comunidad.Nombre);
            Assert.Equal(200, comunidad.Poblacion);
            Assert.Equal(0, comunidad.IngresosAcumulados);
        }

        [Fact]
        public void AgregarIngreso_DebeIncrementarIngresosAcumulados()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Test", "Desc", 100, 30);

            // Act
            comunidad.AgregarIngreso(1000);
            comunidad.AgregarIngreso(500);

            // Assert
            Assert.Equal(1500, comunidad.IngresosAcumulados);
        }

        [Fact]
        public void IncrementarEmpleos_DebeIncrementarEmpleosGenerados()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Test", "Desc", 100, 30);

            // Act
            comunidad.IncrementarEmpleos(5);

            // Assert
            Assert.Equal(5, comunidad.EmpleosGenerados);
        }

        [Fact]
        public void ObtenerPorcentajeParticipacion_DebeCalcularCorrectamente()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Test", "Desc", 200, 50);

            // Act
            double porcentaje = comunidad.ObtenerPorcentajeParticipacion();

            // Assert
            Assert.Equal(25.0, porcentaje);
        }
    }

    public class DestinoTests
    {
        [Fact]
        public void CreacionDestino_DebeCrearCorrectamente()
        {
            // Arrange & Act
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna Azul", "Destino natural", 50, comunidad);

            // Assert
            Assert.Equal("D001", destino.IdDestino);
            Assert.Equal(50, destino.CapacidadDiaria);
            Assert.Equal(0, destino.VisitantesHoy);
        }

        [Fact]
        public void PuedeAceptarVisitantes_DebeValidarCapacidad()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);

            // Act & Assert
            Assert.True(destino.PuedeAceptarVisitantes(30));
            destino.RegistrarVisitantes(40);
            Assert.True(destino.PuedeAceptarVisitantes(10));
            Assert.False(destino.PuedeAceptarVisitantes(20));
        }

        [Fact]
        public void CalcularOcupacionPorcentual_DebeCalcularCorrectamente()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);

            // Act
            destino.RegistrarVisitantes(25);
            double ocupacion = destino.CalcularOcupacionPorcentual();

            // Assert
            Assert.Equal(50.0, ocupacion);
        }

        [Fact]
        public void RegistrarVisitantes_DebeRegistrarCorrectamente()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);

            // Act
            bool resultado = destino.RegistrarVisitantes(30);

            // Assert
            Assert.True(resultado);
            Assert.Equal(30, destino.VisitantesHoy);
        }
    }

    public class ExperienciaTests
    {
        [Fact]
        public void CreacionExperiencia_DebeCrearCorrectamente()
        {
            // Arrange & Act
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);

            // Assert
            Assert.Equal("EXP001", experiencia.IdExperiencia);
            Assert.Equal(100, experiencia.Precio);
            Assert.Equal(0.20, experiencia.PorcentajeComunitario);
        }

        [Fact]
        public void CalcularAporteComunitario_DebeCalcularCorrectamente()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);

            // Act
            double aporte = experiencia.CalcularAporteComunitario(1000);

            // Assert
            Assert.Equal(200, aporte);
        }

        [Fact]
        public void ObtenerResumen_DebeRetornarDictionary()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);

            // Act
            var resumen = experiencia.ObtenerResumen();

            // Assert
            Assert.NotNull(resumen);
            Assert.Equal("EXP001", resumen["id"]);
            Assert.Equal("20%", resumen["aporte_comunitario"]);
        }
    }

    public class VisitanteTests
    {
        [Fact]
        public void CreacionVisitante_DebeCrearCorrectamente()
        {
            // Arrange & Act
            var visitante = new Visitante("V001", "Juan Pérez", "Colombia");

            // Assert
            Assert.Equal("V001", visitante.IdVisitante);
            Assert.Equal("Juan Pérez", visitante.Nombre);
            Assert.Equal(0, visitante.GastoTotal);
        }

        [Fact]
        public void AgregarGasto_DebeAgregarCorrectamente()
        {
            // Arrange
            var visitante = new Visitante("V001", "Juan", "Colombia");

            // Act
            visitante.AgregarGasto(500);
            visitante.AgregarGasto(300);

            // Assert
            Assert.Equal(800, visitante.GastoTotal);
        }

        [Fact]
        public void ObtenerEstadisticas_DebeRetornarDictionary()
        {
            // Arrange
            var visitante = new Visitante("V001", "Juan", "Colombia");
            visitante.AgregarGasto(500);

            // Act
            var stats = visitante.ObtenerEstadisticas();

            // Assert
            Assert.NotNull(stats);
            Assert.Equal("V001", stats["id"]);
            Assert.Equal(500.0, stats["gasto_total"]);
        }
    }

    public class ReservaTests
    {
        [Fact]
        public void CreacionReserva_DebeCrearCorrectamente()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);
            var visitante = new Visitante("V001", "Juan", "Colombia");

            // Act
            var reserva = new Reserva("RES001", visitante, experiencia, "2024-06-15", 2);

            // Assert
            Assert.Equal("RES001", reserva.IdReserva);
            Assert.Equal(2, reserva.CantidadPersonas);
            Assert.Equal(200, reserva.MontoTotal);
            Assert.Equal(40, reserva.MontoComunitario);
        }

        [Fact]
        public void Cancelar_DebeMarcarComoCancelada()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);
            var visitante = new Visitante("V001", "Juan", "Colombia");
            var reserva = new Reserva("RES001", visitante, experiencia, "2024-06-15", 1);

            // Act
            reserva.Cancelar();

            // Assert
            Assert.Equal("cancelada", reserva.Estado);
            Assert.Equal(0, reserva.MontoComunitario);
        }
    }

    public class SistemaTurismoTests
    {
        [Fact]
        public void RegistrarComponentes_DebeRegistrarCorrectamente()
        {
            // Arrange
            var sistema = new SistemaTurismo();
            var comunidad = new Comunidad("COM001", "Comunidad Sierra", "Desc", 200, 60);
            var destino = new Destino("D001", "Laguna Azul", "Destino natural", 100, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 150, destino, 0.25);
            var visitante = new Visitante("V001", "Maria García", "España");

            // Act
            sistema.RegistrarComunidad(comunidad);
            sistema.RegistrarDestino(destino);
            sistema.RegistrarExperiencia(experiencia);
            sistema.RegistrarVisitante(visitante);

            // Assert
            Assert.Single(sistema.ObtenerComunidades());
            Assert.Single(sistema.ObtenerDestinos());
            Assert.Single(sistema.ObtenerExperiencias());
            Assert.Single(sistema.ObtenerVisitantes());
        }

        [Fact]
        public void CrearReserva_DebeCrearCorrectamente()
        {
            // Arrange
            var sistema = new SistemaTurismo();
            var comunidad = new Comunidad("COM001", "Comunidad Sierra", "Desc", 200, 60);
            var destino = new Destino("D001", "Laguna Azul", "Destino natural", 100, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 150, destino, 0.25);
            var visitante = new Visitante("V001", "Maria García", "España");

            sistema.RegistrarComunidad(comunidad);
            sistema.RegistrarDestino(destino);
            sistema.RegistrarExperiencia(experiencia);
            sistema.RegistrarVisitante(visitante);

            // Act
            var reserva = sistema.CrearReserva("V001", "EXP001", "2024-06-15", 2);

            // Assert
            Assert.NotNull(reserva);
            Assert.Equal(2, reserva.CantidadPersonas);
            Assert.Equal(300, reserva.MontoTotal);
            Assert.Equal(75, reserva.MontoComunitario);
        }

        [Fact]
        public void GenerarResumenSostenibilidad_DebeCalcularCorrectamente()
        {
            // Arrange
            var sistema = new SistemaTurismo();
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 150, destino, 0.25);

            sistema.RegistrarComunidad(comunidad);
            sistema.RegistrarDestino(destino);
            sistema.RegistrarExperiencia(experiencia);

            // Crear múltiples visitantes y reservas
            for (int i = 1; i <= 3; i++)
            {
                var v = new Visitante($"V{i}", $"Visitante {i}", "México");
                sistema.RegistrarVisitante(v);
                sistema.CrearReserva($"V{i}", "EXP001", "2024-06-15", 1);
            }

            // Act
            var resumen = sistema.GenerarResumenSostenibilidad();

            // Assert
            Assert.Equal(3, resumen["total_visitantes"]);
            Assert.Equal(3, resumen["total_reservas"]);
            Assert.Equal(450.0, resumen["ingresos_totales"]);
            Assert.Equal(112.5, resumen["aporte_comunitario"]);
        }

        [Fact]
        public void CalcularImpactoComunidad_DebeCalcularCorrectamente()
        {
            // Arrange
            var sistema = new SistemaTurismo();
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 150, destino, 0.25);

            sistema.RegistrarComunidad(comunidad);
            sistema.RegistrarDestino(destino);
            sistema.RegistrarExperiencia(experiencia);

            // Crear visitantes y reservas
            for (int i = 1; i <= 2; i++)
            {
                var v = new Visitante($"V{i}", $"Visitante {i}", "Argentina");
                sistema.RegistrarVisitante(v);
                sistema.CrearReserva($"V{i}", "EXP001", "2024-06-15", 1);
            }

            // Act
            var impacto = sistema.CalcularImpactoComunidad("COM001");

            // Assert
            Assert.Equal(2, impacto["visitantes"]);
            Assert.Equal(75.0, impacto["ingresos"]);
        }

        [Fact]
        public void CancelarReserva_DebeMarcarComoCancelada()
        {
            // Arrange
            var sistema = new SistemaTurismo();
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);
            var visitante = new Visitante("V001", "Usuario", "País");

            sistema.RegistrarComunidad(comunidad);
            sistema.RegistrarDestino(destino);
            sistema.RegistrarExperiencia(experiencia);
            sistema.RegistrarVisitante(visitante);

            var reserva = sistema.CrearReserva("V001", "EXP001", "2024-06-15", 1);

            // Act
            bool cancelada = sistema.CancelarReserva(reserva.IdReserva);
            var reservaCancelada = sistema.ObtenerReserva(reserva.IdReserva);

            // Assert
            Assert.True(cancelada);
            Assert.Equal("cancelada", reservaCancelada.Estado);
        }

        [Fact]
        public void ObtenerEstadisticasGenerales_DebeRetornarEstadisticas()
        {
            // Arrange
            var sistema = new SistemaTurismo();
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);
            var visitante = new Visitante("V001", "Usuario", "País");

            sistema.RegistrarComunidad(comunidad);
            sistema.RegistrarDestino(destino);
            sistema.RegistrarExperiencia(experiencia);
            sistema.RegistrarVisitante(visitante);

            // Act
            var stats = sistema.ObtenerEstadisticasGenerales();

            // Assert
            Assert.NotNull(stats);
            Assert.Contains("resumen", stats.Keys);
            Assert.Contains("visitantes", stats.Keys);
            Assert.Contains("experiencias", stats.Keys);
        }
    }

    public class EvaluacionTests
    {
        [Fact]
        public void CreacionEvaluacion_DebeCrearCorrectamente()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);
            var visitante = new Visitante("V001", "Usuario", "País");
            var reserva = new Reserva("RES001", visitante, experiencia, "2024-06-15", 2);

            // Act
            var evaluacion = new Evaluacion("EV001", reserva, 5, "Excelente experiencia");

            // Assert
            Assert.Equal(5, evaluacion.Calificacion);
            Assert.Equal("Excelente experiencia", evaluacion.Comentario);
        }

        [Fact]
        public void ValidacionCalificacion_DebeValidarRango1a5()
        {
            // Arrange
            var comunidad = new Comunidad("COM001", "Comunidad", "Desc", 100, 30);
            var destino = new Destino("D001", "Laguna", "Desc", 50, comunidad);
            var experiencia = new Experiencia("EXP001", "Senderismo", "Desc", "Naturaleza", 100, destino, 0.20);
            var visitante = new Visitante("V001", "Usuario", "País");
            var reserva = new Reserva("RES001", visitante, experiencia, "2024-06-15", 1);

            // Act
            var evalBaja = new Evaluacion("EV001", reserva, 0, "Malo");
            var evalAlta = new Evaluacion("EV002", reserva, 10, "Perfecto");

            // Assert
            Assert.Equal(1, evalBaja.Calificacion);
            Assert.Equal(5, evalAlta.Calificacion);
        }
    }
}
