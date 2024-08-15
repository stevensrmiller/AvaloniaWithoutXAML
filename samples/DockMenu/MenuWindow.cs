using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class MenuWindow
{
    public Window win;
    Rectangle rCenter;

    Commander c = new Commander();
    public MenuWindow()
    {
        win = new Window
        {
            Title = "MenuWindow v2.0",
            Height = 360,
            Width = 640,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        // A DockPanel is container with five sections, a top, left, right,
        // bottom, and center. The order in which you supply its children
        // governs how much of that edge the child uses. When a child is
        // added to a section, it uses the full width of that section, minus
        // any width used up by earlier children. For example, if you add
        // a child to the top when there are no other children, that child
        // uses the full width of the top edge. If that child is, like the
        // menu used here, 24 pixels high, children added to the left or
        // right do not use the part of their edges that are 24 pixels from
        // They use whatever space is left from the earlier child (or the
        // earlier children). Note that the last child added always goes
        // in the center section.
        //
        // Note that we MUST add at least two children to the DockPanel.
        // That's because the last child is always put in the center
        // unless DockPanel.LastChildFill is set to false.

        var dp = new DockPanel();

        // Avalonia menus are nested arrays of menu items. The depth into
        // the structure automatically controls whether a given item is
        // the top bar, a pull-down, or a pull-side menu. Note that actual
        // commands are implemented with fully defined objects that
        // implement the System.Windows.Input.ICommand interface. It's not
        // a good idea to create those objects in the menu definition below.
        // Create them ahead of time and pass the objects to the "Command"
        // members in the definition. Pay attention to the fact that are
        // passing objects, not delegates. This is different from the event
        // handlers Avalonia typically uses for clicks and presses.

        var menu = new Menu
        {
            Background = Brushes.LightGray,
            Foreground = Brushes.Black,
            FontSize = 16,
            Height = 24,

            ItemsSource = new[]
            {
                new MenuItem
                {
                    Header = "File",

                    ItemsSource = new[]
                    {
                        new MenuItem
                        {
                            Header = "Open",

                            ItemsSource = new[]
                            {
                                new MenuItem
                                {
                                    Header = "Dialog",
                                    Command = c,
                                    CommandParameter = win,
                                },

                                new MenuItem
                                {
                                    Header = "Nope",
                                    IsEnabled = false,
                                },

                                new MenuItem
                                {
                                    Header = "Me either",
                                    IsEnabled = false,
                                },
                            },
                        },

                        new MenuItem
                        {
                            Header = "Save",
                            IsEnabled = false,
                        },

                        new MenuItem
                        {
                            Header = "Close",
                            IsEnabled = false,
                        },
                    },
                },

                new MenuItem
                {
                    Header = "_Edit",
                    IsEnabled = false,

                },

                new MenuItem
                {
                    Header = "_Help",
                    IsEnabled = false,
                },
            },
        };

        // Set the menu to be at the top and add it first,
        // so it uses the whole width of the window.

        menu.SetValue(DockPanel.DockProperty, Dock.Top);

        dp.Children.Add(menu);

        // Now add three more children around the sides, in
        // contrasting colors, so we can clearly see the effect
        // of the order in which they are added on how much of
        // each edge of the window each child gets to use.

        var rLeft = new Rectangle
        {
            Fill = Brushes.Blue,
            Width = 100,
        };

        rLeft.SetValue(DockPanel.DockProperty, Dock.Left);

        dp.Children.Add(rLeft);

        var rBottom = new Rectangle
        {
            Fill = Brushes.Green,
            Height = 100,
        };

        rBottom.SetValue(DockPanel.DockProperty, Dock.Bottom);

        dp.Children.Add(rBottom);

        var rRight = new Rectangle
        {
            Fill = Brushes.Orange,
            Width = 100,
        };

        rRight.SetValue(DockPanel.DockProperty, Dock.Right);

        dp.Children.Add(rRight);

        // The last child will fill the remaining space in the center
        // of the dock panel.

        rCenter = new Rectangle
        {
            Fill = Brushes.Gray,
        };

        dp.Children.Add(rCenter);

        win.Content = dp;
        win.Show();
    }
}

internal class Commander : ICommand
{
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true; // false here to disable this menu item
    }

    public void Execute(object parameter)
    {
        Window win = parameter as Window;

        Window dialog = new Window
        {
            Width = 640,
            Height = 360,
            Background = Brushes.Black,
            Content = new Label { Content = $"Close me to continue...", Foreground = Brushes.Red, FontSize = 24,}
        };

        // Showing a window as a dialog with a parent makes it modal.
        // The user has to close this window to return to the parent.
        
        dialog.ShowDialog(win);
    }
}