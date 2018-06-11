using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Graphics;

namespace SlidingPuzzle
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        #region
        Button resetButton;
        GridLayout mainLayout;

        int gameViewWidth;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SetGameView();
           }

        private void SetGameView()
        {
            resetButton = FindViewById<Button>(Resource.Id.resetButtonId);
            mainLayout = FindViewById<GridLayout>(Resource.Id.gameGridLayoutId);

            gameViewWidth = Resources.DisplayMetrics.WidthPixels;
            mainLayout.ColumnCount = 4;
            mainLayout.RowCount = 4;

            mainLayout.LayoutParameters = new RelativeLayout.LayoutParams(gameViewWidth, gameViewWidth);
            mainLayout.SetBackgroundColor(Color.Gray);
        }
    }
}

