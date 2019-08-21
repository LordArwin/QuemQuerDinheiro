using MySql.Data.MySqlClient;

public class Meta
{
	public double ValorDaMeta { get; private set; }

	public  string Data { get; private set; }

	public string Descricao { get; private set; }

	public double ValorCumprido { get; private set; }

	public Meta(double valorDaMeta, string data, string descricao)
	{
		ValorDaMeta = valorDaMeta;
		Data = data;
		
		Descricao = descricao;
	}

	public Meta()
	{
		ValorDaMeta = 1.0;
		Data = "";

		Descricao = "";
		ValorCumprido = 0.0;
	}

	public bool AdicionarMeta(int id)
	{

		bool result = true;
		try { 


			Conexao.Conectar();
			MySqlCommand verificarData = new MySqlCommand($"select * from meta where data = '{Data}' and idcarteira = '{id}'",Conexao.conexao);
			bool rows = verificarData.ExecuteReader().HasRows;
			Conexao.Desconectar();
			if (rows == true)
			{
				result = false;
			}
			else
			{
				Conexao.Conectar();
				MySqlCommand AdicionarMeta = new MySqlCommand($"call adicionar_meta ('{ValorDaMeta}','{Descricao}','0.00',{id},'{Data}' )", Conexao.conexao);
				AdicionarMeta.ExecuteNonQuery();
				Conexao.Desconectar();

			}
			
			
			
		}
		catch
		{
			result = false;
		}
		return result;

	}
	public static string VerificarData(string mes)
	{
		string[] meses = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
		for (int i = 0; i <= meses.Length; i++)
		{
			if (meses[i] == mes)
			{
				if (i < 9)
				{
					mes = $"0{i}";
				}
				else
				{
					mes = $"{i}";
				}
			}
			
		}
		return mes;
	}
	public static Meta RetornarMeta(string Data , int id)
	{
		Conexao.Conectar();
		MySqlCommand verifica = new MySqlCommand($"select * from meta where idcarteira ='{id}' and data = '{Data}'", Conexao.conexao);
		var dtreader = verifica.ExecuteReader();
		Meta tempMeta = new Meta();
		if (dtreader.HasRows)
		{
			dtreader.Read();
			tempMeta.ValorDaMeta = double.Parse(dtreader["valor"].ToString());
			tempMeta.Data = (dtreader["data"].ToString());
			tempMeta.Descricao = (dtreader["descricao"].ToString());
			tempMeta.ValorCumprido = double.Parse(dtreader["valor_comprido"].ToString());
		}
		Conexao.Desconectar();
		return tempMeta;
	}
	public void Del( int id)
	{
		Conexao.Conectar();
		MySqlCommand deletar = new MySqlCommand($"call remover_meta('{id}','{Data}')", Conexao.conexao);
		deletar.ExecuteNonQuery();
		Conexao.Desconectar();
	
	}
	public static void FormatarMetas(int id)
	{
		Conexao.Conectar();
		MySqlCommand deletar = new MySqlCommand($"delete from meta where idcarteira = '{id}'", Conexao.conexao);
		deletar.ExecuteNonQuery();
		Conexao.Desconectar();

	}
}

