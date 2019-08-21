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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;



namespace QQD_Run
{
	/// <summary>
	/// Lógica interna para Inicio.xaml
	/// </summary>
	/// 
	public partial class Inicio : Window
	{
		private int id =1;
		private Usuario userLogado;
		private Carteira carteiraLogada;
		private Meta metaAtual = new Meta();
		private Meta metaProximo;
		string[] meses = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
		DateTime c = DateTime.Today;


		public Inicio(int user)
		{
			InitializeComponent();
			id = user;
			
			
			



			DataDespesa.Content += $"{c.ToString("yyyy")}-{c.ToString("MM")}-{c.ToString("dd")}";
			DataGanho.Content += $"{c.ToString("yyyy")}-{c.ToString("MM")}-{c.ToString("dd")}";

			IniciarInicio();

			/*MesCanvaDespesa.Content = $"{meses[d - 1]} / {c.ToString("yyyy")}";
			MesCanvaGanhos.Content = $"{meses[d - 1]} / {c.ToString("yyyy")}";
			DespesaMes.Content = Despesa.ValorTotalMes(c.ToString("yyyy"), c.ToString("MM"), carteiraLogada.id);
			GanhoMes.Content = Receita.ValorTotalMes(c.ToString("yyyy"), c.ToString("MM"), carteiraLogada.id);
			DataDespesa.Content += $"{c.ToString("yyyy")}-{c.ToString("MM")}-{c.ToString("dd")}";
			ComboMesMeta.Items[0] = $"{meses[d - 1]}";
			ComboMesMeta.Items[1] = $"{meses[d]}";*/




			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); ;

			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); ;



