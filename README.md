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
* dbo.orders: Proceso de facturación (item 2)
* dbo.personal_information: Aloja la informacion del usuario
* dbo.products: Listado de productos ofertados
* dbo.shopping_cart: Productos agregados al carro de compras

La verificación la realicé mediante Postman, donde este me muestra el archivo JSON de los productos que voy agregando al carrito y se descuentan del listado de productos disponibles. Asi como tambien, agrega y suma el precio de los productos solicitados. 

**Observaciones**
* Debido a que se solicitaba el desarrollo del comercio electronico en .net core C#, entendí que debía realizarse solo la parte de back end y generar el consumo de los servicios. Por tanto, la parte de front end no la realicé pero entiendo que, por el trabajo que he realizaco en la practica, utilizamos Angular en esa parte.
* Entiendo de Docker, lo usé en la universidad para crear contenedores y dividir pool de redes mediante el uso VM Virtual Box. Sin embargo, no estaba seguro del proceso de alojar estos servicios en contenedores para este caso. Creo que era crear la imagen en el cmd (docker build -t nombreimagen) mediante los comandos 

*Docker Desktop*
FROM microsoft/dotnet:2.1-sdk     ->>Software SDK de microsoft

WORKDIR /app                      ->> My app

COPY ./publish .                  ->>Carpeta  publish creada dentro de los archivos API de la tienda de pagos

ENTRYPOINT ["dotnet", "store.dll"]  ->> Apuntador al dll de los archivos de la API

Finalmente, verificar que la imagen haya sido creada en el cmd con el comando (docker images) y luego subir el contenedor con el comando: 

*cmd*
docker run -p 5000:80 nombreimagen

*Los items faltantes pude investigarlos fue debido a tiempo. Mi maquina cuenta con Core i3, 8 RAM, estado sólido y Windows 8 por lo que tuve muchos inconvenientes con la instalación de los aspectos técnicos. El PC de la empresa era ideal, pero no contaba con los permisos para la instalación de dichos softwares. 

¡Muchas gracias!
