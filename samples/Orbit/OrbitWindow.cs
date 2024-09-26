using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

internal class OrbitWindow
{
    public Window win;

    private Canvas canvas;
    private BlobModel blobModel;
    private BlobView blobView;
    private Animator animator;
    public OrbitWindow()
    {
        win = new Window
        {
            Title = "OrbitWindow v0.1",
            Height = 720,
            Width = 1280,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Height = win.Height,
            Width = win.Width,
            Background = Brushes.Black,
        };

        win.Content = canvas;

        blobModel = new BlobModel
        {
            brush = Brushes.Aqua,
            size = 30,
            deltaTheta = .025f,
        };

        blobView = new BlobView(canvas, blobModel);

        win.Resized += (s, a) => OnResized();
        win.KeyDown += (s, a) => OnKeyDown(a.Key);
        win.PointerPressed += (s, a) => OnPointerPressed();
        win.Show();

        animator = new Animator(OnAnimationFrame, 33);
    }

    void OnKeyDown(Key key)
    {
        if (key == Key.F)
        {
            if (win.WindowState == WindowState.FullScreen)
            {
                win.WindowState = WindowState.Normal;
            }
            else
            {
                win.WindowState = WindowState.FullScreen;
            }
        }

        if (key == Key.R)
        {
            animator.Run();
        }

        if (key == Key.S)
        {
            animator.Stop();
        }
    }

    void OnPointerPressed()
    {
        blobModel.IncrementPosition();
    }

    void OnAnimationFrame()
    {
        OnPointerPressed();
    }
    void OnResized()
    {
        canvas.Height = win.Height;
        canvas.Width = win.Width;

        blobView.SetBlobPosition();
    }
}