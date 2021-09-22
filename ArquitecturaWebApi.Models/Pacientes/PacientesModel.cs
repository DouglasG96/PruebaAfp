using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquitecturaWebApi.Models.Pacientes
{
    public class PacientesModel
    {
        public int idPaciente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int estado { get; set; }

    }
}
