using BattleshipWinforms.Properties;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipWinforms.Backend.Audio
{
    internal static class AudioHandler
    {
        public static void PlaySound(SoundType type)
        {
            UnmanagedMemoryStream audioStream;
            switch (type)
            {
                case SoundType.Hit:
                    audioStream = Resources.hitAudio;
                    break;
                case SoundType.Miss:
                    audioStream = Resources.missAudio;
                    break;
                case SoundType.Sunk:
                    audioStream = Resources.sunkAudio;
                    break;
                case SoundType.Victory:
                    audioStream = Resources.victoryAudio;
                    break;
                default:
                    return;
            }
            try
            {
                Task.Run(() =>
                {
                    MemoryStream copy = new MemoryStream();
                    audioStream.CopyTo(copy);
                    copy.Position = 0;

                    WaveFileReader reader = new WaveFileReader(copy);
                    WaveOutEvent output = new WaveOutEvent();
                    output.Init(reader);
                    output.Play();

                    output.PlaybackStopped += (s, e) =>
                    {
                        output.Dispose();
                        reader.Dispose();
                        copy.Dispose();
                    };
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
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
