using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Book
{
	static class Books
	{
		static private int Pages;
		static private int bookBlock;
		static private List<int> pgA = new List<int>();
		static private List<int> pgB = new List<int>();
		static private List<int> devlstA = new List<int>();
		static private List<int> devlstB = new List<int>();
		static private List<int> pgC = new List<int>();
		static private List<int> pgD = new List<int>();
		static private List<int> book = new List<int>();

		static public int CP
		{
			get => Pages;
			set
			{
				while (value % 4 != 0)
				{
					value++;
				}
				Pages = value;
			}
		}
		static public int CPB
		{
			get => bookBlock;
			set
			{
				while (value % 4 != 0)
				{
					value++;
				}
				bookBlock = value;
			}
		}
		static public string PL
		{
			get
			{

				foreach (int nC in pgC)
				{
					book.Add(nC);
				}
				pgD.Reverse();
				foreach (int nD in pgD)
				{
					book.Add(nD);
				}
				return string.Join(",", book);
			}
		}
		static public void GetPages()
		{
			for (int i = 0; i < Pages; i++)
			{

				if (i == 0)
				{

					pgA.Add(i);
				}
				else if (i % 2 == 0)
				{
					pgA.Add(i);
				}
				else
				{
					pgB.Add(i);
				}
			}
			while (pgA.Count > 0 || pgB.Count > 0)
			{
				for (int j = 0; j < bookBlock / 2; j++)
				{
					if (pgA.Count > 0)
					{
						devlstA.Add(pgA.First());
						pgA.Remove(pgA.First());
					}
					if (pgB.Count > 0)
					{
						devlstB.Add(pgB.First());
						pgB.Remove(pgB.First());
					}
				}
				for (int o = 0; o < 2; o++)
				{
					for (int i = 0; i < 4; i++)
					{
						if (devlstA.Count > 0)
						{
							pgC.Add(devlstA.Last() + 1);
							devlstA.Remove(devlstA.Last());
						}
						if (devlstB.Count > 0)
						{
							pgC.Add(devlstB.First() + 1);
							devlstB.Remove(devlstB.First());
						}
						if (devlstB.Count > 0)
						{
							pgD.Add(devlstB.Last() + 1);
							devlstB.Remove(devlstB.Last());
						}
						if (devlstA.Count > 0)
						{
							pgD.Add(devlstA.First() + 1);
							devlstA.Remove(devlstA.First());
						}
					}
				}
			}
		}


		static public void GetPdf(string open, string name)
		{
			PdfReader openReader = new PdfReader(open);
			if (openReader.NumberOfPages < 1)
			{
				Application.Current.MainPage.DisplayAlert("Ошибка","Ошибка! Возможно файл поврежден...", "ОК");
			}
			else if (open.Contains(".pdf"))
			{
				PdfStamper extStamper = new PdfStamper(openReader, new FileStream("/storage/emulated/0/ext.pdf", FileMode.Create));
				for (int numberOfPage = openReader.NumberOfPages + 1; numberOfPage < Pages + 1; numberOfPage++)
				{
					extStamper.InsertPage(numberOfPage, PageSize.A4);
				}
				openReader.Close();
				extStamper.Close();
				PdfReader extReader = new PdfReader("/storage/emulated/0/ext.pdf");
				extReader.SelectPages(PL);
				PdfStamper saveStamper = new PdfStamper(extReader, new FileStream("/storage/emulated/0/FormatsPdf/" + name, FileMode.Create));
				extReader.Close();
				saveStamper.Close();
				File.Delete("/storage/emulated/0/ext.pdf");
				
			}
		}


			static public void GetInfo()
			{
				if (CP / 4 > 0)
				{
					string msg0 = "Тип печати: односторонняя\n";
					string msg = "Необходимо листов бумаги: " + Convert.ToString(CP / 4) + "\n";
					string msg1 = "Лицевая сторона: 1-" + Convert.ToString(Pages / 2) + " страницы\nВнутренняя сторона: " + Convert.ToString(Pages / 2 + 1) + "-" + Convert.ToString(Pages) + " страницы\n";
					string msg2 = "Всего страниц: " + Convert.ToString(Pages);
					Application.Current.MainPage.DisplayAlert("Параметры печати", msg0 + msg + msg1 + msg2, "Ок");
				}
			}
		}
	}
