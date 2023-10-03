using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCsharp
{
    
        interface IOperacoesConta
        {
            void Creditar(double valor);
            void Debitar(double valor);
        }

    
}
