using System.Data.SqlClient;
using RepositorioDTO.Models;
using System.Data;

namespace RepositorioDTO.Repository
{
  public class ProductoRepository
  {
    private readonly string? _connection;

    public ProductoRepository(IConfiguration configuration)
    {
      _connection = configuration.GetConnectionString("connection");
    }

    public IEnumerable<Producto> ObtenerProductos()
    {
      using (SqlConnection connection = new SqlConnection(_connection))
      {
        using (SqlCommand command = new SqlCommand("ObtenerProductos", connection))
        {
          command.CommandType = CommandType.StoredProcedure;
          connection.Open();

          using (SqlDataReader reader = command.ExecuteReader())
          {
            List<Producto> productos = new List<Producto>();
            while (reader.Read())
            {
              Producto producto = new Producto
              {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
              };
              productos.Add(producto);
            }
            return productos;
          }
        }
      }
    }

    public void ActualizarProducto(Producto producto)
    {
      using (SqlConnection connection = new SqlConnection(_connection))
      {
        using (SqlCommand command = new SqlCommand("ActualizarProducto", connection))
        {
          command.CommandType = CommandType.StoredProcedure;
          command.Parameters.AddWithValue("@Id", producto.Id);
          command.Parameters.AddWithValue("@Nombre", producto.Nombre);
          command.Parameters.AddWithValue("@Precio", producto.Precio);
          connection.Open();
          command.ExecuteNonQuery();
        }
      }
    }

    public void CrearProducto(Producto producto)
    {
      using (SqlConnection connection = new SqlConnection(_connection))
      {
        using (SqlCommand command = new SqlCommand("AgregarProducto", connection))
        {
          command.CommandType = CommandType.StoredProcedure;
          command.Parameters.AddWithValue("@Nombre", producto.Nombre);
          command.Parameters.AddWithValue("@Precio", producto.Precio);
          connection.Open();
          command.ExecuteNonQuery();
        }
      }

    }

    public void EliminarProducto(int id)
    {
      using (SqlConnection connection = new SqlConnection(_connection))
      {
        using (SqlCommand command = new SqlCommand("EliminarProducto", connection))
        {
          command.CommandType = CommandType.StoredProcedure;
          command.Parameters.AddWithValue("@Id", id);
          connection.Open();
          command.ExecuteNonQuery();
        }
      }
    }
  }
}