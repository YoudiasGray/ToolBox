using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ToolBox.Model;

namespace ToolBox.ViewModel
{
    // 按钮模型，主要提供显示内容和绑定功能
    public class ButtonModel
    {
        public string Content { get; set; } = "新按钮";
        public string TipMessage { get; set; } = "未绑定";
        public ICommand Command { get; set; } = new RelayCommand(new Action(() => { MessageBox.Show("未绑定"); }));
    }

    public partial class RunModeViewModel : ObservableObject
    {
        public RunModeViewModel()
        {
            InitializeButtons();
        }
        private void InitializeButtons()
        {
            ButtonItems.Clear();
            foreach (var item in ButtonsInfo.Instance.ButtonInfoList)
            {

                ButtonItems.Add(new ButtonModel()
                {
                    Content = item.ButtonName,
                    TipMessage = item.TipDescription,
                    Command = new RelayCommand(new Action(() =>
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(item.ExecutorCommandOrPath))
                            {
                                MessageBox.Show("未绑定执行器");
                                return;
                            }
                            // 将配置转换成一个命令
                            if (item.IsBatCommand)
                            {
                                // 是一个bat命令，直接执行·
                                // 创建一个新的进程
                                Process process = new Process();

                                // 设置进程启动信息
                                process.StartInfo.FileName = "cmd.exe";
                                process.StartInfo.Arguments = "/c " + item.ExecutorCommandOrPath;
                                if (!string.IsNullOrEmpty(item.ExecutroArgs))
                                {
                                    process.StartInfo.Arguments += " " + item.ExecutroArgs;
                                }

                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.RedirectStandardOutput = true;
                                process.StartInfo.RedirectStandardError = true;

                                // 启动进程
                                process.Start();

                                //// 读取标准输出和错误输出
                                //string output = process.StandardOutput.ReadToEnd();
                                //string error = process.StandardError.ReadToEnd();

                                //// 等待进程退出
                                //process.WaitForExit();

                                //// 输出结果
                                //Debug.WriteLine("Output: " + output);
                                //Debug.WriteLine("Error: " + error);
                            }
                            else
                            {
                                // 执行外部一个exe文件
                                // 创建一个新的进程
                                Process process = new Process();

                                // 设置进程启动信息
                                process.StartInfo.FileName = item.ExecutorCommandOrPath;
                                if (string.IsNullOrEmpty(item.ExecutroArgs))
                                {
                                    process.StartInfo.Arguments = item.ExecutroArgs;  // 如果需要传递参数，可以在这里设置
                                }

                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.RedirectStandardOutput = true;
                                process.StartInfo.RedirectStandardError = true;

                                // 启动进程
                                process.Start();

                                //// 读取标准输出和错误输出
                                //string output = process.StandardOutput.ReadToEnd();
                                //string error = process.StandardError.ReadToEnd();

                                //// 等待进程退出
                                //process.WaitForExit();

                                //// 输出结果
                                //Console.WriteLine("Output: " + output);
                                //Console.WriteLine("Error: " + error);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }))
                });
            }
        }

        [ObservableProperty]
        private ObservableCollection<ButtonModel> buttonItems = new ObservableCollection<ButtonModel>();
    }
}
