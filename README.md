# 💰 SavingBack

**SavingBack** es una aplicación web para la **gestión y planificación de ahorros personales**, desarrollada con **ASP.NET Core .NET 8** y **Angular**.

La aplicación permite a los usuarios crear y administrar metas de ahorro, registrar ingresos y egresos, consultar los movimientos asociados a cada meta y realizar un seguimiento del progreso financiero de forma organizada.

El proyecto fue desarrollado aplicando principios de desarrollo de APIs REST, autenticación y autorización, persistencia de datos mediante Entity Framework Core, seguridad basada en **JWT (JSON Web Tokens)** y servicios de correo electrónico mediante **SMTP**.

---

## 🚀 Características

### 🎯 Gestión de metas de ahorro

Los usuarios pueden crear y administrar diferentes metas de ahorro.

Cada meta permite definir información como:

* Nombre de la meta.
* Descripción.
* Valor objetivo.
* Fecha límite.
* Estado de la meta.
* Progreso del ahorro.

Esto permite llevar un seguimiento individual de cada objetivo financiero.

---

### 💵 Gestión de ingresos y egresos

SavingBack permite registrar los movimientos financieros realizados por el usuario.

Los movimientos pueden clasificarse como:

* **Ingresos**
* **Egresos**

Cada movimiento contiene información relevante para mantener un historial organizado de las operaciones realizadas.

---

### 📊 Movimientos asociados a una meta

Los movimientos pueden relacionarse directamente con una meta de ahorro.

Esto permite consultar:

* Ingresos destinados a una meta.
* Egresos asociados.
* Total acumulado.
* Movimientos realizados.
* Progreso de la meta.

De esta manera, el usuario puede conocer cómo cada movimiento afecta el progreso de sus objetivos.

---

### 📧 Notificaciones por correo electrónico

SavingBack incorpora un servicio de envío de correos electrónicos mediante **SMTP**, permitiendo enviar notificaciones relacionadas con las operaciones realizadas dentro de la aplicación.

Por ejemplo, cuando el usuario **añade un ahorro a una meta**, el sistema puede enviar una notificación por correo informando sobre el movimiento realizado y el nuevo progreso de la meta.

Estas notificaciones pueden incluir información como:

* Nombre de la meta.
* Valor del ahorro añadido.
* Fecha del movimiento.

El servicio de correo se encuentra integrado en el backend y utiliza la configuración SMTP definida para la aplicación.

```text id="e2m5vz"
Usuario
   │
   │ Añade ahorro
   ▼
ASP.NET Core API
   │
   ├── Registra movimiento
   │
   ├── Actualiza progreso de la meta
   │
   └── Servicio SMTP
          │
          ▼
      Correo electrónico
          │
          ▼
        Usuario
```

---

### 🔐 Autenticación y autorización

La aplicación implementa un sistema de autenticación basado en **JWT (JSON Web Tokens)**.

Entre las funcionalidades relacionadas con seguridad se encuentran:

* Registro de usuarios.
* Inicio de sesión.
* Generación de tokens JWT.
* Validación de autenticación.
* Protección de endpoints.
* Manejo de sesiones autenticadas.
* Protección de información asociada al usuario.

Los endpoints protegidos requieren un token válido para poder ser consumidos.

---

## 🛠️ Tecnologías utilizadas

### Backend

| Tecnología                | Uso                            |
| ------------------------- | ------------------------------ |
| **C#**                    | Lenguaje principal             |
| **.NET 8**                | Framework de desarrollo        |
| **ASP.NET Core Web API**  | Desarrollo de API REST         |
| **Entity Framework Core** | ORM y acceso a datos           |
| **SQL Server**            | Base de datos                  |
| **JWT**                   | Autenticación y autorización   |
| **SMTP**                  | Envío de correos electrónicos  |
| **Swagger / OpenAPI**     | Documentación y pruebas de API |

### Frontend

| Tecnología       | Uso                             |
| ---------------- | ------------------------------- |
| **Angular**      | Desarrollo de la aplicación web |
| **TypeScript**   | Lenguaje principal del frontend |
| **HTML5**        | Estructura                      |
| **CSS3**         | Estilos                         |
| **Tailwind CSS** | Diseño y estilos de interfaz    |

---

## 🏗️ Arquitectura

El proyecto utiliza una arquitectura basada en una separación entre la aplicación cliente y la API backend.

