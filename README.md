# VideoClubApiRest

Instalación de herramientas

En este repositorio se presenta el funcionamiento de una api rest, los requerimientos principales a tratar son los siguientes:
El sistema debe recibir información en XML. 
Se tiene que validar que  el client_id y el object_id tengan un formato de UUID (ver qué se puede validar)
El status puede ser RENT / DELIVERY_TO_RENT / RETURN / DELIVERY_TO_RETURN.  
La fecha tiene que ser con formato AAAA/MM/DD. 

Trabajaremos en Windows y las herramientas que utilizaremos serán las siguientes:

SQL Server Management Studio 2012

Para manejo de base de datos.

Postman

Para hacer los testeos sobre la Api, uso Postman. Es una herramienta que proporciona Google que se utiliza para el testing de las APIs REST de manejo simple y eficaz, aunque puede tardar bastante en iniciar.

.NET Core 3.1 + Herramientas y extensiones de EntityFramework.

Para desarrollar nuestra API.

Instalar SQL Server Management Studio 2012

Para instalar esta herramienta nos dirigimos a esta página https://www.microsoft.com/en-us/download/details.aspx?id=29062, debajo de la presentación de los productos de Microsoft nos figura una lista seleccionable de idioma donde elegiremos el que es de nuestra preferencia y presionaremos el botón de descargar. Con el archivo.exe descargado ahora podremos ejecutarlo, donde nos aparecerá una pantalla principal de instalación, presionaremos la opción titulada: New SQL Server stand-alone installation or add features to an existing installation” y una nueva pantalla se nos mostrará, allí seleccionaremos el cuadro donde aceptamos las condiciones de licencia. Otra pantalla sobre actualizaciones del producto, allí usted podrá elegir si marcar o no el casillero “Include SQL Server product updates”, continuamos y una nueva ventana con el título “Install Setup Files” nos presenta la configuración de las herramientas que se van a descargar. En las siguientes 4 ventanas daremos en el botón “Next” y por último tendremos la ventana de “Instalación en Progreso”, cuando esta termine una ventana “Complete” nos informará que la instalación ha finalizado y podremos usar la herramienta. 

Instalar Postman

Primero debemos descargar el archivo .exe de la siguiente página https://www.postman.com/downloads/. Esta aplicación se instala automáticamente. Para usarla solo debemos ingresar una cuenta. Luego nos aparecerá una pantalla inicial de las interacciones que podremos realizar con ella.

Instalar .NET Core 3.1.X

Para poder usar Visual Studio Code 2019 nos dirigiremos a aquí https://dotnet.microsoft.com/download/dotnet-core/3.1. Seleccionaremos la versión Community, si bien instale la versión 3.1.3, la nueva actualización 3.1.8 es la que corrí en el proyecto.
Cuando seleccionemos la versión a instalar, una ventana para especificar donde queremos instalarlo aparecerá, seleccionaremos la carpeta por defecto o la que sea de nuestra preferencia, daremos al botón “Save” cuando terminemos una nueva ventana de confirmación se nos aparece y daremos en “Continue”. La siguiente pantalla nos permitirá agregar las funciones/herramientas adicionales para acompañar a .NET Core, si quiere puede seleccionar alguna y si no, haga click en el botón “Install” y se nos muestra el progreso de instalación. Cuando este proceso finalice aparecerá una pantalla indicándonos que se requiere de un reinicio, allí seleccionaremos “Restart” (asegúrese de cerrar programas que estén corriendo por detrás). Y eso es todo, puede comenzar a utilizar la herramienta.

Primeros pasos.

Crear BD

Vamos a crear nuestra base de datos en SQL Server MS 2012 y para ello correremos la herramienta que instalamos, nos aparecerá una ventana que nos indica el nombre de nuestro servidor y nuestro usuario (podremos cambiarlo), daremos en Aceptar y abriremos un archivo query en el cual ingresamos la forma que tendrá nuestra base de datos. 
Este es el script que cree:
        create database VDBD
        use VCBD
        create table RENTS(
          object_id uniqueidentifier not null primary key,
          client_id uniqueidentifier not null,
          detailssatus VARCHAR(50) not null, 
          detailsuntil VARCHAR(50) not null,);

