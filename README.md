# TUYA S.A.
## Store
### API para pagos .net core C#
### Prueba Técnica 
### Desarrollado por: Juan Camilo Jojoa Sánchez
### Practicante de Tecnología
### Universidad de Antioquia

A continuación describo algunas observaciones importantes de la prueba técnica.


**Objetivo**
*La solución contiene los pedidos hacia el Endopint Rest - base de datos SQL. Los datos de mi base datos SQl Server 2017 se encuentran en el archivo appsettings.json

**Software**
*Utilicé Visual Studio 2019
*SQL Server 2017 debido a que cuento con Windows 8.1 Pro y este no soporta SQL Server 2019.
*Utilicé el software generador de peticiones Postman
*Microsoft SQL Management Studio v18.9.1

**Descripción**
La carpeta Controllers de Visual Studio contiene las tareas: Informacion personal de usuario, listado de productos, agregar al carro de compras, obtener el carro de compras, proceso de orden y detalles de la orden. CAda tarea hace la consulta hacia la base de datos que llamé store. Esta contiene cuatro tablas:
* dbo.orders
* dbo.personal_information
* dbo.products
* dbo.shopping_cart