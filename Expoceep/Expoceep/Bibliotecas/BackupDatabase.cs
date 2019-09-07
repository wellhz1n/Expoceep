using Expoceep.DB;
using Expoceep.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Expoceep.Bibliotecas
{
    public class BackupDatabase : IBackupDataBase
    {
        private ERPDatabaseContext _cont;
        public BackupDatabase(ERPDatabaseContext cont)
        {
            _cont = cont;
        }

        public bool GerarBackup()
        {
            var result = false;
            try
            {
                PegaTabela();
                result = true;

            }
            catch
            {
                result = false;
            }
            return result;
        }

        public async void PegaTabela()
        {
            bool Conectou = _cont.Database.CanConnect();
            List<string> json = new List<string>();
            if (Conectou)
            {

                foreach (var item in _cont.Tabelas)
                {
                    json.Add(GeraJson(item));
                }
                foreach (var tab in _cont.Tabelas)
                {
                    foreach (var j in json)
                    {
                        await GerarArquivo(j, $"{tab}Table");
                    }
                }
            }

        }

        private string GeraJson(string item)
        {
            object list;
            string j = "";
            if (item == "Usuario")
            {

                list = _cont.Usuarios.ToList();
                j = new ConversorDeObjetos().ConverterParaString(list);
                list = null;
            }
            else if (item == "Produto")
            {

                list = _cont.Produtos.ToList();
                j = new ConversorDeObjetos().ConverterParaString(list);
                list = null;

            }

            return j;
        }

        public async Task GerarArquivo(string content, string name)
        {
            string path = Directory.GetCurrentDirectory().ToString() + "\\Backup";
            DirectoryInfo dir = Directory.CreateDirectory(path);
            using (StreamWriter stw = new StreamWriter(path + "\\" + name + ".json"))
            {
                await stw.WriteLineAsync(content);
                stw.Close();
            }



        }
        public async Task<object> LerArquivo(string name)
        {
            
            object obj = null;
            string path = Directory.GetCurrentDirectory().ToString() + "\\Backup";
            using (StreamReader str = new StreamReader(path + "\\" + name + ".json"))
            {
               obj = JsonConvert.DeserializeObject(await str.ReadLineAsync());
                str.Close();
            }

            return obj;

        }
    }
}
