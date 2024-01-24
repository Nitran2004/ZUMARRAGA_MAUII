using Foundation;

namespace MartinZumarragaDogApi
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MZMauiProgram.CreateMauiApp();
	}
}
