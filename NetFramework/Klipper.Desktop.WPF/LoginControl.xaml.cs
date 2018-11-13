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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Klipper.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public event EventHandler<Tuple<string, string>> LoginAttempted;
        public event EventHandler<string> PasswordForgotten;

        public LoginControl()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {

        }

        private void ForgotPasswordLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
