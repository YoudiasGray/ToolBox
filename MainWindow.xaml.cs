using System.Windows;
using ToolBox.View;

namespace ToolBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            ToolBoxMainWindowView toolBoxView = new ToolBoxMainWindowView();
            toolBoxView.Show();
            this.Close();
        }
    }
}
