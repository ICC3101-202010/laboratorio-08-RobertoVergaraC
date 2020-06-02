using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio8RobertoVergaraC
{
    [Serializable]
    class Restaurante : Stores
    {
        public Restaurante(string dueñoName, string identificador, string horarioInicio, string horarioFinal, string extra) : base(dueñoName, identificador, horarioInicio, horarioFinal, extra)
        {
            this.DueñoName = dueñoName;
            this.Identificador = identificador;
            this.HorarioInicio = horarioInicio;
            this.HorarioFinal = horarioFinal;
            this.Extra = extra;
            this.Type = "Restaurante";
        }
    }
}
