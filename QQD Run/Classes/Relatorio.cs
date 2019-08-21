using System.Collections.Generic;
using System.IO;// A BIBLIOTECA DE ENTRADA E SAIDA DE ARQUIVOS
using System.Windows;
using iTextSharp;//E A BIBLIOTECA ITEXTSHARP E SUAS EXTENÇÕES
using iTextSharp.text;//ESTENSAO 1 (TEXT)
using iTextSharp.text.pdf;//ESTENSAO 2 (PDF)


public static class Relatorio
{



	public static void gerarRelatorioDespesa(string dataSlc, int id)
	{
		try
		{
			Document doc = new Document(PageSize.A4, 0, 0, 5, 0);


			string caminho = @"C:\Users\cleit\Desktop\QQD Run\QQD Run\Relatorios\" + $"Despesas {dataSlc}.pdf"; 


			PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
			doc.Open();

			Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 15);
			Paragraph Inicio = new Paragraph($"Relatório de Despesas", fonte);
			Inicio.IndentationLeft = 55;
			Paragraph Space = new Paragraph("        ");
			Font fontePhrase = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8);
			Paragraph Frase = new Paragraph($"Relatório com todas as despesas do periodo selecionado {dataSlc}, Com data em que foi realizada, categoria a qual pertence,\n" +
	$"além do valor em BRL e Descrição, como também a soma total do valor de todas as despesas", fontePhrase);
			Frase.IndentationLeft = 60;
			doc.Add(Inicio);
			doc.Add(Space);
			doc.Add(Frase);
			doc.Add(Space);
			//Criando Tabela
			PdfPTable tabelaDespesas = new PdfPTable(4);
			

			
			Paragraph coluna1 = new Paragraph("Data", fonte);
			Paragraph coluna2 = new Paragraph("Categoria", fonte);
			Paragraph coluna3 = new Paragraph("Valor", fonte);
			Paragraph coluna4 = new Paragraph("Descrição", fonte);

			var cell1 = new PdfPCell();
			var cell2 = new PdfPCell();
			var cell3 = new PdfPCell();
			var cell4 = new PdfPCell();

			cell1.AddElement(coluna1);
			cell2.AddElement(coluna2);
			cell3.AddElement(coluna3);
			cell4.AddElement(coluna4);
			tabelaDespesas.AddCell(cell1);
			tabelaDespesas.AddCell(cell2);
			tabelaDespesas.AddCell(cell3);
			tabelaDespesas.AddCell(cell4);
			Despesa d = new Despesa();
			List<Despesa> despesas = d.PegarDespesas(dataSlc, id);
			double total = 0;
			foreach(var despesa in despesas)
			{

				Phrase data = new Phrase(despesa.Data,fontePhrase);
				var cel1 = new PdfPCell(data);
				tabelaDespesas.AddCell(cel1);

				Phrase categoria = new Phrase(despesa.Tipo,fontePhrase);
				var cel2 = new PdfPCell(categoria);
				tabelaDespesas.AddCell(cel2);
				Phrase valor = new Phrase($"R$ {despesa.Valor}",fontePhrase);
				var cel3 = new PdfPCell(valor);
				tabelaDespesas.AddCell(cel3);

				Phrase descricao = new Phrase(despesa.Descricao,fontePhrase);
				var cel4 = new PdfPCell(descricao);
				tabelaDespesas.AddCell(cel4);
				total += despesa.Valor;

			}
			Phrase p1 = new Phrase("");
			var celSpace = new PdfPCell(p1);
			tabelaDespesas.AddCell(celSpace);
			Phrase p2 = new Phrase("Total:");
			var celSpace2 = new PdfPCell(p2);
			tabelaDespesas.AddCell(celSpace2);
			Phrase p3 = new Phrase($"R$ {total}");
			var celSpace3 = new PdfPCell(p3);
			tabelaDespesas.AddCell(celSpace3);
			Phrase p4 = new Phrase("");
			var celSpace4 = new PdfPCell(p4);
			tabelaDespesas.AddCell(celSpace4);

			

			doc.Add(tabelaDespesas);