Explicación del script

Los identificadores de las películas (object_id) y de los clientes (client_id) estarán en formato UUID, por lo tanto existen dos maneras de manejar en SQL Server este tipo de identificadores con uniqueidentifier. Además, si queremos podremos hacer que la BD las genere con: Newid() y Newsequentialid() la diferencia está en que el primero genera claves de manera aleatoria y el segundo es secuencial. A la hora de ver cuál nos conviene utilizar debemos tener en cuenta que para insertar el segundo método es el más conveniente aunque con este no se podrán realizar consultas especializadas.

Creación de proyecto en .NET Core

Una vez creada la base de datos nos queda armar el proyecto para nuestra API REST en .NET Core.
Para ello, en el menú principal que nos figura cuando iniciamos la aplicación debemos seleccionar la opción de "Crear nuevo proyecto", eso nos abrirá una buscador con opciones para indicar a qué tipo de proyecto nosotros vamos a abocarnos, buscaremos con las siguientes palabras claves "api web ASP.NET core".
Luego nos aparecerá otro menú para que el proyecto inicie con la estructura que le indiquemos, en nuestro caso será "API", para hacerlo paso a paso, eliminaremos el proyecto que nos figura dentro de nuestra Solución.

Creación de proyectos, directorios y clases.

Es necesario respetar el paso a paso de a continuación para que nuestra api funcione correctamente.
Para todos los casos de creación del proyecto deberemos ir a nuestra Solución, hacer click derecho, elegir agregar y luego dar en Nuevo Proyecto, esto nos abrirá una ventana con un buscador para poder elegir el tipo que queramos.
Para el primer proyecto colocaremos en el buscador "api" y se nos listarán unas opciones, elegiremos la que incluye el lenguaje c# y se llama ASP.NET Core Web Application. Luego aparecerá otra ventana para elegir como inicia esta aplicación, seleccionaremos la que dice API. A este proyecto lo llamaremos VideoClibApiRest.Core. 
En segundo lugar, crearemos un class Library, en el buscador colocaremos "class Library" y seleccionaremos  colocaremos el nombre VideoClibApiRest.Core.
Y en tercer lugar, otra del mismo tipo que el anterior pero la llamaremos VideoClibApiRest.Infraestructure.
Con esto tendremos una arquitectura bien definida para separar las responsabilidades que habrá en el proyecto.
Luego crearemos en el proyecto .Core las carpetas: Entities para crear cada entidad del negocio, Interfaces para las interfaces que nos servirán de comunicación entre capas de los proyectos. 
En el proyecto .Infraestructure crearemos la carpeta Data para reflejar nuestra base de datos y Repository que es la implementación de la conexión a la BD. Trata de desacoplar la lógica del negocio de la lógica de la BD. En .api creamos controllers

Inyección de Dependencias

Para la integración de cada uno de estos proyectos tendremos que ir agregando las dependencias de cada uno. Para el primero que creamos (.Api), daremos click derecho, seleccionaremos agregar y abriremos References Manager, marcaremos los proyectos .Core e .Infraestructure, y para las dependencias de .Infraestructure solo seleccionaremos el proyecto .Core (esto solo sirve para realizar test unitarios). Mientras que para .Core no generamos dependencias porque es el nivel más interno y no necesita de los demás.
Como vamos a realizar pruebas unitarias, también crearemos un proyecto, el tipo de este será Xunit.

Implementación de Inyección de Dependencias

