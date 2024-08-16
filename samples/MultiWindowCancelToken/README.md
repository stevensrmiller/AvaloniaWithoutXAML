# MultiWindowCancelToken

Multiple windows with no single "main" window. Close them in any order. The application runs until you close them all.

The difference between this and the MultiWindowNoMain sample is that this one uses a shared common object
that tracks how many window are open, and signals a cancelation token when the last one closes. In fact, you can signal
the token on any condition that you would like to use for terminating your application, even with several windows
still open.

![Multiple windows](ScreenCap.png "Multiple windows.")