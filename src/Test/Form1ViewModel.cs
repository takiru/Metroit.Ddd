using Metroit.Mvvm.WinForms.ReactiveProperty.ViewModels;
using Reactive.Bindings;
using System.Reactive.Linq;

namespace Test
{
    public class Form1ViewModel : ViewModelBase
    {
        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>(string.Empty);

        public ReactiveCommand TestCommand { get; }

        public Form1ViewModel()
        {
            TestCommand = Text.Select(x => x == "10").ToReactiveCommand();
        }
    }
}
