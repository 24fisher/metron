# Metron is C# crossplatform (WPF and xamarin) metronome app, that implements data binding,
MVVM and Bridge design patterns, uses async/await, XDocument class.
The core of the app is .Net Standard library
Xamarin.Forms and WPF UIs both use application core.
The core uses data binding based on INotifyPropertyChanged event model.
Native android functionality is being accessed from Xamarin cross-platform with dependency service.
App has compatibility to use different Timers via adapters in order to optimize perfomance in various environments.
Supports user beat-patterns.