			doc.Close();
			MessageBox.Show($"Relatório Gerado Com Sucesso, Procure na pasta Raiz do Sistema QQD procure por .../Relatórios/Despesas {dataSlc}.pdf ", "Gerada Com Sucesso");





		}
		catch
		{
			MessageBox.Show("O Sistema está impedindo o salvamento do Relatório");
		}

	}
	public static void gerarRelatorioGanho(string dataSlc, int id)
	{
		try
		{
			Document doc = new Document(PageSize.A4, 0, 0, 5, 0);


			string caminho = @"C:\Users\cleit\Desktop\QQD Run\QQD Run\Relatorios\" + $"Ganhos {dataSlc}.pdf";


			PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
			doc.Open();

			Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 15);
			Paragraph Inicio = new Paragraph($"Relatório de Ganhos", fonte);
			Inicio.IndentationLeft = 55;
			Paragraph Space = new Paragraph("        ");
			Font fontePhrase = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8);
			Paragraph Frase = new Paragraph($"Relatório com todos os Ganhos do periodo selecionado {dataSlc}, Com data em que foi realizada, categoria a qual pertence,\n" +
	$"além do valor em BRL e Descrição, como também a soma total do valor de todos os Ganhos", fontePhrase);
			Frase.IndentationLeft = 60;
			doc.Add(Inicio);
			doc.Add(Space);
			doc.Add(Frase);
			doc.Add(Space);


			//Criando Tabela
			PdfPTable tabelaGanhos = new PdfPTable(4);


		
			Paragraph coluna1 = new Paragraph("Data", fonte);
			Paragraph coluna2 = new Paragraph("Categoria", fonte);
			Paragraph coluna3 = new Paragraph("Valor", fonte);
			Paragraph coluna4 = new Paragraph("Descrição", fonte);

			var cell1 = new PdfPCell();
			var cell2 = new PdfPCell();
			var cell3 = new PdfPCell();
			var cell4 = new PdfPCell();

			cell1.AddElement(coluna1);
			cell2.AddElement(coluna2);
			cell3.AddElement(coluna3);
			cell4.AddElement(coluna4);

			tabelaGanhos.AddCell(cell1);
			tabelaGanhos.AddCell(cell2);
			tabelaGanhos.AddCell(cell3);
			tabelaGanhos.AddCell(cell4);
			double total = 0;
			Receita g = new Receita();
			List<Receita> receitas = g.PegarReceitas(dataSlc, id);
			foreach (var receita in receitas)
			{

				Phrase data = new Phrase(receita.Data, fontePhrase);
				var cel1 = new PdfPCell(data);
				tabelaGanhos.AddCell(cel1);

				Phrase categoria = new Phrase(receita.Categoria, fontePhrase);
				var cel2 = new PdfPCell(categoria);
				tabelaGanhos.AddCell(cel2);
				Phrase valor = new Phrase($"R$ {receita.Valor}", fontePhrase);
				var cel3 = new PdfPCell(valor);
				tabelaGanhos.AddCell(cel3);

				Phrase descricao = new Phrase(receita.Descricao, fontePhrase);
				var cel4 = new PdfPCell(descricao);
				tabelaGanhos.AddCell(cel4);
				total += receita.Valor;

			}
			Phrase p1 = new Phrase("");
			var celSpace = new PdfPCell(p1);
			tabelaGanhos.AddCell(celSpace);
			Phrase p2 = new Phrase("Total:");
			var celSpace2 = new PdfPCell(p2);
			tabelaGanhos.AddCell(celSpace2);
			Phrase p3 = new Phrase($"R$ {total}");
			var celSpace3 = new PdfPCell(p3);
			tabelaGanhos.AddCell(celSpace3);
			Phrase p4 = new Phrase("");
			var celSpace4 = new PdfPCell(p4);
			tabelaGanhos.AddCell(celSpace4);
			doc.Add(tabelaGanhos);

			doc.Close();

			MessageBox.Show($"Relatório Gerado Com Sucesso, Procure na pasta Raiz do Sistema QQD procure por .../Relatórios/Ganhos {dataSlc}.pdf ", "Gerada Com Sucesso");



		}
		catch
		{
			MessageBox.Show("O Sistema está impedindo o salvamento do Relatório");
		}

	}

}

