using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace haberBot
{
    public partial class LoadingForm : Form
    {
        private Label loadingLabel;
        public LoadingForm()
        {
            InitializeComponent();
            this.Width = 200;
            this.Height = 100;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            loadingLabel = new Label();
            loadingLabel.Text = "Yükleniyor...";
            loadingLabel.AutoSize = true;
            loadingLabel.Location = new System.Drawing.Point(20, 20);

            this.Controls.Add(loadingLabel);
        }
    }
}
