using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

public class Despesa
{
	public double Valor { get; set; }

	public string Tipo { get; set; }

	public string Data { get; set; }
	public string Descricao { get; set; }
	public Despesa(double valor, string tipo, string data, string descricao)
	{
		Valor = valor;
		Tipo = tipo;
		Data = data;
		Descricao = descricao;
	}
	public Despesa()
	{
		
	}

	public bool AdicionarDespesa(int id)
	{
		bool result = true;
		try
		{
			Conexao.Conectar();
			MySqlCommand AdicionarDespesa = new MySqlCommand($"call adicionar_despesa('{Valor}','{Tipo}','{id}','{Data}','{Descricao}' )", Conexao.conexao);
			AdicionarDespesa.ExecuteNonQuery();
			Conexao.Desconectar();
		}
		catch
		{
			result = false;
		}
		return result;


	}
	public static double ValorTotalMes(string ano, string mes, int id)
	{
		Conexao.Conectar();
		MySqlCommand RetornarValor = new MySqlCommand($"select valor from despesa where data like '{ano}-{mes}%' and idcarteira = {id}", Conexao.conexao);
		var dtreader = RetornarValor.ExecuteReader();
		Despesa despesaTotal = new Despesa();
		if (dtreader.HasRows)
		{
			while (dtreader.Read())
			{
				despesaTotal.Valor += double.Parse(dtreader["valor"].ToString());
			}
		}
		Conexao.Desconectar();


		return Math.Round(despesaTotal.Valor, 2);
	}
	public static void FormatarDespesas(int id, string data)
	{
		Conexao.Conectar();
		MySqlCommand deletar = new MySqlCommand($"delete from despesa where idcarteira = '{id}'and Data like '{data}%'", Conexao.conexao);
		deletar.ExecuteNonQuery();
		Conexao.Desconectar();

	}
	public List<Despesa> PegarDespesas(string data, int id)
	{
		List<Despesa> despesas = new List<Despesa>();
		Conexao.Conectar();
		MySqlCommand RetornarValor = new MySqlCommand($"select * from despesa where idcarteira = '{id}' and data like '{data}%'" , Conexao.conexao);
		MySqlDataReader dtreader = RetornarValor.ExecuteReader();
		
		while (dtreader.Read())
		{
			Despesa despesa = new Despesa();
			despesa.Data = dtreader["data"].ToString();
			despesa.Valor = double.Parse(dtreader["valor"].ToString());
			despesa.Tipo = dtreader["categoria"].ToString();
			despesa.Descricao = dtreader["descricao"].ToString();
			

			despesas.Add(despesa);


		}
		Conexao.Desconectar();

		return despesas;

	}
}


