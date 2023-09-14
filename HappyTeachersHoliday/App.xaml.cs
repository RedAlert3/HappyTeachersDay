using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows;

namespace HappyTeachersHoliday;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static (bool, Data.ClassModel?) IsInAnyClass()
    {
        foreach (var currentClass in Data.Classes)
        {
            if (currentClass.Activated) continue;

            var now = DateTime.Now;
            var begin = currentClass.Begin;
            var end = currentClass.End;
            var inDuration = now > begin && now < end;

            if (inDuration) return (true, currentClass);
        }

        return (false, null);
    }

    private static (bool, Data.ClassModel?) IsInClassWithoutWps()
    {
        foreach (var currentClass in Data.Classes)
        {
            if (currentClass.Activated) continue;

            var now = DateTime.Now;
            var begin = currentClass.Begin;
            var end = currentClass.End;
            var inDuration = now > begin && now < end;

            if (inDuration && ((now - begin).TotalSeconds >= 5 * 60))
                return (true, currentClass);
        }

        return (false, null);
    }

    private static (bool, Process[], Process[]) IsWppRunning()
    {
        var wpp_processes = Process.GetProcessesByName("wpp");
        var wps_processes = Process.GetProcessesByName("wps");

        return (wpp_processes.Any(), wpp_processes, wps_processes);
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        new KeepLiveWindow().Show();

        Data.Load();
        Data.Save();

        var loopCondition = true;

        new Thread(() =>
        {
            while (loopCondition)
            {
                var isWppRunning = IsWppRunning();
                var isInClassWithoutWps = IsInClassWithoutWps();
                var isInAnyClass = IsInAnyClass();
                var shouldAppear = false
                    || (isWppRunning.Item1 && isInAnyClass.Item1)
                    || isInClassWithoutWps.Item1;

                if (!shouldAppear)
                {
                    Thread.Sleep(3 * 1000);
                    continue;
                }

                foreach (var wpp_process in isWppRunning.Item2)
                    wpp_process.Kill();
                foreach (var wps_process in isWppRunning.Item3)
                    wps_process.Kill();

#if DEBUG
                Data.Classes[0].Activated = true;

                Dispatcher.Invoke(new(() =>
                {
                    new BlueScreenWindow()
                    {
                        CurrentClass = Data.Classes[0],
                    }.Show();
                }));
#else

                var currentClass = isInAnyClass.Item2;
                currentClass!.Activated = true;

                Data.Save();

                Dispatcher.Invoke(new(() =>
                {
                    new BlueScreenWindow()
                    {
                        CurrentClass = currentClass,
                    }.Show();
                }));
#endif

                Thread.Sleep(3 * 1000);
            }

        }).Start();
    }
}
