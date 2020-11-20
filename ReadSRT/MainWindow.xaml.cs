using ReadSRT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ReadSRT
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SRTManager manager;
        IList<SRT> listSRT;
        static Timer myTimer = new Timer();
        private int counterTranslate = 0;
        static int alarmCounter = 0;
        public MainWindow()
        {
            InitializeComponent();
            manager = new SRTManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonStart.IsEnabled = false;
            GetTextToDisplay();
        }

        private async Task GetTextToDisplay()
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() => {
                    counterTranslate = 0;
                    labelTranslate.Content = listSRT[counterTranslate]?.Translation;
                    myTimer.Tick += new EventHandler(TimerEventProcessor);
                    myTimer.Interval = 10;
                    myTimer.Enabled = true;
                    myTimer.Start();
                });
                
            });
            

        }

        private void TimerEventProcessor(Object myObject,
                                            EventArgs myEventArgs)
        {
            alarmCounter += myTimer.Interval;
            labelTime.Content = DateTime.Now.ToString("HH:mm:ss");
            if (listSRT[counterTranslate]?.Interval <= alarmCounter)
            {
                alarmCounter = 0;

                counterTranslate++;
                if (counterTranslate >= listSRT.Count() - 1)
                {
                    myTimer.Stop();
                    ButtonStart.IsEnabled = true;
                    counterTranslate = 0;
                }
                labelTranslate.Content = listSRT[counterTranslate]?.Translation;
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonStart.IsEnabled = false;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    listSRT = manager.getListSTR(openFileDialog.FileName);
                    ButtonStart.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error");
            }
        }
    }
    
}
