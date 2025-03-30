馃搶 Documentaci贸n de la API - WalletAPI
馃摉 Descripci贸n
La WalletAPI permite gestionar billeteras digitales, realizar transferencias de saldo y consultar transacciones.

馃敆 Endpoints Disponibles
1锔忊儯 Gesti贸n de Billeteras (Wallets)
馃搶 Crear una billetera
Endpoint: POST /api/wallets

Descripci贸n: Crea una nueva billetera con saldo inicial.

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
馃搶 Obtener todas las billeteras
Endpoint: GET /api/wallets

Descripci贸n: Retorna una lista de todas las billeteras disponibles.

Respuesta esperada (200 OK):

[
  {
    "id": 1,
    "name": "Mi Billetera",
    "balance": 100.00
  }
]
馃搶 Obtener una billetera por ID
Endpoint: GET /api/wallets/{id}

Descripci贸n: Obtiene informaci贸n de una billetera espec铆fica.

Ejemplo de Respuesta (200 OK):

{
  "id": 1,
  "name": "Mi Billetera",
  "balance": 100.00
}
Errores posibles:

404 Not Found 鈫?La billetera no existe.

馃搶 Actualizar el saldo de una billetera
Endpoint: PUT /api/wallets/{id}

Descripci贸n: Permite actualizar el saldo de una billetera.

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

400 Bad Request 鈫?Si el saldo es menor a 0.

404 Not Found 鈫?Si la billetera no existe.

馃搶 Eliminar una billetera
Endpoint: DELETE /api/wallets/{id}

Descripci贸n: Elimina una billetera de la base de datos.

Respuesta esperada (204 No Content)

Errores posibles:

404 Not Found 鈫?Si la billetera no existe.

2锔忊儯 Transferencias de Saldo
馃搶 Realizar una transferencia
Endpoint: POST /api/wallets/transfer

Descripci贸n: Transfiere saldo de una billetera a otra.

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

400 Bad Request 鈫?Si el monto es menor o igual a 0.

400 Bad Request 鈫?Si la billetera de origen no tiene saldo suficiente.

404 Not Found 鈫?Si alguna de las billeteras no existe.

3锔忊儯 Transacciones
馃搶 Obtener transacciones de una billetera
Endpoint: GET /api/wallets/{id}/transactions

Descripci贸n: Obtiene el historial de transacciones de una billetera.

Ejemplo de Respuesta (200 OK):

[
  {
    "id": 1,
    "walletId": 1,
    "amount": -50.00,
    "type": "D茅bito",
    "createdAt": "2024-03-29T10:00:00Z"
  },
  {
    "id": 2,
    "walletId": 2,
    "amount": 50.00,
    "type": "Cr茅dito",
    "createdAt": "2024-03-29T10:00:01Z"
  }
]
Errores posibles:

404 Not Found 鈫?Si la billetera no existe.

鈿欙笍 Configuraci贸n y Uso
馃殌 Instalaci贸n y Ejecuci贸n
Clonar el repositorio

git clone https://github.com/calderonwh/WalletAPI.git
cd wallet-api
Configurar la base de datos (SQL Server)

Modificar el archivo appsettings.json con la cadena de conexi贸n correcta.

Ejecutar migraciones

dotnet ef database update
Iniciar la API

dotnet run
Abrir en el navegador

Acceder a Swagger: http://localhost:5144/swagger/index.html

馃洜 Tecnolog铆as Utilizadas
.NET 8

Entity Framework Core

SQL Server

xUnit y Moq para pruebas unitarias

WebApplicationFactory para pruebas de integraci贸n

馃搶 Notas Finales
La API incluye manejo de errores, asegurando que las respuestas sean claras.

Se realizaron pruebas unitarias e integraci贸n para garantizar la estabilidad del sistema.