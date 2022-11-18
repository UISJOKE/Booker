using System;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Core;

namespace Book
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage()
		{
			InitializeComponent();
			
			//Delete navBar
			NavigationPage.SetHasNavigationBar(this, false);
			
			//Set background image from assets
			BackgroundImageSource = ImageSource.FromStream(() => Ui.GetAsset("background.jpg"));
			logo.Source= ImageSource.FromStream(()=>Ui.GetAsset("logo.png"));
			
			//Go to work with pdf document
			MainPageBtn.Clicked += async (s, e) =>
			{
				await Navigation.PushAsync(new MainPage());
			};
			
			InfoPageBtn.IsEnabled = true;
			
			//View info about app
			InfoPageBtn.Clicked += async (s, e) =>
			{
				await Navigation.PushModalAsync(new InfoPage());
			};
			
			//Exit from app
			exitbtn.Clicked += delegate
			{


				Environment.Exit(0);
			};


		}
	}
}