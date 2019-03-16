using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMyGallery
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> GamesControl = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAnimation(int type)
        {
            var widthAnimation = new DoubleAnimation(80, TimeSpan.FromSeconds(0.5)) { AutoReverse = true };
            if (type == 0)
            {
                //LeftButton.BeginAnimation(WidthProperty, null);
                //LeftButton.BeginAnimation(WidthProperty, widthAnimation);
            }
            else
            {
                //RightButton.BeginAnimation(WidthProperty, null);
                //RightButton.BeginAnimation(WidthProperty, widthAnimation);
            }
        }

        private void InitControl()
        {
            if (GamesControl == null || GamesControl.Count == 0)
            {
                GamesControl = GetGameControl();
            }
        }

        private List<Button> GetGameControl()
        {
            var result = new List<Button>();
            for (int i = 0; i < 100; i++)
            {
                var name = $"Games{i}";
                var c = FindName(name) as Button;
                if (c != null)
                {
                    result.Add(c);
                }
            }
            return result;
        }

        private void SetMovieAnimation(double time)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                
                ////for (int i = 0; i < 10; i++)
                ////{
                ////    GamesControl.Add(new Button() {  Content = i.ToString()});
                ////}


                var games = GamesControl;
                var bTemp = false;
                for (int i = 0; i < GamesControl.Count; i++)
                {
                    if (games[i].Visibility == Visibility.Hidden) continue;
                    var left = new DoubleAnimationUsingKeyFrames();
                    var leftkeyFrames = left.KeyFrames;
                    leftkeyFrames.Add(new LinearDoubleKeyFrame(Canvas.GetLeft(games[i]), TimeSpan.FromSeconds(0)));
                    var top = new DoubleAnimationUsingKeyFrames();
                    var topkeyFrames = top.KeyFrames;
                    topkeyFrames.Add(new LinearDoubleKeyFrame(Canvas.GetTop(games[i]), TimeSpan.FromSeconds(0)));
                    var width = new DoubleAnimationUsingKeyFrames();
                    var widthkeyFrames = width.KeyFrames;
                    widthkeyFrames.Add(new LinearDoubleKeyFrame(games[i].Width, TimeSpan.FromSeconds(0)));
                    var height = new DoubleAnimationUsingKeyFrames();
                    var heightkeyFrames = height.KeyFrames;
                    heightkeyFrames.Add(new LinearDoubleKeyFrame(games[i].Height, TimeSpan.FromSeconds(0)));
                    var opacity = new DoubleAnimationUsingKeyFrames();
                    var opacitykeyFrames = opacity.KeyFrames;
                    opacitykeyFrames.Add(new LinearDoubleKeyFrame(games[i].Opacity, TimeSpan.FromSeconds(0)));
                    var zindex = new Int32AnimationUsingKeyFrames();
                    var zindexkeyFrames = zindex.KeyFrames;
                    zindexkeyFrames.Add(new LinearInt32KeyFrame(Panel.GetZIndex(games[i]), TimeSpan.FromSeconds(0)));
                    for (var j = (i + 1) % games.Count; j != i; j = (j + 1) % games.Count)
                    {
                        if (games[j].Visibility != Visibility.Hidden)
                        {
                            leftkeyFrames.Add(new LinearDoubleKeyFrame(Canvas.GetLeft(games[j]), TimeSpan.FromSeconds(time)));
                            topkeyFrames.Add(new LinearDoubleKeyFrame(Canvas.GetTop(games[j]), TimeSpan.FromSeconds(time)));
                            widthkeyFrames.Add(new LinearDoubleKeyFrame(games[j].Width, TimeSpan.FromSeconds(time)));
                            heightkeyFrames.Add(new LinearDoubleKeyFrame(games[j].Height, TimeSpan.FromSeconds(time)));
                            opacitykeyFrames.Add(new LinearDoubleKeyFrame(games[j].Opacity, TimeSpan.FromSeconds(time)));
                            zindexkeyFrames.Add(new LinearInt32KeyFrame(Panel.GetZIndex(games[j]), TimeSpan.FromSeconds(0.1)));
                            break;
                        }
                    }
                    //if (games[i].IsSelected && !bTemp)
                    //{
                    //    for (var j = (games.Count + i - 1) % games.Count; j != i; j = (games.Count + j - 1) % games.Count)
                    //    {
                    //        if (games[j].Visibility != Visibility.Hidden)
                    //        {
                    //            bTemp = true;
                    //            games[i].IsSelected = false;
                    //            games[j].IsSelected = true;
                    //            SelectGame = BasicData[j];
                    //            break;
                    //        }
                    //    }

                    //}

                    games[i].BeginAnimation(Canvas.LeftProperty, left);
                    games[i].BeginAnimation(Canvas.TopProperty, top);
                    games[i].BeginAnimation(WidthProperty, width);
                    games[i].BeginAnimation(HeightProperty, height);
                    games[i].BeginAnimation(OpacityProperty, opacity);
                    games[i].BeginAnimation(Panel.ZIndexProperty, zindex);

                }
            }));
        }

        public void MoveToNext(double time)
        {
            SetMovieAnimation(time);
        }

        private void rbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbutton_Click(object sender, RoutedEventArgs e)
        {
            MoveToNext(0.5);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitControl();
        }
    }
}
