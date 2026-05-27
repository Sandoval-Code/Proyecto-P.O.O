# Instrucciones de Compilación y Ejecución

## Prerequisitos

- **Windows 10/11** con .NET SDK 6.0 o superior instalado
- **Visual Studio Code** (recomendado) o **Visual Studio 2022**

### Verificar instalación de .NET

```powershell
dotnet --version
```

---

## Compilación y Ejecución

### 1. Restaurar Dependencias

```powershell
cd "c:\Users\USER\Desktop\POO-HK"
dotnet restore
```

### 2. Compilar el Proyecto

```powershell
dotnet build
```

**Salida esperada:**
```
Build started...
Build succeeded.
```

### 3. Ejecutar la Aplicación

```powershell
dotnet run
```

**La aplicación mostrará el menú interactivo principal.**

---

## Ejecución de Pruebas Unitarias

### Ejecutar todas las pruebas

```powershell
dotnet test
```

### Ejecutar con salida detallada

```powershell
dotnet test --verbosity detailed
```

### Ejecutar pruebas específicas

```powershell
dotnet test --filter "DestinoTests"
```

---

## Estructura de Archivos

```
POO-HK/
├── Models.cs                 # Clases de dominio
├── SistemaTurismo.cs         # Orquestador del sistema
├── Program.cs                # Aplicación consola
├── UnitTests.cs              # Pruebas unitarias
├── TurismoSostenible.csproj  # Configuración del proyecto
└── README.md                 # Documentación
```

---

## Solución de Problemas

### Error: `dotnet: No se reconoce como comando`

**Solución:** Reinstalar .NET SDK desde: https://dotnet.microsoft.com/download

### Error: `nuget restore failed`

**Solución:** Ejecutar:
```powershell
dotnet nuget locals all --clear
dotnet restore
```

### Error en pruebas: `xunit not found`

**Solución:** Restaurar dependencias:
```powershell
dotnet restore
```

---

## Opciones de Compilación Avanzadas

### Compilar en Release (optimizado)

```powershell
dotnet build -c Release
dotnet run -c Release
```

### Limpiar artefactos de compilación

```powershell
dotnet clean
```

---

## Entrada de Ejemplo en la Aplicación

1. Selecciona opción **1** para gestionar destinos
2. Selecciona opción **2** para ver visitantes registrados
3. Selecciona opción **4** para crear una reserva
4. Selecciona opción **5** para ver reportes
5. Selecciona opción **6** para salir

---

## Notas

- El proyecto usa **.NET 6.0** como target framework
- Las pruebas usan **xUnit 2.6.3**
- Compatible con Windows, Linux y macOS
- Todos los archivos están en **UTF-8**

---

## Documentación Adicional

Ver **README.md** para:
- Descripción del sistema
- Casos de uso
- Características principales
- Principios de POO implementados

---

**Fecha:** Mayo 2026  
**Versión:** 1.0  
**Estado:** Listo para Producción