			FrameworkElement.LanguageProperty.OverrideMetadata(

			  typeof(FrameworkElement),

			  new FrameworkPropertyMetadata(

					XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));




		}

		public string SelectCombo(ComboBox combo)
		{
			string Teste = combo.SelectedItem.ToString();
			int index = Teste.IndexOf(':');
			return Teste.Substring(index + 2);

		}
		public void IniciarInicio()
		{

			SelecionarTipoRela.Items[0] = "Despesas";
			SelecionarTipoRela.Items[1] = "Ganhos";
			userLogado = Usuario.RetornarDados(id);
			carteiraLogada = Carteira.InstanciarCarteira(userLogado.id);

			bem_vindo.Content = $"Bem Vindo(a), {userLogado.nome}";
			string dataAtual = $"{c.ToString("yyyy")}-{c.ToString("MM")}";
			ValorCarteira.Content = carteiraLogada.ConsultarSaldo().ToString();

			int d = int.Parse(c.ToString("MM"));
			MesCanvaDespesa.Content = $"{meses[d - 1]} / {c.ToString("yyyy")}";
			MesCanvaGanhos.Content = $"{meses[d - 1]} / {c.ToString("yyyy")}";
			DespesaMes.Content = Despesa.ValorTotalMes(c.ToString("yyyy"), c.ToString("MM"), carteiraLogada.id);
			GanhoMes.Content = Receita.ValorTotalMes(c.ToString("yyyy"), c.ToString("MM"), carteiraLogada.id);

			ComboMesMeta.Items[0] = c.ToString("MM");
			string mes = "";
			string mesAnterior = "";
			string mesAnterior2 = "";
			string mesAtual = "";
			if (c.Month <= 12 && c.Month > 0)
			{ 
				if (c.Month <9)
				{
					mesAtual = $"0{c.Month}";
					mes = $"0{c.Month + 1}";
				}
				else if (c.Month == 12)
				{
					mes = "01";
					mesAtual = "12";
				}
				else
				{
					mesAtual = $"{c.Month}";
				}
				
				if (c.Month > 2 && c.Month < 9)
				{
					mesAnterior = $"0{c.Month - 1}";
					mesAnterior2 = $"0{c.Month - 2}";
				}
				else if (c.Month > 1  && c.Month < 9)
				{
					mesAnterior = $"0{c.Month - 1}";
					mesAnterior2 = "12";
				}
				else if (c.Month == 1)
				{
					mesAnterior = "12";
					mesAnterior2 = "11";
				}
				else if(c.Month == 10 )
				{
					mesAnterior = $"0{c.Month - 1}";
					mesAnterior2 = "08";
				}
				else if (c.Month == 11)
				{
					mesAnterior = $"0{c.Month - 1}";
					mesAnterior2 = "09";
				}
				else
				{
					mesAnterior = $"{c.Month - 1}";
					mesAnterior2 = $"{c.Month - 2}";
				}





			}

			metaAtual = Meta.RetornarMeta(dataAtual, carteiraLogada.id);
			metaProximo = Meta.RetornarMeta($"{c.ToString("yyyy")}-{mes}", carteiraLogada.id);

			ComboMesMeta.Items[1] = mes;
			MesRela.Items[0] = $"{mesAnterior2}";
			MesRela.Items[1] = $"{mesAnterior}";
			MesRela.Items[2] = $"{mesAtual}";

			//Atual
			DescricaoMeta1.Content = metaAtual.Descricao;
			lblAtual.Content = $"{ metaAtual.ValorCumprido} R$/ { metaAtual.ValorDaMeta} R$";
			ProgressoAtual.Maximum = metaAtual.ValorDaMeta;
			ProgressoBarra(ProgressoAtual, metaAtual, PorcentagemAtual);
			MesMeta.Content = $"Meta de: {metaAtual.Data}";
			//Proxima
			DescricaoMeta2.Content = metaProximo.Descricao;
			MetaMesProximo.Content = $"{ metaProximo.ValorCumprido} R$/ { metaProximo.ValorDaMeta} R$";
			ProgressoProximo.Maximum = metaProximo.ValorDaMeta;
			ProgressoBarra(ProgressoProximo, metaProximo, PorcentagemProximo);
			lblProximo.Content = $"Meta de: {metaProximo.Data}";

		}
		public void Zerar()
		{
			textoboxvalor.Text = "0.00";
			descricaoDespesa.Text = "";
			textoboxvalorGanho.Text = "0.00";
			CampoGanho.Text = "";
			textoboxvalormeta.Text = "0.00";
			descricaoMeta.Text = "";
			TextNomeAlt.Text = "";
			TextLoginAlt.Text = "";
			TextSenhaAlt.Clear();
		}
		public bool Verificar(string pmtro)
		{
			bool veric = true;
			int tamanho = pmtro.Length;
	
			if (tamanho < 4)
			{
				veric = false;
				
			}
			else if (Regex.IsMatch(pmtro, (@"[^a-zA-Z0-9]")))
			{
				veric = false;
				

			}
			return veric;
		}

		public void ProgressoBarra(ProgressBar barra, Meta meta, Label l)
		{
			barra.Value = meta.ValorCumprido;
			int valor = (int)(meta.ValorCumprido * 100) / (int)meta.ValorDaMeta;
			if (valor >= 100)
			{
				valor = 100;
				var converter = new System.Windows.Media.BrushConverter();
				var brush = (Brush)converter.ConvertFromString("#B22222");
				barra.Foreground = brush;


			}
			l.Content = $"{valor} %";
		}

		private void MenuOpen(object sender, RoutedEventArgs e)
		{
			if (Menu.IsExpanded == false)
			{
				Menu.IsExpanded = true;
			}
			else
			{
				Menu.IsExpanded = false;
			}
			if (GridSecundario.Margin == GridPrincipal.Margin)
			{
				GridSecundario.Margin = GridRevolt.Margin;
			}
			else
			{
				GridSecundario.Margin = GridPrincipal.Margin;

			}
			/*
	private void Recursivo(object sender, MouseEventArgs e)
	{
		if (GridSecundario.Margin == GridPrincipal.Margin)
		{
			GridSecundario.Margin = GridRevolt.Margin;
		}
		else
		{
			GridSecundario.Margin = GridPrincipal.Margin;
		}
	}*/
		}

		private void Sair(object sender, RoutedEventArgs e)
		{
		

			Application.Current.Shutdown();
		}

		private void AbrirInicio(object sender, RoutedEventArgs e)
		{
			IniciarInicio();
			CanvaRelatorio.Visibility = Visibility.Hidden;
			CanvaCarteira.Visibility = Visibility.Hidden;
			CanvaMetas.Visibility = Visibility.Hidden;
			CanvaGanho.Visibility = Visibility.Hidden;
			CanvaDespesa.Visibility = Visibility.Hidden;
			CanvaInicio.Visibility = Visibility.Visible;
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FFDDDDDD");
			bntCarteira.Background = brush;
			bntDespesa.Background = brush;
			bntMetas.Background = brush;
			bntRelatorio.Background = brush;
			bntGanho.Background = brush;
			converter = new System.Windows.Media.BrushConverter();
			brush = (Brush)converter.ConvertFromString("#FFBEE5FB");
			bntInicio.Background = brush;


		}
		private void AbrirDespesa(object sender, RoutedEventArgs e)
		{
			
			CanvaRelatorio.Visibility = Visibility.Hidden;
			CanvaCarteira.Visibility = Visibility.Hidden;
			CanvaMetas.Visibility = Visibility.Hidden;
			CanvaGanho.Visibility = Visibility.Hidden;
			CanvaInicio.Visibility = Visibility.Hidden;
			CanvaDespesa.Visibility = Visibility.Visible;
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FFDDDDDD");
			bntCarteira.Background = brush;
			bntInicio.Background = brush;
			bntMetas.Background = brush;
			bntRelatorio.Background = brush;
			bntGanho.Background = brush;
			converter = new System.Windows.Media.BrushConverter();
			brush = (Brush)converter.ConvertFromString("#FFBEE5FB");
			bntDespesa.Background = brush;
			Zerar();
		}

		private void AbrirGanho(object sender, RoutedEventArgs e)
		{
			
			CanvaRelatorio.Visibility = Visibility.Hidden;
			CanvaCarteira.Visibility = Visibility.Hidden;
			CanvaMetas.Visibility = Visibility.Hidden;
			CanvaInicio.Visibility = Visibility.Hidden;
			CanvaDespesa.Visibility = Visibility.Hidden;
			CanvaGanho.Visibility = Visibility.Visible;
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FFDDDDDD");
			bntCarteira.Background = brush;
			bntDespesa.Background = brush;
			bntMetas.Background = brush;
			bntRelatorio.Background = brush;
			bntInicio.Background = brush;
			converter = new System.Windows.Media.BrushConverter();
			brush = (Brush)converter.ConvertFromString("#FFBEE5FB");
			bntGanho.Background = brush;
			Zerar();
		}

		private void AbrirMetas(object sender, RoutedEventArgs e)
		{
			Zerar();
			ComboMesMeta.SelectedIndex = 0;
			CanvaRelatorio.Visibility = Visibility.Hidden;
			CanvaCarteira.Visibility = Visibility.Hidden;
			CanvaInicio.Visibility = Visibility.Hidden;
			CanvaDespesa.Visibility = Visibility.Hidden;
			CanvaGanho.Visibility = Visibility.Hidden;
			CanvaMetas.Visibility = Visibility.Visible;
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FFDDDDDD");
			bntCarteira.Background = brush;
			bntDespesa.Background = brush;
			bntInicio.Background = brush;
			bntRelatorio.Background = brush;
			bntGanho.Background = brush;
			converter = new System.Windows.Media.BrushConverter();
			brush = (Brush)converter.ConvertFromString("#FFBEE5FB");
			bntMetas.Background = brush;
		}

		private void AbrirCarteira(object sender, RoutedEventArgs e)
		{
			Zerar();
			CanvaRelatorio.Visibility = Visibility.Hidden;
			CanvaInicio.Visibility = Visibility.Hidden;
			CanvaDespesa.Visibility = Visibility.Hidden;
			CanvaGanho.Visibility = Visibility.Hidden;
			CanvaMetas.Visibility = Visibility.Hidden;
			CanvaCarteira.Visibility = Visibility.Visible;
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FFDDDDDD");
			bntInicio.Background = brush;
			bntDespesa.Background = brush;
			bntMetas.Background = brush;
			bntRelatorio.Background = brush;
			bntGanho.Background = brush;
			converter = new System.Windows.Media.BrushConverter();
			brush = (Brush)converter.ConvertFromString("#FFBEE5FB");
			bntCarteira.Background = brush;

		}

		private void AbrirRelatorio(object sender, RoutedEventArgs e)
		{
			CanvaInicio.Visibility = Visibility.Hidden;
			CanvaDespesa.Visibility = Visibility.Hidden;
			CanvaGanho.Visibility = Visibility.Hidden;
			CanvaMetas.Visibility = Visibility.Hidden;
			CanvaCarteira.Visibility = Visibility.Hidden;
			CanvaRelatorio.Visibility = Visibility.Visible;
			var converter = new System.Windows.Media.BrushConverter();
			var brush = (Brush)converter.ConvertFromString("#FFDDDDDD");
			bntCarteira.Background = brush;
			bntDespesa.Background = brush;
			bntMetas.Background = brush;
			bntInicio.Background = brush;
			bntGanho.Background = brush;
			converter = new System.Windows.Media.BrushConverter();
			brush = (Brush)converter.ConvertFromString("#FFBEE5FB");
			bntRelatorio.Background = brush;


		}

		private void Focusbnt(object sender, MouseEventArgs e)
		{
			bntInicio.BorderThickness = new Thickness(1, 1, 1, 1);
		}

		private void Desfocusbnt(object sender, MouseEventArgs e)
		{
			bntInicio.BorderThickness = new Thickness(0, 0, 0, 0);
		}

		private void Focusbntdespesa(object sender, MouseEventArgs e)
		{
			bntDespesa.BorderThickness = new Thickness(1, 1, 1, 1);
		}

		private void Desfocusbntdespesa(object sender, MouseEventArgs e)
		{
			bntDespesa.BorderThickness = new Thickness(0, 0, 0, 0);
		}
		private void Focusbntganho(object sender, MouseEventArgs e)
		{
			bntGanho.BorderThickness = new Thickness(1, 1, 1, 1);
		}

		private void Desfocusbntganho(object sender, MouseEventArgs e)
		{
			bntGanho.BorderThickness = new Thickness(0, 0, 0, 0);
		}
		private void Focusbntmetas(object sender, MouseEventArgs e)
		{
			bntMetas.BorderThickness = new Thickness(1, 1, 1, 1);
		}

		private void Desfocusbntmetas(object sender, MouseEventArgs e)
		{
			bntMetas.BorderThickness = new Thickness(0, 0, 0, 0);
		}
		private void Focusbntcarteira(object sender, MouseEventArgs e)
		{
			bntCarteira.BorderThickness = new Thickness(1, 1, 1, 1);
		}

		private void Desfocusbntcarteira(object sender, MouseEventArgs e)
		{
			bntCarteira.BorderThickness = new Thickness(0, 0, 0, 0);
		}
		private void Focusbntrelatorio(object sender, MouseEventArgs e)
		{
			bntRelatorio.BorderThickness = new Thickness(1, 1, 1, 1);
		}

		private void Desfocusbntrelatorio(object sender, MouseEventArgs e)
		{
			bntRelatorio.BorderThickness = new Thickness(0, 0, 0, 0);
		}


		private void Textoboxvalor_KeyDown(object sender, KeyEventArgs e)
		{
			var strKey = new KeyConverter().ConvertToString(e.Key);

			if ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9) || (e.Key >= Key.D0) && (e.Key <= Key.D9))
			{

				if (textoboxvalor.Text.Contains("."))
				{
					string[] words = textoboxvalor.Text.Split('.');
					if (words[1].Length >= 0 && words[1].Length < 2)
					{
						e.Handled = false;
					}
					else
					{
						e.Handled = true;
					}

				}
				else
				{
					e.Handled = false;
				}
			}

			else if (strKey == "OemPeriod")
			{
				if (textoboxvalor.Text == "")
				{
					e.Handled = true;
				}
				else if (textoboxvalor.Text.Contains("."))
				{

					e.Handled = true;

				}
				else
				{

					e.Handled = false;
				}
			}
			else
			{

				e.Handled = true;

			}




		}

		private void Textoboxvalor_GotFocus(object sender, RoutedEventArgs e)
		{
			textoboxvalor.Text = "";
		}

		private void Textoboxvalormeta_KeyDown(object sender, KeyEventArgs e)
		{
			var strKey = new KeyConverter().ConvertToString(e.Key);

			if ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9) || (e.Key >= Key.D0) && (e.Key <= Key.D9))
			{

				if (textoboxvalormeta.Text.Contains("."))
				{
					string[] words = textoboxvalormeta.Text.Split('.');
					if (words[1].Length >= 0 && words[1].Length < 2)
					{
						e.Handled = false;
					}
					else
					{
						e.Handled = true;
					}

				}
				else
				{
					e.Handled = false;
				}
			}

			else if (strKey == "OemPeriod")
			{
				if (textoboxvalormeta.Text == "")
				{
					e.Handled = true;
				}
				else if (textoboxvalormeta.Text.Contains("."))
				{

					e.Handled = true;

				}
				else
				{

					e.Handled = false;
				}
			}
			else
			{

				e.Handled = true;

			}



		}

		private void Textoboxvalormeta_GotFocus(object sender, RoutedEventArgs e)
		{
			textoboxvalormeta.Text = "";
			textoboxvalorGanho.Text = "";
		}

		private void SaveDespea_Click(object sender, RoutedEventArgs e)

		{
			
			string categoria = SelectCombo(CategoriaDespesa);
			string dataHoje = $"{c.ToString("yyyy")}-{c.ToString("MM")}-{c.ToString("dd")}";
			if (textoboxvalor.Text == "")
			{
				textoboxvalor.Text = "0";
			}
			try
			{
				double formatar = double.Parse(textoboxvalor.Text);
				Despesa despesaCriada = new Despesa(formatar, categoria, dataHoje, descricaoDespesa.Text);
				bool commit = despesaCriada.AdicionarDespesa(carteiraLogada.id);
				if (commit == false)
				{

					MessageBoxResult result = MessageBox.Show($"Erro ao Adicionar Despesa", "Adicionar Despesa");
					Zerar();
				}
				else
				{
					MessageBoxResult result = MessageBox.Show("Despesa Adicionada Com Sucesso", "Adicionar Despesa");
					Zerar();
				}
			}
			catch
			{
				MessageBox.Show("Erro ao adicionar Despesa", "ERROR");
			}


		}
		private void TextoboxvalorGanho_KeyDown(object sender, KeyEventArgs e)
		{
			var strKey = new KeyConverter().ConvertToString(e.Key);

			if ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9) || (e.Key >= Key.D0) && (e.Key <= Key.D9))
			{

				if (textoboxvalorGanho.Text.Contains("."))
				{
					string[] words = textoboxvalorGanho.Text.Split('.');
					if (words[1].Length >= 0 && words[1].Length < 2)
					{
						e.Handled = false;
					}
					else
					{
						e.Handled = true;
					}

				}
				else
				{
					e.Handled = false;
				}
			}

			else if (strKey == "OemPeriod")
			{
				if (textoboxvalorGanho.Text == "")
				{
					e.Handled = true;
				}
				else if (textoboxvalorGanho.Text.Contains("."))
				{

					e.Handled = true;

				}
				else
				{

					e.Handled = false;
				}
			}
			else
			{

				e.Handled = true;

			}

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
		{

		}

		private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
		{

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				string data = $"{c.ToString("yyyy")}-{MesRela.SelectedItem.ToString()}";
				if (SelecionarTipoRela.SelectedItem.ToString() == "Despesas")
				{
					Relatorio.gerarRelatorioDespesa(data, carteiraLogada.id);
				}
				else if (SelecionarTipoRela.SelectedItem.ToString() == "Ganhos")
				{
					Relatorio.gerarRelatorioGanho(data, carteiraLogada.id);
				}
				else
				{
					MessageBox.Show("O Sistema está impedindo o salvamento do Relatório");
				}
			}
			catch
			{
				MessageBox.Show("Veja se todos os campos estão selecionados", "ERROR");
			}
			
			
		}

		private void ComboBoxGanho_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ButtonSalvarGanho_Click(object sender, RoutedEventArgs e)
		{
			
			string categoria = SelectCombo(ComboBoxGanho);
			string dataHoje = $"{c.ToString("yyyy")}-{c.ToString("MM")}-{c.ToString("dd")}";
			if (textoboxvalorGanho.Text == "")
			{
				textoboxvalorGanho.Text = "0";
			}
			try
			{
				double formatar = double.Parse(textoboxvalorGanho.Text);
				Receita ganhoCriado = new Receita(formatar, categoria, dataHoje, descricaoDespesa.Text);
				bool commit = ganhoCriado.AdicionarGanho(carteiraLogada.id);
				if (commit == false)
				{
					MessageBox.Show($"Erro ao adicionar Ganho", "Adicionar Ganho");
					Zerar();
				}
				else
				{
					MessageBox.Show("Renda Adicionada Com Sucesso", "Adicionar Ganho");
					Zerar();
				}
			}
			catch
			{
				MessageBox.Show("Erro ao adicinar Ganho", "ERROR");
			}

		}

		private void BntSalvarMeta_Click(object sender, RoutedEventArgs e)
		{
			
			string mes = ComboMesMeta.SelectedItem.ToString();

			/*if (textoboxvalormeta.Text == "" || textoboxvalormeta.Text == "0" || textoboxvalormeta.Text == "0.0" || textoboxvalormeta.Text == "0.00" || textoboxvalormeta.Text == "0.")
			{
				MessageBox.Show("O Valor da sua meta não pode ser igual a zero", "Adicionar Meta");
			}*/
			string dataHoje = $"{c.ToString("yyyy")}-{mes}";
			if (textoboxvalormeta.Text == ""){
				textoboxvalormeta.Text = "0.00";
			}
			try
			{
				double formatar = double.Parse(textoboxvalormeta.Text);
				if (formatar == 0.0)
				{
					MessageBox.Show("Não é possivel adicionar meta igual a zero", "ERROR");
				}
				else
				{
					Meta metaCriada = new Meta(formatar, dataHoje, descricaoMeta.Text);
					bool commit = metaCriada.AdicionarMeta(carteiraLogada.id);
					if (commit == false)
					{
						MessageBox.Show("Você só pode adicionar 1 meta por mês", "ERROR");
						Zerar();

					}
					else
					{
						MessageBox.Show("Meta Adicionada Com Sucesso", "Adicionar Meta");
						Zerar();
					}
				}
			}
			catch {
				MessageBox.Show("Erro ao adicionar Meta", "ERROR");
			}
		}

		private void Buttondelete1_Click(object sender, RoutedEventArgs e)
		{
			metaAtual.Del(carteiraLogada.id);
			IniciarInicio();
		}

		private void ButtonClear2(object sender, RoutedEventArgs e)
		{
			metaProximo.Del(carteiraLogada.id);
			IniciarInicio();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			
			MessageBoxImage icone = MessageBoxImage.Warning;

			if (MessageBox.Show("Deseja Formatar Todas as Suas Metas ?", "Confirmação", MessageBoxButton.YesNo, icone) == MessageBoxResult.Yes)
			{
				Meta.FormatarMetas(carteiraLogada.id);
			}
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			MessageBoxImage icone = MessageBoxImage.Warning;

			if (MessageBox.Show("Deseja Formatar o Saldo Por Completo Da Carteira ?", "Confirmação", MessageBoxButton.YesNo,icone) == MessageBoxResult.Yes)
			{
				carteiraLogada.FormatarCarteira();
			}
		}

		private void Button_Click_4(object sender, RoutedEventArgs e)

		{
			MessageBoxImage icone = MessageBoxImage.Warning;
			if (MessageBox.Show("Deseja Formatar Todas as Despesas Deste Mês ?", "Confirmação", MessageBoxButton.YesNo, icone) == MessageBoxResult.Yes)
			{

				Despesa.FormatarDespesas(carteiraLogada.id,$"{c.ToString("yyyy")}-{c.ToString("MM")}");
			}
		}

		private void Button_Click_5(object sender, RoutedEventArgs e)
		{
			MessageBoxImage icone = MessageBoxImage.Warning;
			if (MessageBox.Show("Deseja Formatar Todos os Ganhos Deste Mes ?", "Confirmação", MessageBoxButton.YesNo, icone) == MessageBoxResult.Yes) ;
			Receita.FormatarReceitas(carteiraLogada.id, $"{c.ToString("yyyy")}-{c.ToString("MM")}");
		}

		private void ButtonEditarUsu_Click(object sender, RoutedEventArgs e)

		{
			
			if (Verificar(TextLoginAlt.Text) && TextNomeAlt.Text.Length>=4 && Verificar(TextSenhaAlt.Password.ToString()))
			{
				Usuario.AlterarDados(TextNomeAlt.Text, TextSenhaAlt.Password.ToString(), TextLoginAlt.Text, userLogado.id);
				Zerar();
				MessageBox.Show("Alterações realizadas com sucesso");

			}
			else
			{
				MessageBox.Show("Erro no cadastro preencha todos os campos corretamente\n *Não utilize caracteres especiais e o minimo de caracteres é 4");

			}
			
		}

		private void Button_Click_6(object sender, RoutedEventArgs e)
		{
			Zerar();
		}

		private void ButtonLimparGanho_Click(object sender, RoutedEventArgs e)
		{
			Zerar();
		}

		private void Button_Click_7(object sender, RoutedEventArgs e)
		{
			Zerar();
		}
	}
}













/*
private void Progress(object sender, RoutedEventArgs e)
{
Barra.Value = Barra.Value+10;
double percent = ((double)Barra.Value/(double)Barra.Maximum) *100;
porcentagem.Content = percent.ToString()+"%";
}*/


