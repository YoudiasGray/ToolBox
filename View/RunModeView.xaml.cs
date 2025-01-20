using System.Windows.Controls;
using ToolBox.ViewModel;

namespace ToolBox.View
{
    /// <summary>
    /// RunModeView.xaml 的交互逻辑
    /// </summary>
    public partial class RunModeView : Page
    {
        public RunModeView()
        {
            InitializeComponent();
            this.DataContext = new RunModeViewModel();
        }
    }
}
