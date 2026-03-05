using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;

namespace mp3
{
    public partial class Form1 : Form
    {
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;
        private readonly HttpClient http = new HttpClient();

        // Nuevo: clave de API de YouTube Data v3 (lee de variable de entorno o sustituye por tu clave)
        private readonly string youtubeApiKey;

        // tbDetails eliminado

        // Nuevo: indica si el usuario está arrastrando el trackbar (no sobreescribir mientras tanto)
        private bool isUserSeeking = false;

        // Diccionario para guardar portadas en alta resolución (índice -> imagen original)
        private readonly Dictionary<int, Image> highResCoverImages = new();

        // Índice de la pista actual seleccionada
        private int currentTrackIndex = -1;

        // Lista de canciones seleccionadas para guardar
        private readonly List<string> playlistToSave = new();
        private readonly Random random = new();
        private bool isRepeatTrackEnabled;
        private bool isShuffleEnabled;
        private int[] eqBandValues = new int[5];
        private GraphicEqualizerSampleProvider? equalizerProvider;
        private Image? playImage;
        private Image? pauseImage;

        public Form1()
        {
            InitializeComponent();
            lvTracks.SmallImageList = imageListCovers;
            lvTracks.View = View.Details;

            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            playImage = (Image?)resources.GetObject("pictureBox1.Image");
            pauseImage = CreatePauseImage(32, 32);
            pictureBox1.Image = playImage;

            // Cargar clave desde variable de entorno para no hardcodear en el binario.
            youtubeApiKey = "AIzaSyDPkcd73ZrTFCukHY5ic1iMu5pIgpsEPT8";

            pictureBox8.Paint += PictureBoxMode_Paint;
            pictureBox9.Paint += PictureBoxMode_Paint;
        }

        private async Task<Image?> FetchCoverAsync(string artist, string title, string album)
        {
            Image? cover = null;
            var queries = GenerateSearchQueries(artist, title, album);

            foreach (var query in queries)
            {
                if (string.IsNullOrWhiteSpace(query)) continue;

                // 1. Deezer (mejor calidad, más rápido)
                cover = await FetchCoverFromDeezerAsync(query);
                if (cover != null) return cover;

                // 2. iTunes (buena cobertura)
                cover = await FetchCoverFromiTunesAsync(query);
                if (cover != null) return cover;

                // 3. MusicBrainz + Cover Art Archive (más completo, especialmente para música menos comercial)
                cover = await FetchCoverFromMusicBrainzAsync(query);
                if (cover != null) return cover;

                // 4. Last.fm (fallback adicional)
                cover = await FetchCoverFromLastFmAsync(query);
                if (cover != null) return cover;
            }

            return null;
        }

        // Genera múltiples variantes de búsqueda para maximizar resultados
        private List<string> GenerateSearchQueries(string artist, string title, string album)
        {
            var queries = new List<string>();

            var cleanTitle = CleanSearchTerm(title);
            var cleanArtist = CleanSearchTerm(artist);
            var cleanAlbum = CleanSearchTerm(album);

            // Query completa: artista + título
            if (!string.IsNullOrWhiteSpace(cleanArtist) && !string.IsNullOrWhiteSpace(cleanTitle))
                queries.Add($"{cleanArtist} {cleanTitle}");

            // Solo título (limpio)
            if (!string.IsNullOrWhiteSpace(cleanTitle))
                queries.Add(cleanTitle);

            // Artista + álbum
            if (!string.IsNullOrWhiteSpace(cleanArtist) && !string.IsNullOrWhiteSpace(cleanAlbum))
                queries.Add($"{cleanArtist} {cleanAlbum}");

            // Solo álbum
            if (!string.IsNullOrWhiteSpace(cleanAlbum))
                queries.Add(cleanAlbum);

            // Extraer palabras clave del título (últimos 2-3 palabras significativas)
            var keywords = ExtractKeywords(cleanTitle);
            if (!string.IsNullOrWhiteSpace(keywords) && keywords != cleanTitle)
                queries.Add(keywords);

            return queries.Distinct().ToList();
        }

