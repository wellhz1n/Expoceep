using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Bibliotecas
{
   public interface IBackupDataBase
    {
        bool GerarBackup();
        Resultado AplicarBackup();
    }
}
