using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;


namespace Book
{
	public partial class App : Application
	{
		public App()
		{

			InitializeComponent();
			/*Rerquest permissions*/
			Permissions.RequestAsync<Permissions.StorageRead>();
			Permissions.RequestAsync<Permissions.StorageWrite>();
			
			MainPage = new NavigationPage(new MenuPage());

		}
	}
}