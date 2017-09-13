using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {const byte CtrlMask = 8;
        public MainForm()
        {
            InitializeComponent();
          textBox1 .MouseDown +=textBox1_MouseDown;
          textBox2.DragEnter += textBox2_DragEnter;
          textBox2.DragDrop += textBox2_DragDrop;

        }

        void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            textBox2.Text = e.Data.GetData(DataFormats.Text).ToString();
            // 如果 Ctrl 键没有被按下，移除源文字以便营造出移动文字的效果。
            if ((e.KeyState & CtrlMask) != CtrlMask)
            { textBox1.Text = ""; }
        }

        void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                // 如果在拖曳期间按着 Ctrl 键，则执行复制操作；反之，则执行移动操作。
                if ((e.KeyState & CtrlMask) == CtrlMask)
                { e.Effect = DragDropEffects.Copy; }
                else
                { e.Effect = DragDropEffects.Move; }
            }
            else
            { e.Effect = DragDropEffects.None; }
        }
        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //全选
                textBox1.SelectAll();
                //拖放
                textBox1.DoDragDrop(textBox1.SelectedText, DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        private void Closethis(object sender, FormClosedEventArgs e)
        {
            //Environment.Exit(0);
            Application.Exit();
        }
    }
}
