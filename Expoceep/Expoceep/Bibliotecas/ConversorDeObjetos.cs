using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Bibliotecas
{
    public class ConversorDeObjetos
    {

        public string ConverterParaString(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public T ConverterParaObject<T>(string Jsonstring)
        {
            if (Jsonstring == "" || Jsonstring == null)
                return default(T);
            else
                return JsonConvert.DeserializeObject<T>(Jsonstring);
        }
    }
}
