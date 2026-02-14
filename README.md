# Popular Seguros – Prueba Técnica

Proyecto desarrollado como parte de la prueba técnica para **Popular Seguros**. 

---

## Información General

- **Frontend:** React + Vite  
- **Backend:** .NET 10.0.0 Web API
- **Versiones utilizadas:**
  - Visual Studio 2026
  - Node.js 18+
  - Vite 5+
  - SQL Management 22
  - SQL Server 2022+

---

# Contenido

- [Preguntas](#preguntas)
- [Ejecución](#ejecución)
  - [Frontend](#frontend)
  - [Backend](#backend)
- [Login](#login)
- [Producción](#producción)

---

# Preguntas

## ¿Qué es una arquitectura de microservicio y cómo se diferencia de una arquitectura monolítica?

La arquitectura de microservicios se trata de construir una aplicación como un conjunto de pequeños servicios independientes que se comunican entre sí mediante APIs. Cada microservicio gestiona una funcionalidad específica y puede desplegarse, escalarse y mantenerse de forma autónoma, asi que en teoría si un microservicio falla, los demás deben seguir funcionando.

Por otro lado, una arquitectura monolítica concentra todas las funcionalidades en una sola aplicación fuertemente acoplada. Esto provoca que cualquier cambio o fallo pueda afectar todo el sistema.

---

## Mencione al menos 2 patrones de diseño y explique brevemente

### Patrón Singleton
Es un patrón de creación que asegura que una clase solo pueda tener una instancia única y que exista un acceso global a ella. Se aplica en situaciones donde se necesita un único objeto en todo el sistema, como en la gestión de configuraciones o en la conexión a una base de datos.

### Patrón Observer
Se trata de un patrón de comportamiento que establece un sistema de suscripción, permitiendo que varios objetos reciban notificaciones automáticamente cuando otro objeto cambia su estado.

---

## ¿Cuál es la diferencia entre una base de datos relacional y una no relacional?

### Base de Datos Relacional

Las bases de datos relacionales organizan la información en tablas compuestas por filas y columnas, donde los datos pueden relacionarse entre sí mediante llaves foráneas. Utilizan el lenguaje SQL para realizar consultas, inserciones, actualizaciones y eliminaciones de información.

### Base de Datos No Relacional

Las bases de datos no relacionales, en cambio, ofrecen una estructura más flexible para el almacenamiento de la información. No requieren un esquema fijo y permiten almacenar datos en distintos formatos como documentos JSON, estructuras clave-valor o colecciones. 

---

# Ejecución

---

## Frontend

### Clonar repositorio

```bash
git clone https://github.com/Christofer2002/popular-seguros-polizas.git
```

### Entrar a la carpeta web e instalar dependencias

```bash
cd popular-seguros-polizas/popular-seguros-web/
```

```bash
npm install
```

### Ejecutar entorno de desarrollo

```bash
npm run dev
```

> [!NOTE]
> Verificar que el proyecto frontend se esté ejecutando en el puerto **5173**, ya que la configuración de CORS del backend permite solicitudes únicamente desde ese origen.

## Backend

- En Visual Studio, abrir la solución **PopularSeguros.Microservicios.slnx** en la carpeta **PopularSeguros.Microservicios**, después:

    - Click derecho en la solución PopularSeguros.Microservicios.

    - Propiedades.

    - Seleccionar "Varios proyectos de inicio" y seleccionar, Poliza, Cliente y Autenticacion.

    - Iniciar proyecto.

Se creó un Data Seeder que inserta datos iniciales de las tablas si la base de datos no se encuentra o está vacía, así que automaticamente se realiza la migración completa + inserción de datos.

> [!NOTE]
> Se creó un Data Seeder que inserta datos iniciales de las tablas si la base de datos no se encuentra o está vacía, así que automaticamente se realiza la migración completa + inserción de datos al iniciar los microservicios.

# Login

- Claves que se proveen para ingresar al sistema:

    - **Usuario**: allfernandez
    - **Contraseña**: Popularseguros2026

<img width="1747" height="749" alt="image" src="https://github.com/user-attachments/assets/24294204-d1f3-4a21-981c-322726fe69b1" />

# Producción

El sistema también se encuentra desplegado en un entorno de producción y puede ser accedido desde el siguiente enlace:

https://devbychris.com/popular-seguros-polizas/web/

En este entorno se encuentran configurados:

- Microservicios desplegados en VPS
- API Gateway por path mediante Nginx
- Comunicación segura mediante HTTPS
