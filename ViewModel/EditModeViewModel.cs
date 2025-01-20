using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using ToolBox.Model;

namespace ToolBox.ViewModel
{
    public partial class EditModeViewModel : ObservableObject
    {
        public EditModeViewModel()
        {
            CommandsList = new ObservableCollection<ToolBoxModel>(ButtonsInfo.Instance.ButtonInfoList);
        }

        #region 属性
        [ObservableProperty]
        private ObservableCollection<ToolBoxModel> commandsList = new ObservableCollection<ToolBoxModel>();

        partial void OnCommandsListChanged(ObservableCollection<ToolBoxModel> value)
        {
            ButtonsInfo.Instance.ButtonInfoList = CommandsList.ToList();
        }

        #endregion

        #region 命令
        [RelayCommand]
        public void MoveUp(object? obj)
        {
            if (obj is not ToolBoxModel parameter)
            {
                return;
            }

            int index = CommandsList.IndexOf(parameter);
            if (index > 0)
            {
                CommandsList[index] = CommandsList[index - 1];
                CommandsList[index - 1] = parameter;
            }
        }

        [RelayCommand]
        public void MoveDown(object? obj)
        {
            if (obj is not ToolBoxModel parameter)
            {
                return;
            }

            int index = CommandsList.IndexOf(parameter);
            if (index < CommandsList.Count - 1)
            {
                CommandsList[index] = CommandsList[index + 1];
                CommandsList[index + 1] = parameter;
            }
        }

        [RelayCommand]
        public void SelectFile(object? obj)
        {
            if (obj is not ToolBoxModel parameter)
            {
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() is true)
            {
                int index = CommandsList.IndexOf(parameter);
                parameter.ExecutorCommandOrPath = openFileDialog.FileName;
                CommandsList[index] = parameter;

                // 数据更新，但是UI未更新
                CommandsList.RemoveAt(index);
                CommandsList.Insert(index, parameter);
            }
        }

        #endregion
    }
}
