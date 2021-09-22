using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Importar los poryectos Models y Data.Access, para acceder al directorio Usuario y DAL
using ArquitecturaWebApi.Models.Pacientes;
using ArquitecturaWebApi.Data.Access.DAL;

namespace ArquitecturaWebApi.Data.Business
{
    public class Pacientes
    {        
        public static IEnumerable<PacientesModel> ListPacientes()
        {
            try
            {
                //Retornamos la lista de valores del método ListUsers() que invocamos
                return PacientesDAL.ListPacientes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int InsertPacientes(PacientesModel pacienteModel)
        {
            try
            {
                return PacientesDAL.InsertPacientes(pacienteModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdatePacientes(int id, PacientesModel pacienteModel)
        {
            try
            {
                return PacientesDAL.UpdatePacientes(id, pacienteModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeletePacientes(int id)
        {
            try
            {
                return PacientesDAL.DeletePacientes(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}