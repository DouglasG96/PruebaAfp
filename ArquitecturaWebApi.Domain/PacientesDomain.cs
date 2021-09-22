using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArquitecturaWebApi.Models.Pacientes;
using ArquitecturaWebApi.Data.Business;

namespace ArquitecturaWebApi.Domain
{
    public class PacientesDomain
    {
        public IEnumerable<PacientesModel> ListPacientes()
        {
            try
            {//Invocamos al método listar de la capa negocio "Data.Business"
                return Pacientes.ListPacientes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertPacientes(PacientesModel pacienteModel)
        {
            try
            {
                return Pacientes.InsertPacientes(pacienteModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdatePacientes(int id, PacientesModel pacienteModel)
        {
            try
            {
                return Pacientes.UpdatePacientes(id, pacienteModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeletePacientes(int id)
        {
            try
            {
                return Pacientes.DeletePacientes(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}              