# AvaloniaWithoutXAML
 
Sample Avalonia C# programs that do not use any XAML.

## Contents

Here you will find small programs written entirely in C# that demonstrate various Avalonia controls. In the style of 20th century educational code, the programs are commented with the intent that, by reading the code, you will be able to learn what you need to use the same techniques in your own code. The directory names will typically tell you what each sample demonstrates.

## Why no XAML?

I teach programming to people with little or no prior experience writing code. Most of them are artists, designers, or others driven by a desire to create something interesting. C# and Avalonia make that possible, but both C# and Avalonia are a challenge to learn (and to teach). A designer interested in creating a visual display will need to know how to manipulate shapes, lines, text, and so forth in code. This means they will need to learn most of what is necessary to creating an Avalonia user interface in code. Asking them to learn XAML so they can do something they already know (or mostly know) how to do in C# adds needless complexity. Even if they do learn it, the dynamic nature of the creations they want to make will tend to call for a lot of C# driving the display at run time. Again, asking them to learn how to do the same thing in XAML would be hard to justify.

Also, while the basic concepts underlying the Avalonia library's design are common to most graphic toolkits, not all of those toolkits use any kind of markup language (and not all of those that do use XAML). Because my goal is to teach concepts, not just tools, I try to make my examples as nearly generic as possible. Keeping it all in C# avoids teaching anything unnecessarily specific to one implementation.

None of this is meant to say that XAML is a bad thing to use, or that working without it is preferable to using it. But there is definitely a desire on the part of some programmers to use Avalonia without it. This collection of samples is for them.
