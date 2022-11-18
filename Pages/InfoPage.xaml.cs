using System;
using System.IO;
using Xamarin.Forms;

namespace Book
{
	public partial class InfoPage : ContentPage
	{
		
		public InfoPage()
		{
			InitializeComponent();
			
			//Set bacground image from assets
			BackgroundImageSource = ImageSource.FromStream(() => Ui.GetAsset("background.jpg"));
			vk.Source= ImageSource.FromStream(()=>Ui.GetAsset("vk.png"));
			telegram.Source= ImageSource.FromStream(()=>Ui.GetAsset("telegram.png"));
			github.Source= ImageSource.FromStream(()=>Ui.GetAsset("github.png"));
			
			//Close from the swipe down
			var swipedown = new SwipeGestureRecognizer { Direction = SwipeDirection.Down };
			
			swipedown.Swiped += async (s, e) =>
			{
				await Navigation.PopModalAsync();
			};
			stack.GestureRecognizers.Add(swipedown);
			var tapvk = new TapGestureRecognizer();
			
			tapvk.Tapped+= async(s,e)=>
			{
				Device.OpenUri(new Uri("https://vk.com/stasikextazik"));
			};
			vk.GestureRecognizers.Add(tapvk);
	
			var taptg = new TapGestureRecognizer();
			
			taptg.Tapped+= async(s,e)=>
			{
				Device.OpenUri(new Uri("https://tele.click/CHSS99"));
			};
			telegram.GestureRecognizers.Add(taptg);
			
			var tapgit = new TapGestureRecognizer();
			
			tapgit.Tapped+= async(s,e)=>
			{
				Device.OpenUri(new Uri("https://github.com/UISJOKE"));
			};
			github.GestureRecognizers.Add(tapgit);
			
			
		}
	}
}