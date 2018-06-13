using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Graphics;
using Android.Views;
using System.Collections;
namespace SlidingPuzzle
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        #region
        Button resetButton;
        GridLayout mainLayout;

        int gameViewWidth;
        int tileWidth;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SetGameView();
            MakeTiles();
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

        private void MakeTiles()
        {
            tileWidth = gameViewWidth / 4;
            int tileCount = 1;
            for (int row=0; row<4; row++)
            {
               
                for (int col =0; col<4; col++)
                {
                    TextView tileText = new TextView(this);
                    GridLayout.Spec rowSpec = GridLayout.InvokeSpec(row);
                    GridLayout.Spec colSpec = GridLayout.InvokeSpec(col);

                    GridLayout.LayoutParams tileLayoutParams = new GridLayout.LayoutParams(rowSpec, colSpec);
                    tileText.Text = tileCount.ToString();
                    tileText.SetTextColor(Color.Black);
                    tileText.TextSize = 40;
                    tileText.Gravity = GravityFlags.Center;


                    tileLayoutParams.Width = tileWidth-10;
                    tileLayoutParams.Height = tileWidth-10;
                    tileLayoutParams.SetMargins(5, 5, 5, 5);

                    tileText.LayoutParameters = tileLayoutParams;
                    tileText.SetBackgroundColor(Color.Green);
                    mainLayout.AddView(tileText);

                    tileCount = tileCount+1;
                }
            }  
        }
    }
}