```text id="r3m3z6"
┌───────────────────────────────┐
│           Angular             │
│          Frontend             │
│                               │
│  Components                   │
│  Services                     │
│  Stores / State Management    │
│  Routing                      │
└───────────────┬───────────────┘
                │
                │ HTTP / REST
                │ JWT
                ▼
┌───────────────────────────────┐
│        ASP.NET Core 8         │
│           Web API             │
│                               │
│  Controllers                  │
│  Services                     │
│  Authentication               │
│  Authorization                │
│  Business Logic               │
│  Email / SMTP Service         │
└───────────────┬───────────────┘
                │
                │ Entity Framework Core
                ▼
┌───────────────────────────────┐
│          SQL Server           │
│                               │
│  Users                        │
│  Goals                        │
│  Movements                    │
│  Financial Data               │
└───────────────────────────────┘

                │
                │ SMTP
                ▼
┌───────────────────────────────┐
│       Email Provider          │
│                               │
│  Email Notifications          │
└───────────────────────────────┘
```

---

## 🔄 Flujo de autenticación

El proceso de autenticación funciona mediante tokens JWT.

```text id="272oja"
Usuario
   │
   ▼
Login
   │
   ▼
ASP.NET Core API
   │
   ├── Validación de credenciales
   │
   ▼
Generación JWT
   │
   ▼
Angular recibe token
   │
   ▼
Token almacenado
   │
   ▼
HTTP Interceptor
   │
   ▼
Authorization: Bearer <token>
   │
   ▼
Endpoint protegido
```

El frontend incorpora el token JWT en las solicitudes realizadas hacia los endpoints protegidos de la API.

---

## 📧 Flujo de notificaciones por correo

Cuando se realiza una operación que requiere una notificación, el backend procesa la información y utiliza el servicio SMTP para enviar el correo correspondiente.

Por ejemplo, al añadir un ahorro a una meta:

```text id="a7d31k"
Usuario
   │
   ▼
Añadir ahorro
   │
   ▼
API REST
   │
   ├── Validar usuario
   │
   ├── Registrar movimiento
   │
   ├── Actualizar meta
   │
   └── Obtener información del usuario
          │
          ▼
      Servicio de correo
          │
          ▼
         SMTP
          │
          ▼
     Correo enviado
```

El servicio permite centralizar la lógica relacionada con el envío de correos y mantener separada esta responsabilidad de los controladores de la API.

---

## 🗄️ Persistencia de datos

La aplicación utiliza **Entity Framework Core** como ORM para interactuar con SQL Server.

Entity Framework Core permite:

* Mapear entidades C# hacia tablas de SQL Server.
* Realizar consultas mediante LINQ.
* Crear y modificar registros.
* Gestionar relaciones entre entidades.
* Utilizar migraciones para controlar cambios en la estructura de la base de datos.

La comunicación con la base de datos se realiza mediante el contexto de Entity Framework Core.

```text id="4abl8i"
C# Entities
     │
     ▼
Entity Framework Core
     │
     ▼
SQL Server
```

---

## 📌 Principales módulos

### 🎯 Metas

* Crear metas.
* Consultar metas.
* Actualizar metas.
* Gestionar estados.
* Consultar progreso.
* Asociar movimientos.
* Actualizar el progreso al realizar nuevos ahorros.

### 💰 Movimientos

* Registrar ingresos.
* Registrar egresos.
* Consultar movimientos.
* Asociar movimientos con metas.
* Consultar movimientos específicos de una meta.
* Generar notificaciones relacionadas con determinados movimientos.

### 📧 Servicio de correo

* Configuración de servidor SMTP.
* Envío de correos desde el backend.
* Notificaciones relacionadas con operaciones financieras.
* Envío de información sobre ahorros realizados.
* Integración del servicio de correo con la lógica de negocio.

### 📊 Dashboard

El dashboard permite visualizar de forma resumida la información financiera del usuario, facilitando el seguimiento de sus metas y movimientos.

---

## 🔒 Seguridad

SavingBack implementa diferentes mecanismos para proteger los recursos de la aplicación.

Entre ellos:

* Autenticación mediante JWT.
* Autorización de endpoints.
* Validación de credenciales.
* Protección de rutas.
* Envío del token mediante `Authorization Bearer`.
* Separación de información según el usuario autenticado.
* Configuración segura de las credenciales utilizadas por el servicio SMTP.

Las credenciales y configuraciones sensibles relacionadas con el servidor de correo deben mantenerse fuera del código fuente y configurarse mediante variables de entorno o mecanismos seguros de configuración.

Los recursos protegidos de la API requieren una autenticación válida antes de permitir el acceso.

---

## 📡 API REST

El backend está construido siguiendo el enfoque REST, utilizando métodos HTTP para las diferentes operaciones:

