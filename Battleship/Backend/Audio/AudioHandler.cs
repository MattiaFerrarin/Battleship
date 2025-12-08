using Battleship.Properties;
using NAudio.Mixer;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship.Backend.Audio
{
    internal static class AudioHandler
    {
        private static readonly MixingSampleProvider Mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2)) { ReadFully = true };
        private static readonly WaveOutEvent Output = new WaveOutEvent();
        private static readonly Dictionary<SoundType, Func<byte[]>> SoundBytes = new Dictionary<SoundType, Func<byte[]>>()
        {
            { SoundType.Hit, () => ReadAllBytes(Resources.hitAudio) },
            { SoundType.Miss, () => ReadAllBytes(Resources.missAudio) },
            { SoundType.Sunk, () => ReadAllBytes(Resources.sunkAudio) },
            { SoundType.Victory, () => ReadAllBytes(Resources.victoryAudio) }
        };

        static AudioHandler()
        {
            Output.Init(Mixer);
            Output.Play();
        }

        public static void PlaySound(SoundType type)
        {
            if (!SoundBytes.ContainsKey(type))
                return;
            byte[] audioStream = SoundBytes[type]();

            try
            {
                var ms = new MemoryStream(audioStream, false);
                var reader = new WaveFileReader(ms);

                ISampleProvider provider = reader.ToSampleProvider();

                if (provider.WaveFormat.SampleRate != Mixer.WaveFormat.SampleRate)
                {
                    provider = new WdlResamplingSampleProvider(provider, Mixer.WaveFormat.SampleRate);
                }
                if (provider.WaveFormat.Channels == 1)
                {
                    provider = new MonoToStereoSampleProvider(provider);
                }

                Mixer.AddMixerInput(provider);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
            }
        }
        private static byte[] ReadAllBytes(UnmanagedMemoryStream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
    public enum SoundType
    {
        Hit,
        Miss,
        Sunk,
        Victory
    }
}
