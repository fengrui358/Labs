using System.Windows;
using StrongNameLibrary;

namespace StrongNameLab
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// 禁用强名称跳过：https://docs.microsoft.com/zh-cn/dotnet/standard/assembly/disable-strong-name-bypass-feature
    /// 强名称程序集引用非强名称程序集：https://www.cnblogs.com/dotnet261010/p/12401843.html
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Key { get; set; }

        public MainWindow()
        {
            var c = new Class1();
            if (!c.IsValid())
            {
                Key = "无效";
            }
            else
            {
                Key = "有效";
            }

            InitializeComponent();

            DataContext = this;
        }
    }
}
