using Sparkle.Appearance;
using Sparkle.Controls.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Klipper.Desktop.WPF
{
    internal class LoginLauncher
    {
        LoginControl _loginControl = null;

        public LoginLauncher()
        {
        }

        internal void Launch()
        {
            _loginControl = new LoginControl();
            _loginControl.LoginAttempted += OnLogin;
            _loginControl.PasswordForgotten += OnPasswordForgotten;

            var control = new ContentControl()
            {
                Content = _loginControl
            };

            AnimatedDialog dialog = new AnimatedDialog(
                new Point(0, 0),
                BalloonPopupPosition.ScreenCenter,
                control,
                50, true, true, null, null)
            {
                ShowHeaderPanel = true,
                ShowTickButton = false
            };

            //dialog.KeyUp += (s, args) =>
            //{
            //    var dlg = s as AnimatedDialog;
            //    dlg?.Close();
            //};
            dialog.ShowCloseButton = true;
            dialog.ShowMaximizeRestore = false;
            AppearanceManager.SetAppearance(dialog);
            dialog.Show();
        }

        private void OnPasswordForgotten(object sender, string e)
        {
            throw new NotImplementedException();
        }

        private void OnLogin(object sender, Tuple<string, string> e)
        {
            throw new NotImplementedException();
        }
    }
}