using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class Receita
{
	public double Valor { get; set; }

	public string Data { get; set; }

	public string Categoria { get; set; }

	public string Descricao { get; set; }

	public Receita(double valor, string categoria, string data, string descricao)
	{
		Valor = valor;
		Data = data;
		Categoria = categoria;
		Descricao = descricao;
	}
	public Receita()
	{

	}
	public bool AdicionarGanho(int id)
	{
		bool result = true;
		try
		{
			Conexao.Conectar();
			MySqlCommand AdicionarGanho = new MySqlCommand($"call adicionar_ganho('{Valor}','{Data}','{Categoria}','{id}','{Descricao}' )", Conexao.conexao);
			AdicionarGanho.ExecuteNonQuery();
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
		MySqlCommand RetornarValor = new MySqlCommand($"select valor from receita where data like '{ano}-{mes}%' and idcarteira = {id}", Conexao.conexao);
		var dtreader = RetornarValor.ExecuteReader();
		Receita receitaTotal = new Receita();
		if (dtreader.HasRows)
		{
			while (dtreader.Read())
			{
				receitaTotal.Valor += double.Parse(dtreader["valor"].ToString());
			}
		}
		Conexao.Desconectar();


		return Math.Round(receitaTotal.Valor, 2);

	}
	public static void FormatarReceitas(int id, string data)
	{
		Conexao.Conectar();
		MySqlCommand deletar = new MySqlCommand($"delete from receita where idcarteira = '{id}'and Data like '{data}%'", Conexao.conexao);
		deletar.ExecuteNonQuery();
		Conexao.Desconectar();

	}
	public List<Receita> PegarReceitas(string data, int id)
	{
		List<Receita> receitas = new List<Receita>();
		Conexao.Conectar();
		MySqlCommand RetornarValor = new MySqlCommand($"select * from receita where idcarteira = '{id}' and data like '{data}%'", Conexao.conexao);
		MySqlDataReader dtreader = RetornarValor.ExecuteReader();
		while (dtreader.Read())
		{
			Receita receita = new Receita();
			receita.Data = dtreader["data"].ToString();
			receita.Valor = double.Parse(dtreader["valor"].ToString());
			receita.Categoria = dtreader["categoria"].ToString();
			receita.Descricao = dtreader["descricao"].ToString();


			receitas.Add(receita);


		}
		Conexao.Desconectar();

		return receitas;
	}
}




