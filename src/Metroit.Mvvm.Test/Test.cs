using CommunityToolkit.Mvvm.ComponentModel;
using Metroit.Mvvm.ViewModels;

namespace Metroit.Mvvm.Test
{
    public partial class Test : ChangesObservableObject
    {
        [ObservableProperty]
        private string _fuga;

        public Test()
        {
            var a = Fuga;
        }
    }
}
