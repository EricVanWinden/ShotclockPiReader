using ShotclockPiReader.Converter;
using ShotclockPiReader.Messaging;
using System;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace ShotclockPiReader.WinForm
{
    public partial class Main : Form
    {
        /// <summary>
        /// The URI of the ShotclockPi REST API.
        /// </summary>
        private const string _uri = "http://192.168.1.102:8080/time";

        public Main()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, System.EventArgs e)
        {
            if (!bgwRestApi.IsBusy)
            {
                rtbLog.AppendText($"Start reading from {_uri}.\n");
                bgwRestApi.RunWorkerAsync();
            }
            else
            {
                rtbLog.AppendText("Already connected.\n");
                rtbLog.ScrollToCaret();
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (bgwRestApi.IsBusy && bgwRestApi.WorkerSupportsCancellation)
            {
                bgwRestApi.CancelAsync();
                rtbLog.AppendText("Disconnect requested, stopping background worker...\n");
                rtbLog.ScrollToCaret();
            }
            else
            {
                rtbLog.AppendText("Background worker is not running.\n");
                rtbLog.ScrollToCaret();
            }
        }

        private void SetTime(string json)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate { SetTime(json); }));
            }
            else
            {
                rtbLog.AppendText($"{json}\n");
                rtbLog.ScrollToCaret();
                try
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse>(json);
                    if (apiResponse != null && apiResponse.Time.HasValue)
                    {
                        var time = apiResponse.Time.Value;
                        // if second > 15:
                        //    clock.config(fg="lime", bg="black")
                        //    root.configure(background='black')
                        if (time > 15)
                        {
                            lblTime.ForeColor = System.Drawing.Color.Lime;
                            lblTime.BackColor = System.Drawing.Color.Black;
                        }
                        // elif 15 >= second > 5:
                        //  clock.config(fg = "yellow", bg = "black")
                        //  root.configure(background = 'black')
                        else if (time > 5)
                        {
                            lblTime.ForeColor = System.Drawing.Color.Yellow;
                            lblTime.BackColor = System.Drawing.Color.Black;
                        }
                        //elif 5 >= second > 0:
                        //    clock.config(fg="red", bg="black")
                        //    root.configure(background='black')
                        else if (time > 0)
                        {
                            lblTime.ForeColor = System.Drawing.Color.Red;
                            lblTime.BackColor = System.Drawing.Color.Black;
                        }
                        //elif second == 0:
                        //    clock.config(fg="white", bg="red")
                        //    root.configure(background='red')
                        else if (time == 0)
                        {
                            lblTime.ForeColor = System.Drawing.Color.White;
                            lblTime.BackColor = System.Drawing.Color.Red;
                        }

                        //if second < 5:
                        //    clock.config(text=f"{second:.1f}")
                        //elif second < 10:
                        //    clock.config(text=f"0{int(second)}")
                        //else:
                        //    clock.config(text=str(int(second)))
                        if (time < 5)
                        {
                            lblTime.Text = $"{time:F1}";
                        }
                        else if (time < 10)
                        {
                            lblTime.Text = $"0{(int)time}";
                        }
                        else
                        {
                            lblTime.Text = $"{(int)time}";
                        }
                    }
                }
                catch (JsonException exception)
                {
                    rtbLog.AppendText($"{exception}\n");
                    rtbLog.ScrollToCaret();
                }
            }
        }

        private void bgwRestApi_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var worker = sender as System.ComponentModel.BackgroundWorker;
            while (worker != null && !worker.CancellationPending)
            {
                var json = RestApiReader.GetMessage(_uri);
                SetTime(json);
                Thread.Sleep(50);
            }

            if (worker != null && worker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void bgwRestApi_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                rtbLog.AppendText("Background worker stopped.\n");
                rtbLog.ScrollToCaret();
            }
            else if (e.Error != null)
            {
                rtbLog.AppendText($"Background worker error: {e.Error}\n");
                rtbLog.ScrollToCaret();
            }
            else
            {
                rtbLog.AppendText("Background worker completed.\n");
                rtbLog.ScrollToCaret();
            }
        }


    }
}