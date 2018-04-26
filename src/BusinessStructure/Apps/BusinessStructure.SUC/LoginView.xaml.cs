#if NETFX_CORE
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#else
using System.Windows.Controls;

#endif
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BusinessStructure.SUC
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }
    }
}