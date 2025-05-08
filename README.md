# EmployeeBackend

Este proyecto es una API para la gestión de empleados. Permite realizar operaciones CRUD (crear, leer, actualizar y eliminar) sobre empleados, así como consultar empleados contratados después de una fecha específica. La aplicación está desarrollada con .NET y utiliza una arquitectura modular para garantizar escalabilidad y mantenibilidad.

## Características principales

- Gestión de empleados: crear, editar, eliminar y consultar empleados.
- Consulta de empleados contratados después de una fecha específica.
- Arquitectura modular basada en capas: API, Aplicación, Dominio e Infraestructura.
- Pruebas unitarias implementadas con xUnit.

---

## Instrucciones de instalación

Sigue estos pasos para instalar y ejecutar la aplicación en tu entorno local:

### 1. Clonar el repositorio
Clona este repositorio en tu máquina local utilizando el siguiente comando:

```bash
git clone https://github.com/mandrescatalan/EmployeeBackend.git
```

### 2. Instalar dependencias
Navega al directorio del proyecto y ejecuta el siguiente comando para restaurar las dependencias necesarias:

```bash
cd EmployeeBackend
dotnet restore
```

### 3. Ejecutar la aplicación
Inicia la aplicación con el siguiente comando:

```bash
dotnet run
```

Esto iniciará el servidor en `http://localhost:5000/` donde podrás interactuar con la API.

---

## Arquitectura de la aplicación

La aplicación sigue una arquitectura **Clean Architecture** con las siguientes capas principales:

### **1. Core**
La carpeta `Core` está dividida en dos subcapas principales: `Domain` y `Application`.

#### **1.1 Domain**
- Contiene las **entidades del dominio**, como `Employee`, que representan los objetos principales del negocio.

#### **1.2 Application**
- Define las **interfaces de repositorio**, como `IEmployeeRepository`, que abstraen el acceso a la base de datos.
- Contiene los **servicios de aplicación**, como `IEmployeeService` y su implementación, que encapsulan la lógica de negocio y coordinan las operaciones entre las diferentes capas.
- Define los **casos de uso** o **interactores**, que representan las acciones específicas que la aplicación puede realizar, como "Obtener empleados contratados después de una fecha".
- Incluye los **DTOs (Data Transfer Objects)** para transferir datos entre las capas de manera eficiente.

### **2. Infrastructure**
- Implementa las interfaces definidas en la capa `EmployeeBackend.Application`, como `EmployeeRepository`, utilizando **Entity Framework Core** para interactuar con la base de datos.
- Incluye la configuración del contexto de la base de datos (`EmployeeDbContext`), donde se definen las tablas, relaciones y configuraciones específicas de la base de datos.
- Maneja la persistencia de datos y la integración con servicios externos, si es necesario.

### **3. API**
- Proporciona los **controladores**, como `EmployeeController`, que exponen los endpoints de la API para interactuar con la aplicación.
- Maneja las solicitudes HTTP (GET, POST, PUT, DELETE) y delega la lógica de negocio a los servicios definidos en la capa `Core.Application`.
- Incluye configuraciones específicas de la API, como el registro de rutas, middleware y configuración de dependencias.

Esta estructura asegura una separación clara de responsabilidades, facilitando la escalabilidad, mantenibilidad y pruebas del sistema.

---

## Decisiones técnicas

1. **Clean Architecture**:
   - Se implementó Clean Architecture para garantizar una separación clara de responsabilidades entre las capas, facilitando la escalabilidad, mantenibilidad y pruebas del sistema.

2. **Patrón Repositorio**:
   - Se utilizó el patrón repositorio para abstraer el acceso a la base de datos, mejorar la mantenibilidad y desacoplar la lógica de negocio de la infraestructura.

3. **Pruebas Unitarias**:
   - Se implementaron pruebas unitarias utilizando xUnit y Moq para validar la lógica de negocio y garantizar la calidad del código.

4. **Inyección de Dependencias**:
   - Se utilizó inyección de dependencias para desacoplar las dependencias entre las capas, facilitando la extensibilidad y las pruebas unitarias.

---

## Licencia

1. *Nombre del Proyecto:* EmployeeBackend
2. *Descripción:* API para la gestión de empleados.
3. *Créditos:* Michael Andres Catalan Gaviria.
