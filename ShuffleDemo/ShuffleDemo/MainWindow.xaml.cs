using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShuffleDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BitmapImage[] pokers = new BitmapImage[54];
        CardPosition[] cardPositions = new CardPosition[54];
        Image[] images = new Image[54];
        ResultInfo resultInfo;
        int stepNumber, stepTotal;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int i;
            for (i = 0; i < 54; i++)
            {
                cardPositions[i] = new CardPosition(new Thickness(50 + 20 * i, this.Height / 2, 0, 0), i);
            }
            for (i = 0; i < 54; i++)
            {
                pokers[i] = new BitmapImage(new Uri(Environment.CurrentDirectory + "/image/" + (i + 1) + ".jpg", UriKind.RelativeOrAbsolute));
                images[i] = new Image();
                images[i].Width = 105;
                images[i].Height = 150;
                images[i].Margin = cardPositions[i].Margin;
                images[i].HorizontalAlignment = HorizontalAlignment.Left;
                images[i].Source = pokers[i];
                images[i].Opacity = 0;
                grid.Children.Add(images[i]);
                Grid.SetZIndex(images[i], cardPositions[i].ZIndex);
            }
            //Thickness m1 = images[1].Margin, m2 = images[20].Margin;
            //new Thread(() =>
            //{
            //    Thread.Sleep(1000);
            //    animation2(images[1], m1, m2, 20);
            //    animation2(images[20], m2, m1, 1);
            //}).Start();





        }


        private void animation1(Image image, CardPosition cardPosition)
        {
            image.Margin = cardPosition.Margin;
            Grid.SetZIndex(image, cardPosition.ZIndex);
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    image.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        image.Opacity = (double)i / 100;
                    }));
                    Thread.Sleep(5);
                }
            });
            thread.Start();
        }

        private void animation2(Image image, Thickness margin1, Thickness margin2, int index)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i <= 305; i++)
                {
                    image.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        image.Margin = new Thickness(image.Margin.Left, image.Margin.Top - 1, 0, 0);
                    }));
                    Thread.Sleep(1);
                }
                grid.Dispatcher.Invoke(new Action(() =>
                {
                    Grid.SetZIndex(image, index);
                }));
                int interval = (margin1.Left < margin2.Left) ? 1 : -1;
                for (int i = 0; i <= Math.Abs(margin1.Left - margin2.Left); i++)
                {
                    image.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        image.Margin = new Thickness(image.Margin.Left + interval, image.Margin.Top, 0, 0);
                    }));
                    Thread.Sleep(1);
                }
                for (int i = 0; i <= 305; i++)
                {
                    image.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        image.Margin = new Thickness(image.Margin.Left, image.Margin.Top + 1, 0, 0);
                    }));
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String s1 = GetResult.compileMain(compileText.Text, runText.Text);
            MessageBox.Show("Compile success", "Compile Message");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            long start = 0, end = 0;
            String s2 = GetResult.getResultString(runText.Text, ref start, ref end);
            resultInfo = new ResultInfo(s2, start, end);
            preText.Text = "";
            curText.Text = "";
            stepNumber = 0;
            stepTotal = resultInfo.Result.GetLength(0);
            for (int i = 0; i < 54; i++)
            {
                curText.Text += (resultInfo.Result[0, i] + " ");
                if (resultInfo.Result[0, i] != -1) animation1(images[resultInfo.Result[0, i]], cardPositions[i]);
            }
            stepLabel.Content = "步数：" + (++stepNumber);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(numText.Text);
            int[,] results;
            long start = 0, end = 0;
            int[,] analysis = new int[54, 54];
            int i, j;

            String tmp = GetResult.getResultString2(runText.Text, n, ref start, ref end);
            results = new ResultInfo(tmp, 0, 0).Result;

            for (i = 0; i < 54; i++)
            {
                for (j = 0; j < 54; j++)
                {
                    analysis[i, j] = 0;
                }
            }
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < 54; j++)
                {
                    analysis[results[i, j], j]++;
                }
            }

            String s1 = "";
            if (n < 200)
            {
                s1 = "|0|1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|\r\n";
                s1 += "|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|\r\n";
                for (i = 0; i < n; i++)
                {
                    s1 += "|";
                    for (j = 0; j < 54; j++)
                    {
                        s1 += results[i, j] + "|";
                    }
                    s1 += "\r\n";
                }
            }
            s1 += $"> StartTime: {start}\tEndTime: {end}\tDuration: {end - start}";

            String s2 = "|*|0|1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|\r\n";
            s2 += "|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|\r\n";
            for (i = 0; i < 54; i++)
            {
                s2 += $"|{i}|";
                for (j = 0; j < 54; j++)
                {
                    s2 += analysis[i, j] + "|";
                }
                s2 += "\r\n";
            }

            StreamWriter sw = new StreamWriter("result.md", false);
            sw.WriteLine($"# Experiment Results\r\n{s1}\r\n# Analysis\r\n{s2}");
            sw.Close();
            MessageBox.Show("See result.md");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int cur = stepNumber;
            if (cur == stepTotal)
            {
                MessageBox.Show("End!");
                return;
            }
            int i, j;
            stepLabel.Content = "步数：" + (++stepNumber);
            preText.Text = curText.Text;
            curText.Text = "";
            for (i = 0; i < 54; i++)
            {
                curText.Text += (resultInfo.Result[cur, i] + " ");
            }
            for (i = 0; i < 54; i++)
            {
                if (resultInfo.Result[cur, i] != -1)
                {
                    for (j = 0; j < 54; j++)
                    {
                        if (resultInfo.Result[cur, i] == resultInfo.Result[cur - 1, j])
                        {
                            if (i == j) break;
                            animation2(images[resultInfo.Result[cur - 1, j]]
                                , cardPositions[j].Margin
                                , cardPositions[i].Margin
                                , cardPositions[i].ZIndex);
                            break;
                        }
                    }
                    if (j == 54) animation1(images[resultInfo.Result[cur, i]], cardPositions[i]);
                }
            }
        }
    }

    class CardPosition
    {
        public Thickness Margin { get; set; }
        public int ZIndex { get; set; }

        public CardPosition(Thickness margin, int zIndex)
        {
            Margin = margin;
            ZIndex = zIndex;
        }
    }
}
