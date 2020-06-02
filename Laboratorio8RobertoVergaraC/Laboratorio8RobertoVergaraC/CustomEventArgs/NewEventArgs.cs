using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio8RobertoVergaraC.CustomEventArgs
{
    public class NewEventArgs: EventArgs
    {
        public string IdentificadorText { get; set; }
        public string TipoText { get; set; }
        public string NombreDueñoText { get; set; }
        public string HorarioInicioText { get; set; }
        public string HorarioFinText { get; set; }
        public string ExtraText { get; set; }
    }
}
