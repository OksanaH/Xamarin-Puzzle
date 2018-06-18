using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Graphics;
using Android.Views;
using System.Collections;
using System;

namespace SlidingPuzzle
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        #region
        Button resetButton;
        GridLayout mainLayout;
        ArrayList tilesList = new ArrayList();
        ArrayList coordList = new ArrayList();

        int gameViewWidth;
        int tileWidth;

        Point emptySpot;
        
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
            resetButton = (Button)FindViewById<Button>(Resource.Id.resetButtonId);
            resetButton.Click += (object sender, EventArgs e) => {
                Reset(sender, e);
            };
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
                    MyTextView tileText = new MyTextView(this);
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

                    //save coordinates of tile
                    Point tileLocation =new Point(col, row);
                    coordList.Add(tileLocation);
                    tilesList.Add(tileText);

                    //remember the position of the tile
                    tileText.coordX = tileLocation.X;
                    tileText.coordY = tileLocation.Y;

                    tileText.Touch += TouchTile;
                    mainLayout.AddView(tileText);

                    tileCount = tileCount+1;
                }
            }
            mainLayout.RemoveView((MyTextView)tilesList[15]);
            tilesList.RemoveAt(15);
        }

        private void RandomiseTiles()
        {
            Random randomiser = new Random();
            ArrayList copyCoordList = new ArrayList(coordList);

            foreach (MyTextView tile in tilesList)
            {
                int randIndex = randomiser.Next(0, copyCoordList.Count);
                Point randomisedLocation = (Point)copyCoordList[randIndex];

                GridLayout.Spec rowSpec = GridLayout.InvokeSpec(randomisedLocation.X);
                GridLayout.Spec colSpec = GridLayout.InvokeSpec(randomisedLocation.Y);
                GridLayout.LayoutParams randTileParams = new GridLayout.LayoutParams(rowSpec, colSpec);

                //keep location of tile
                tile.coordX = randomisedLocation.X;
                tile.coordY = randomisedLocation.Y;
                
                randTileParams.Width = tileWidth - 10;
                randTileParams.Height = tileWidth - 10;
                randTileParams.SetMargins(5, 5, 5, 5);

                tile.LayoutParameters = randTileParams;
                copyCoordList.RemoveAt(randIndex);
            }
            emptySpot = (Point)copyCoordList[0];
        }

        void Reset(object sender, EventArgs e)
        {
            RandomiseTiles();
        }  
        void TouchTile(object sender, View.TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Up)
            {
                MyTextView tile = (MyTextView)sender;

                Console.WriteLine("tile is at x={0}, y={0}", tile.coordX, tile.coordY);

                //calculate distance between empty spot and the touched tile
                float xDif = (float)Math.Pow(tile.coordX - emptySpot.X, 2);
                float yDif = (float)Math.Pow(tile.coordY - emptySpot.Y, 2);
                float distance = (float)Math.Sqrt(xDif + yDif);
                //tile next to empty spot
                if (distance ==1)
                {
                    Point currentPoint = new Point(tile.coordX, tile.coordY);
                    GridLayout.Spec rowSpec = GridLayout.InvokeSpec(emptySpot.X);
                    GridLayout.Spec colSpec = GridLayout.InvokeSpec(emptySpot.Y);
                    GridLayout.LayoutParams newLocationParams = new GridLayout.LayoutParams(rowSpec, colSpec);
                    tile.coordX = emptySpot.X;
                    tile.coordY = emptySpot.Y;

                    newLocationParams.Width = tileWidth - 10;
                    newLocationParams.Height = tileWidth - 10;
                    newLocationParams.SetMargins(5, 5, 5, 5);
                    tile.LayoutParameters = newLocationParams;
                    emptySpot = currentPoint;
                }
            }
        }
    }
    class MyTextView : TextView
    {
        Activity myContext;
        public MyTextView(Activity context):base(context)
        {
            myContext = context;
        }
        public int coordX { get; set; }
        public int coordY { get; set; }
    }
}

