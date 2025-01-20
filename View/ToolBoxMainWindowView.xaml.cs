using System.Windows;
using ToolBox.ViewModel;

namespace ToolBox.View
{
    /// <summary>
    /// ToolBoxMainWindowView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolBoxMainWindowView : Window
    {
        public ToolBoxMainWindowView()
        {
            InitializeComponent();
            this.DataContext = new ToolBoxMainWindowViewModel();
        }
    }
}
