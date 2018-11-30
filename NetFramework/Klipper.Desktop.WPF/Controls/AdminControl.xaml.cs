﻿using Klipper.Desktop.WPF.CustomControls;
using Models.Core;
using Models.Core.Employment;
using Newtonsoft.Json;
using Sparkle.Appearance;
using Sparkle.Controls.Buttons;
using Sparkle.Controls.Dialogs;
using Sparkle.Controls.Panels;
using Sparkle.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Klipper.Desktop.WPF.Controls
{
    /// <summary>
    /// Interaction logic for AdminControl.xaml
    /// </summary>
    public partial class AdminControl : UserControl
    {
        public AdminControl()
        {
            InitializeComponent();
            AdminMenu.CollapsedWidth = 50.0;
            AdminMenu.ExpandedWidth = 220.0;
            AdminMenu.SelectedIndex = 0;
            AdminMenu.MenuSelectionChanged += OnMenuSelectionChanged;
            AdminMenu.Expand();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            var strs = new Dictionary<string, string>() {
                { "User Management", "Continue_disabled.png" },
                { "Settings", "Continue_disabled.png" },
            };

            foreach (var k in strs.Keys)
            {
                var imageName = strs[k];
                var tab = new SelectableItem(k, GetControl(imageName), "./Images/Generic/" + imageName)
                {
                    IconHeight = 35,
                    IconWidth = 35,
                    ItemHeight = 50
                };
                AdminMenu.AddMenuItem(tab);
            }
        }

        private ContentControl GetControl(string imageName)
        {
            var imageSource = (ImageSource)new ImageSourceConverter().ConvertFromString("./Images/Generic/" + imageName);
            var image = new Image()
            {
                Source = imageSource,
                Width = 290
            };

            var b = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                Child = image,
                CornerRadius = new CornerRadius(0),
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                Background = new SolidColorBrush(Color.FromRgb(45, 55, 65)),
            };
            return new ContentControl()
            {
                Content = b,
                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };

        }

        private void OnMenuSelectionChanged(object sender, SelectableItemSelectionChangedEventArgs e)
        {
            if (e.Current.Header.Equals("User Management",StringComparison.OrdinalIgnoreCase))
            {
                InteractionArea.Content = new EmployeeListPanelControl(); //load dynamic panel
            }
            else
            {
                InteractionArea.Content = null;
            }
            AdminMenu.ForceResize();
        }

        private void AddEmployee_click(object sender, RoutedEventArgs e)
        {
            GenericDialog dlg = new GenericDialog();
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dlg.CollapseBottomRegion();
            dlg.ShowCloseButton = true;
            dlg.Header = "Add New Employee";
            AppearanceManager.SetAppearance(dlg);

            var cp = new BasicDialogPanel();
            var control = new AddEmployeeControl();
            cp.Container.Content = control;

            dlg.SetDialogRegion(cp);
            dlg.DialogClosed += (s, args) => { dlg.Close(); };

            var btn = new PanelButton() { ButtonWidth = 150, ButtonText = "close!" };
            btn.Clicked += (s, args) => { dlg.Close(); };
            dlg.AddButton(btn);
            dlg.ShowDialog();
            //AddChild(dlg);
        }
    }
}
