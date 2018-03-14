using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelegateDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void DoSomething(string s);

        private Action<string> action;

        private event DoSomething ChangeNumEvent;

        private int count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Work(string s)
        {
            Console.WriteLine("Work: {0}", s);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //传统写法
            DoSomething doWorkHandler = new DoSomething(Work);
            doWorkHandler("Hello");

            //匿名函数写法，与下面Lambda表达式写法等效
            doWorkHandler = delegate (string s)
            {
                Console.WriteLine("匿名方法: {0}", s);
            };
            doWorkHandler("Hello");

            //Lambda表达式写法，与上面匿名函数写法等效
            doWorkHandler = (string s) =>
            {
                Console.WriteLine("匿名方法: {0}", s);
            };

            doWorkHandler("Hello");

            //Action
            action = new Action<string>(Work);
            action("From Action");

            //Event
            ChangeNumEvent += new DoSomething(Work);
        }

        private void ChangeNum()
        {
            count++;
            OnNumChanged(count);
        }

        protected virtual void OnNumChanged(int count)
        {
            if (ChangeNumEvent != null)
            {
                ChangeNumEvent("事件触发, " + count); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("事件未触发");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeNum();
        }
    }
}
