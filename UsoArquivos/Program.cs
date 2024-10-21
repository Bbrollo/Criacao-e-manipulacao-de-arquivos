using System.IO;
using System.Globalization;
using UsoArquivos.Entities;

namespace UsoArquivos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Digite o diretório do caminho de origem: ");
                string caminhoProduct = Console.ReadLine(); //salva o diretório com o file .csv



                string pastaOut = Path.GetDirectoryName(caminhoProduct) + @"\out"; //salva na string o diretório sem o file .csv
                DirectoryInfo di = new DirectoryInfo(pastaOut); //instancia objeto do tipo DirectoryInfo, para usar seus metodos
                di.Create(); //Cria a pasta out, também é possivel fazer o mesmo sem instanciar objeto, com o struct Directory

                string arquivoSummary = pastaOut + @"\summary.csv"; //salva na string o nome do arquivo csv a ser criado
                try
                {
                    using (FileStream fs = File.Create(arquivoSummary)) //cria o arquivo Summary.csv
                    {

                    }

                    string[] linhas = File.ReadAllLines(caminhoProduct); //salva no array o conteudo do produtos.csv
                    foreach (string line in linhas) //laço de repetição
                    {
                        string[] infos = line.Split(','); //salva no array cada uma das linhas cortando por virgulas
                        string nome = infos[0]; //salva o nome na string
                        double preco = double.Parse(infos[1]); //converte e salva o preço na variavel
                        int quant = int.Parse(infos[2]); //converte e salva a quantidade na variavel

                        Product product = new Product(nome, preco, quant); //instancia o objeto Product

                        using (StreamWriter sw = File.AppendText(arquivoSummary)) //abre o arquivo Summary no modo appendtext
                        {
                            sw.WriteLine(product.Name + "," + product.Total().ToString("F2", CultureInfo.InvariantCulture));
                            //escreve no arquivo Summary o Product Name concatenado com virgula concatenado com o método total
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.Message);
            }
        }
    }
}
