# Arquitectura del Sistema - Turismo Sostenible

## Diagrama de Clases UML

```
┌─────────────────────────────────────────────────────────────┐
│                   SistemaTurismo (Orquestador)              │
├─────────────────────────────────────────────────────────────┤
│ - comunidades: Dictionary<string, Comunidad>                │
│ - destinos: Dictionary<string, Destino>                     │
│ - experiencias: Dictionary<string, Experiencia>             │
│ - visitantes: Dictionary<string, Visitante>                 │
│ - reservas: Dictionary<string, Reserva>                     │
├─────────────────────────────────────────────────────────────┤
│ + RegistrarComunidad(Comunidad): bool                       │
│ + CrearReserva(id, id, fecha, cantidad): Reserva            │
│ + GenerarResumenSostenibilidad(): Dictionary                │
│ + CalcularImpactoComunidad(id): Dictionary                  │
└─────────────────────────────────────────────────────────────┘
          ▲
          │ usa/gestiona
          │
    ┌─────┴─────┬──────────────┬──────────────┬──────────────┐
    │            │              │              │              │
    ▼            ▼              ▼              ▼              ▼
┌────────────┐ ┌────────────┐ ┌────────────┐ ┌────────────┐ ┌────────────┐
│ Comunidad  │ │ Destino    │ │Experiencia │ │ Visitante  │ │ Reserva    │
├────────────┤ ├────────────┤ ├────────────┤ ├────────────┤ ├────────────┤
│ id         │ │ id         │ │ id         │ │ id         │ │ id         │
│ nombre     │ │ nombre     │ │ nombre     │ │ nombre     │ │ visitante  │
│ ingresos   │ │ capacidad  │ │ tipo       │ │ origen     │ │ experiencia│
│ empleos    │ │ visitantes │ │ precio     │ │ gasto      │ │ fecha      │
│            │ │ comunidad  │ │ comunidad  │ │ reservas   │ │ cantidad   │
│            │ │            │ │ destino    │ │            │ │ monto      │
│            │ │            │ │ % aporte   │ │            │ │ estado     │
└────────────┘ └────────────┘ └────────────┘ └────────────┘ └────────────┘
                    ▲            ▲
                    │            │
                    │ vinculado  │ contiene
                    │            │
                    └────────────┘
                     (relación 1-n)
```

## Relaciones de Entidades

### 1. Comunidad ← Destino (1 a N)
- Una comunidad puede tener múltiples destinos turísticos
- Cada destino pertenece a una única comunidad
- Los ingresos de visitantes se distribuyen a la comunidad

### 2. Destino ← Experiencia (1 a N)
- Un destino puede ofrecer múltiples experiencias
- Cada experiencia está asociada a un destino único
- Control de capacidad a nivel de destino

### 3. Experiencia ← Reserva (1 a N)
- Una experiencia puede tener múltiples reservas
- Cada reserva está vinculada a una experiencia específica
- Calcula automáticamente aportes a la comunidad

### 4. Visitante ← Reserva (1 a N)
- Un visitante puede hacer múltiples reservas
- Cada reserva registra un visitante
- Acumula gastos totales del visitante

## Flujo de Datos

### Crear Reserva (Flujo Principal)

```
1. Visitante selecciona Experiencia
        ↓
2. Sistema valida capacidad del Destino
        ↓
3. Sistema calcula:
   - MontoTotal = Experiencia.Precio × Cantidad
   - MontoComunitario = MontoTotal × Experiencia.PorcentajeComunitario
        ↓
4. Sistema crea objeto Reserva
        ↓
5. Actualiza estadísticas:
   - Visitante.AgregarGasto(MontoTotal)
   - Destino.RegistrarVisitantes(Cantidad)
   - Comunidad.AgregarIngreso(MontoComunitario)
        ↓
6. Reserva confirmada ✓
```

## Patrones de Diseño Implementados

### 1. **Repository Pattern**
```csharp
// SistemaTurismo actúa como repositorio centralizado
private Dictionary<string, Comunidad> comunidades;
private Dictionary<string, Destino> destinos;

public Comunidad ObtenerComunidad(string id)
public List<Comunidad> ObtenerComunidades()
```

### 2. **Singleton Pattern**
```csharp
// Una única instancia de SistemaTurismo por aplicación
var sistema = new SistemaTurismo();
```

### 3. **Factory Pattern**
```csharp
// CrearReserva actúa como factory
public Reserva CrearReserva(string idVis, string idExp, 
                            string fecha, int cantidad)
```

