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
        public Resultado AplicarBackup()
        {
            var result = false;
            string msg;
            try
            {
                AplicarArquivo();
                result = true;
                msg = "";

            }
            catch(Exception e)
            {
                result = false;
                msg = e.Message;
            }
            return new Resultado{resultado = result, erro = msg };
        }


        private void PegaTabela()
        {
            bool Conectou = _cont.Database.CanConnect();
            List<string> tables = _cont.Tabelas;
            List<string> json = new List<string>();
            if (Conectou)
            {

                foreach (var item in _cont.Tabelas)
                {
                    json.Add(GeraJson(item));
                }
                int i = 0;
                foreach (var j in json)
                {
                    GerarArquivo(j, tables[i]);
                    i++;
                }
            }

        }
        private  void AplicarArquivo()
        {
            bool Conectou = _cont.Database.CanConnect();
            if (Conectou)
            {
                List<string> tables = _cont.Tabelas;
                object obj = null;
                try
                {
                    foreach (var item in tables)
                    {
                        switch (item)
                        {
                            case "Usuario":
                                //obj = new List<Usuario>();
                                obj =  LerArquivo<Usuario>(item);
                                if (obj != null)
                                {
                                    _cont.Usuarios.RemoveRange(_cont.Usuarios.ToList());
                                    _cont.Usuarios.AddRange((List<Usuario>)obj);
                                   


                                }
                                break;
                            case "Produto":

                                obj =  LerArquivo<Produto>(item);
                               
                                if (obj != null)
                                {
                                    _cont.Produtos.RemoveRange(_cont.Produtos.ToList());
                                      _cont.Produtos.AddRange((List<Produto>)obj);
                                  
                                }
                                break;
                            case "ProdutoPropriedades":

                                obj = LerArquivo<ProdutoPropriedades>(item);

                                if (obj != null)
                                {
                                    _cont.ProdutosPropriedadess.RemoveRange(_cont.ProdutosPropriedadess.ToList());
                                    _cont.ProdutosPropriedadess.AddRange((List<ProdutoPropriedades>)obj);

                                }
                                break;
                            case "Cliente":

                                obj = LerArquivo<ProdutoPropriedades>(item);

                                if (obj != null)
                                {
                                    _cont.Clientes.RemoveRange(_cont.Clientes.ToList());
                                    _cont.Clientes.AddRange((List<Cliente>)obj);

                                }
                                break;
                            default:
                                throw new Exception("Criar case da nova tabela!!!!!");
                        }
                    }
                    _cont.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
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
            else if (item == "ProdutoPropriedades")
            {

                list = _cont.ProdutosPropriedadess.ToList();
                j = new ConversorDeObjetos().ConverterParaString(list);
                list = null;

            }

            return j;
        }

        private async void GerarArquivo(string content, string name)
        {
            string path = Directory.GetCurrentDirectory().ToString() + "\\Backup";
            DirectoryInfo dir = Directory.CreateDirectory(path);
            using (StreamWriter stw = new StreamWriter(path + "\\" + name + "Table.json"))
            {
               await stw.WriteLineAsync(content);
                stw.Close();
            }



        }
        private List<T> LerArquivo<T>(string name)
        {

            object obj;
            string path = Directory.GetCurrentDirectory().ToString() + "\\Backup";
            using (StreamReader str = new StreamReader(path + "\\" + name + "Table.json"))
            {
                obj = JsonConvert.DeserializeObject<List<T>>(str.ReadLine()) as List<T>;

                str.Close();
            }

            return (List<T>)obj;

        }
        //private List<T> ConverteListaStringAsyncParaListGeneric<T>(object lista)
        //{

        //        object l = JsonConvert.DeserializeObject<T>(lista.ToString()) as List<T>;

        //    return (List<T>)l;
        //}
    }
    public class Resultado
    {
        public bool resultado { get; set; }
        public string erro { get; set; }
    }
}
