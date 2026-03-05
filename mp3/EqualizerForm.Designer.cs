namespace mp3
{
    partial class EqualizerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ComboBox presetComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblLow;
        private System.Windows.Forms.TrackBar sliderLow;
        private System.Windows.Forms.Label lblLowValue;
        private System.Windows.Forms.Label lblMidLow;
        private System.Windows.Forms.TrackBar sliderMidLow;
        private System.Windows.Forms.Label lblMidLowValue;
        private System.Windows.Forms.Label lblMid;
        private System.Windows.Forms.TrackBar sliderMid;
        private System.Windows.Forms.Label lblMidValue;
        private System.Windows.Forms.Label lblMidHigh;
        private System.Windows.Forms.TrackBar sliderMidHigh;
        private System.Windows.Forms.Label lblMidHighValue;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.TrackBar sliderHigh;
        private System.Windows.Forms.Label lblHighValue;
        private System.Windows.Forms.FlowLayoutPanel soundTypePanel;
        private System.Windows.Forms.Label lblSoundTypes;
        private System.Windows.Forms.Button btnAmbient;
        private System.Windows.Forms.Button btnClub;
        private System.Windows.Forms.Button btnStudio;
        private System.Windows.Forms.Button btnCinematic;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnClose;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new System.Windows.Forms.Label();
            mainPanel = new System.Windows.Forms.Panel();
            presetComboBox = new System.Windows.Forms.ComboBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lblLow = new System.Windows.Forms.Label();
            sliderLow = new System.Windows.Forms.TrackBar();
            lblLowValue = new System.Windows.Forms.Label();
            lblMidLow = new System.Windows.Forms.Label();
            sliderMidLow = new System.Windows.Forms.TrackBar();
            lblMidLowValue = new System.Windows.Forms.Label();
            lblMid = new System.Windows.Forms.Label();
            sliderMid = new System.Windows.Forms.TrackBar();
            lblMidValue = new System.Windows.Forms.Label();
            lblMidHigh = new System.Windows.Forms.Label();
            sliderMidHigh = new System.Windows.Forms.TrackBar();
            lblMidHighValue = new System.Windows.Forms.Label();
            lblHigh = new System.Windows.Forms.Label();
            sliderHigh = new System.Windows.Forms.TrackBar();
            lblHighValue = new System.Windows.Forms.Label();
            soundTypePanel = new System.Windows.Forms.FlowLayoutPanel();
            lblSoundTypes = new System.Windows.Forms.Label();
            btnAmbient = new System.Windows.Forms.Button();
            btnClub = new System.Windows.Forms.Button();
            btnStudio = new System.Windows.Forms.Button();
            btnCinematic = new System.Windows.Forms.Button();
            btnLive = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            mainPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sliderLow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliderMidLow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliderMid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliderMidHigh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliderHigh).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lblTitle.Location = new System.Drawing.Point(27, 19);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(338, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "EQUILIBRIO SONORO PRO";
            //
            // mainPanel
            //
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            mainPanel.Controls.Add(btnClose);
            mainPanel.Controls.Add(soundTypePanel);
            mainPanel.Controls.Add(tableLayoutPanel1);
            mainPanel.Controls.Add(presetComboBox);
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Location = new System.Drawing.Point(12, 12);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new System.Windows.Forms.Padding(16);
            mainPanel.Size = new System.Drawing.Size(462, 448);
            mainPanel.TabIndex = 1;
            //
            // presetComboBox
            //
            presetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            presetComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            presetComboBox.FormattingEnabled = true;
            presetComboBox.Location = new System.Drawing.Point(27, 68);
            presetComboBox.Name = "presetComboBox";
            presetComboBox.Size = new System.Drawing.Size(308, 29);
            presetComboBox.TabIndex = 2;
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(lblLow, 0, 0);
            tableLayoutPanel1.Controls.Add(sliderLow, 1, 0);
            tableLayoutPanel1.Controls.Add(lblLowValue, 2, 0);
            tableLayoutPanel1.Controls.Add(lblMidLow, 0, 1);
            tableLayoutPanel1.Controls.Add(sliderMidLow, 1, 1);
            tableLayoutPanel1.Controls.Add(lblMidLowValue, 2, 1);
            tableLayoutPanel1.Controls.Add(lblMid, 0, 2);
            tableLayoutPanel1.Controls.Add(sliderMid, 1, 2);
            tableLayoutPanel1.Controls.Add(lblMidValue, 2, 2);
            tableLayoutPanel1.Controls.Add(lblMidHigh, 0, 3);
            tableLayoutPanel1.Controls.Add(sliderMidHigh, 1, 3);
            tableLayoutPanel1.Controls.Add(lblMidHighValue, 2, 3);
            tableLayoutPanel1.Controls.Add(lblHigh, 0, 4);
            tableLayoutPanel1.Controls.Add(sliderHigh, 1, 4);
            tableLayoutPanel1.Controls.Add(lblHighValue, 2, 4);
            tableLayoutPanel1.Location = new System.Drawing.Point(27, 113);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(404, 250);
            tableLayoutPanel1.TabIndex = 3;
            //
            // lblLow
            //
            lblLow.Anchor = AnchorStyles.Left;
            lblLow.AutoSize = true;
            lblLow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblLow.Location = new System.Drawing.Point(3, 16);
            lblLow.Name = "lblLow";
            lblLow.Size = new System.Drawing.Size(76, 19);
            lblLow.TabIndex = 0;
            lblLow.Text = "60 Hz - Bass";
            //
            // sliderLow
            //
            sliderLow.AutoSize = false;
            sliderLow.Location = new System.Drawing.Point(186, 3);
            sliderLow.Maximum = 8;
            sliderLow.Minimum = -8;
            sliderLow.Name = "sliderLow";
            sliderLow.Size = new System.Drawing.Size(191, 44);
            sliderLow.TabIndex = 1;
            sliderLow.TickStyle = System.Windows.Forms.TickStyle.Both;
            sliderLow.Scroll += Slider_ValueChanged;
            sliderLow.ValueChanged += Slider_ValueChanged;
            //
            // lblLowValue
            //
            lblLowValue.Anchor = AnchorStyles.Right;
            lblLowValue.AutoSize = true;
            lblLowValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblLowValue.Location = new System.Drawing.Point(385, 20);
            lblLowValue.Name = "lblLowValue";
            lblLowValue.Size = new System.Drawing.Size(16, 15);
            lblLowValue.TabIndex = 2;
            lblLowValue.Text = "0";
            //
            // lblMidLow
            //
            lblMidLow.Anchor = AnchorStyles.Left;
            lblMidLow.AutoSize = true;
            lblMidLow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblMidLow.Location = new System.Drawing.Point(3, 66);
            lblMidLow.Name = "lblMidLow";
            lblMidLow.Size = new System.Drawing.Size(110, 19);
            lblMidLow.TabIndex = 3;
            lblMidLow.Text = "250 Hz - Warmth";
            //
            // sliderMidLow
            //
            sliderMidLow.AutoSize = false;
            sliderMidLow.Location = new System.Drawing.Point(186, 53);
            sliderMidLow.Maximum = 8;
            sliderMidLow.Minimum = -8;
            sliderMidLow.Name = "sliderMidLow";
            sliderMidLow.Size = new System.Drawing.Size(191, 44);
            sliderMidLow.TabIndex = 4;
            sliderMidLow.TickStyle = System.Windows.Forms.TickStyle.Both;
            sliderMidLow.Scroll += Slider_ValueChanged;
            sliderMidLow.ValueChanged += Slider_ValueChanged;
            //
            // lblMidLowValue
            //
            lblMidLowValue.Anchor = AnchorStyles.Right;
            lblMidLowValue.AutoSize = true;
            lblMidLowValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblMidLowValue.Location = new System.Drawing.Point(385, 74);
            lblMidLowValue.Name = "lblMidLowValue";
            lblMidLowValue.Size = new System.Drawing.Size(16, 15);
            lblMidLowValue.TabIndex = 5;
            lblMidLowValue.Text = "0";
            //
            // lblMid
            //
            lblMid.Anchor = AnchorStyles.Left;
            lblMid.AutoSize = true;
            lblMid.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblMid.Location = new System.Drawing.Point(3, 116);
            lblMid.Name = "lblMid";
            lblMid.Size = new System.Drawing.Size(96, 19);
            lblMid.TabIndex = 6;
            lblMid.Text = "1 kHz - Voice";
            //
            // sliderMid
            //
            sliderMid.AutoSize = false;
            sliderMid.Location = new System.Drawing.Point(186, 103);
            sliderMid.Maximum = 8;
            sliderMid.Minimum = -8;
            sliderMid.Name = "sliderMid";
            sliderMid.Size = new System.Drawing.Size(191, 44);
            sliderMid.TabIndex = 7;
            sliderMid.TickStyle = System.Windows.Forms.TickStyle.Both;
            sliderMid.Scroll += Slider_ValueChanged;
            sliderMid.ValueChanged += Slider_ValueChanged;
            //
            // lblMidValue
            //
            lblMidValue.Anchor = AnchorStyles.Right;
            lblMidValue.AutoSize = true;
            lblMidValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblMidValue.Location = new System.Drawing.Point(385, 124);
            lblMidValue.Name = "lblMidValue";
            lblMidValue.Size = new System.Drawing.Size(16, 15);
            lblMidValue.TabIndex = 8;
            lblMidValue.Text = "0";
            //
            // lblMidHigh
            //
            lblMidHigh.Anchor = AnchorStyles.Left;
            lblMidHigh.AutoSize = true;
            lblMidHigh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblMidHigh.Location = new System.Drawing.Point(3, 166);
            lblMidHigh.Name = "lblMidHigh";
            lblMidHigh.Size = new System.Drawing.Size(134, 19);
            lblMidHigh.TabIndex = 9;
            lblMidHigh.Text = "4 kHz - Clarity";
            //
            // sliderMidHigh
            //
            sliderMidHigh.AutoSize = false;
            sliderMidHigh.Location = new System.Drawing.Point(186, 153);
            sliderMidHigh.Maximum = 8;
            sliderMidHigh.Minimum = -8;
            sliderMidHigh.Name = "sliderMidHigh";
            sliderMidHigh.Size = new System.Drawing.Size(191, 44);
            sliderMidHigh.TabIndex = 10;
            sliderMidHigh.TickStyle = System.Windows.Forms.TickStyle.Both;
            sliderMidHigh.Scroll += Slider_ValueChanged;
            sliderMidHigh.ValueChanged += Slider_ValueChanged;
            //
            // lblMidHighValue
            //
            lblMidHighValue.Anchor = AnchorStyles.Right;
            lblMidHighValue.AutoSize = true;
            lblMidHighValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblMidHighValue.Location = new System.Drawing.Point(385, 174);
            lblMidHighValue.Name = "lblMidHighValue";
            lblMidHighValue.Size = new System.Drawing.Size(16, 15);
            lblMidHighValue.TabIndex = 11;
            lblMidHighValue.Text = "0";
            //
            // lblHigh
            //
            lblHigh.Anchor = AnchorStyles.Left;
            lblHigh.AutoSize = true;
            lblHigh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblHigh.Location = new System.Drawing.Point(3, 216);
            lblHigh.Name = "lblHigh";
            lblHigh.Size = new System.Drawing.Size(101, 19);
            lblHigh.TabIndex = 12;
            lblHigh.Text = "16 kHz - Air";
            //
            // sliderHigh
            //
            sliderHigh.AutoSize = false;
            sliderHigh.Location = new System.Drawing.Point(186, 203);
            sliderHigh.Maximum = 8;
            sliderHigh.Minimum = -8;
            sliderHigh.Name = "sliderHigh";
            sliderHigh.Size = new System.Drawing.Size(191, 44);
            sliderHigh.TabIndex = 13;
            sliderHigh.TickStyle = System.Windows.Forms.TickStyle.Both;
            sliderHigh.Scroll += Slider_ValueChanged;
            sliderHigh.ValueChanged += Slider_ValueChanged;
            //
            // lblHighValue
            //
            lblHighValue.Anchor = AnchorStyles.Right;
            lblHighValue.AutoSize = true;
            lblHighValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblHighValue.Location = new System.Drawing.Point(385, 224);
            lblHighValue.Name = "lblHighValue";
            lblHighValue.Size = new System.Drawing.Size(16, 15);
            lblHighValue.TabIndex = 14;
            lblHighValue.Text = "0";
            //
            // soundTypePanel
            //
            soundTypePanel.AutoSize = true;
            soundTypePanel.AutoSizeMode = AutoSizeMode.GrowOnly;
            soundTypePanel.BackColor = System.Drawing.Color.Transparent;
            soundTypePanel.Controls.Add(lblSoundTypes);
            soundTypePanel.Controls.Add(btnAmbient);
            soundTypePanel.Controls.Add(btnClub);
            soundTypePanel.Controls.Add(btnStudio);
            soundTypePanel.Controls.Add(btnCinematic);
            soundTypePanel.Controls.Add(btnLive);
            soundTypePanel.Dock = DockStyle.Bottom;
            soundTypePanel.FlowDirection = FlowDirection.LeftToRight;
            soundTypePanel.Location = new System.Drawing.Point(27, 378);
            soundTypePanel.Margin = new System.Windows.Forms.Padding(0);
            soundTypePanel.Name = "soundTypePanel";
            soundTypePanel.TabIndex = 4;
            //
            // lblSoundTypes
            //
            lblSoundTypes.AutoSize = true;
            lblSoundTypes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSoundTypes.Location = new System.Drawing.Point(3, 0);
            lblSoundTypes.Margin = new System.Windows.Forms.Padding(3, 0, 6, 0);
            lblSoundTypes.Name = "lblSoundTypes";
            lblSoundTypes.Size = new System.Drawing.Size(83, 17);
            lblSoundTypes.TabIndex = 0;
            lblSoundTypes.Text = "Tipos de sonido:";
            //
            // btnAmbient
            //
            btnAmbient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAmbient.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAmbient.Location = new System.Drawing.Point(95, 3);
            btnAmbient.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            btnAmbient.Name = "btnAmbient";
            btnAmbient.Size = new System.Drawing.Size(70, 34);
            btnAmbient.TabIndex = 1;
            btnAmbient.Text = "Ambient";
            btnAmbient.UseVisualStyleBackColor = true;
            //
            // btnClub
            //
            btnClub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnClub.Location = new System.Drawing.Point(171, 3);
            btnClub.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            btnClub.Name = "btnClub";
            btnClub.Size = new System.Drawing.Size(58, 34);
            btnClub.TabIndex = 2;
            btnClub.Text = "Club";
            btnClub.UseVisualStyleBackColor = true;
            //
            // btnStudio
            //
            btnStudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnStudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnStudio.Location = new System.Drawing.Point(235, 3);
            btnStudio.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            btnStudio.Name = "btnStudio";
            btnStudio.Size = new System.Drawing.Size(60, 34);
            btnStudio.TabIndex = 3;
            btnStudio.Text = "Studio";
            btnStudio.UseVisualStyleBackColor = true;
            //
            // btnCinematic
            //
            btnCinematic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCinematic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCinematic.Location = new System.Drawing.Point(301, 3);
            btnCinematic.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            btnCinematic.Name = "btnCinematic";
            btnCinematic.Size = new System.Drawing.Size(72, 34);
            btnCinematic.TabIndex = 4;
            btnCinematic.Text = "Cinemat";
            btnCinematic.UseVisualStyleBackColor = true;
            //
            // btnLive
            //
            btnLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnLive.Location = new System.Drawing.Point(379, 3);
            btnLive.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            btnLive.Name = "btnLive";
            btnLive.Size = new System.Drawing.Size(56, 34);
            btnLive.TabIndex = 5;
            btnLive.Text = "Live";
            btnLive.UseVisualStyleBackColor = true;
            //
            // btnClose
            //
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.BackColor = System.Drawing.Color.FromArgb(31, 112, 227);
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(335, 438);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(96, 28);
            btnClose.TabIndex = 5;
            btnClose.Text = "Cerrar";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += (_, _) => Close();
            //
            // EqualizerForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            ClientSize = new System.Drawing.Size(486, 472);
            Controls.Add(mainPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EqualizerForm";
            Padding = new System.Windows.Forms.Padding(4);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ecualizador Maestro";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sliderLow).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliderMidLow).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliderMid).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliderMidHigh).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliderHigh).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
