# ImageProcessingEmbed

This one loads a bitmap and displays it as an image. When you hold down the mouse button over the image, the colors
are inverted.

When you run a DotNet program, it might run from a directory where you've installed it for regular use, or it might
run from a directory in a subtree of where your source files are. This means that, if your image file is in the source
directory, your code might have to play some messy tricks to try to find the image file. The image might be in the
same directory your program ran from, or it might be above it in the subtree. In a working context, you'd probably
have the user select the file from a filepicker (we have a sample for those), or else keep it in a well-defined place.

Here, we're using the option to embed resources right into the assembly file that includes our executable. When it
runs, our program will read the image data out of itself. That's right! You can actually embed whole files into your
executable and open them at run time without having to keep external copies anywhere. This is a great way to make
sample programs that will process data without having to figure out where the data is in a context that might make
that change. Note that embedding data this way is part of how DotNet works. You don't need Avalonia for it.

I've been doing graphics programming for a long time. If you have too, you probably recognize the gentleman in the picture.

![A mandrill and his negative image.](ScreenCap.png "A mandrill and his negative image.")