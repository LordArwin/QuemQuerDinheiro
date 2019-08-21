using MySql.Data.MySqlClient;

public class Carteira
{
	public int id { get; private set; }
	private double Saldo { get; set; }

	

	public double ConsultarSaldo()
	{
		Conexao.Conectar();
		MySqlCommand RetornarSaldo = new MySqlCommand($"select saldo from carteira where idcarteira = {id}", Conexao.conexao);
		var dtreader = RetornarSaldo.ExecuteReader();
		if (dtreader.HasRows)
		{
			while (dtreader.Read())
			{
				Saldo = double.Parse(dtreader["saldo"].ToString());
			}
		}
		Conexao.Desconectar();
		return Saldo;
	}

	
	public static Carteira InstanciarCarteira(int idusuario)
	{
		Conexao.Conectar();
		MySqlCommand verifica = new MySqlCommand($"select * from carteira where idusuario='{idusuario}'", Conexao.conexao);
		var dtreader = verifica.ExecuteReader();
		Carteira Carteira1 = new Carteira();
		if (dtreader.HasRows)
		{
			dtreader.Read();
			Carteira1.Saldo = double.Parse(dtreader["saldo"].ToString());
			Carteira1.id = int.Parse(dtreader["idcarteira"].ToString());
		}
		Conexao.Desconectar();

		return Carteira1;
		/*  Conexao.Open();//Abre conexão
            MySqlDataReader dtreader = Query.ExecuteReader();//Crie um objeto do tipo reader para ler os dados do banco
            List<Cliente> listaDeRetorno = new List<Cliente>();//Crie uma lista de Cliente
            Cliente cliente = new Cliente();//Estancia objeto do tipo cliente
            while (dtreader.Read())//Enquanto existir dados no select
            {
                cliente.nome = dtreader["nome"].ToString();//Preencha objeto do tipo cliente com dados vindo do banco de dados
                cliente.cpf = dtreader["cpf"].ToString();
                cliente.RG = dtreader["rg"].ToString();
                cliente.telefone = dtreader["telefone"].ToString();
                cliente.bairro = dtreader["bairro"].ToString();
                cliente.cidade = dtreader["cidade"].ToString();
                listaDeRetorno.Add(cliente);//Adiciona na lista um objeto do tipo cliente*/

	}


	public  void FormatarCarteira()
	{

		Conexao.Conectar();
		MySqlCommand deletar = new MySqlCommand($"update carteira set saldo = 0.0 where idcarteira = '{id}'", Conexao.conexao);
		deletar.ExecuteNonQuery();
		Conexao.Desconectar();


	}
}


