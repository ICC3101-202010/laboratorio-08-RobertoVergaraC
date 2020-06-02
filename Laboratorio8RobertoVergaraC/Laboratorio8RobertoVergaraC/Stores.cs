using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio8RobertoVergaraC
{
    [Serializable]
    class Stores
    {
        private string dueñoName;
        private string identificador;
        private string horarioInicio;
        private string horarioFinal;
        private string extra;
        private string type;

        public string DueñoName { get => dueñoName; set => dueñoName = value; }
        public string Identificador { get => identificador; set => identificador = value; }
        public string HorarioInicio { get => horarioInicio; set => horarioInicio = value; }
        public string HorarioFinal { get => horarioFinal; set => horarioFinal = value; }
        public string Extra { get => extra; set => extra = value; }
        public string Type { get => type; set => type = value; }

        public Stores(string dueñoName, string identificador, string horarioInicio, string horarioFinal, string extra)
        {
            this.DueñoName = dueñoName;
            this.Identificador = identificador;
            this.HorarioInicio = horarioInicio;
            this.HorarioFinal = horarioFinal;
            this.Extra = extra;
        }

        public bool CheckLocal(string iden, string type)
        {
            if (this.Identificador.Equals(iden) && this.Type.Equals(type))
                return true;
            return false;
        }   
    }
}
