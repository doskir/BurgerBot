using System;
using System.Drawing;
using System.Drawing.Imaging;
using Form = System.Windows.Forms.Form;

namespace BurgerBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadGame();
            FlashAutomation.FlashHandle = axShockwaveFlash1.Handle;
            FlashAutomation.FlashObject = axShockwaveFlash1;
#if DEBUG
            LogButton.Visible = true;
#endif
        }
        public void LoadGame()
        {
            Log.AddMessage("Loading game...",Log.LoggingLevel.Debug);
            axShockwaveFlash1.Stop();
            axShockwaveFlash1.Base = "http://chat.kongregate.com/gamez/0009/8237/live/";
            axShockwaveFlash1.AllowNetworking = "all";
            axShockwaveFlash1.AllowScriptAccess = "never";
            axShockwaveFlash1.BGColor = "#ffffff";
            axShockwaveFlash1.Movie = "http://chat.kongregate.com/gamez/0009/8237/live/papasburgeria_kong.swf?kongregate_game_version=1292084147";
            Log.AddMessage("Done Loading",Log.LoggingLevel.Debug);
        }

        private void RunBotButton_Click(object sender, EventArgs e)
        {
            //DragDrop(557, 133, 292, 14);
            Bot.RunBot();
        }


        private void StopBotButton_Click(object sender, EventArgs e)
        {
            Bot.StopBot();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Bot.ContinueAfterThisDay = checkBox1.Checked;
            Log.AddMessage("ContinueAfterThisDay changed to" + Bot.ContinueAfterThisDay,
                           Log.LoggingLevel.Debug);
        }

        private LogWindow lw;
        private void LogButton_Click(object sender, EventArgs e)
        {
            if(lw == null || !lw.CanSelect)
                lw = new LogWindow();
            lw.Show();
            lw.Activate();
        }
    }
}
