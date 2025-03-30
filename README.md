📌 Documentación de la API - WalletAPI
📖 Descripción
La WalletAPI permite gestionar billeteras digitales, realizar transferencias de saldo y consultar transacciones.

🔗 Endpoints Disponibles
1️⃣ Gestión de Billeteras (Wallets)
📌 Crear una billetera
Endpoint: POST /api/wallets

Descripción: Crea una nueva billetera con saldo inicial.

Ejemplo de Request (JSON):

{
  "name": "Mi Billetera",
  "balance": 100.00
}

Respuesta esperada (201 Created):

{
  "id": 1,
  "name": "Mi Billetera",
  "balance": 100.00
}

📌 Obtener todas las billeteras
Endpoint: GET /api/wallets

Descripción: Retorna una lista de todas las billeteras disponibles.

Respuesta esperada (200 OK):

[
  {
    "id": 1,
    "name": "Mi Billetera",
    "balance": 100.00
  }
]

📌 Obtener una billetera por ID
Endpoint: GET /api/wallets/{id}

Descripción: Obtiene información de una billetera específica.

Ejemplo de Respuesta (200 OK):

{
  "id": 1,
  "name": "Mi Billetera",
  "balance": 100.00
}

Errores posibles:

404 Not Found → La billetera no existe.

📌 Actualizar el saldo de una billetera
Endpoint: PUT /api/wallets/{id}

Descripción: Permite actualizar el saldo de una billetera.

Ejemplo de Request (JSON):

{
  "balance": 150.00
}

Respuesta esperada (200 OK):

{
  "id": 1,
  "name": "Mi Billetera",
  "balance": 150.00
}

Errores posibles:

400 Bad Request → Si el saldo es menor a 0.

404 Not Found → Si la billetera no existe.

📌 Eliminar una billetera
Endpoint: DELETE /api/wallets/{id}

Descripción: Elimina una billetera de la base de datos.

Respuesta esperada (204 No Content)

Errores posibles:

404 Not Found → Si la billetera no existe.

2️⃣ Transferencias de Saldo
📌 Realizar una transferencia
Endpoint: POST /api/wallets/transfer

Descripción: Transfiere saldo de una billetera a otra.

Ejemplo de Request (JSON):

{
  "fromWalletId": 1,
  "toWalletId": 2,
  "amount": 50.00
}

Respuesta esperada (200 OK):

{
  "message": "Transferencia exitosa"
}

Errores posibles:

400 Bad Request → Si el monto es menor o igual a 0.

400 Bad Request → Si la billetera de origen no tiene saldo suficiente.

404 Not Found → Si alguna de las billeteras no exist

3️⃣ Transacciones
📌 Obtener transacciones de una billetera
Endpoint: GET /api/wallets/{id}/transactions

Descripción: Obtiene el historial de transacciones de una billetera.

Ejemplo de Respuesta (200 OK):

[
  {
    "id": 1,
    "walletId": 1,
    "amount": -50.00,
    "type": "Débito",
    "createdAt": "2024-03-29T10:00:00Z"
  },
  {
    "id": 2,
    "walletId": 2,
    "amount": 50.00,
    "type": "Crédito",
    "createdAt": "2024-03-29T10:00:01Z"
  }
]

Errores posibles:

404 Not Found → Si la billetera no existe.

⚙️ Configuración y Uso
🚀 Instalación y Ejecución
Clonar el repositorio

git clone https://github.com/calderonwh/WalletAPI.git
cd wallet-api

Configurar la base de datos (SQL Server)

Modificar el archivo appsettings.json con la cadena de conexión correcta.

Ejecutar migraciones

dotnet ef database update

Iniciar la API

dotnet run

Abrir en el navegador

Acceder a Swagger: http://localhost:5144/swagger/index.html

🛠 Tecnologías Utilizadas
.NET 8

Entity Framework Core

SQL Server

xUnit y Moq para pruebas unitarias

WebApplicationFactory para pruebas de integración

📌 Notas Finales
La API incluye manejo de errores, asegurando que las respuestas sean claras.

Se realizaron pruebas unitarias e integración para garantizar la estabilidad del sistema.
