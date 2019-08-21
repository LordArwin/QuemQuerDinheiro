using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace QQD
{
	/// <summary>
	/// Interação lógica para MainWindow.xam
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Thread.Sleep(3000);

		}


		private string resulta;
		public bool verificar(string pmtro)
		{
			bool veric = true;
			int tamanho = pmtro.Length;

			if (tamanho < 4)
			{
				veric = false;
				resulta = "Erro no cadastro preencha todos os campos corretamente\n *Minimo de 4 caracteres em cada campo";
			}
			else if (Regex.IsMatch(pmtro, (@"[^a-zA-Z0-9]")))
			{
				veric = false;
				resulta = "Erro no cadastro preencha todos os campos corretamente\n *Não utilize caracteres especiais";

			}
			return veric;
		}

		private void Acender(object sender, MouseEventArgs e)
		{
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FF0000");
			CadLabel1.Foreground = brush;
		}

		private void ApagarCor(object sender, MouseEventArgs e)
		{
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#000000");
			CadLabel1.Foreground = brush;
		}

		private void AlterVisi(object sender, MouseButtonEventArgs e)
		{

			if ((string)Entrarbnt.Content == "Entrar")
			{
				Username.Text = "";
				Senha.Clear();
				TextNome.Text = "";
				TextNome.Visibility = Visibility.Visible;
				NomeLab.Visibility = Visibility.Visible;
				Entrarbnt.Content = "Cadastrar";
				CadLabel1.Content = "Logar-se";
				Icon.Margin = new Thickness(171, 27, 186, 0);
				MessageBoxResult result = MessageBox.Show("Entrou no modo de cadastro", "Modo Cadastro");
			}
			else
			{
				Username.Text = "";
				Senha.Clear();
				TextNome.Text = "";
				TextNome.Visibility = Visibility.Hidden;
				NomeLab.Visibility = Visibility.Hidden;
				Entrarbnt.Content = "Entrar";
				CadLabel1.Content = "Cadastrar-se";
				Icon.Margin = new Thickness(171, 71, 186, 0);
				MessageBoxResult result = MessageBox.Show("Entrou no modo de Login", "Modo Login");
			}

		}


		private void Entrar(object sender, RoutedEventArgs e)
		{
			if ((string)Entrarbnt.Content == "Cadastrar")
			{
				Usuario newUser = new Usuario(TextNome.Text, Username.Text, Senha.Password.ToString());
				if (verificar(Username.Text) & verificar(Senha.Password.ToString()) & TextNome.Text.Length >= 4)
				{
					try
					{

						bool resultado = newUser.VerificarUser();

						if (resultado == false)
						{

							newUser.CadastrarUsuario();
							MessageBoxResult result = MessageBox.Show("Usuario cadastrado com sucesso", "Cadastro Realizado");
							Username.Text = "";
							Senha.Clear();
							TextNome.Text = "";

						}
						else
						{
							MessageBoxResult result = MessageBox.Show("Usuário já cadastrado, Faça Login", "ERRO");
							Username.Text = "";
							Senha.Clear();
							TextNome.Text = "";

						}
					}

					catch
					{
						MessageBoxResult result = MessageBox.Show("ERRO DE SERVIDOR TENTE MAIS TARDE", "ERRO 404");

					}

				}
				else
				{
					MessageBoxResult result = MessageBox.Show(resulta);
				}

			}
			else if ((string)Entrarbnt.Content == "Entrar")

			{
				Usuario newUser = new Usuario(TextNome.Text, Username.Text, Senha.Password.ToString());


				try
				{
					bool resultado = newUser.VerificarLogin();
					if (resultado == true)
					{

						int id = newUser.ConsultarId();
						QQD_Run.Inicio inicio = new QQD_Run.Inicio(id);
						inicio.Show();
						Close();



					}
					else
					{
						MessageBoxResult result = MessageBox.Show($"Erro ao Fazer Login");

					}
				}
				catch
				{
					MessageBoxResult result = MessageBox.Show("Usuário ou Senha Invalidos");

					Username.Text = "";
					Senha.Clear();
					TextNome.Text = "";
					Conexao.Desconectar();


				}
			}


		}
	}


}