Para usar la base de datos utilizaremos la Inyección de Dependencias, crearemos una interfaz para cada clase que tengamos en el repositorio, en este caso tendremos una que está dirigida a Alquileres que denominaremos InterfaceRentsRepository, cada clase deberá ser publica para que puedan ser usada por los controladores alojados en el proyecto .Api.
Sobre la carpeta Controllers daremos click derecho para agregar un archivo de tipo API Controller - Empty. El controlador tendrá los métodos Http y se comunicará con una interfaz.
Las interfaces se declaran de tipo private readonly y para poder usarlas debemos crear la instrucción para indicar de donde obtener la interfaz, en este caso es: using VideoClubApiRest.Core.Interfaces;
Esto se realiza por cada elemento que tengamos que corresponde a otra parte de la arquitectura en la que trabajamos.
Con la variable que representa la interfaz vamos a solicitarle que nos obtenga una respuesta a las solicitudes Http sobre nuestra base de datos. 

En Repositories crearemos RentsRepository.cs al cual le diremos que implementará la interfaz InterfaceRentsRepository (recordamos que las clases deben ser creadas como públicas). Este tomará todos los llamados de interface y se comunicara con la base de datos para traer la información en el formato solicitado.
         private readonly VCBDContext _context;
         public RentsRepository(VCBDContext context)
                {
                    _context = context;
                }
En starups.cs tenemos que decirle qué dependencias vamos a resolver, para ello nos vamos a su ConfigureServices y colocamos
Services.AddTranscient<InterfaceRentsRepository, RentsRepository>();
Esto genera una abstracción que permite estar preparados ante cambios en el tipo de base de datos usada.

Para trabajar con la base de datos y realizar un "scaffolding" debemos descargar los paquetes Microsoft.EntityFrameworkCore.SqlServer y también vamos a descargar Microsoft.EntityFrameworkCore.Tools de Nuget dentro del proyecto .Infraestructure. Y por último el paquete Microsoft.EntityFrameworkCore.Desing sobre el proyecto .Api.

Paso siguiente (para que esto funcione el código debe estar limpio, como hemos creado ciertas variables y métodos que aún no tienen asociación integra con el proyecto, lo que haremos es comentarlas por el momento, una vez que nos indica que la construcción fue exitosa podemos descomentar), vamos a crear todos los modelos de la base de datos a la carpeta Data. Con la herramienta Package Manager Console de Nuget se coloca el comando:
Scaffold-DbContext "Server=NameServer\SQLEXPRESS; Database=NameDataBase; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data

Corroborar que este comando sea arrojado en el proyecto .Infraestructure.
Este comando contiene el servidor, el nombre de la BD, la seguridad de Microsoft que tenemos, el paquete a usar, y en qué carpeta ponemos los objetos. 
Vemos que nos crea un archivo VCBDContext que tiene la definición de cada una de las tablas y una clase Rents que es modelo de la tabla de la BD, tiene todas las propiedades asociadas a la tabla RENTS pero debe llevarse a la carpeta de Entities de .Core, ¿por qué no los creamos directamente ahí? Si bien pueden crearse en ese directorio, no es apropiado. Esos paquetes de Entity Framework deben ir en la capa .Infrastructure y no el .Core, es para separar funcionalidades. 

