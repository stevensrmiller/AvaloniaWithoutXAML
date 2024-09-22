# ImageRotation

Use the Image class's RenderTransform member to rotate an image. By multiplying
the transform's matrix by another matrix that encodes a small rotation, over and
over, the image is rotated while on the screen. Click the window to start a full
rotation.

Note that this example also uses an animation timer running on a worker thread.
Posting work to the UI thread returns immediately, with the UI thread running
what you posted at some future time. This means the queue of pending work could
conceivably fill up. A situation like that calls for synchronizing the worker
thread with the UI thread. There are a number of ways to do that, but none is
used here to keep this example simple.

![A rotating image.](ScreenCap.gif "A rotating image.")