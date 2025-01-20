using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using ToolBox.Model;
using ToolBox.View;

namespace ToolBox.ViewModel
{
    public partial class ToolBoxMainWindowViewModel : ObservableObject
    {
        private static RunModeView _runModeView = new RunModeView();

        private static EditModeView _editModeView = new EditModeView();

        public ToolBoxMainWindowViewModel()
        {
            // 首次启动尝试读取配置文件
            if (File.Exists("config.json"))
            {
                string? config = File.ReadAllText("config.json", Encoding.UTF8);
                if (config != null)
                {
                    var buttonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ToolBoxModel>>(config);
                    ButtonsInfo.Instance.ButtonInfoList = buttonList ?? new List<ToolBoxModel>();
                }
                else
                {
                    ButtonsInfo.Instance.ButtonInfoList = new List<ToolBoxModel>();
                }
            }
            else
            {
                ButtonsInfo.Instance.ButtonInfoList = new List<ToolBoxModel>();
            }

            PageContent = _runModeView;

            BuildVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";
            VersionInfo = $"V{MajorVersion}.{MinorVersion}.{PathcVersion}.{BuildVersion}";
        }

        #region 属性
        private string MajorVersion = "0";
        private string MinorVersion = "0";
        private string PathcVersion = "0";
        private string BuildVersion = "";

        [ObservableProperty]
        // 版本号：1 接口版本 2 功能版本 3 修复版本 4 编译版本
        private string versionInfo = "";

        [ObservableProperty]
        private Visibility editModeVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private string workMode = "切换到设计模式";
        private static readonly string workModeButtonContent_Run = "切换到设计模式";
        private static readonly string workModeButtonContent_Edit = "切换到运行模式";

        [ObservableProperty]
        private bool isRunMode = true;

        partial void OnIsRunModeChanged(bool oldValue, bool newValue)
        {
            if (oldValue == newValue)
            {
                return;
            }

            if (newValue)
            {
                ShowPage = "RunModeView.xaml";
                //pageContent = new RunModeViewModel();
                PageContent = new RunModeView();
            }
            else
            {
                ShowPage = "EditModeView.xaml";
                //pageContent = new EditModeViewModel();
                PageContent = new EditModeView();
            }
        }

        [ObservableProperty]
        //private string showPage = "EditModeView.xaml";
        private string showPage = "RunModeView.xaml";

        [ObservableProperty]
        private object pageContent;

        #endregion

        #region 数据

        #endregion
        #region 命令
        [RelayCommand]
        public void SwitchWorkMode()
        {
            // 切换运行模式
            IsRunMode = !IsRunMode;

            if (IsRunMode)
            {
                WorkMode = workModeButtonContent_Run;
                // 运行模式
                // 1 窗口显示编辑窗口
                // 2 显示相关控制按钮
                EditModeVisibility = Visibility.Collapsed;
            }
            else
            {
                WorkMode = workModeButtonContent_Edit;
                EditModeVisibility = Visibility.Visible;
            }
        }

        [RelayCommand]
        public void Save()
        {
            if (PageContent is EditModeView editModeView)
            {
                if (editModeView.DataContext is EditModeViewModel viewModel)
                {
                    var commands = viewModel.CommandsList ?? new System.Collections.ObjectModel.ObservableCollection<ToolBoxModel>();
                    ButtonsInfo.Instance.ButtonInfoList = commands.ToList();
                    string configStr = Newtonsoft.Json.JsonConvert.SerializeObject(ButtonsInfo.Instance.ButtonInfoList, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText("config.json", configStr, Encoding.UTF8);
                }
            }
        }

        [RelayCommand]
        public void Import()
        {
            if (PageContent is EditModeView editModeView)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "配置文件|*.json",
                    Title = "导入配置文件",
                    DefaultExt = ".json"
                };
                if (openFileDialog.ShowDialog() is true)
                {
                    string fileName = openFileDialog.FileName;
                    string? config = File.ReadAllText(fileName, Encoding.UTF8);
                    if (config != null)
                    {
                        var buttonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ToolBoxModel>>(config);
                        ButtonsInfo.Instance.ButtonInfoList = buttonList ?? new List<ToolBoxModel>();

                        if (editModeView.DataContext is EditModeViewModel viewModel)
                        {
                            viewModel.CommandsList = new System.Collections.ObjectModel.ObservableCollection<ToolBoxModel>(
                                ButtonsInfo.Instance.ButtonInfoList ?? new List<ToolBoxModel>());
                        }
                    }
                }
            }
        }

        [RelayCommand]
        public void Export()
        {
            if (PageContent is EditModeView editModeView)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "配置文件|*.json",
                    Title = "导出配置文件",
                    DefaultExt = ".json"
                };
                if (saveFileDialog.ShowDialog() is true)
                {
                    if (editModeView.DataContext is EditModeViewModel viewModel)
                    {
                        ButtonsInfo.Instance.ButtonInfoList = viewModel.CommandsList?.ToList() ?? new List<ToolBoxModel>();
                        string configStr = Newtonsoft.Json.JsonConvert.SerializeObject(ButtonsInfo.Instance.ButtonInfoList, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(saveFileDialog.FileName, configStr, Encoding.UTF8);
                    }
                }
            }
        }
        #endregion
    }
}