        // Extrae palabras clave significativas
        private string ExtractKeywords(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Where(w => w.Length > 2) // Ignorar palabras muy cortas
                            .ToArray();

            if (words.Length <= 3) return text;

            // Tomar las últimas 3 palabras (usualmente el nombre real de la canción)
            return string.Join(" ", words.TakeLast(3));
        }

        // Limpia términos de búsqueda eliminando caracteres problemáticos
        private string CleanSearchTerm(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return "";

            var cleaned = term;

            // Eliminar extensiones de archivo
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"\.(mp3|m4a|wav|flac|ogg|wma)$", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Eliminar paréntesis/corchetes y su contenido
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"\s*[\(\[][^\)\]]*[\)\]]", "");

            // Eliminar "feat.", "ft.", "featuring", etc.
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"\s*(feat\.?|ft\.?|featuring|prod\.?|produced by).*$", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Eliminar números al inicio (01, 02-, Track 1, etc.)
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"^(\d+[\.\-\s]*|track\s*\d+[\.\-\s]*)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Reemplazar guiones bajos y guiones múltiples por espacios
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"[_\-]+", " ");

            // Eliminar caracteres especiales excepto letras, números y espacios
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"[^\w\s\u00C0-\u017F]", " ");

            // Normalizar espacios múltiples
            cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, @"\s+", " ");

            return cleaned.Trim();
        }

        private async Task<string?> FetchYouTubeInfoAsync(string artist, string title)
        {
            try
            {


                var query = string.IsNullOrWhiteSpace(artist) ? title : $"{artist} {title}";
                var url = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q={Uri.EscapeDataString(query)}&type=video&maxResults=1&key={Uri.EscapeDataString(youtubeApiKey)}";

                using var resp = await http.GetAsync(url);
                if (!resp.IsSuccessStatusCode) return null;

                using var stream = await resp.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);
                var root = doc.RootElement;
                if (root.TryGetProperty("items", out var items) && items.GetArrayLength() > 0)
                {
                    var first = items[0];
                    var id = first.GetProperty("id").GetProperty("videoId").GetString();
                    var snippet = first.GetProperty("snippet");
                    var videoTitle = snippet.GetProperty("title").GetString();
                    var channel = snippet.GetProperty("channelTitle").GetString();
                    var description = snippet.TryGetProperty("description", out var desc) ? desc.GetString() ?? "" : "";
                    if (description.Length > 800) description = description.Substring(0, 800) + "…";

                    return $"YouTube: {videoTitle}{Environment.NewLine}Canal: {channel}{Environment.NewLine}Id: {id}{Environment.NewLine}Descripción: {description}";
                }
            }
            catch
            {
                // ignore
            }
            return null;
        }

        private async Task AddFileAsync(string path)
        {
            try
            {
                var tagFile = TagLib.File.Create(path);
                var title = string.IsNullOrEmpty(tagFile.Tag.Title) ? Path.GetFileNameWithoutExtension(path) : tagFile.Tag.Title;
                var album = tagFile.Tag.Album ?? "";
                var artist = tagFile.Tag.FirstPerformer ?? (tagFile.Tag.Performers?.FirstOrDefault() ?? "");
                var duration = tagFile.Properties.Duration;
                var durationText = duration.ToString(@"mm\:ss");

                int imageIndex = -1;
                Image? coverImage = null;

                if (tagFile.Tag.Pictures != null && tagFile.Tag.Pictures.Length > 0)
                {
                    var pic = tagFile.Tag.Pictures[0];
                    using var ms = new MemoryStream(pic.Data.Data);
                    coverImage = Image.FromStream(ms);
                }
                else
                {
                    // reemplazamos la llamada a iTunes por búsqueda en YouTube (si está configurada la API)
                    coverImage = await FetchCoverAsync(artist, title, album);
                }

                if (coverImage == null)
                {
                    // placeholder
                    coverImage = new Bitmap(64, 64);
                    using (var g = Graphics.FromImage(coverImage))
                    {
                        g.Clear(Color.DarkGray);
                    }
                }

                // Guardar la imagen en alta resolución para el PictureBox
                int nextIndex = imageListCovers.Images.Count;
                highResCoverImages[nextIndex] = (Image)coverImage.Clone();

                // Crear miniatura de 64x64 para el ImageList del ListView
                var thumbnail = new Bitmap(coverImage, new Size(64, 64));
                imageListCovers.Images.Add(thumbnail);
                imageIndex = imageListCovers.Images.Count - 1;

                var lvi = new ListViewItem(title) { ImageIndex = imageIndex };
                lvi.SubItems.Add(album);
                lvi.SubItems.Add(artist);
                lvi.SubItems.Add(durationText);
                lvi.SubItems.Add(path); // oculto
                lvTracks.Items.Add(lvi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error leyendo {path}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnOpenFile_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Audio files|*.mp3;*.m4a;*.wav;*.flac|All files|*.*",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    await AddFileAsync(file);
                }
            }
        }

        private async void btnOpenFolder_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var files = Directory.EnumerateFiles(fbd.SelectedPath, "*.mp3", SearchOption.TopDirectoryOnly);
                foreach (var f in files)
                {
                    await AddFileAsync(f);
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (lvTracks.SelectedItems.Count == 0)
            {
                if (lvTracks.Items.Count > 0)
                {
                    lvTracks.Items[0].Selected = true;
                }
                else return;
            }

            currentTrackIndex = lvTracks.SelectedItems[0].Index;
            var path = lvTracks.SelectedItems[0].SubItems[4].Text;
            PlayFile(path);
        }

        // Modificar PlayFile para aplicar el volumen inicial:
        private void PlayFile(string path)
        {
            try
            {
                // Detener el timer primero para evitar conflictos
                playbackTimer.Stop();

                // Limpiar dispositivo anterior sin resetear UI aún
                try
                {
                    if (outputDevice != null)
                    {
                        outputDevice.PlaybackStopped -= OutputDevice_PlaybackStopped;
                        outputDevice.Stop();
                        outputDevice.Dispose();
                        outputDevice = null;
                    }
                    if (audioFile != null)
                    {
                        audioFile.Dispose();
                        audioFile = null;
                    }
            equalizerProvider = null;
                }
                catch { }

                audioFile = new AudioFileReader(path);
                equalizerProvider = new GraphicEqualizerSampleProvider(audioFile);
                equalizerProvider.SetGains(eqBandValues.Select(v => (float)v).ToArray());
                outputDevice = new WaveOutEvent();
                outputDevice.Init(equalizerProvider);
                outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;

                // Aplicar volumen actual
                outputDevice.Volume = trackBarVolume.Value / 100f;

                // Configurar trackbar ANTES de reproducir
                var totalSeconds = (int)audioFile.TotalTime.TotalSeconds;
                if (totalSeconds < 1) totalSeconds = 1;

                trackBar1.Minimum = 0;
                trackBar1.Maximum = totalSeconds;
                trackBar1.Value = 0;
                trackBar1.Enabled = true;

                // Sincronizar etiquetas de tiempo
                lblNow.Text = "00:00";
                lblDuration.Text = audioFile.TotalTime.ToString(@"mm\:ss");

                // Actualizar portada y datos de la canción actual
                UpdateCurrentTrackInfo();

                // Iniciar reproducción
                outputDevice.Play();
                pictureBox1.Image = pauseImage;
                playbackTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se puede reproducir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopPlayback();
            }
        }

        // Nuevo método para actualizar la información de la pista actual
        private void UpdateCurrentTrackInfo()
        {
            if (currentTrackIndex < 0 || currentTrackIndex >= lvTracks.Items.Count)
                return;

            var item = lvTracks.Items[currentTrackIndex];

            // Actualizar portada
            try
            {
                if (item.ImageIndex >= 0 && highResCoverImages.TryGetValue(item.ImageIndex, out var hiRes))
                {
                    pbCover.Image = hiRes;
                }
                else if (item.ImageIndex >= 0 && item.ImageIndex < imageListCovers.Images.Count)
                {
                    pbCover.Image = imageListCovers.Images[item.ImageIndex];
                }
                else
                {
                    pbCover.Image = null;
                }
            }
            catch
            {
                pbCover.Image = null;
            }

        }

        private void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            // Usar BeginInvoke para operaciones de UI desde otro hilo
            BeginInvoke(() =>
            {
                playbackTimer.Stop();

                // Solo resetear si no se está cambiando de canción (audioFile sería null si StopPlayback fue llamado)
                if (audioFile != null)
                {
                    audioFile.Position = 0;
                    lblNow.Text = "00:00";
                    pictureBox1.Image = playImage;
                    try
                    {
                        trackBar1.Value = 0;
                    }
                    catch { }
                    if (isRepeatTrackEnabled)
                    {
                        PlayCurrentTrack();
                    }
                }
            });
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (outputDevice == null) return;
            if (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
            }
            else if (outputDevice.PlaybackState == PlaybackState.Paused)
            {
                outputDevice.Play();
            }
        }



        private void StopPlayback()
        {
            playbackTimer.Stop();
            try
            {
                if (outputDevice != null)
                {
                    outputDevice.PlaybackStopped -= OutputDevice_PlaybackStopped;
                    outputDevice.Stop();
                    outputDevice.Dispose();
                    outputDevice = null;
                }
                if (audioFile != null)
                {
                    audioFile.Dispose();
                    audioFile = null;
                }
            }
            catch { }

            // Reiniciar UI
            lblNow.Text = "00:00";
            lblDuration.Text = "00:00";
            pictureBox1.Image = playImage;

            try
            {
                trackBar1.Value = 0;
                trackBar1.Maximum = 1; // Evitar Maximum = 0
                trackBar1.Enabled = false;
            }
            catch { }
        }

        private void playbackTimer_Tick(object? sender, EventArgs e)
        {
            if (audioFile == null || outputDevice == null) return;
            if (outputDevice.PlaybackState != PlaybackState.Playing) return;

            var pos = audioFile.CurrentTime;
            lblNow.Text = pos.ToString(@"mm\:ss");

            // Actualizar trackbar solo si el usuario no está buscando
            if (!isUserSeeking)
            {
                try
                {
                    var seconds = (int)pos.TotalSeconds;
                    if (seconds >= trackBar1.Minimum && seconds <= trackBar1.Maximum)
                    {
                        trackBar1.Value = seconds;
                    }
                }
                catch
                {
                    // Ignorar excepciones del trackbar
                }
            }
        }

        // Evento: muestra vista previa mientras arrastra
        private void trackBar1_Scroll(object? sender, EventArgs e)
        {
            try
            {
                var seconds = trackBar1.Value;
                lblNow.Text = TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
            }
            catch { }
        }

        // Usuario inicia el seek: evitar actualizaciones automáticas
        private void trackBar1_MouseDown(object? sender, MouseEventArgs e)
        {
            isUserSeeking = true;
        }

        // Usuario suelta el seek: aplicar posición al audio
        private void trackBar1_MouseUp(object? sender, MouseEventArgs e)
        {
            try
            {
                var seconds = trackBar1.Value;
                if (audioFile != null)
                {
                    var newPos = TimeSpan.FromSeconds(seconds);
                    if (newPos < audioFile.TotalTime)
                    {
                        audioFile.CurrentTime = newPos;
                    }
                    else
                    {
                        audioFile.CurrentTime = audioFile.TotalTime;
                    }
                }
            }
            catch
            {
                // ignore seek errors
            }
            finally
            {
                isUserSeeking = false;
            }
        }

        private void SeekBySeconds(int offsetSeconds)
        {
            if (audioFile == null)
            {
                return;
            }

            var target = audioFile.CurrentTime + TimeSpan.FromSeconds(offsetSeconds);
            if (target < TimeSpan.Zero)
            {
                target = TimeSpan.Zero;
            }
            else if (target > audioFile.TotalTime)
            {
                target = audioFile.TotalTime;
            }

            audioFile.CurrentTime = target;

            lblNow.Text = target.ToString(@"mm\:ss");

            if (!isUserSeeking)
            {
                try
                {
                    var seconds = (int)target.TotalSeconds;
                    if (seconds < trackBar1.Minimum) seconds = trackBar1.Minimum;
                    if (seconds > trackBar1.Maximum) seconds = trackBar1.Maximum;
                    trackBar1.Value = seconds;
                }
                catch
                {
                    // ignore trackbar update errors
                }
            }
        }

        private void UpdateEqualizerGain(int bandIndex, int value)
        {
            if (bandIndex < 0 || bandIndex >= eqBandValues.Length)
            {
                return;
            }

            eqBandValues[bandIndex] = value;
            equalizerProvider?.SetGain(bandIndex, value);
        }

        private void btnAddToPlaylist_Click(object sender, EventArgs e)
        {
            if (lvTracks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecciona una canción para añadir a la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var path = lvTracks.SelectedItems[0].SubItems[4].Text;
            var title = lvTracks.SelectedItems[0].SubItems[0].Text;

            // Evitar duplicados
            if (playlistToSave.Contains(path))
            {
                MessageBox.Show($"'{title}' ya está en la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            playlistToSave.Add(path);
            lblPlaylistCount.Text = $"En lista: {playlistToSave.Count}";

            // Feedback visual
            MessageBox.Show($"'{title}' añadida a la lista.\nTotal: {playlistToSave.Count} canciones.", "Añadida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSavePlaylist_Click(object sender, EventArgs e)
        {
            if (playlistToSave.Count == 0)
            {
                MessageBox.Show("No hay canciones en la lista para guardar.\nUsa el botón '+ Lista' para añadir canciones.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Lista de reproducción|*.txt",
                DefaultExt = "txt",
                FileName = "MiPlaylist"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllLines(sfd.FileName, playlistToSave);
                    MessageBox.Show($"Lista guardada con {playlistToSave.Count} canciones.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar la lista después de guardar
                    playlistToSave.Clear();
                    lblPlaylistCount.Text = "En lista: 0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error guardando: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnLoadPlaylist_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Lista de reproducción|*.txt|Todos los archivos|*.*",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Detener reproducción actual
                    StopPlayback();

                    var lines = await System.IO.File.ReadAllLinesAsync(ofd.FileName);

                    // Limpiar lista actual
                    lvTracks.Items.Clear();
                    imageListCovers.Images.Clear();
                    highResCoverImages.Clear();
                    currentTrackIndex = -1;
                    pbCover.Image = null;

                    // Cargar archivos de la lista
                    foreach (var l in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(l) && System.IO.File.Exists(l))
                        {
                            await AddFileAsync(l);
                        }
                    }

                    // Seleccionar primera pista si existe
                    if (lvTracks.Items.Count > 0)
                    {
                        lvTracks.Items[0].Selected = true;
                        currentTrackIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cargando lista: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOpenInWmp_Click(object sender, EventArgs e)
        {
            if (lvTracks.SelectedItems.Count == 0) return;
            var path = lvTracks.SelectedItems[0].SubItems[4].Text;
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "wmplayer.exe",
                    Arguments = $"\"{path}\"",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir en Windows Media Player: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOpenEqualizer_Click(object sender, EventArgs e)
        {
            using var eq = new EqualizerForm();
            eq.GainChanged = UpdateEqualizerGain;
            eq.SetValues(eqBandValues);
            eq.ShowDialog(this);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopPlayback();
            http.Dispose();
            base.OnFormClosing(e);
        }

        private void volumeMeter1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
                pictureBox1.Image = playImage;
                return;
            }

            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Paused)
            {
                outputDevice.Play();
                pictureBox1.Image = pauseImage;
                return;
            }

            // No hay reproducción activa: iniciar la pista seleccionada o la primera
            if (lvTracks.SelectedItems.Count == 0)
            {
                if (lvTracks.Items.Count > 0)
                    lvTracks.Items[0].Selected = true;
                else
                    return;
            }

            currentTrackIndex = lvTracks.SelectedItems[0].Index;
            var path = lvTracks.SelectedItems[0].SubItems[4].Text;
            PlayFile(path);
        }

        private void pbCover_Click(object sender, EventArgs e)
        {

        }

        /// Draws two vertical bars (pause icon) on a transparent bitmap.
        private static Bitmap CreatePauseImage(int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var brush = new SolidBrush(Color.FromArgb(30, 30, 30));
            int barW = width / 5;
            int barH = (int)(height * 0.6);
            int top = (height - barH) / 2;
            int left1 = (int)(width * 0.22);
            int left2 = (int)(width * 0.55);

            g.FillRectangle(brush, left1, top, barW, barH);
            g.FillRectangle(brush, left2, top, barW, barH);

            return bmp;
        }

        private void PictureBoxMode_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not PictureBox pb)
            {
                return;
            }

            bool active = (pb == pictureBox8 && isRepeatTrackEnabled)
                       || (pb == pictureBox9 && isShuffleEnabled);

            if (!active)
            {
                return;
            }

            using var pen = new Pen(Color.DodgerBlue, 2);
            var rect = new Rectangle(1, 1, pb.Width - 3, pb.Height - 3);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int radius = 5;
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.DrawPath(pen, path);
        }

        // pictureBox2 eliminated; play/pause merged into pictureBox1_Click

        



        private void PlayCurrentTrack()
        {
            if (currentTrackIndex < 0 || currentTrackIndex >= lvTracks.Items.Count)
            {
                return;
            }

            var item = lvTracks.Items[currentTrackIndex];
            item.Selected = true;
            lvTracks.Select();
            var path = item.SubItems[4].Text;
            PlayFile(path);
        }

        private void AdvanceTrack(bool forward)
        {
            if (lvTracks.Items.Count == 0)
            {
                return;
            }

            if (isRepeatTrackEnabled)
            {
                if (currentTrackIndex < 0 || currentTrackIndex >= lvTracks.Items.Count)
                {
                    currentTrackIndex = 0;
                }

                PlayCurrentTrack();
                return;
            }

            if (isShuffleEnabled)
            {
                if (lvTracks.Items.Count == 1)
                {
                    currentTrackIndex = 0;
                }
                else
                {
                    var next = currentTrackIndex;
                    do
                    {
                        next = random.Next(lvTracks.Items.Count);
                    } while (next == currentTrackIndex);

                    currentTrackIndex = next;
                }

                PlayCurrentTrack();
                return;
            }

            if (forward)
            {
                if (currentTrackIndex < 0 || currentTrackIndex >= lvTracks.Items.Count - 1)
                {
                    currentTrackIndex = 0;
                }
                else
                {
                    currentTrackIndex++;
                }
            }
            else
            {
                if (currentTrackIndex <= 0)
                {
                    currentTrackIndex = lvTracks.Items.Count - 1;
                }
                else
                {
                    currentTrackIndex--;
                }
            }

            PlayCurrentTrack();
        }

        private async void lvTracks_SelectedIndexChanged_1(object? sender, EventArgs e)
        {
            if (lvTracks.SelectedItems.Count == 0)
            {
                pbCover.Image = null;
                return;
            }

            var item = lvTracks.SelectedItems[0];
            currentTrackIndex = item.Index;

            // Actualizar portada inmediatamente
            try
            {
                if (item.ImageIndex >= 0 && highResCoverImages.TryGetValue(item.ImageIndex, out var hiRes))
                {
                    pbCover.Image = hiRes;
                }
                else if (item.ImageIndex >= 0 && item.ImageIndex < imageListCovers.Images.Count)
                {
                    pbCover.Image = imageListCovers.Images[item.ImageIndex];
                }
                else
                {
                    pbCover.Image = null;
                }
            }
            catch
            {
                pbCover.Image = null;
            }
        }

        // NUEVOS manejadores para anterior / siguiente
        private void btnPrev_Click(object sender, EventArgs e)
        {
            AdvanceTrack(forward: false);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            AdvanceTrack(forward: true);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AdvanceTrack(forward: true);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AdvanceTrack(forward: false);
        }



        // Añadir este método en Form1.cs (junto a los otros manejadores de eventos):
        private void trackBarVolume_Scroll(object? sender, EventArgs e)
        {
            lblVolume.Text = $"{trackBarVolume.Value}%";
            if (outputDevice != null)
            {
                // El volumen de WaveOutEvent va de 0.0 a 1.0
                outputDevice.Volume = trackBarVolume.Value / 100f;
            }
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            if (lvTracks.Items.Count == 0)
            {
                MessageBox.Show("La lista ya está vacía.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"¿Estás seguro de que quieres limpiar la lista?\nSe eliminarán {lvTracks.Items.Count} canciones.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Detener reproducción actual
                StopPlayback();

                // Limpiar todo
                lvTracks.Items.Clear();
                imageListCovers.Images.Clear();
                highResCoverImages.Clear();
                playlistToSave.Clear();

                // Resetear índice y UI
                currentTrackIndex = -1;
                pbCover.Image = null;
                lblPlaylistCount.Text = "En lista: 0";
            }
        }

        private async Task<Image?> FetchCoverFromDeezerAsync(string query)
        {
            try
            {
                var url = $"https://api.deezer.com/search?q={Uri.EscapeDataString(query)}&limit=3";

                using var resp = await http.GetAsync(url);
                if (!resp.IsSuccessStatusCode) return null;

                using var stream = await resp.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);
                var root = doc.RootElement;

                if (root.TryGetProperty("data", out var data) && data.GetArrayLength() > 0)
                {
                    // Intentar con los primeros resultados
                    foreach (var item in data.EnumerateArray())
                    {
                        string? coverUrl = null;
                        if (item.TryGetProperty("album", out var albumObj))
                        {
                            if (albumObj.TryGetProperty("cover_xl", out var xl)) coverUrl = xl.GetString();
                            if (string.IsNullOrEmpty(coverUrl) && albumObj.TryGetProperty("cover_big", out var big)) coverUrl = big.GetString();
                            if (string.IsNullOrEmpty(coverUrl) && albumObj.TryGetProperty("cover_medium", out var med)) coverUrl = med.GetString();
                        }

                        if (!string.IsNullOrEmpty(coverUrl))
                        {
                            var bytes = await http.GetByteArrayAsync(coverUrl);
                            using var ms = new MemoryStream(bytes);
                            return Image.FromStream(ms);
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private async Task<Image?> FetchCoverFromiTunesAsync(string query)
        {
            try
            {
                var url = $"https://itunes.apple.com/search?term={Uri.EscapeDataString(query)}&media=music&limit=3";

                using var resp = await http.GetAsync(url);
                if (!resp.IsSuccessStatusCode) return null;

                using var stream = await resp.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);
                var root = doc.RootElement;

                if (root.TryGetProperty("results", out var results) && results.GetArrayLength() > 0)
                {
                    foreach (var item in results.EnumerateArray())
                    {
                        if (item.TryGetProperty("artworkUrl100", out var artwork))
                        {
                            var artworkUrl = artwork.GetString()?.Replace("100x100", "600x600");
                            if (!string.IsNullOrEmpty(artworkUrl))
                            {
                                var bytes = await http.GetByteArrayAsync(artworkUrl);
                                using var ms = new MemoryStream(bytes);
                                return Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private async Task<Image?> FetchCoverFromMusicBrainzAsync(string query)
        {
            try
            {
                // Configurar User-Agent requerido por MusicBrainz
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://musicbrainz.org/ws/2/recording?query={Uri.EscapeDataString(query)}&limit=1&fmt=json");
                request.Headers.Add("User-Agent", "MP3Player/1.0 (contacto@ejemplo.com)");

                using var resp = await http.SendAsync(request);
                if (!resp.IsSuccessStatusCode) return null;

                using var stream = await resp.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);
                var root = doc.RootElement;

                if (root.TryGetProperty("recordings", out var recordings) && recordings.GetArrayLength() > 0)
                {
                    var first = recordings[0];
                    if (first.TryGetProperty("releases", out var releases) && releases.GetArrayLength() > 0)
                    {
                        var release = releases[0];
                        if (release.TryGetProperty("id", out var releaseId))
                        {
                            var mbid = releaseId.GetString();
                            // Obtener portada de Cover Art Archive
                            var coverUrl = $"https://coverartarchive.org/release/{mbid}/front-500";

                            try
                            {
                                var bytes = await http.GetByteArrayAsync(coverUrl);
                                using var ms = new MemoryStream(bytes);
                                return Image.FromStream(ms);
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private async Task<Image?> FetchCoverFromLastFmAsync(string query)
        {
            try
            {
                // Last.fm API pública (sin key para búsquedas básicas usando scraping de imagen)
                var url = $"https://www.last.fm/music/{Uri.EscapeDataString(query.Replace(" ", "+"))}";

                // Intentar buscar por track
                var searchUrl = $"https://ws.audioscrobbler.com/2.0/?method=track.search&track={Uri.EscapeDataString(query)}&api_key=57ee3318536b23ee81d6b27e36997cde&format=json&limit=1";

                using var resp = await http.GetAsync(searchUrl);
                if (!resp.IsSuccessStatusCode) return null;

                using var stream = await resp.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);
                var root = doc.RootElement;

                if (root.TryGetProperty("results", out var results) &&
                    results.TryGetProperty("trackmatches", out var matches) &&
                    matches.TryGetProperty("track", out var tracks) &&
                    tracks.GetArrayLength() > 0)
                {
                    var track = tracks[0];
                    if (track.TryGetProperty("image", out var images) && images.GetArrayLength() > 0)
                    {
                        // Buscar imagen extralarge o large
                        foreach (var img in images.EnumerateArray())
                        {
                            if (img.TryGetProperty("size", out var size) &&
                                (size.GetString() == "extralarge" || size.GetString() == "large"))
                            {
                                if (img.TryGetProperty("#text", out var imgUrl))
                                {
                                    var imageUrl = imgUrl.GetString();
                                    if (!string.IsNullOrEmpty(imageUrl) && !imageUrl.Contains("2a96cbd8b46e442fc41c2b86b821562f"))
                                    {
                                        var bytes = await http.GetByteArrayAsync(imageUrl);
                                        using var ms = new MemoryStream(bytes);
                                        return Image.FromStream(ms);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SeekBySeconds(10);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SeekBySeconds(-10);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            isShuffleEnabled = !isShuffleEnabled;
            if (isShuffleEnabled)
            {
                isRepeatTrackEnabled = false;
            }

            pictureBox8.Invalidate();
            pictureBox9.Invalidate();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            isRepeatTrackEnabled = !isRepeatTrackEnabled;
            if (isRepeatTrackEnabled)
            {
                isShuffleEnabled = false;
            }

            pictureBox8.Invalidate();
            pictureBox9.Invalidate();
        }
    }
}
