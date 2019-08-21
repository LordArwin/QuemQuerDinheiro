using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
class Conexao
{
	public static MySqlConnection conexao = new MySqlConnection("server=localhost;port=3306;;User Id = root;database = qqd;");

	public static void Conectar()
	{
		conexao.Open();
	}
	public static void Desconectar()
	{
		conexao.Close();
	}

}