### 4. **Encapsulation**
```csharp
// Propiedades con acceso controlado
public string IdComunidad { get; set; }
public double IngresosAcumulados { get; set; }
```

## Responsabilidades de Clases

### **Comunidad**
- ✓ Almacenar datos de comunidad local
- ✓ Registrar ingresos del turismo
- ✓ Calcular participación comunitaria
- ✓ Generar empleos

### **Destino**
- ✓ Validar capacidad de visitantes
- ✓ Mantener control de flujo de visitantes
- ✓ Calcular ocupación porcentual
- ✓ Vincular con comunidad beneficiaria

### **Experiencia**
- ✓ Definir actividades turísticas
- ✓ Establecer precios
- ✓ Calcular distribución de ingresos
- ✓ Verificar disponibilidad

### **Visitante**
- ✓ Registrar datos del turista
- ✓ Acumular gastos realizados
- ✓ Mantener historial de reservas
- ✓ Proporcionar estadísticas personales

### **Reserva**
- ✓ Formalizar bookings
- ✓ Calcular costos
- ✓ Gestionar cancelaciones
- ✓ Rastrear estado de confirmación

### **SistemaTurismo**
- ✓ Orquestar todas las operaciones
- ✓ Gestionar persistencia en memoria
- ✓ Generar reportes y análisis
- ✓ Validar transacciones
- ✓ Calcular impacto de sostenibilidad

## Principios SOLID Aplicados

### **S** - Single Responsibility
Cada clase tiene una única razón para cambiar

### **O** - Open/Closed
Las clases están abiertas para extensión (nuevos tipos de experiencias)

### **L** - Liskov Substitution
Las subclases pueden reemplazar a sus padres sin romper el sistema

### **I** - Interface Segregation
Métodos públicos bien definidos y específicos

### **D** - Dependency Inversion
SistemaTurismo depende de abstracciones (Interfaces implícitas)

## Casos de Uso Implementados

```
┌─────────────────────────────────────────────────┐
│         CASOS DE USO PRINCIPALES                │
├─────────────────────────────────────────────────┤
│                                                 │
│  [Operador]                                     │
│      ├─ Registrar Destino                       │
│      ├─ Crear Experiencia                       │
│      └─ Consultar Ocupación                     │
│                                                 │
│  [Comunidad]                                    │
│      ├─ Ver Ingresos Acumulados                 │
│      ├─ Consultar Empleos Generados            │
│      └─ Obtener Impacto Comunitario             │
│                                                 │
│  [Visitante]                                    │
│      ├─ Registrarse                             │
│      ├─ Realizar Reserva                        │
│      ├─ Cancelar Reserva                        │
│      └─ Evaluar Experiencia                     │
│                                                 │
│  [Administrador]                                │
│      ├─ Generar Reportes                        │
│      ├─ Analizar Sostenibilidad                 │
│      ├─ Ver Estadísticas Generales              │
│      └─ Exportar Datos                          │
│                                                 │
└─────────────────────────────────────────────────┘
```

## Validaciones del Sistema

### En Creación de Reserva:

1. **Validar Visitante existe**
   ```csharp
   var visitante = ObtenerVisitante(idVisitante);
   if (visitante == null) return null;
   ```

2. **Validar Experiencia existe**
   ```csharp
   var experiencia = ObtenerExperiencia(idExperiencia);
   if (experiencia == null) return null;
   ```

3. **Validar Disponibilidad/Capacidad**
   ```csharp
   if (!experiencia.VerificarDisponibilidad(fecha, cantidadPersonas))
       return null;
   ```

4. **Validar Cálculos**
   ```csharp
   MontoComunitario = MontoTotal * PorcentajeComunitario;
   ```

## Extensibilidad Futura

### Puntos de Extensión:

1. **Persistencia (Base de Datos)**
   ```csharp
   // Implementar IRepository para EntityFramework
   public interface IRepository<T>
   {
       void Save(T entity);
       T GetById(string id);
   }
   ```

2. **API REST**
   ```csharp
   // Controladores ASP.NET Core
   [ApiController]
   [Route("api/[controller]")]
   public class DestinosController { }
   ```

3. **Notificaciones**
   ```csharp
   // INotificationService
   public interface INotificationService
   {
       void NotificarNuevaReserva(Reserva reserva);
   }
   ```

4. **Pagos**
   ```csharp
   // IPagosService
   public interface IPagosService
   {
       bool ProcesarPago(Reserva reserva);
   }
   ```

---

**Arquitectura diseñada para**: Mantenibilidad, Escalabilidad, Testabilidad
**Versión:** 1.0 | **Fecha:** Mayo 2026
