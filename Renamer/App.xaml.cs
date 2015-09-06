using GalaSoft.MvvmLight.Threading;

namespace Renamer
{
    /// <summary>Interaction logic for App.xaml</summary>
    public partial class App
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
