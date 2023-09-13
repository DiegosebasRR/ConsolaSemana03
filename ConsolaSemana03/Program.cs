
//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using ConsolaSemana03;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-28\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=admin;Password=admin";


    static void Main()
    {
  
        List<Student> students = ListarEstudiantesListaObjetos();
        foreach (var item in students)
        {
            Console.WriteLine($"ID: {item.StudentId}, Nombre: {item.FirstName}, Apellido: {item.LastName}");
        }
  
    }
    //De forma conectada
    private static List<Student> ListarEstudiantesListaObjetos()
    {
        List<Student> empleados = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT StudentId,FirstName,LastName FROM Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Estudiantes:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            empleados.Add(new Student
                            {
                                StudentId = (int)reader["StudentId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });

                        }
                    }
                }
            }
            // Cerrar la conexión
            connection.Close();


        }
        return empleados;

    }


}
