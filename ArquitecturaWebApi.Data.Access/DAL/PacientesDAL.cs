using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArquitecturaWebApi.Models.Pacientes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ArquitecturaWebApi.Data.Access.DAL
{
    public class PacientesDAL
    {
        public static IEnumerable<PacientesModel> ListPacientes()
        {
            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            List<PacientesModel> resultUsers = new List<PacientesModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SP_SELECCIONAR_PACIENTE", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader(); 
                while (rdr.Read())
                {
                    var paciente = new PacientesModel();
                    paciente.idPaciente = Convert.ToInt32(rdr["idPaciente"]);
                    paciente.nombres = rdr["nombres"].ToString();
                    paciente.apellidos = rdr["apellidos"].ToString();
                    paciente.fechaNacimiento = DateTime.Parse(rdr["fechaNacimiento"].ToString()); ;
                    paciente.estado = Convert.ToInt32(rdr["estado"]);
                    resultUsers.Add(paciente);
                }
                return resultUsers;//retorno todo los valores
            }
        }

        public static int InsertPacientes(PacientesModel pacienteModel)
        {//Acceder al archivo de configuración para ller la cadena de conexión

            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            int result;

            using (SqlConnection con = new SqlConnection(cs))//Inicio la conexión a la base de datos
            {
                con.Open();//Abrimos la conexión a la base de datos
                SqlCommand com = new SqlCommand("SP_INSERTAR_PACIENTE", con);//Definimos el comando a ejeuctar
                com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado

                //Agregamos los parametros y pasamos los datos a nuestro modelo
                com.Parameters.Add("@nombres", SqlDbType.VarChar, 50).Value = pacienteModel.nombres;
                com.Parameters.Add("@apellidos", SqlDbType.VarChar, 50).Value = pacienteModel.apellidos;
                com.Parameters.Add("@fechaNacimiento", SqlDbType.Date, 5).Value = pacienteModel.fechaNacimiento;
                com.Parameters.Add("@estado", SqlDbType.Int).Value = pacienteModel.estado;

                com.Parameters.Add("@oid", SqlDbType.Int, 5).Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();
                                      
                result = Convert.ToInt32(com.Parameters["@oid"].Value); //Paso el ID generado en la base de datos
            }
            return result;//Captura el ID generado
        }


        public static int UpdatePacientes(int id, PacientesModel pacienteModel)
        {
            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            int result;
            using (SqlConnection con = new SqlConnection(cs))//Inicio la conexión a la base de datos
            {
                con.Open();//Abrimos la conexión a la base de datos
                SqlCommand com = new SqlCommand("SP_ACTUALIZAR_PACIENTE", con);//Definimos el procedimiento a ejecutar
                com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado

                //Agregamos y hacemos match los parametros definidos
                com.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = id;
                com.Parameters.Add("@nombres", SqlDbType.VarChar, 50).Value = pacienteModel.nombres;
                com.Parameters.Add("@apellidos", SqlDbType.VarChar, 50).Value = pacienteModel.apellidos;
                com.Parameters.Add("@fechaNacimiento", SqlDbType.Date, 5).Value = pacienteModel.fechaNacimiento;
                com.Parameters.Add("@estado", SqlDbType.Int, 5).Value = pacienteModel.estado;
                result = com.ExecuteNonQuery();//Ejecuto y comando y actulizo los datos
            }
            return result;// Captura la lista de la identidad
        }

        public static int DeletePacientes(int id)//Como parámetro de entrada pasamos el id
        {//Acceder al archivo de configuración para leer la cadena de conexión
            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            int result;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();//Abrimos la conexión a la base de datos
                SqlCommand com = new SqlCommand("SP_ELIMINAR_PACIENTE", con);//Definimos el procedimiento a ejecutar
                com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado

                com.Parameters.Add("@id", SqlDbType.Int, 5).Value = id;//Agregamos y hacemos match el id en referencia al parametro de entrada

                result = com.ExecuteNonQuery();//Ejecutar el comando y eliminar el registro
            }
            return result;
        }

    }
}