namespace ToolBox.Model
{
    /*
     * update 2024年12月19日09:20:17
     * 重构，区分编辑模式运行模式
     * 进入编辑模式后， 操作只是针对表格，实现增删改查，导入导出保存
     * 进入运行模式后，自动将配置文件生成按钮列表，实现点击触发
     * 
     * 对应的数据模型，需要：
     * 1 是否是bat，区分执行方式，如果是bat，直接运行，如果外部程序，需要调用，其中bat也可以通过编写外部bat脚本实现
     * 
     * 2 执行器
     * 3 参数
     * 4 提示信息
     */

    public class ToolBoxModel
    {
        public string ButtonName { get; set; } = "";
        public bool IsBatCommand { get; set; } = false;
        public string ExecutorCommandOrPath { get; set; } = "";
        public string ExecutroArgs { get; set; } = "";
        public string TipDescription { get; set; } = "";

    }
}
