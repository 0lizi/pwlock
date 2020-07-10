using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace pwlock
{
    public partial class LockImage : Form
    {
        public LockImage()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            int lab = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 57;
            timelab.Top = lab;
        }

        string weekstr = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            timelab2.Text = DateTime.Now.ToShortTimeString().ToString();
            timelab.Text = DateTime.Now.Month.ToString() +"月" + DateTime.Now.Day.ToString() +"日," + weekstr;
        }


        private void LockImage_Load(object sender, EventArgs e)
        {
            System.Security.Principal.WindowsIdentity currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            string usersid = currentUser.User.ToString();//获取当前用户sid
            Image myimage;
            try
            {
                DirectoryInfo LockPath1 = new DirectoryInfo(@"C:\ProgramData\Microsoft\Windows\SystemData\" + usersid + @"\ReadOnly");
                DirectoryInfo[] LockPath1_1 = LockPath1.GetDirectories();
                if (LockPath1_1.Length > 1)
                {
                    myimage = new Bitmap(LockPath1_1[LockPath1_1.Length - 1].FullName + @"\LockScreen.jpg");
                    BackgroundImage = myimage;
                }
                else
                {
                    DirectoryInfo LockPath2 = new DirectoryInfo(@"C:\Windows\Web\Screen");
                    FileInfo[] lockimage2 = LockPath2.GetFiles();
                    DirectoryInfo LockPath3 = new DirectoryInfo(@"C:\Windows\Web\Wallpaper\Windows");
                    FileInfo[] lockimage3 = LockPath3.GetFiles();
                    if (File.Exists(lockimage2[0].FullName))
                    {
                        myimage = new Bitmap(lockimage2[0].FullName);
                    }
                    else
                    {
                        myimage = new Bitmap(lockimage3[0].FullName);
                    }
                }
            }
            catch
            {
                DirectoryInfo LockPath2 = new DirectoryInfo(@"C:\Windows\Web\Screen");
                FileInfo[] lockimage2 = LockPath2.GetFiles();
                DirectoryInfo LockPath3 = new DirectoryInfo(@"C:\Windows\Web\Wallpaper\Windows");
                FileInfo[] lockimage3 = LockPath3.GetFiles();
                if (File.Exists(lockimage2[0].FullName))
                {
                    myimage = new Bitmap(lockimage2[0].FullName);
                }
                else
                {
                    myimage = new Bitmap(lockimage3[0].FullName);
                }
            }
            
            
            //if (File.Exists(@"C:\ProgramData\Microsoft\Windows\SystemData\" + usersid + @"\ReadOnly\LockScreen_A\LockScreen.jpg"))
            //{
                //myimage = new Bitmap(@"C:\ProgramData\Microsoft\Windows\SystemData\" + usersid + @"\ReadOnly\LockScreen_A\LockScreen.jpg");
            //}
           
            BackgroundImage = myimage;
            timer1.Start();
            //把得到的星期转换成中文
            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday": weekstr = "星期一"; break;
                case "Tuesday": weekstr = "星期二"; break;
                case "Wednesday": weekstr = "星期三"; break;
                case "Thursday": weekstr = "星期四"; break;
                case "Friday": weekstr = "星期五"; break;
                case "Saturday": weekstr = "星期六"; break;
                case "Sunday": weekstr = "星期日"; break;
            }
            timelab2.Top = 805;
            timelab2.Left = 64;
            timelab.Top = 900;
            timelab.Left = 80;
            
            
        }

        private void LockImage_Click(object sender, EventArgs e)
        {
            timelab.Visible = false;
            timelab2.Visible = false;
            LockScreenForm f2 = new LockScreenForm();
            f2.ShowDialog();
        }

        private void LockImage_KeyDown(object sender, KeyEventArgs e)
        {
            timelab.Visible = false;
            timelab2.Visible = false;
            LockScreenForm f2 = new LockScreenForm();
            f2.ShowDialog();
            
        }
    }
}
