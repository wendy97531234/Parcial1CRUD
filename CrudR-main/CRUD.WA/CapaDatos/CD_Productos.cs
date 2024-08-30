using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    // La clase CD_Productos representa la capa de acceso a datos para los productos.
    public class CD_Productos
    {
        // Se crea una instancia de CD_Conexion para manejar la conexión a la base de datos.
        private CD_Conexion conexion = new CD_Conexion();

        // SqlDataReader para leer datos de la base, DataTable para almacenar resultados y SqlCommand para ejecutar comandos SQL.
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        // Método para obtener todos los productos desde la base de datos.
        public DataTable Mostrar()
        {
            // Se abre la conexión, se configura el comando para ejecutar un procedimiento almacenado y se carga la tabla con los datos.
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        // Método para insertar un nuevo producto en la base de datos.
        public void Insertar(string nombre, string desc, string marca, double precio, int stock)
        {
            // Se configura y ejecuta el comando para insertar un producto utilizando un procedimiento almacenado.
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "InsetarProductos"; 
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@descrip", desc);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock); 

            comando.ExecuteNonQuery();

            // Se limpian los parámetros para evitar conflictos en futuras llamadas.
            comando.Parameters.Clear();
        }

        // Método para editar un producto existente en la base de datos.
        public void Editar(string nombre, string desc, string marca, double precio, int stock, int id)
        {
            // Se configura y ejecuta el comando para actualizar un producto utilizando un procedimiento almacenado.
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EditarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@descrip", desc);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();

            // Se limpian los parámetros para evitar conflictos en futuras llamadas.
            comando.Parameters.Clear();
        }

        // Método para eliminar un producto de la base de datos.
        public void Eliminar(int id)
        {
            // Se configura y ejecuta el comando para eliminar un producto utilizando un procedimiento almacenado.
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idpro", id);

            comando.ExecuteNonQuery();

            // Se limpian los parámetros para evitar conflictos en futuras llamadas.
            comando.Parameters.Clear();
        }

        // Método para buscar un producto por nombre en la base de datos.
        public DataTable Buscar(string nombre)
        {
            // Se configura y ejecuta el comando para buscar un producto por nombre utilizando un procedimiento almacenado.
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "BuscarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);

            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();

            // Se limpian los parámetros para evitar conflictos en futuras llamadas.
            comando.Parameters.Clear();

            return tabla;
        }
    }
}
