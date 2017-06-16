using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFWindowsForms
{
    public partial class MainFrom : Form
    {
        private WorkflowApplication wfApplication;
        public MainFrom()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore("Server=127.0.0.1;Initial Catalog=Heima8OADb;uid=sa;pwd=scu510scu510");
            
            wfApplication = new WorkflowApplication(new StateMoneyActivity(), new Dictionary<string, object>()
            {
                {"InputBookMarkName",this.tb_bookMark.Text}   
            });
            wfApplication.InstanceStore = store;
            wfApplication.Idle += onWfAppIdle;
            wfApplication.OnUnhandledException += onWfAppException;
            wfApplication.Unloaded += a => { Console.WriteLine("工作流被卸载了"); };
            wfApplication.Aborted += a => { Console.WriteLine("aborted!"); };
            

            //启用序列化必须使用此方法。
            wfApplication.PersistableIdle = a => { return PersistableIdleAction.Unload; };

            tb_persistence_guid.Text = wfApplication.Id.ToString();

            wfApplication.Run();



        }


        private UnhandledExceptionAction onWfAppException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("出现未处理的异常"+arg.UnhandledException.ToString());
            throw arg.UnhandledException;
        }

        private void onWfAppIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("工作流停下来了");
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {

            //简单的恢复书签
            //wfApplication.ResumeBookmark(tb_bookMark.Text, int.Parse(this.tb_money.Text));


            //从数据库中加载工作流实例的状态
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore("Server=127.0.0.1;Initial Catalog=Heima8OADb;uid=sa;pwd=scu510scu510");

            WorkflowApplication wfApp = new WorkflowApplication(new StateMoneyActivity());

            wfApp.InstanceStore = store;

            wfApp.Idle += onWfAppIdle;
            wfApp.OnUnhandledException += onWfAppException;
            wfApp.Unloaded += a => { Console.WriteLine("工作流被卸载了"); };
            wfApp.Aborted += a => { Console.WriteLine("aborted!"); };

            //启用序列化必须使用此方法。
            wfApp.PersistableIdle = a => { return PersistableIdleAction.Unload; };

            //加载数据库中的实例的状态。（其实就是重新加载wfApplication）
            wfApp.Load(Guid.Parse(tb_persistence_guid.Text));
            wfApp.ResumeBookmark(tb_bookMark.Text, int.Parse(tb_money.Text));

        }

        private void btn_startStateMachine_Click(object sender, EventArgs e)
        {
            WorkflowApplication wfApp = new WorkflowApplication(new StateActivity());
            wfApp.Run();
        }
    }
}
