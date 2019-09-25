using Android.App;

namespace ToolkitDemo.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            System.Threading.Thread.Sleep(3000);
            StartActivity(typeof(MainActivity));
        }
    }
}
