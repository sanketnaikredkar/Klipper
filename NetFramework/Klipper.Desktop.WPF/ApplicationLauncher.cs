using Sparkle.Appearance;
using Sparkle.Controls.Panels;
using Sparkle.Controls.Workflows;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using Sparkle.Controls.Buttons;
using Sparkle.DataStructures;
using Sparkle.Controls.Navigators;
using System.Windows.Controls;

namespace Klipper.Desktop.WPF
{
    public class ApplicationLauncher
    {
        private MultiColorTextPanel _textPanel;

        public void Launch()
        {
            StockApplicationWindow w = new StockApplicationWindow()
            {
                WindowBackground = AppearanceManager.GetCurrentSkinResource("BackgroundBase_02") as Brush,
                TopPanelBackground = AppearanceManager.GetCurrentSkinResource("BackgroundBase_04") as Brush,
                BottomPanelBackground = AppearanceManager.GetCurrentSkinResource("BackgroundBase_02") as Brush,
                SideToolbarBackground = AppearanceManager.GetCurrentSkinResource("BackgroundBase_05") as Brush,
                TopPanelHeight = 55,
                BottomPanelHeight = 65,
                TopStripHeight = 10,
                BottomStripHeight = 5,
                WindowHeaderIcon = (ImageSource)new ImageSourceConverter().ConvertFromString("./Images/Klingelnberg/Klingelnberg_Logo.png"),
                WindowHeader = "Klipper",
                ShowSideToolbar = true
            };
            LoadNavigator(w);
            LoadTopPanel(w);
            LoadBottomPanel(w);
            LoadSideToolbar(w);

            w.WindowState = System.Windows.WindowState.Maximized;
            w.Topmost = true;

            w.Show();
        }

        private void LoadSideToolbar(StockApplicationWindow w)
        {
            var s = w.SideToolbar;
            s.HorizontalAlignment = HorizontalAlignment.Stretch;
            var iconSize = 35.0;
            s.AddTool(GetToolbarButton("DeveloperToolbox/Save_environment", iconSize, () => { MessageBox.Show("Save test environment clicked."); }, "Save test environment", true));
            s.AddTool(GetToolbarButton("DeveloperToolbox/Load_environment", iconSize, () => { MessageBox.Show("Load test environment clicked."); }, "Load test environment", true));
            s.AddToolSeparator();
            s.AddTool(GetToolbarButton("DeveloperToolbox/Create_test", iconSize, () => { MessageBox.Show("Create test clicked."); }, "Create a automated regression test", true));
            s.AddTool(GetToolbarButton("DeveloperToolbox/Test_manager", iconSize, () => { MessageBox.Show("Test manager clicked."); }, "Open test manager", true));
            s.AddTool(GetToolbarButton("DeveloperToolbox/Report_bug", iconSize, () => { MessageBox.Show("Report bug clicked."); }, "Report a bug/issue", true));
        }

        private void LoadBottomPanel(StockApplicationWindow w)
        {
            var b = w.BottomPanel;
            _textPanel = new MultiColorTextPanel()
            {
                IsSelectable = true,
                HorizontalAlignment = HorizontalAlignment.Left,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                SelectedBackground = AppearanceManager.GetCurrentSkinResource("BackgroundBase_05") as Brush,
            };
            b.Content = _textPanel;
            _textPanel.IsSelectable = true;
            _textPanel.Clicked += (s, e) => { MessageBox.Show("Override action when clicked on text-panel, such as showing session log."); };
            _textPanel.SetText("One can add application status here.", new SolidColorBrush(Colors.YellowGreen));
            _textPanel.AddText(" Click to execute the registered action.", new SolidColorBrush(Colors.Orange));
        }

        private void LoadTopPanel(StockApplicationWindow w)
        {
            var t = w.TopPanel;
            t.Loaded += (s, e) =>
            {
                var iconSize = 35.0;
                w.TopToolContainer.Children.Add(GetToolbarButton("Krypton/ApplicationManager", iconSize, () => { MessageBox.Show("Launch application manager clicked."); }, "Launch Application manager"));
                w.TopToolContainer.Children.Add(GetToolbarButton("Krypton/Komet5", iconSize, () => { MessageBox.Show("Launch Komet 5.0 clicked."); }, "Launch Komet 5.0"));
                w.TopToolContainer.Children.Add(GetToolbarButton("Krypton/StylusManager", iconSize, () => { MessageBox.Show("Launch stylus manager clicked."); }, "Launch Stylus manager"));
                w.TopToolContainer.Children.Add(new VerticalSeparator() { ColorBrush = AppearanceManager.GetCurrentSkinResource("BackgroundBase_12") as Brush });
                w.TopToolContainer.Children.Add(GetToolbarButton("Krypton/MountingDistance", iconSize, () => { MessageBox.Show("Update mounting distance clicked."); }, "Update mounting distance"));
                w.TopToolContainer.Children.Add(new VerticalSeparator() { ColorBrush = AppearanceManager.GetCurrentSkinResource("BackgroundBase_12") as Brush });
                w.TopToolContainer.Children.Add(GetToolbarButton("Krypton/Save", iconSize, () => { MessageBox.Show("Save drawing changes clicked."); }, "Save drawing changes"));
            };
        }

        private void LoadNavigator(StockApplicationWindow w)
        {
            var n = w.Navigator;
            n.Menu.HeaderText = "Main Menu";
            n.Menu.CollapsedWidth = 50.0;
            n.Menu.ExpandedWidth = 200.0;
            n.Menu.MenuSelectionChanged += OnMenuSelectionChanged;
            n.Menu.SelectedIndex = 0;
            n.Menu.Expand();

            LoadMenuItems(n);
        }

        private ToolButton GetToolbarButton(string iconId, double iconSize, Action action, string toolTip, bool showGlow = false)
        {
            var s = "./Images/" + iconId;
            var btn = new ToolButton(s + "_enabled.png", s + "_disabled.png", s + "_mouse_over.png");
            btn.Width = iconSize;
            btn.Height = iconSize;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Action = action;
            btn.TooltipText = toolTip;
            btn.GlowOnMouseOver = showGlow;
            btn.MouseOverGlowColor = Colors.SkyBlue;
            return btn;
        }

        private void LoadMenuItems(HamburgerNavigator n)
        {
            var strs = new Dictionary<string, string>() {
                { "Home", "Klingelnberg_building.jpg" },
                { "Data", "klingelnberg_office.jpg" },
                { "Measurements", "P40.png" },
                { "Reports", "SpeedViper300.png" },
                { "Analysis", "G60.png" },
                { "Settings", "TM_65.png" },
                { "Help", "Klingelnberg_Drive_Technology.png" },
            };

            foreach (var k in strs.Keys)
            {
                var imageName = strs[k];
                var item = new SelectableItem(k, GetControl(imageName), "./Images/Generic/" + k + "_white.png")
                {
                    IconHeight = 35,
                    IconWidth = 35,
                    ItemHeight = 50
                };
                n.Menu.AddMenuItem(item);
            }
        }

        private ContentControl GetControl(string imageName)
        {
            var imageSource = (ImageSource)new ImageSourceConverter().ConvertFromString("./Images/Klingelnberg/" + imageName);
            var image = new Image()
            {
                Source = imageSource,
            };

            var b = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                Child = image,
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
            if (_textPanel == null)
            {
                return;
            }
            var currItem = e.Current;
            var title = currItem.Header;
            _textPanel.SetText("Current Session: ", new SolidColorBrush(Colors.YellowGreen));
            _textPanel.AddText(title, new SolidColorBrush(Colors.Orange));
        }
    }
}
