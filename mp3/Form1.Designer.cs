namespace mp3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView lvTracks;
        private System.Windows.Forms.ImageList imageListCovers;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnOpenInWmp;
        private System.Windows.Forms.PictureBox pbCover;
        private System.Windows.Forms.Label lblNow;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Timer playbackTimer;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chAlbum;
        private System.Windows.Forms.ColumnHeader chArtist;
        private System.Windows.Forms.ColumnHeader chDuration;
        private System.Windows.Forms.ColumnHeader chPath;

        #region Windows Form Designer generated code

        private PictureBox pictureBox1;
        private TrackBar trackBar1;
        private TrackBar trackBarVolume;
        private Label lblVolume;
        private Button btnSavePlaylist;
        private Button btnLoadPlaylist;
        private Button btnAddToPlaylist;
        private Label lblPlaylistCount;
        private Button btnClearList;
        private Button btnOpenEqualizer;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lvTracks = new ListView();
            chTitle = new ColumnHeader();
            chAlbum = new ColumnHeader();
            chArtist = new ColumnHeader();
            chDuration = new ColumnHeader();
            chPath = new ColumnHeader();
            imageListCovers = new ImageList(components);
            btnOpenFile = new Button();
            btnOpenFolder = new Button();
            btnOpenInWmp = new Button();
            pbCover = new PictureBox();
            lblNow = new Label();
            lblDuration = new Label();
            playbackTimer = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            trackBar1 = new TrackBar();
            trackBarVolume = new TrackBar();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            lblVolume = new Label();
            btnSavePlaylist = new Button();
            btnLoadPlaylist = new Button();
            btnAddToPlaylist = new Button();
            lblPlaylistCount = new Label();
            btnClearList = new Button();
            btnOpenEqualizer = new Button();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox9 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbCover).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            SuspendLayout();
            // 
            // lvTracks
            // 
            lvTracks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lvTracks.Columns.AddRange(new ColumnHeader[] { chTitle, chAlbum, chArtist, chDuration, chPath });
            lvTracks.FullRowSelect = true;
            lvTracks.Location = new Point(628, 3);
            lvTracks.MultiSelect = false;
            lvTracks.Name = "lvTracks";
            lvTracks.Size = new Size(621, 401);
            lvTracks.SmallImageList = imageListCovers;
            lvTracks.TabIndex = 0;
            lvTracks.UseCompatibleStateImageBehavior = false;
            lvTracks.View = View.Details;
            lvTracks.SelectedIndexChanged += lvTracks_SelectedIndexChanged_1;
            // 
            // chTitle
            // 
            chTitle.Text = "Título";
            chTitle.Width = 200;
            // 
            // chAlbum
            // 
            chAlbum.Text = "Álbum";
            chAlbum.Width = 120;
            // 
            // chArtist
            // 
            chArtist.Text = "Artista";
            chArtist.Width = 120;
            // 
            // chDuration
            // 
            chDuration.Text = "Duración";
            chDuration.Width = 79;
            // 
            // chPath
            // 
            chPath.Text = "Ruta";
            chPath.Width = 0;
            // 
            // imageListCovers
            // 
            imageListCovers.ColorDepth = ColorDepth.Depth32Bit;
            imageListCovers.ImageSize = new Size(64, 64);
            imageListCovers.TransparentColor = Color.Transparent;
            // 
            // btnOpenFile
            // 
            btnOpenFile.Location = new Point(628, 410);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(102, 28);
            btnOpenFile.TabIndex = 1;
            btnOpenFile.Text = "Abrir archivo";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.Location = new Point(736, 410);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(117, 28);
            btnOpenFolder.TabIndex = 2;
            btnOpenFolder.Text = "Abrir carpeta";
            btnOpenFolder.UseVisualStyleBackColor = true;
            btnOpenFolder.Click += btnOpenFolder_Click;
            // 
            // btnOpenInWmp
            // 
            btnOpenInWmp.Location = new Point(1147, 410);
            btnOpenInWmp.Name = "btnOpenInWmp";
            btnOpenInWmp.Size = new Size(102, 28);
            btnOpenInWmp.TabIndex = 8;
            btnOpenInWmp.Text = "Abrir WMP";
            btnOpenInWmp.UseVisualStyleBackColor = true;
            btnOpenInWmp.Click += btnOpenInWmp_Click;
            // 
            // pbCover
            // 
            pbCover.Location = new Point(76, 21);
            pbCover.Name = "pbCover";
            pbCover.Size = new Size(295, 181);
            pbCover.SizeMode = PictureBoxSizeMode.Zoom;
            pbCover.TabIndex = 9;
            pbCover.TabStop = false;
            pbCover.Click += pbCover_Click;
            // 
            // lblNow
            // 
            lblNow.AutoSize = true;
            lblNow.Location = new Point(36, 212);
            lblNow.Name = "lblNow";
            lblNow.Size = new Size(44, 20);
            lblNow.TabIndex = 10;
            lblNow.Text = "00:00";
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.Location = new Point(337, 212);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(44, 20);
            lblDuration.TabIndex = 11;
            lblDuration.Text = "00:00";
            // 
            // playbackTimer
            // 
            playbackTimer.Interval = 500;
            playbackTimer.Tick += playbackTimer_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(171, 270);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // trackBar1
            // 
            trackBar1.Enabled = false;
            trackBar1.Location = new Point(76, 208);
            trackBar1.Maximum = 0;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(264, 56);
            trackBar1.TabIndex = 15;
            trackBar1.TickStyle = TickStyle.None;
            trackBar1.Scroll += trackBar1_Scroll;
            trackBar1.MouseDown += trackBar1_MouseDown;
            trackBar1.MouseUp += trackBar1_MouseUp;
            // 
            // trackBarVolume
            // 
            trackBarVolume.Location = new Point(20, 12);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Orientation = Orientation.Vertical;
            trackBarVolume.Size = new Size(56, 150);
            trackBarVolume.TabIndex = 18;
            trackBarVolume.TickFrequency = 10;
            trackBarVolume.Value = 20;
            trackBarVolume.Scroll += trackBarVolume_Scroll;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(133, 270);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(32, 32);
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.TabIndex = 18;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(247, 270);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(35, 32);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 19;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // lblVolume
            // 
            lblVolume.AutoSize = true;
            lblVolume.Location = new Point(20, 165);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(37, 20);
            lblVolume.TabIndex = 19;
            lblVolume.Text = "20%";
            // 
            // btnSavePlaylist
            // 
            btnSavePlaylist.Location = new Point(927, 410);
            btnSavePlaylist.Name = "btnSavePlaylist";
            btnSavePlaylist.Size = new Size(113, 28);
            btnSavePlaylist.TabIndex = 21;
            btnSavePlaylist.Text = "Guardar lista";
            btnSavePlaylist.UseVisualStyleBackColor = true;
            btnSavePlaylist.Click += btnSavePlaylist_Click;
            // 
            // btnLoadPlaylist
            // 
            btnLoadPlaylist.Location = new Point(1046, 410);
            btnLoadPlaylist.Name = "btnLoadPlaylist";
            btnLoadPlaylist.Size = new Size(95, 28);
            btnLoadPlaylist.TabIndex = 22;
            btnLoadPlaylist.Text = "Cargar lista";
            btnLoadPlaylist.UseVisualStyleBackColor = true;
            btnLoadPlaylist.Click += btnLoadPlaylist_Click;
            // 
            // btnAddToPlaylist
            // 
            btnAddToPlaylist.Location = new Point(859, 410);
            btnAddToPlaylist.Name = "btnAddToPlaylist";
            btnAddToPlaylist.Size = new Size(62, 28);
            btnAddToPlaylist.TabIndex = 20;
            btnAddToPlaylist.Text = "+ Lista";
            btnAddToPlaylist.UseVisualStyleBackColor = true;
            btnAddToPlaylist.Click += btnAddToPlaylist_Click;
            // 
            // lblPlaylistCount
            // 
            lblPlaylistCount.AutoSize = true;
            lblPlaylistCount.Location = new Point(387, 212);
            lblPlaylistCount.Name = "lblPlaylistCount";
            lblPlaylistCount.Size = new Size(71, 20);
            lblPlaylistCount.TabIndex = 23;
            lblPlaylistCount.Text = "En lista: 0";
            // 
            // btnClearList
            // 
            btnClearList.Location = new Point(481, 208);
            btnClearList.Name = "btnClearList";
            btnClearList.Size = new Size(95, 28);
            btnClearList.TabIndex = 24;
            btnClearList.Text = "Limpiar lista";
            btnClearList.UseVisualStyleBackColor = true;
            btnClearList.Click += btnClearList_Click;
            // 
            // btnOpenEqualizer
            // 
            btnOpenEqualizer.Location = new Point(519, 410);
            btnOpenEqualizer.Name = "btnOpenEqualizer";
            btnOpenEqualizer.Size = new Size(103, 28);
            btnOpenEqualizer.TabIndex = 35;
            btnOpenEqualizer.Text = "Ecualizador";
            btnOpenEqualizer.UseVisualStyleBackColor = true;
            btnOpenEqualizer.Click += btnOpenEqualizer_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(289, 270);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(51, 32);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 28;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(76, 270);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(51, 32);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 29;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(209, 270);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(32, 32);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 30;
            pictureBox7.TabStop = false;
            pictureBox7.Click += pictureBox7_Click;
            // 
            // pictureBox8
            // 
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(36, 270);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(50, 32);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 33;
            pictureBox8.TabStop = false;
            pictureBox8.Click += pictureBox8_Click;
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(346, 270);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(56, 32);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 34;
            pictureBox9.TabStop = false;
            pictureBox9.Click += pictureBox9_Click;
            // 
            // Form1
            // 
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1251, 450);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(btnClearList);
            Controls.Add(lblPlaylistCount);
            Controls.Add(btnLoadPlaylist);
            Controls.Add(btnSavePlaylist);
            Controls.Add(btnAddToPlaylist);
            Controls.Add(btnOpenEqualizer);
            Controls.Add(lblVolume);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(trackBarVolume);
            Controls.Add(trackBar1);
            Controls.Add(pictureBox1);
            Controls.Add(lblDuration);
            Controls.Add(lblNow);
            Controls.Add(pbCover);
            Controls.Add(btnOpenInWmp);
            Controls.Add(btnOpenFolder);
            Controls.Add(btnOpenFile);
            Controls.Add(lvTracks);
            MinimumSize = new Size(800, 480);
            Name = "Form1";
            Text = "Reproductor MP3";
            ((System.ComponentModel.ISupportInitialize)pbCover).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
    }
}
