🛍️ MiniShop — Sistema de Productos y Ventas (.NET + WinForms)

📖 Descripción general
MiniShop es un sistema de ejemplo desarrollado con .NET 8, compuesto por:
- Una API Web (ASP.NET Core) que gestiona autenticación, productos y ventas.
- Una aplicación de escritorio WinForms que consume la API.
- Base de datos SQLite integrada.

El objetivo es demostrar una solución End-to-End con autenticación JWT y comunicación cliente-servidor mediante JSON.

⚙️ Requerimientos
- Visual Studio 2022 (v17.8 o superior)
- .NET 8 SDK
- Git (opcional)
- SQLite (ya integrado)

🧩 Estructura de la solución
MiniShop.sln
 - MiniShop.Domain/  → Entidades principales
 - MiniShop.Infrastructure/   → EF Core, DbContext, migraciones
 - MiniShop.WebApi/    → API con JWT y Swagger
 - MiniShop.WinFormsClient/ → Aplicación WinForms (frontend)

🚀 Ejecución de la solución
1. Abrir la solución MiniShop.sln en Visual Studio.
2. Establecer arranque múltiple:
   - MiniShop.WebApi → Iniciar
   - MiniShop.WinFormsClient → Iniciar
3. Ejecutar con Ctrl + F5 (sin depuración).
   Se abrirán:
   - La API (Swagger)
   - El cliente WinForms (Login).

🌐 Configuración de la URL de la API
Si el puerto de la API cambia, actualizarlo en:
MiniShop.WinFormsClient/Services/AppConfig.cs

Ejemplo:
public static string ApiBaseUrl = "https://localhost:7155";

🧠 Flujo funcional
1. Login o registro de usuario (JWT).
2. CRUD de productos con imagen.
3. Registro de ventas tipo carrito.
4. Reporte por rango de fechas.
5. Logout / cierre de sesión.

💾 Base de datos
- SQLite (archivo MiniShop.db creado automáticamente).
- No requiere configuración manual.

🔒 Seguridad
- Implementación JWT (JSON Web Token).
- Endpoints protegidos con autorización.

🧰 Tecnologías
.NET 8 | ASP.NET Core Web API | WinForms | EF Core (SQLite) | Swagger | C# 12

👨‍💻 Autor
Andrés David Losada Valderrama
Proyecto técnico — MiniShop (.NET + WinForms)
Noviembre 2025
