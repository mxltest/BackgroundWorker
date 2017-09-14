using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;//报告完成进度
            backgroundWorker1.WorkerSupportsCancellation = true;//允许用户终止后台线程
        }

        private void Rundowork(object sender, DoWorkEventArgs e)
        {
          
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(100);
                int j =(int)e.Argument;
               // int k = (int)e.Result;
            }
            e.Result = ListNumber(backgroundWorker1, e);//运算结果保存在e.Result中
        }

        private void RunProgress(object sender, ProgressChangedEventArgs e)
        {
            int i = (int)e.ProgressPercentage;
            progressBar1.Value = i;
        }

        private void Runwork(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "finish";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "start";
            backgroundWorker1.RunWorkerAsync(100);//触发DoWorker事件
           // backgroundWorker1.ReportProgress(100); // 触发bgw.ProgressChanged事件  e.ProgressPercentage和e.UserState注意本方法使用前,需要将bgw的WorkerReportsProgress值设为true,否则将不会触发事件.
            //backgroundWorker1.CancelAsync();//将CancellationPending 值设为true 注意本方法使用前,需要将bgw的WorkerSupportsCancellation 值设为true,否则将不起作用.
        }
        bool ListNumber(object sender, DoWorkEventArgs e)
        {
            int num = (int)e.Argument;//接收传入的参数
            for (int i = 1; i <= num; i++)
            {
                if (backgroundWorker1.CancellationPending)//判断是否请求了取消后台操作
                {
                    e.Cancel = true;
                    return false;
                }
                //backgroundWorker1.ReportProgress(i * 100 / num);
                backgroundWorker1.ReportProgress(i * 100 / num, i);//报告完成进度
             
                Thread.Sleep(0);//后台线程交出时间片
            }
            
            return true;
        }
    }
}
