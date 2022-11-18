using System;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mono.CSharp;
using Xamarin.Essentials;

namespace Book
{
	public partial class MainPage : ContentPage
	{
		private string path;
		private string filename;

		public MainPage()
		{
			InitializeComponent();
			/*Rerquest permissions*/
			Permissions.RequestAsync<Permissions.StorageRead>();
			Permissions.RequestAsync<Permissions.StorageWrite>();
			
			/*Delete Title bar*/
			NavigationPage.SetHasNavigationBar(this, false);
			
			/*Unavailable functions on android <=10*/
			if (DeviceInfo.Version.Major <= 10)
			{
				openpdfbtn.IsVisible = false;
				pickerlabel.IsVisible = false;
				picker.IsVisible = false;
			}
			/*set backgroundimage from assets*/
			BackgroundImageSource = ImageSource.FromStream(() => Ui.GetAsset("background.jpg"));
			
			/* Check exist of directory and create*/
			if (Directory.Exists("/storage/emulated/0/FormatsPdf") == false)
			{
				Directory.CreateDirectory("/storage/emulated/0/FormatsPdf");
			}
			/* Back to menu page button*/
			backbtn.Clicked += async (s, e) =>
			{
				await Navigation.PopAsync();
			};
			//Choose file 
			openfilebtn.Clicked += async (s, e) =>
				{
					try
					{
						var open = await FilePicker.PickAsync();
						path = open.FullPath;
						filename = open.FileName;
						openfilebtn.Text = path;
						if (openfilebtn.Text == "")
						{
							openfilebtn.Text = "Выбрать файл...";
						}
						if (openfilebtn.Text.Contains(".pdf"))
						{
							startbtn.IsEnabled = true;
						}
					}
					catch
					{
						openfilebtn.Text = "Выбрать файл...";
					}

				};
			//Start format PDF from Book algoritm
			startbtn.Clicked += delegate
			{
				PdfReader lenPdf = new PdfReader(path);
				Books.CP = lenPdf.NumberOfPages;
				Books.CPB = Convert.ToInt32(pagesblock.Text);
				Books.GetPages();
				Books.GetPdf(path, filename);
				picker.Items.Add("/storage/emulated/0/FormatsPdf/" + filename);
				picker.SelectedItem = picker.Items.Last();
				if (DeviceInfo.Version.Major >= 11)
				{
					File.Delete(path);
				}

				if (File.Exists("/storage/emulated/0/FormatsPdf/" + filename))
				{
					Application.Current.MainPage.DisplayAlert("Сохранение","Файл сохранен как: /storage/emulated/0/FormatsPdf/" + filename, "ОК");
				}
				openfilebtn.Text = "Выбрать файл...";
				startbtn.IsEnabled = false;
			};

			//Get info of print button
			printbtn.Clicked += delegate
			{
				PdfReader lenPdf = new PdfReader(picker.SelectedItem.ToString());
				Books.CP = lenPdf.NumberOfPages;
				Books.CPB = Convert.ToInt32(pagesblock.Text);
				Books.GetPages();
				Books.GetInfo();
			};

			//Open pdf in other app
			openpdfbtn.Clicked += delegate
			{

				Xamarin.Essentials.Launcher.OpenAsync(new OpenFileRequest
				{
					File = new ReadOnlyFile(picker.SelectedItem.ToString())
				});
			};
			
			//Unavailable function on andtoid <=10
			if (DeviceInfo.Version.Major >= 11)
			{
				foreach (string file in Directory.GetFiles("/storage/emulated/0/FormatsPdf"))
				{
					if (picker.Items.Contains(file) == false)
					{
						picker.Items.Add(file);
						picker.SelectedItem = picker.Items.Last();
					}
				}
			}
		}
	}
}