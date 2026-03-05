using System;
using NAudio.Dsp;
using NAudio.Wave;

namespace mp3
{
    internal class GraphicEqualizerSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        private readonly BiQuadFilter[,] filters;
        private readonly float[] bandGains;
        private readonly double[] centerFrequencies = { 60, 250, 1000, 4000, 16000 };
        private readonly double[] bandwidths = { 1.0, 1.0, 1.0, 1.0, 1.0 };

        public GraphicEqualizerSampleProvider(ISampleProvider source)
        {
            this.source = source;
            var channels = source.WaveFormat.Channels;
            bandGains = new float[centerFrequencies.Length];
            filters = new BiQuadFilter[channels, centerFrequencies.Length];
            CreateFilters();
        }

        public WaveFormat WaveFormat => source.WaveFormat;

        public void SetGain(int bandIndex, float gain)
        {
            if (bandIndex < 0 || bandIndex >= centerFrequencies.Length)
            {
                return;
            }

            bandGains[bandIndex] = gain;
            UpdateFilters(bandIndex);
        }

        public void SetGains(float[] gains)
        {
            if (gains == null) return;

            var length = Math.Min(gains.Length, bandGains.Length);
            for (var i = 0; i < length; i++)
            {
                bandGains[i] = gains[i];
                UpdateFilters(i);
            }
        }

        private void CreateFilters()
        {
            for (var channel = 0; channel < source.WaveFormat.Channels; channel++)
            {
                for (var band = 0; band < centerFrequencies.Length; band++)
                {
                    filters[channel, band] = CreateFilter(channel, band);
                }
            }
        }

        private void UpdateFilters(int bandIndex)
        {
            for (var channel = 0; channel < source.WaveFormat.Channels; channel++)
            {
                filters[channel, bandIndex] = CreateFilter(channel, bandIndex);
            }
        }

        private BiQuadFilter CreateFilter(int channel, int bandIndex)
        {
            return BiQuadFilter.PeakingEQ(
                source.WaveFormat.SampleRate,
                (float)centerFrequencies[bandIndex],
                (float)bandwidths[bandIndex],
                bandGains[bandIndex]);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samplesRead = source.Read(buffer, offset, count);
            if (samplesRead == 0)
            {
                return 0;
            }

            var channels = source.WaveFormat.Channels;
            for (var sample = offset; sample < offset + samplesRead; sample += channels)
            {
                for (var channel = 0; channel < channels; channel++)
                {
                    var index = sample + channel;
                    var value = buffer[index];
                    for (var band = 0; band < centerFrequencies.Length; band++)
                    {
                        value = filters[channel, band].Transform(value);
                    }

                    buffer[index] = value;
                }
            }

            return samplesRead;
        }
    }
}
