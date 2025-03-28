
using OWB.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDK;






namespace TEST
{
    public partial class MainForm : Form
    {
        private object TimerLock = new object();
        private object CSVLock = new object();
        private List<string> csvBuffer = new List<string>();
        private const int bufferSize = 10;                  // 批次緩衝大小

        public static Point _MouseHoverPoint;
        private float WidthRatio;
        private float HeightRatio;
        private bool _RecordDone = true;
        private string selectedPath;

            // 背景執行緒
        public static bool _MouseIsRunning = false;       // 執行緒控制旗標
        public static bool _VideoIsplay = false;
        
        private Form currentChildForm; // 用於追蹤當前顯示的子窗體

        public MainForm()
        {
            InitializeComponent();
            MouseHoverPoint = new Point(-1, -1);
            MainWindowPbx.Enabled = false;
            RecordThread = null;
            Homebtn_Click(this, null);
        }


        private bool DisConnect()
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                Playtmr.Enabled = false;
                OWBGlobal.Camera.LogoutCamera();
                return true;
            }
            return false;
        }

        private void Playbtn_Click(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                if (OWBGlobal.Camera.IsPlay)
                {
                    OWBGlobal.Camera.StopStream();
                    Playbtn.Image = Properties.Resources.play__3_;
                    Playtmr.Enabled = false;
                    MainWindowPbx.Image = null;
                    MainWindowPbx.Enabled = false;
                    


                }
                else
                {
                    if (OWBGlobal.Camera.StartStream(LoginType.H264, StreamType.PRI))
                    {
                        Playtmr.Enabled = true;
                        Playbtn.Image = Properties.Resources.pause;
                        MainWindowPbx.Enabled = true;
                        OWBGlobal.Camera.PutTimeVisible(true);
                        OWBGlobal.Camera.PutUnitVisible(true);
                    }
                }
            }
        }

        private void Playtmr_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(TimerLock))
            {
                try
                {
                    if (OWBGlobal.Camera.IsConnected)
                    {
                        Image image = null;
                        OWBGlobal.Camera.GetH264Frame(out image);
                        if (image != null)
                        {
                            MainWindowPbx.Image = image;
                        }
                     
                    }
                    Thread.Sleep(33);
                    
                }
                finally
                {
                    Monitor.Exit(TimerLock);
                }
            }
        }

        public Thread RecordThread { get; set; }

        private void Recordbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedPath))
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = fbd.SelectedPath;
                        MessageBox.Show("選擇的資料夾路徑: " + selectedPath, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("請選擇一個資料夾路徑。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            string csvDirectory = Path.Combine(selectedPath, "CSV", DateTime.Now.ToString("yyyy-MM-dd"));
            string videoDirectory = Path.Combine(selectedPath, "Video", DateTime.Now.ToString("yyyy-MM-dd"));

            // Create directories if they do not exist
            if (!Directory.Exists(csvDirectory))
            {
                Directory.CreateDirectory(csvDirectory);
            }
            if (!Directory.Exists(videoDirectory))
            {
                Directory.CreateDirectory(videoDirectory);
            }

            string RecordCSVFileName = Path.Combine(csvDirectory, DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".csv");
            string RecordVideoFileName = Path.Combine(videoDirectory, DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".mp4");

            if (OWBGlobal.Camera.IsConnected)
            {
                if (OWBGlobal.Camera.IsRecord)
                {
                    Recordbtn.Image = Properties.Resources.record;
                    Recordbtn.Text = "Record";
                    RecordDone = true;
                    OWBGlobal.Camera.StopRecordStream();

                    if (RecordThread != null)
                    {
                        RecordThread.Interrupt();
                        RecordThread.Join(); // 確保執行緒安全結束
                        RecordThread = null;
                    }
                    Console.WriteLine("錄製已停止並保存。");
                }
                else
                {
                    if (OWBGlobal.Camera.StartStream(LoginType.RAW, StreamType.PRI))
                    {
                        if (OWBGlobal.Camera.StartRecordStream(videoDirectory))
                        {
                            Recordbtn.Image = Properties.Resources.recording;
                            Recordbtn.Text = "Stop";
                            RecordDone = false;
                            RecordThread = new Thread(() => RecordCSVCallback(RecordCSVFileName));
                            RecordThread.Priority = ThreadPriority.Lowest;
                            RecordThread.Start();
                            Console.WriteLine("錄影已啟動...");
                        }
                        else
                        {
                            MessageBox.Show("無法開始錄製。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("相機未連接。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool RecordDone
        {
            get { return _RecordDone; }
            set { _RecordDone = value; }
        }

        private void SaveThermalVideoToCsv(string filePath)
        {

            if (string.IsNullOrEmpty(selectedPath))
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = fbd.SelectedPath;
                        MessageBox.Show("選擇的資料夾路徑: " + selectedPath, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("請選擇一個資料夾路徑。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            float[,] temperatureValues = null;
            OWBGlobal.Camera.UpdateFactoryLUT();
            try
            {
                lock (CSVLock)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        int frameCounter = 0;
                        DateTime startTime = DateTime.Now;
                        while (!RecordDone)
                        {

                            if (!OWBGlobal.Camera.GetTemperatureFrame(out temperatureValues))
                            {
                                // 直接跳過這一幀，避免卡死
                                continue;
                            }
                            if (temperatureValues == null)
                            {
                                MessageBox.Show("無法獲取溫度影像。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // 計算時間間隔
                            TimeSpan elapsedTime = DateTime.Now - startTime;
                            if (elapsedTime.TotalSeconds >= 5) //幾秒寫入一次
                            {
                                WriteToCsvBuffer(temperatureValues, ++frameCounter);
                                startTime = DateTime.Now;
                            }
                            // 批次寫入 CSV
                            if (csvBuffer.Count >= bufferSize)
                            {
                                FlushCsvBuffer(sw);
                            }
                            Thread.Sleep(100); // 避免過於頻繁的檢查
                        }
                        // 錄製結束時將剩餘資料寫入
                        FlushCsvBuffer(sw);
                    }

                }
            }
            catch
            {
            }
        }
        private void FlushCsvBuffer(StreamWriter sw)
        {
            lock (CSVLock)
            {
                foreach (string line in csvBuffer)
                {
                    sw.WriteLine(line);
                }
                csvBuffer.Clear(); // 清空緩衝區
            }
        }
        private void WriteToCsvBuffer(float[,] temperatureValues, int frameCounter)
        {
            // 加入幀標記與時間戳
            csvBuffer.Add($"Frame {frameCounter} - Time: {DateTime.Now:yyyy-MM-dd HH-mm-ss}");

            // 加入溫度資料
            for (int i = 0; i < OWBGlobal.Camera.Height; i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j = 0; j < OWBGlobal.Camera.Width; j++)
                {
                    line.Append(temperatureValues[j, i].ToString("F1"));
                    if (j < OWBGlobal.Camera.Width - 1)
                        line.Append(",");
                }
                csvBuffer.Add(line.ToString());
            }
            csvBuffer.Add(""); // 空白行分隔幀
        }


        private void RecordCSVCallback(string filePath)
        {
            try
            {
                // 開始錄製
                SaveThermalVideoToCsv(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"錄製執行緒發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       


        private void MainWindowPbx_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            WidthRatio = MainWindowPbx.Width / OWBGlobal.Camera.Width;
            HeightRatio = MainWindowPbx.Height / OWBGlobal.Camera.Height;

            p.X = (int)((float)e.X / WidthRatio);
            p.Y = (int)((float)e.Y / HeightRatio);
            MouseHoverPoint = p;
        }

        public static Point MouseHoverPoint
        {
            get { return _MouseHoverPoint; }
            set
            {
                if (_MouseHoverPoint != value)
                {
                    _MouseHoverPoint = value;
                }
            }
        }

        private void MainWindowPbx_MouseLeave(object sender, EventArgs e)
        {
            MouseHoverPoint = new Point(-1, -1);
        }

        private void MainWindowPbx_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rx = (int)(MouseHoverPoint.X * WidthRatio);
            int ry = (int)(MouseHoverPoint.Y * HeightRatio);
            g.DrawLine(Pens.White, rx, ry - 4, rx, ry + 4);
            g.DrawLine(Pens.White, rx - 4, ry, rx + 4, ry);
        }
      

        private void TempCalbtn_Click(object sender, EventArgs e)
        {
            if (OWBGlobal.Camera.IsConnected)
            {
                OWBGlobal.Camera.PostCali();
            }
        }

        private async void Capturebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedPath))
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = fbd.SelectedPath;
                        MessageBox.Show("選擇的資料夾路徑: " + selectedPath, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("請選擇一個資料夾路徑。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (!OWBGlobal.Camera.StartStream(LoginType.RAW, StreamType.PRI))
            {
                MessageBox.Show("相機未串流。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float[,] temperatureValues = null;
            OWBGlobal.Camera.UpdateFactoryLUT();
            OWBGlobal.Camera.GetTemperatureFrame(out temperatureValues);

            string filePath = Path.Combine(selectedPath, "Capture", DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileNamePath = Path.Combine(filePath, "IR_" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".csv");

            try
            {
                
                await SaveTemperatureFastCSV(temperatureValues, fileNamePath);
                OWBGlobal.Camera.StopStream();
                OWBGlobal.Camera.StartStream(LoginType.H264, StreamType.PRI);
                MessageBox.Show("溫度快照已儲存成功！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存失敗：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task SaveTemperatureFastCSV(float[,] temperatureValues, string filePath)
        {
            int height = OWBGlobal.Camera.Height;
            int width = OWBGlobal.Camera.Width;

            // 用 StringBuilder 先在記憶體中把資料串好
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            // 將每一列的溫度值轉成 CSV 字串（仍需一次性迴圈）
            for (int i = 0; i < height; i++)
            {
                string[] row = new string[width];
                for (int j = 0; j < width; j++)
                {
                    row[j] = temperatureValues[j, i].ToString("F1");
                }
                sb.AppendLine(string.Join(",", row));
            }

            // 將整份字串一次寫入檔案
            await Task.Run(() => File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8));
        }

        bool MenuIsOpen = false;
        bool HomePageOpen = true;
        bool SettingIsOpen = false;

        private void Menubtn_Click(object sender, EventArgs e)
        {
            Infopanel.Dock = DockStyle.None;
            Mainpanel.Dock = DockStyle.None;
            panel3.Dock = DockStyle.None;  
            MenuPanel.BringToFront();
            
            Menutmr.Start();
            
        }
       

        private void Menutmr_Tick(object sender, EventArgs e)
        {
            if (!MenuIsOpen)
            {
                MenuPanel.Width += 10;

                if(MenuPanel.Width >=200)
                {
                    Menutmr.Stop();
                    MenuIsOpen = true;
                }
            }
            else
            {
                MenuPanel.Width -= 10;
                if (MenuPanel.Width == 0)
                {
                    Menutmr.Stop();
                    MenuIsOpen = false;
                }
            }
            
        }
        private void ReflashPanel()
        {
            if (SettingIsOpen == true && HomePageOpen == false )
            {
              
                Settingbtn.BackColor = Color.FromArgb(72, 65, 75);
                Settingbtn.ForeColor = Color.FromArgb(233, 175, 255);
                Homebtn.BackColor = Color.FromArgb(22, 23, 29);
                Homebtn.ForeColor = Color.FromArgb(237, 242, 242);

            }
            else if (SettingIsOpen == false && HomePageOpen == true)
            {
                Settingbtn.BackColor = Color.FromArgb(22, 23, 29);
                Settingbtn.ForeColor = Color.FromArgb(237, 242, 242);
                Homebtn.BackColor = Color.FromArgb(72, 65, 75);
                Homebtn.ForeColor = Color.FromArgb(233, 175, 255);
            }
        }

        private void Settingbtn_Click(object sender, EventArgs e)
        {
            SettingIsOpen = true;
            HomePageOpen = false;
            ReflashPanel();
            OpenChildForm(new SettingForm());
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
                currentChildForm.Hide();

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.Infopanel.Controls.Add(childForm);
            this.panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
      

        private void Homebtn_Click(object sender, EventArgs e)
        {
            HomePageOpen = true;
            SettingIsOpen = false;
            _MouseIsRunning = true;
            ReflashPanel();
            
            OpenChildForm(new UserForm());
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OWBGlobal.Camera.LogoutCamera();
        }





       
    }
}

