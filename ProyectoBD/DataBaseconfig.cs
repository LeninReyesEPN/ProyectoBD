using System;

namespace ProyectoBD
{
    public static class DatabaseConfig
    {
        // Método para obtener la cadena de conexión basada en los parámetros proporcionados
        public static string GetConnectionString(string userId, string password)
        {
            // Construir la cadena de conexión utilizando los parámetros
            return $"Data Source=//localhost:1521/orcl;User Id={userId};Password={password};";
        }
    }
}
