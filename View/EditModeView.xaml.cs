using System.Windows.Controls;
using ToolBox.ViewModel;

namespace ToolBox.View
{
    /// <summary>
    /// EditModeView.xaml 的交互逻辑
    /// </summary>
    public partial class EditModeView : Page
    {
        public EditModeView()
        {
            InitializeComponent();
            this.DataContext = new EditModeViewModel();
        }
    }
}
