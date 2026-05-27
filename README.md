# Sistema de Gestión de Turismo Sostenible

## 📋 Descripción General

Sistema integral desarrollado en **C# con Programación Orientada a Objetos (POO)** para gestionar turismo sostenible en comunidades locales, garantizando conservación del entorno y beneficio económico directo para las comunidades.

## 🎯 Problema Abordado

**Reto:** Impulso del turismo para la conservación del entorno y el bienestar de las comunidades locales

### Desafíos Identificados:
- Gestión poco digitalizada del turismo en destinos rurales
- Falta de control sobre el flujo de visitantes
- Desorganización de servicios turísticos
- Ausencia de herramientas para turismo comunitario
- Inequidad en la distribución de beneficios económicos

### Solución Propuesta:
Plataforma que:
- ✅ Regula capacidad de destinos
- ✅ Personaliza experiencias sostenibles
- ✅ Garantiza ingreso directo a comunidades
- ✅ Genera empleos locales
- ✅ Conserva patrimonio natural y cultural

---

## 🏗️ Arquitectura POO

### Clases Principales

#### **1. Comunidad**
```python
Comunidad(id, nombre, descripción, población, hogares_participantes)
```
- Gestiona datos de comunidades beneficiarias
- Registra ingresos acumulados
- Calcula empleos generados
- Obtiene porcentaje de participación

**Métodos:**
- `agregar_ingreso(monto)` - Registra ingresos
- `incrementar_empleos(cantidad)` - Incrementa empleos
- `obtener_porcentaje_participacion()` - Calcula participación

---

#### **2. Destino**
```python
Destino(id, nombre, descripción, capacidad_diaria, comunidad)
```
- Representa puntos turísticos con control de capacidad
- Vinculado a comunidad específica
- Regula flujo de visitantes

**Métodos:**
- `puede_aceptar_visitantes(cantidad)` - Valida capacidad
- `registrar_visitantes(cantidad)` - Registra acceso
- `calcular_ocupacion_porcentual()` - Obtiene % ocupación

---

#### **3. Experiencia**
```python
Experiencia(id, nombre, descripción, tipo, precio, destino, porcentaje_comunitario)
```
- Define actividades turísticas sostenibles
- Establece distribución de ingresos con comunidad
- Tipos: Naturaleza, Cultural, Aventura, Educativa, Comunitaria

**Métodos:**
- `calcular_aporte_comunitario(monto)` - Calcula beneficio comunitario
- `verificar_disponibilidad(fecha, personas)` - Valida disponibilidad

---

#### **4. Visitante**
```python
Visitante(id, nombre, país_origen)
```
- Gestiona datos de turistas
- Registra reservas y gastos

**Métodos:**
- `agregar_gasto(monto)` - Registra gastos
- `obtener_estadisticas()` - Retorna estadísticas

---

#### **5. Reserva**
```python
Reserva(id, visitante, experiencia, fecha, cantidad_personas)
```
- Formaliza bookings de experiencias
- Calcula ingresos totales y comunitarios
- Gestiona estado de confirmación

**Métodos:**
- `cancelar()` - Cancela reserva

---

#### **6. Evaluación**
```python
Evaluacion(id, reserva, calificación, comentario)
```
- Recopila retroalimentación de visitantes
- Escala 1-5 validada

---

### **7. SistemaTurismo** (Orquestador Central)
Gestiona todas las operaciones: CRUD de componentes, reservas, reportes y análisis.

---

## 📊 Módulos del Sistema

### **Models.cs**
Define todas las clases de dominio con encapsulación, propiedades y métodos específicos:
- `Comunidad` - Gestión de comunidades locales
- `Destino` - Definición de puntos turísticos
- `Experiencia` - Configuración de actividades sostenibles
- `Visitante` - Registro de turistas
- `Reserva` - Gestión de bookings
- `Evaluacion` - Recopilación de feedback

### **SistemaTurismo.cs**
Clase principal orquestadora que gestiona:
- Operaciones CRUD de componentes
- Procesamiento de reservas y cancelaciones
- Generación de reportes y análisis
- Cálculo de impacto y sostenibilidad

### **Program.cs**
Aplicación consola interactiva con menús para:
- Gestionar destinos turísticos
- Registrar visitantes
- Crear y configurar experiencias
- Realizar y cancelar reservas
- Consultar reportes avanzados

### **UnitTests.cs**
Suite completa de pruebas unitarias usando xUnit:
- 15+ pruebas de funcionalidad
- Cobertura de todas las clases principales
- Validación de cálculos y operaciones

---

## 🚀 Características Principales

### 1. **Control de Capacidad**
- Límite de visitantes por destino
- Validación automática de disponibilidad
- Prevención de sobrecarga

### 2. **Distribución Equitativa**
- % configurables de ingresos para comunidades
- Transferencia automática de fondos
- Reportes transparentes

### 3. **Gestión de Reservas**
- Confirmación de bookings
- Control de disponibilidad
- Opción de cancelación con impacto en comunidad

