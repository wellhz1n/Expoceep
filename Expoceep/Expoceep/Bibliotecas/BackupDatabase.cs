using Expoceep.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            bool a = _cont.Database.CanConnect();
            if (a)
            {
                var list = _cont.Usuarios.ToList();
                var json = new ConversorDeObjetos().ConverterParaString(list);
                await GerarArquivo(json, "UsuarioTable" + System.DateTime.Now.Day.ToString());
                var listProduto = _cont.Produtos.ToList();
                json = new ConversorDeObjetos().ConverterParaString(listProduto);
                await GerarArquivo(json, "ProdutoTable" + System.DateTime.Now.Day.ToString());
            } 

        }
        public async Task GerarArquivo(string content, string name)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Backup";
            DirectoryInfo dir = Directory.CreateDirectory(path);
            using (StreamWriter stw = new StreamWriter(path + "\\" + name + ".json"))
            {
                await stw.WriteLineAsync(content);
                stw.Close();
            }



        }
    }
}