Como vamos a usar Inyección de Dependencias, nos colocamos en el archivo VCBDContext y eliminaremos el método OnConfiguring() que incluye una mala implementación de la cadena de conexión a la base de datos.
Se puede ver que por cada modelo se crea un DBSet y en OnModelCreating tenemos las reglas definidas de las tablas.
Siguiente, vamos a colocar la siguiente línea de código en Startup.cs para indicar que ahora vamos a usar la base de datos que hemos configurado: 
          services.AddControllers();
                      services.AddDbContext<VCBDContext> (options =>
                      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                      services.AddMvc();

y modificaremos el appsettings.json configurando la ruta de conexión a la base de datos:
        "ConnectionStrings": { "DefaultConnection": ""Server=NameServer\SQLEXPRESS; Database=NameDataBase; Trusted_Connection=True;"},

Y en properties, cambiar el launchsettings.json al siguiente:
              "SistemaInscripcionUniversitaria.Api": {
                    "commandName": "Project",
                    "launchBrowser": true,
                    "launchUrl": "api",
                    "applicationUrl": "https://localhost:5001;http://localhost:5000",
                    "environmentVariables": {
                      "ASPNETCORE_ENVIRONMENT": "Development"
                    }

Colocaremos el siguiente código en el Controlador para comprobar que realmente hemos podido comunicarnos con la base de datos:
         [HttpGet]
                public async Task<IActionResult> GetRents()
                {
                    var rents = await _rentsRepository.GetRents();
                    return Ok(rents);
                }
A modo de ejemplo, se insertaron una serie de datos en nuestra base de datos para que el método HttpGet nos traiga un listado de los alquileres guardados en la base de datos.
Abriremos Postman y correremos un GET sobre la url terminada en api/rents, y vemos que el return Ok() nos devuelve 200 porque la operación ha  sido exitosa.

XML

Como nos van a ingresar datos en formato XML debemos configurar los servicios correspondientes en nuestro startup.cs para que soporte estas entradas y pueda responder correctamente, lo que haremos es agregar a services.AddControllers() lo siguiente: .AddXmlDataContractSerializerFormatters();
Con esto se nos crea un schema que sigue el patrón del .json y podremos trabajar en ambos tipos, nuestro proyecto va a responder ambas peticiones.

Validaciones

Para las validaciones, podemos trabajar con una [Apicontroller] que se coloca sobre las clases Controller que tengamos antes de las creaciones de los métodos http, nos brinda un control sobre comportamientos sobre requisitos de enrutamiento de atributos, respuestas HTTP 400 automáticas, inferencia de parámetros de origen de enlace y de llenado de formularios y nos otorga detalles del problema para códigos de error del status code.

Para crear nuestros controles personalizados, nos dirigimos a la carpeta Filters en .Infraestructure e instalamos el paquete Nuget FluentApi.ASPNetCore la versión 8.6.2 porque es la que tiene compatibilidad con la librería NETStandar que usamos y con demás herramientas.Creamos en este proyecto y la llamamos Validators, y dentro creamos las clases a validar, en nuestro la clase tendrá el nombre RentsValidator. Cuando finalicemos debemos ir a Startup.cs y agregar los validators: services.AddMvc().AddFlentValidation(options =>{options.RegisterValidatorsFromAssemplies(AppDomain.CurrentDomain.GetAssemblies());});

Unitest.

En la solución vamos a crear un nuevo proyecto del tipo xUnit. Se deben instalar los paquetes Nuget: Microsoft.AspNetCore.Mvc, Microsoft.EntityFrameworkCore y Xunit.
En este nuevo proyecto crearemos una clase que se encargará de imitarnos a nuestra base de datos y poder hacer los test allí.
Para ello es esencial crear un método que nos entregue dicha base de datos falsa y debe tener este formato:
                  private ExampleDBContext CreateDatabase()
                          {
                              DbContextOptions<ExampleDBContext > options;
                              var builder = new DbContextOptionsBuilder<ExampleDBContext >();
                              builder.UseInMemoryDatabase(Guid.NewGuid().ToString("N"));
                              options = builder.Options;
                              ExampleDBContext exampleDBContext = new ExampleDBContext (options);
                              exampleDBContext .Database.EnsureDeleted();
                              exampleDBContext .Database.EnsureCreated();
                              return exampleDBContext ;
                          }
UseInMemoryDatabase(Guid.NewGuid().ToString("N")); esto nos crea una base de datos en memoria, su parámetro es el nombre que recibirá esa base de datos.

Un ejemplo de uso realizado con POSTMAN:
GET: http://localhost:5000/api/rents
Obtenemos lo siguiente:
{
"rentId": "1",
"objectId":"4f9019ff-8b86-d088-b42d-00c04fc964f",
"clientId":"4f9019ff-8b86-d088-b42d-00c04fc964f",
"status":"RENTED",
"until":"23/08/1919"
}
