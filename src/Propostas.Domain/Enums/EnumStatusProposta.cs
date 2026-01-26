using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propostas.Domain.Enums
{
    

    public enum EnumStatusProposta
    {
        
        [Description("Criada")]
        Criada = 0,

        [Description("Em análise")]
        EmAnalise = 1,

        [Description("Aprovada")]
        Aprovada = 2,

        [Description("Recusada")]
        Recusada = 3,

        [Description("Expirada")]
        Expirada = 4
    }

   

}
