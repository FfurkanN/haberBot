﻿namespace haberBot
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.run = new System.Windows.Forms.Button();
            this.techInside = new System.Windows.Forms.Button();
            this.shiftDelete = new System.Windows.Forms.Button();
            this.technologyreview = new System.Windows.Forms.Button();
            this.mashable = new System.Windows.Forms.Button();
            this.zdnet = new System.Windows.Forms.Button();
            this.webrazzi = new System.Windows.Forms.Button();
            this.futurism = new System.Windows.Forms.Button();
            this.readwrite = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.documentPanel = new System.Windows.Forms.Panel();
            this.newsRichTextbox = new System.Windows.Forms.RichTextBox();
            this.frekansPanel = new System.Windows.Forms.Panel();
            this.frequencyBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // run
            // 
            this.run.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.run.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.run.Location = new System.Drawing.Point(0, 0);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(194, 60);
            this.run.TabIndex = 0;
            this.run.Text = "Haberleri çek";
            this.run.UseVisualStyleBackColor = false;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // techInside
            // 
            this.techInside.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.techInside.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.techInside.Location = new System.Drawing.Point(0, 150);
            this.techInside.Name = "techInside";
            this.techInside.Size = new System.Drawing.Size(194, 60);
            this.techInside.TabIndex = 5;
            this.techInside.Text = "techinside";
            this.techInside.UseVisualStyleBackColor = false;
            this.techInside.Click += new System.EventHandler(this.techInside_Click);
            // 
            // shiftDelete
            // 
            this.shiftDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.shiftDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.shiftDelete.Location = new System.Drawing.Point(0, 210);
            this.shiftDelete.Name = "shiftDelete";
            this.shiftDelete.Size = new System.Drawing.Size(194, 60);
            this.shiftDelete.TabIndex = 6;
            this.shiftDelete.Text = "ShiftDelete";
            this.shiftDelete.UseVisualStyleBackColor = false;
            this.shiftDelete.Click += new System.EventHandler(this.shiftDelete_Click);
            // 
            // technologyreview
            // 
            this.technologyreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.technologyreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.technologyreview.Location = new System.Drawing.Point(0, 270);
            this.technologyreview.Name = "technologyreview";
            this.technologyreview.Size = new System.Drawing.Size(194, 60);
            this.technologyreview.TabIndex = 7;
            this.technologyreview.Text = "technologyreview";
            this.technologyreview.UseVisualStyleBackColor = false;
            this.technologyreview.Click += new System.EventHandler(this.technologyreview_Click);
            // 
            // mashable
            // 
            this.mashable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.mashable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mashable.Location = new System.Drawing.Point(0, 330);
            this.mashable.Name = "mashable";
            this.mashable.Size = new System.Drawing.Size(194, 60);
            this.mashable.TabIndex = 8;
            this.mashable.Text = "mashable";
            this.mashable.UseVisualStyleBackColor = false;
            this.mashable.Click += new System.EventHandler(this.mashable_Click);
            // 
            // zdnet
            // 
            this.zdnet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.zdnet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.zdnet.Location = new System.Drawing.Point(0, 390);
            this.zdnet.Name = "zdnet";
            this.zdnet.Size = new System.Drawing.Size(194, 60);
            this.zdnet.TabIndex = 9;
            this.zdnet.Text = "zdnet";
            this.zdnet.UseVisualStyleBackColor = false;
            this.zdnet.Click += new System.EventHandler(this.zdnet_Click);
            // 
            // webrazzi
            // 
            this.webrazzi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.webrazzi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.webrazzi.Location = new System.Drawing.Point(0, 450);
            this.webrazzi.Name = "webrazzi";
            this.webrazzi.Size = new System.Drawing.Size(194, 60);
            this.webrazzi.TabIndex = 10;
            this.webrazzi.Text = "webrazzi";
            this.webrazzi.UseVisualStyleBackColor = false;
            this.webrazzi.Click += new System.EventHandler(this.webrazzi_Click);
            // 
            // futurism
            // 
            this.futurism.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.futurism.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.futurism.Location = new System.Drawing.Point(0, 510);
            this.futurism.Name = "futurism";
            this.futurism.Size = new System.Drawing.Size(194, 60);
            this.futurism.TabIndex = 11;
            this.futurism.Text = "futurism";
            this.futurism.UseVisualStyleBackColor = false;
            this.futurism.Click += new System.EventHandler(this.futurism_Click);
            // 
            // readwrite
            // 
            this.readwrite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.readwrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.readwrite.Location = new System.Drawing.Point(0, 570);
            this.readwrite.Name = "readwrite";
            this.readwrite.Size = new System.Drawing.Size(194, 60);
            this.readwrite.TabIndex = 12;
            this.readwrite.Text = "readwrite";
            this.readwrite.UseVisualStyleBackColor = false;
            this.readwrite.Click += new System.EventHandler(this.readwrite_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.frequencyBtn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.run);
            this.panel1.Controls.Add(this.techInside);
            this.panel1.Controls.Add(this.shiftDelete);
            this.panel1.Controls.Add(this.readwrite);
            this.panel1.Controls.Add(this.technologyreview);
            this.panel1.Controls.Add(this.futurism);
            this.panel1.Controls.Add(this.mashable);
            this.panel1.Controls.Add(this.webrazzi);
            this.panel1.Controls.Add(this.zdnet);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 650);
            this.panel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(38, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 13;
            this.label1.Text = "Haberler";
            // 
            // documentPanel
            // 
            this.documentPanel.AutoScroll = true;
            this.documentPanel.Location = new System.Drawing.Point(225, 5);
            this.documentPanel.Name = "documentPanel";
            this.documentPanel.Size = new System.Drawing.Size(220, 650);
            this.documentPanel.TabIndex = 16;
            // 
            // newsRichTextbox
            // 
            this.newsRichTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.newsRichTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newsRichTextbox.ForeColor = System.Drawing.Color.White;
            this.newsRichTextbox.Location = new System.Drawing.Point(450, 5);
            this.newsRichTextbox.Name = "newsRichTextbox";
            this.newsRichTextbox.Size = new System.Drawing.Size(500, 650);
            this.newsRichTextbox.TabIndex = 17;
            this.newsRichTextbox.Text = "";
            // 
            // frekansPanel
            // 
            this.frekansPanel.AutoScroll = true;
            this.frekansPanel.Location = new System.Drawing.Point(965, 5);
            this.frekansPanel.Name = "frekansPanel";
            this.frekansPanel.Size = new System.Drawing.Size(290, 650);
            this.frekansPanel.TabIndex = 18;
            // 
            // frequencyBtn
            // 
            this.frequencyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.frequencyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.frequencyBtn.Location = new System.Drawing.Point(0, 60);
            this.frequencyBtn.Name = "frequencyBtn";
            this.frequencyBtn.Size = new System.Drawing.Size(194, 60);
            this.frequencyBtn.TabIndex = 14;
            this.frequencyBtn.Text = "Frekanslar";
            this.frequencyBtn.UseVisualStyleBackColor = false;
            this.frequencyBtn.Click += new System.EventHandler(this.frequencyBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.frekansPanel);
            this.Controls.Add(this.newsRichTextbox);
            this.Controls.Add(this.documentPanel);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Haber Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button run;
        private System.Windows.Forms.Button techInside;
        private System.Windows.Forms.Button shiftDelete;
        private System.Windows.Forms.Button technologyreview;
        private System.Windows.Forms.Button mashable;
        private System.Windows.Forms.Button zdnet;
        private System.Windows.Forms.Button webrazzi;
        private System.Windows.Forms.Button futurism;
        private System.Windows.Forms.Button readwrite;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel documentPanel;
        private System.Windows.Forms.RichTextBox newsRichTextbox;
        private System.Windows.Forms.Panel frekansPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button frequencyBtn;
    }
}