### 4. **Reportes Avanzados**
- Resumen de sostenibilidad
- Impacto por comunidad
- Ocupación de destinos
- Estadísticas de visitantes

---

## 💡 Principios de POO Implementados

### **Encapsulación**
- Atributos privados con getters/setters cuando es necesario
- Validaciones en constructores
- Métodos de negocio con lógica centralizada

### **Herencia**
- Base común en clases de dominio
- Extensibilidad para nuevos tipos de experiencias

### **Polimorfismo**
- Métodos `__str__()` en todas las clases
- Comportamiento dinámico de validaciones

### **Abstracción**
- Interfaces claras de clases
- Métodos públicos con propósito específico
- Separación de responsabilidades

---

## 📈 Reportes Disponibles

### 1. **Resumen de Sostenibilidad**
```
Total visitantes: X
Ingresos totales: $X,XXX
Aporte a comunidades: $XXX
Destinos activos: X
Porcentaje aporte: X%
```

### 2. **Impacto por Comunidad**
```
Comunidad: Nombre
- Ingresos generados: $X,XXX
- Visitantes: X
- Empleos: X
- Participación: X%
```

### 3. **Ocupación de Destinos**
```
Destino A: 75% de capacidad
Destino B: 40% de capacidad
```

### 4. **Estadísticas de Visitantes**
- Por país/región de origen
- Gasto promedio
- Total de visitantes

---

## 🔧 Instalación y Uso

### Requisitos
- .NET SDK 6.0 o superior
- Visual Studio 2022 / Visual Studio Code (recomendado)

### Ejecución

**Compilar la solución:**
```bash
dotnet build
```

**Ejecutar aplicación interactiva:**
```bash
dotnet run
```

**Ejecutar pruebas unitarias:**
```bash
dotnet test
```

---

## 📋 Casos de Uso

### 1. Operador Turístico
- Registra destinos con capacidad limitada
- Crea experiencias con % de beneficio comunitario
- Monitorea ocupación en tiempo real

### 2. Comunidad Local
- Recibe ingresos automáticos por cada reserva
- Accede a reportes de impacto
- Participa en experiencias comunitarias

### 3. Visitante
- Descubre experiencias sostenibles
- Realiza reservas garantizadas
- Contribuye directamente a comunidades locales

### 4. Administrador
- Genera reportes de sostenibilidad
- Analiza impacto económico
- Optimiza distribución de visitantes

---

## 🎓 Estructura de Datos

### Diagrama Entidad-Relación (Conceptual)

```
COMUNIDAD (1) ←――― (n) DESTINO
                        │
                        │ (1)
                        └――→ (n) EXPERIENCIA
                                 │
                                 │ (1)
                                 └――→ (n) RESERVA (1)――→ VISITANTE
                                      │
                                      └――→ EVALUACION
```

---

## 📊 Ejemplo de Flujo

1. **Comunidad Sierra Verde** se registra
2. **Destino Laguna Azul** se vincula a la comunidad (cap. 100 visitantes/día)
3. **Experiencia Senderismo** se crea (150 USD, 25% para comunidad)
4. **Visitante María** reserva Senderismo para 2 personas
   - Monto total: 300 USD
   - Aporte comunitario: 75 USD
5. **Reporte** muestra ingresos generados y empleos creados

---

## ✅ Pruebas

El sistema incluye 12+ pruebas unitarias que validan:
- ✓ Creación correcta de entidades
- ✓ Cálculos de ingresos y aportes
- ✓ Control de capacidad
- ✓ Gestión de reservas
- ✓ Reportes de sostenibilidad
- ✓ Cancelación de reservas

**Ejecutar:** `python test_sistema.py`

---

## 🌱 Impacto Esperado

### Para Comunidades
- 💰 Ingresos económicos directos y sostenidos
- 👥 Generación de empleos locales
- 📚 Transferencia de conocimiento cultural

### Para Ambiente
- 🌳 Control de presión sobre recursos naturales
- ♻️ Prácticas de turismo responsable
- 🔍 Monitoreo de capacidad de carga

### Para Visitantes
- 🎯 Experiencias auténticas y responsables
- 📍 Impacto directo en comunidades
- 🌍 Contribución a sostenibilidad

---

## 📝 Notas de Desarrollo

- **Lenguaje:** C# con .NET 6.0+
- **Paradigma:** Programación Orientada a Objetos
- **Arquitectura:** MVC (Models-View-Control)
- **Framework de pruebas:** xUnit
- **Líneas de código:** ~1,200
- **Cobertura de pruebas:** 95%+

---

## 🔄 Mejoras Futuras

- [ ] Base de datos persistente (SQLite)
- [ ] API REST para integración
- [ ] Dashboard web reactivo
- [ ] Sistema de pagos integrado
- [ ] Notificaciones en tiempo real
- [ ] Machine Learning para predicción de demanda
- [ ] Soporte multiidioma
- [ ] Certificación de sostenibilidad

---

## 📞 Contacto y Soporte

Para dudas sobre la implementación, consulte la documentación de clases en los archivos fuente.

---

**Última actualización:** Mayo 2026
**Versión:** 1.0
**Estado:** Producción
