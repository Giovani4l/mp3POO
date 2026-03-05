using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace mp3
{
    public partial class EqualizerForm : Form
    {
        private readonly Dictionary<string, int[]> presets = new()
        {
            { "Flat", new[] { 0, 0, 0, 0, 0 } },
            { "Bass Boost", new[] { 6, 4, 0, -2, -4 } },
            { "Vocal", new[] { -2, 0, 4, 3, 0 } },
            { "Rock", new[] { 5, 3, 1, 2, 4 } },
            { "Classical", new[] { -1, 1, 2, 3, 4 } },
            { "Jazz", new[] { 3, 1, 0, 2, 1 } }
        };

        private readonly Dictionary<TrackBar, SliderMetadata> sliderMetadata = new();
        private bool suppressGainEvents;

        public Action<int, int>? GainChanged { get; set; }

        public EqualizerForm()
        {
            InitializeComponent();
            ConfigureSliders();
            InitializePresetControl();
            ApplyValuesInternal(presets["Flat"]);
        }

        private void InitializePresetControl()
        {
            presetComboBox.Items.AddRange(presets.Keys.ToArray());
            presetComboBox.SelectedIndex = 0;
            presetComboBox.SelectedIndexChanged += (_, _) =>
            {
                if (presetComboBox.SelectedItem is string key && presets.TryGetValue(key, out var values))
                {
                    ApplyValuesInternal(values);
                }
            };
        }

        public void SetValues(int[] values)
        {
            if (values == null)
            {
                return;
            }

            ApplyValuesInternal(values);
            if (presetComboBox.SelectedIndex >= 0)
            {
                presetComboBox.SelectedIndex = -1;
            }
        }

        private void ConfigureSliders()
        {
            ConfigureSlider(sliderLow, lblLowValue, 0);
            ConfigureSlider(sliderMidLow, lblMidLowValue, 1);
            ConfigureSlider(sliderMid, lblMidValue, 2);
            ConfigureSlider(sliderMidHigh, lblMidHighValue, 3);
            ConfigureSlider(sliderHigh, lblHighValue, 4);
        }

        private void ConfigureSlider(TrackBar slider, Label label, int bandIndex)
        {
            sliderMetadata[slider] = new SliderMetadata(label, bandIndex);
        }

        private void ApplyValuesInternal(int[] values)
        {
            suppressGainEvents = true;
            var sliders = new[] { sliderLow, sliderMidLow, sliderMid, sliderMidHigh, sliderHigh };
            for (var i = 0; i < sliders.Length && i < values.Length; i++)
            {
                sliders[i].Value = Math.Clamp(values[i], sliders[i].Minimum, sliders[i].Maximum);
                if (sliderMetadata.TryGetValue(sliders[i], out var metadata))
                {
                    metadata.Label.Text = FormatDecibels(sliders[i].Value);
                }
            }

            suppressGainEvents = false;
            NotifyGainChanged();
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            if (sender is TrackBar slider)
            {
                if (!sliderMetadata.TryGetValue(slider, out var metadata))
                {
                    return;
                }

                metadata.Label.Text = FormatDecibels(slider.Value);

                if (!suppressGainEvents)
                {
                    GainChanged?.Invoke(metadata.BandIndex, slider.Value);
                }
            }
        }

        private void NotifyGainChanged()
        {
            foreach (var kvp in sliderMetadata)
            {
                GainChanged?.Invoke(kvp.Value.BandIndex, kvp.Key.Value);
            }
        }

        private static string FormatDecibels(int value)
            => value switch
            {
                > 0 => $"+{value} dB",
                < 0 => $"{value} dB",
                _ => "0 dB"
            };

        private sealed record SliderMetadata(Label Label, int BandIndex);
    }
}
