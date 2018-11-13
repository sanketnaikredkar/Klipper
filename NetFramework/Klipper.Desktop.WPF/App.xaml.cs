using System.Windows;

namespace Klipper.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            if(!LoginExists())
            {
                new LoginLauncher().Launch();
            }
            else
            {
                new ApplicationLauncher().Launch();
            }
        }

        private bool LoginExists()
        {
            return false;
        }
    }
}
