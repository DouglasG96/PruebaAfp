using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquitecturaWebApi.Models.Consultas
{
    public class ConsultasModel
    {
        public int idConsulta { get; set; }
        public int idMedico { get; set; }
        public int idPaciente { get; set; }
        public int idCita { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public string diagnosico { get; set; }
        public string resumen { get; set; }
        public int estado { get; set; }

    }
}
