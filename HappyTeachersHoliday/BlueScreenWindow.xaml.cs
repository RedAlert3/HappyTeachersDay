using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HappyTeachersHoliday
{
    /// <summary>
    /// BlueScreenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BlueScreenWindow : Window
    {
        public BlueScreenWindow()
        {
            InitializeComponent();

            RestartingContentContainer.Visibility = Visibility.Hidden;
        }

        internal Data.ClassModel? CurrentClass;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BlueScreenContentContainer.Visibility = Visibility.Hidden;
            RestartingContentContainer.Visibility = Visibility.Visible;

            Topmost = false;

            new Thread(() =>
            {
                Thread.Sleep(3 * 1000);

                Dispatcher.Invoke(new(() =>
                {
                    var win = new MainWindow();
                    win.CurrentClass = CurrentClass ?? Data.Classes[0];

                    win.Activated += (_, _) => Task.Run(() =>
                    {
                        Thread.Sleep(6 * 1000);
                        Dispatcher.Invoke(new(() => Hide()));
                    });

                    win.Show();
                    win.Activate();
                }));

                Thread.Sleep(10 * 1000);

                Dispatcher.Invoke(() => Close());
            }).Start();
        }

        private static Random random = new();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                var previousVolume = AudioManager.GetMasterVolume();
                AudioManager.SetMasterVolume(100.0f);
                SystemSounds.Hand.Play();
                new Thread(() =>
                {
                    Thread.Sleep(1500);
                    AudioManager.SetMasterVolume(previousVolume);
                }).Start();
            }).Start();

            new Thread(() =>
            {
                for (var i = 0; i <= 100; ++ i)
                {
                    Dispatcher.Invoke(new(() =>
                    {
                        CompletePercentTextBlock.Text = i.ToString();
                    }));
                    Thread.Sleep(random.Next(0, 20));
                }
            }).Start();
        }
    }
}
