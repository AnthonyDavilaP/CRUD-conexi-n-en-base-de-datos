CRUD con Conexión a Base de Datos

Este proyecto es una aplicación de consola desarrollada en C# que implementa operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre una base de datos utilizando Entity Framework 6. Fue realizado como parte de la materia de Programación Orientada a Objetos.

📌 Descripción

La aplicación permite gestionar registros en una base de datos mediante una interfaz de consola. Utiliza Entity Framework 6 para interactuar con la base de datos y realizar las operaciones CRUD.

🛠️ Tecnologías Utilizadas

C#

Entity Framework 6

Windows Forms

SQL Server

🔧 Requisitos

Microsoft Visual Studio 2019 o superior

SQL Server Express o superior

.NET Framework 4.7.2 o superior

🚀 Instrucciones de Uso

Clona este repositorio en tu máquina local:

git clone https://github.com/AnthonyDavilaP/CRUD-conexi-n-en-base-de-datos.git


Abre el proyecto en Microsoft Visual Studio.

Configura la cadena de conexión en el archivo App.config para que apunte a tu base de datos SQL Server.

Compila y ejecuta el proyecto.

Sigue las instrucciones en la consola para realizar operaciones CRUD.

📝 Funcionalidades

Agregar nuevos registros a la base de datos.

Visualizar todos los registros almacenados.

Actualizar información de registros existentes.

Eliminar registros de la base de datos.

🖥️ Formularios

La aplicación cuenta con varios formularios que permiten interactuar con la base de datos:

Formulario de Productos: Permite agregar, editar y eliminar productos.

Formulario de Bodegas: Permite gestionar los registros de bodegas y visualizarlas.

Formulario de Clientes: Permite registrar y visualizar los clientes.

📄 Estructura del Proyecto

El proyecto está organizado de la siguiente manera:

WinFormsEF6Demo.sln: Archivo de solución de Visual Studio.

App.config: Configuración de la aplicación, incluyendo la cadena de conexión a la base de datos.

README.md: Este archivo de documentación.

WinFormsEF6Demo: Proyecto principal que contiene la lógica de la aplicación.
