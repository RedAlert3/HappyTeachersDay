using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HappyTeachersHoliday;

public static class Animator
{

    public static void AnimateOpacity(DependencyObject target, double from, double to, double milliSeconds = 500, IEasingFunction? easing = null)
    {
        var opacityAnimation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = TimeSpan.FromMilliseconds(milliSeconds),
        };

        if (easing is not null) opacityAnimation.EasingFunction = easing;

        Storyboard.SetTarget(opacityAnimation, target);
        Storyboard.SetTargetProperty(
            opacityAnimation, new("Opacity", null)
        );

        var storyboard = new Storyboard()
        {
            FillBehavior = FillBehavior.HoldEnd
        };
        storyboard.Children.Add(opacityAnimation);
        storyboard.Begin();
    }

    public static void AnimateSize(DependencyObject target, (double, double) from, (double, double) to, double milliSeconds = 500, IEasingFunction? easing = null)
    {
        var widthAnimation = new DoubleAnimation
        {
            From = from.Item1,
            To = to.Item1,
            Duration = TimeSpan.FromMilliseconds(milliSeconds),
        };
        var heightAnimation = new DoubleAnimation
        {
            From = from.Item2,
            To = to.Item2,
            Duration = TimeSpan.FromMilliseconds(milliSeconds),
        };

        if (easing is not null)
        {
            widthAnimation.EasingFunction = easing;
            heightAnimation.EasingFunction = easing;
        }

        Storyboard.SetTarget(widthAnimation, target);
        Storyboard.SetTargetProperty(
            widthAnimation, new("Width", null)
        );

        Storyboard.SetTarget(heightAnimation, target);
        Storyboard.SetTargetProperty(
            heightAnimation, new("Height", null)
        );

        var storyboard = new Storyboard
        {
            FillBehavior = FillBehavior.HoldEnd
        };
        storyboard.Children.Add(widthAnimation);
        storyboard.Children.Add(heightAnimation);
        storyboard.Begin();
    }


    public static void AnimateMargin(DependencyObject target, Thickness from, Thickness to, double milliSeconds = 500, IEasingFunction? easing = null)
    {
        var thicknessAnimation = new ThicknessAnimation
        {
            From = from,
            To = to,
            Duration = TimeSpan.FromMilliseconds(milliSeconds),
        };

        if (easing is not null)
            thicknessAnimation.EasingFunction = easing;

        Storyboard.SetTarget(thicknessAnimation, target);
        Storyboard.SetTargetProperty(
            thicknessAnimation, new("Margin", null)
        );

        var storyboard = new Storyboard
        {
            FillBehavior = FillBehavior.HoldEnd
        };
        storyboard.Children.Add(thicknessAnimation);
        storyboard.Begin();
    }
}
