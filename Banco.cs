using System.Data.SqlClient;

namespace Projeto_Web_Lh_Pets_versão_1
{
    class Banco
    
    { 
        private readonly List<Clientes> lista = new();
        public List<Clientes> GetLista(){
            return lista;
        }
	
    
	public Banco()
	{
	 	try
                {
                    SqlConnectionStringBuilder builder = new(
                    "User ID=sa;Password=1234;" +
                    "Server=localhost\\SQLEXPRESS;" +
                    "Database=vendas;" +
                    "Trusted_Connection=False;"
                    );

                using SqlConnection conexao = new(builder.ConnectionString);
                String sql = "SELECT * FROM tblclientes";
                using SqlCommand comando = new(sql, conexao);
                conexao.Open();
                using SqlDataReader tabela = comando.ExecuteReader();

                while (tabela.Read())
                {
                    lista.Add(new Clientes()
                    {
                        Cpf_cnpj = tabela["Cpf_cnpj"].ToString(),
                        Nome = tabela["Nome"].ToString(),
                        Endereco = tabela["Endereco"].ToString(),
                        Rg_ie = tabela["Rg_ie"].ToString(),
                        Tipo = tabela["Tipo"].ToString(),
                        Valor = (float)Convert.ToDecimal(tabela["valor"]),
                        Valor_imposto = (float)Convert.ToDecimal(tabela["valor_imposto"]),
                        Total = (float)Convert.ToDecimal(tabela["total"])
                    });
                }
            }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
	}
	
  
 
	public String GetListaString()
	{
		string enviar= "<!DOCTYPE html>\n<html>\n<head>\n<meta charset='utf-8' />\n"+
                      "<title>Cadastro de Clientes</title>\n</head>\n<body>";
        enviar += "<b>   CPF / CNPJ    -      Nome    -    Endereço    -   RG / IE   -   Tipo  -   Valor   - Valor Imposto -   Total  </b>";

        int i=0;
            foreach (Clientes cli in GetLista())
            {
                string corfundo;
                string cortexto;
                if (i % 2 == 0)
                { corfundo = "#6f47ff"; cortexto = "white"; }
                else
                { corfundo = "#ffffff"; cortexto = "#6f47ff"; }
                i++;


                enviar = enviar +
               $"\n<br><div style='background-color:{corfundo};color:{cortexto};'>" +
                cli.Cpf_cnpj + " - " +
                cli.Nome + " - " + cli.Endereco + " - " + cli.Rg_ie + " - " +
                cli.Tipo + " - " + cli.Valor.ToString("C") + " - " +
                cli.Valor_imposto.ToString("C") + " - " + cli.Total.ToString("C") + "<br>" +
                 "</div>";
            }
            return enviar;
	}

	public void ImprimirListaConsole(){

                Console.WriteLine("   CPF / CNPJ   " + " - " + "    Nome   " + 
                    " - " + "   Endereço   " + " - " + "  RG / IE  " + " - " +
                    "  Tipo " + " - " + "  Valor  " + " - " + "Valor Imposto" + 
                    " - " + "  Total  ");

                foreach (Clientes cli in GetLista())
                {
                    Console.WriteLine(cli.Cpf_cnpj + " - " + 
                    cli.Nome + " - " + cli.Endereco + " - " + cli.Rg_ie + " - " +
                    cli.Tipo + " - " + cli.Valor.ToString("C") + " - " + 
                    cli.Valor_imposto.ToString("C") + " - " + cli.Total.ToString("C"));
                }
        }

        
    }
}