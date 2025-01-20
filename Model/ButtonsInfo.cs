namespace ToolBox.Model
{
    public class ButtonsInfo
    {
        // 静态内部类方式提供统一的数据访问
        // TODO# 后面考虑使用IOC
        private ButtonsInfo() { }
        internal static class InnerClass
        {
            internal static ButtonsInfo instance = new ButtonsInfo();
        }
        public static ButtonsInfo Instance => InnerClass.instance;

        // 共享的数据是一个清单列表
        public List<ToolBoxModel> ButtonInfoList { get; set; } = new List<ToolBoxModel>() { };
    }
}