| Método   | Operación             |
| -------- | --------------------- |
| `GET`    | Consultar información |
| `POST`   | Crear recursos        |
| `PUT`    | Actualizar recursos   |
| `DELETE` | Eliminar recursos     |

Los endpoints se encuentran documentados mediante Swagger/OpenAPI para facilitar las pruebas y el consumo de la API.

---

## 🧩 Conceptos aplicados

Durante el desarrollo de SavingBack se aplicaron diferentes conceptos de desarrollo de software:

* Programación orientada a objetos.
* Desarrollo de APIs REST.
* Arquitectura cliente-servidor.
* Entity Framework Core.
* ORM.
* LINQ.
* Inyección de dependencias.
* Autenticación y autorización.
* JWT.
* HTTP y códigos de respuesta.
* Manejo de relaciones entre entidades.
* Migraciones de Entity Framework Core.
* Gestión de estado en Angular.
* Consumo de APIs mediante HTTP.
* Interceptores HTTP.
* Routing y protección de rutas.
* Diseño responsive.
* Integración con servicios SMTP.
* Envío de correos electrónicos desde el backend.
* Separación de responsabilidades mediante servicios.

---

## ⚙️ Requisitos

Para ejecutar el proyecto localmente es necesario contar con:

* .NET 8 SDK
* Node.js
* Angular CLI
* SQL Server
* Servidor SMTP o proveedor de correo compatible
* Git

---

## ▶️ Ejecución del Backend

Clonar el repositorio:

```bash id="6qoma5"
git clone https://github.com/Ginto11/SavingBack
```

Ingresar al proyecto backend:

```bash id="o5aeg8"
cd SavingBack
```

Restaurar las dependencias:

```bash id="aohi5m"
dotnet restore
```

Configurar la cadena de conexión a SQL Server en `appsettings.json`:

```json id="7pqj5y"
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CONFIGURACION_EMAIL": {
    "HOST": "smtp.gmail.com",
    "PUERTO": 587,
    "EMAIL": "Correo",
    "PASSWORD": "Contrasena"
  },
  "ConnectionStrings": {
    "SqlServer": "Url"
  },
  "Encrypting": {
    "IV": "Vector",
    "Key": "Llave"
  },
  "Jwt": {
    "Issuer": "Url de quien emite",
    "Audience": "Url de quien recibe",
    "Key": "Llave"
  }
}
```


> ⚠️ Las credenciales SMTP no deben incluirse directamente en el repositorio. Se recomienda utilizar variables de entorno, `User Secrets` u otro mecanismo seguro de configuración. Como usar el .gitignore.

Ejecutar las migraciones:

```bash id="0hl7h2"
dotnet ef database update
```

Iniciar la API:

```bash id="38yg76"
dotnet run
```

Una vez iniciada, la API estará disponible en la URL configurada para el proyecto.

---

## ▶️ Ejecución del Frontend

Copiar proyecto Front:

```bash id="u97ojo"
git clone https://github.com/Ginto11/SavingFront
```

Ingresar al proyecto Angular:

```bash id="u97ojo"
cd SavingFront
```

Instalar dependencias:

```bash id="6ddvbo"
npm install
```

Ejecutar la aplicación:

```bash id="m1fspp"
ng serve
```

Después, abrir la URL proporcionada por Angular en el navegador.

---

## 📚 Objetivo del proyecto

SavingBack fue desarrollado con el objetivo de construir una aplicación completa para la gestión de ahorros, aplicando conocimientos de desarrollo **Full-Stack** y buenas prácticas en la construcción de aplicaciones web modernas.

El proyecto integra un frontend desarrollado con Angular con una API REST construida utilizando ASP.NET Core 8, conectada a SQL Server mediante Entity Framework Core.

Además, incorpora autenticación y autorización mediante JWT para proteger los recursos de la aplicación y garantizar que cada usuario pueda gestionar su propia información.

Como parte de la experiencia de usuario, se integró un servicio de correo electrónico mediante SMTP para enviar notificaciones relacionadas con las operaciones realizadas dentro de la plataforma, como el registro de nuevos ahorros asociados a una meta.

---

## 👨‍💻 Autor

**Nelson Muñoz**

Desarrollador Junior | Full-Stack Developer

Tecnologías principales:

`C#` · `.NET 8` · `ASP.NET Core` · `Angular` · `TypeScript` · `Entity Framework Core` · `SQL Server` · `JWT` · `SMTP` · `Tailwind CSS`

---

## 📄 Licencia

Este proyecto fue desarrollado con fines educativos y de portafolio.
