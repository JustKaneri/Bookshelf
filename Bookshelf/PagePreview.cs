using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Bookshelf.Controler;

namespace Bookshelf
{
    [Activity(Label = "PagePreview")]
    public class PagePreview : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            SetContentView(Resource.Layout.PreviewPage);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(async () => { await SimulateStartupAsync(); });
            startupWork.Start();
        }

        private async Task SimulateStartupAsync()
        {
            await Task.Delay(1000);
            MainActivity._userControler = new UserControler();
            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(Result.Ok, intent);
            Finish();
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(Result.Canceled, intent);
            Finish();
        }
    }
}