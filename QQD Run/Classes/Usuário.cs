using MySql.Data.MySqlClient;
public class Usuario
{
	public int id { get; private set; }
	public string nome { get; private set; }

	public string login { get; private set; }

	private string senha;

	public Usuario(string nome, string login, string senha)
	{
		this.nome = nome;
		this.login = login;
		this.senha = senha;
	}
	public Usuario() {
	}

	public void CadastrarUsuario()
	{
		Conexao.Conectar();
		MySqlCommand cadastra = new MySqlCommand($"call adicionar_usuario('{login}','{nome}','{senha}')", Conexao.conexao);
		cadastra.ExecuteNonQuery();
		Conexao.Desconectar();
	}

	public bool VerificarUser()
	{
		Conexao.Conectar();
		MySqlCommand verifica = new MySqlCommand($"select * from usuario where login='{login}'", Conexao.conexao);
		bool resultado = verifica.ExecuteReader().HasRows;
		Conexao.Desconectar();
		return resultado;
		
	}
	public bool VerificarLogin()
	{
		Conexao.Conectar();
		MySqlCommand verifica = new MySqlCommand($"select * from usuario where login='{login}' and senha='{senha}'", Conexao.conexao);
		bool resultado = verifica.ExecuteReader().HasRows;
		Conexao.Desconectar();
		return resultado;

	}
	public int ConsultarId()
	{
		Conexao.Conectar();
		MySqlCommand verifica = new MySqlCommand($"select idUsuario from usuario where login='{login}'", Conexao.conexao);
		var dtreader = verifica.ExecuteReader();
		int resultado = 1;
		if (dtreader.HasRows)
		{
			dtreader.Read();
			resultado = int.Parse(dtreader["idUsuario"].ToString());
		}
		Conexao.Desconectar();
		return resultado;
	}
	public static Usuario RetornarDados(int id)
	{
		Conexao.Conectar();
		MySqlCommand verifica = new MySqlCommand($"select * from usuario where idUsuario='{id}'", Conexao.conexao);
		var dtreader = verifica.ExecuteReader();
		Usuario tempUser = new Usuario();
		if (dtreader.HasRows)
		{
			dtreader.Read();
			tempUser.id = int.Parse(dtreader["idUsuario"].ToString());
			tempUser.nome = (dtreader["nome"].ToString());
			tempUser.login = (dtreader["login"].ToString());
		}
		Conexao.Desconectar();
		return tempUser;
		
	}
	public static void AlterarDados(string nome, string senha, string login,int id)
	{
		Conexao.Conectar();
		MySqlCommand alterar = new MySqlCommand($"update usuario set nome = '{nome}' where idusuario = '{id}'", Conexao.conexao);
		alterar.ExecuteNonQuery();
		alterar = new MySqlCommand($"update usuario set senha = '{senha}' where idusuario = '{id}'", Conexao.conexao);
		alterar.ExecuteNonQuery();
		alterar = new MySqlCommand($"update usuario set login = '{login}' where idusuario = '{id}'", Conexao.conexao);
		alterar.ExecuteNonQuery();
		Conexao.Desconectar();

	}

}

