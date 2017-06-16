using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDemo1;
using WWFWinFormDemo.Properties;

namespace WWFWinFormDemo
{
    public partial class Form1 : Form
    {
        private WorkflowApplication workflowApplication;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //            WorkflowInvoker.Invoke(new LeaveActivity(),new Dictionary<string, object>()
            //            {
            //                {"BookMarkName",this.tb_bookMarkName.Text}
            //            });

            workflowApplication = new WorkflowApplication(new LeaveActivity(), new Dictionary<string, object>()
            {
                {"BookMarkName",this.tb_bookMarkName.Text}
            });

            workflowApplication.Idle += AfterWorkFlowIdle;
            workflowApplication.Run();
            //wfi.Invoke(,)

        }

        private void AfterWorkFlowIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine(Resources.Form1_AfterWorkFlowIdle_工作流停下来了_);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            workflowApplication.ResumeBookmark(tb_bookMarkName.Text, int.Parse(tb_money.Text));
        }
    }
}
