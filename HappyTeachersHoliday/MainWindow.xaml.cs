using System;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static HappyTeachersHoliday.Data;

namespace HappyTeachersHoliday;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        FlowerImage.Opacity = 0;
        GreeingContainer.Opacity = 0;
        QuitButton.Opacity = 0;
        FadingEllipse.Width = 0;
        FadingEllipse.Height = 0;
        WindowState = WindowState.Maximized;
    }

    internal ClassModel? CurrentClass
    {
        set
        {
            TargetTextBlock.Text = value?.Teacher;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        new Thread(() =>
        {
            Dispatcher.Invoke(new(() =>
            {
                Animator.AnimateSize(
                    FadingEllipse,
                    (0, 0), (500, 500), 700,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    }
                );
            }));

            Thread.Sleep(3 * 1000 / 10);

            Dispatcher.Invoke(new(() =>
            {
                Animator.AnimateOpacity(
                    FlowerImage, 0, 1, 550,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseIn,
                    }
                );
            }));

            Thread.Sleep(12 * 1000 / 10);

            Dispatcher.Invoke(new(() =>
            {
                Animator.AnimateSize(
                    FadingEllipse,
                    (500, 500), (3500, 3500), 1200,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    }
                );
            }));

            Thread.Sleep(3 * 1000 / 10);

            Dispatcher.Invoke(new(() =>
            {
                Animator.AnimateMargin(
                    FlowerTransfermationContainer,
                    new(0),
                    new(0, 0, 1200, 0),
                    450,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseInOut,
                    }
                );

                Animator.AnimateOpacity(
                    GreeingContainer,
                    0,
                    1,
                    450,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseInOut,
                    }
                );

                Animator.AnimateMargin(
                    GreetingContent,
                    new(0),
                    new(300, 0, 300, 0),
                    450,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseInOut,
                    }
                );
            }));

            Thread.Sleep(6 * 1000 / 10);

            Dispatcher.Invoke(new(() =>
            {
                Animator.AnimateOpacity(
                    QuitButton,
                    0,
                    1,
                    450,
                    easing: new CubicEase()
                    {
                        EasingMode = EasingMode.EaseInOut,
                    }
                );
            }));
        }).Start();

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
    }

    private void Window_Activated(object sender, EventArgs e)
    {

    }

    private int quitButtonClickedCount = 0;
    private readonly Random random = new();

    private void QuitButton_Click(object sender, RoutedEventArgs e)
    {
        quitButtonClickedCount++;

        if (quitButtonClickedCount >= 3)
        {
            Animator.AnimateOpacity(
                this, 1, 0, 400,
                easing: new CubicEase()
                {
                    EasingMode = EasingMode.EaseIn
                }
            );

            new Thread(() =>
            {
                Thread.Sleep(5 * 1000);

                Dispatcher.Invoke(new(() =>
                {
                    Close();
                }));
            }).Start();
        }
        else
        {
            var x = random.Next(90, 1600);
            var y = random.Next(90 + 400, 920);

            Canvas.SetLeft(QuitButton, x);
            Canvas.SetTop(QuitButton, y);
        }
    }
}
