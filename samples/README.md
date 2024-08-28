# Samples

These directories each contain a self-contained sample. That is, you can download
them individually to build and run their samples. None of them depends on any of
the others.

Even if you don't actually download all of them, you can learn a lot about how to
use Avalonia without XAML by reading the comments in the source code.

A good learning path would be this order.

| Name                          | Description                                                              |
|-------------------------------|--------------------------------------------------------------------------|
| HelloWorldWindow<br>&nbsp;    | The classic starter.<br>Just a window with the title, "Hello World!"     |
| HelloWorldLabel               | Just a window with a text label in it, saying "Hello World!"             |
| StackOfLabels                 | Three labels in various colors, arranged vertically in a layout manager. |
| ButtonHandler                 | A single button that will call your code when pressed.                   |
| ButtonColors                  | Replace the transparent button background with your own choices.         |
| DrawSquareLines               | Use four lines to draw a square.                                         |
| DrawSquareBetter              | Use four lines to draw a square. Centralizes the line-drawing code.      |
| DrawCircle                    | Use 100 lines to draw a circle.                                          |
| MultiWindowGraphics           | Multiple independent windows, each filled with graphic objects.          |
| MultiWindowNoMain             | Multiple independent windows, none of which is the "main" app window.    |
| MultiWindowCancelToken        | Multiple independent windows, with no "main" window, closing on a signal.|
| ImageProcessingEmbed<br>&nbsp;| Display a bitmap image, modify its data, and display it again.<br>The image is included in the runtime assembly as an embedded resource.   |
| DockMenu                      | A window with a menubar using the "dock" layout.                         |
| TextBoxWindow                 | A text box copied to a label as each character is entered.               |
| GridWindow                    | Some controls laid out in a grid.                                        |
| SlidersProgress               | Sliders and progress bars.                                               |
| Harmonograph                  | Draw [harmonograph](https://en.wikipedia.org/wiki/Harmonograph) pictures.|
| MultiThreading                | Make changes to your UI from a worker thread.                            |

In addition to the above, some samples that might interest you are these. They are listed apart from
the ones in the first table because they don't really have any natural place in the learning order, and
they also aren't as well written. Rather than hide them, they're offered to you in their own list, in case
you might find them useful to something specific you are doing.

| Name                       | Description                                                              |
|----------------------------|--------------------------------------------------------------------------|
| OpenFile                   | Use a filepicker dialog box to get a filename.                           |
| ImageProcessing<br>&nbsp;  | Display a bitmap image, modify its data, and display it again.<br>The image is in an external file, which leads to some messy code.|
| ShowIcon                   | Get a Windows icon and display it as an Avalonia image. Windows only!    |
| PlaySound                  | Load a .wav file and play it when a button is pressed. Windows only!     |
| Starrs                     | Somewhat similar to the Harmonograph. This one draws "Starr" roses.      |