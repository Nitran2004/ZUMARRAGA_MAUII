using Android.App;
using Android.Runtime;

namespace MartinZumarragaDogApi
{
	[Application]
	public class MainApplication : MauiApplication
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership)
			: base(handle, ownership)
		{
		}

		protected override MauiApp CreateMauiApp() => MZMauiProgram.CreateMauiApp();
	}
}
