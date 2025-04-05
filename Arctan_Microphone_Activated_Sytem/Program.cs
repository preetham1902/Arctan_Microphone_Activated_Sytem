using Microsoft.VisualBasic;
using NAudio.Wave;
using System;
using System.IO;
using System.Timers;

class Program
{
    static WaveInEvent waveIn;
    static WaveFileWriter writer;
    static string outputFolder = "Recordings";
    static float silenceThreshold = 0.02f;
    static int silenceTimeoutMs = 2000;
    static DateTime lastSoundDetected = DateTime.MinValue;
    static System.Timers.Timer checkSilenceTimer;

    static void Main(string[] args)
    {
        Directory.CreateDirectory(outputFolder);
        waveIn = new WaveInEvent
        {
            WaveFormat = new WaveFormat(44100, 1) // 44.1kHz mono
        };

        waveIn.DataAvailable += OnDataAvailable;
        waveIn.StartRecording();

        checkSilenceTimer = new System.Timers.Timer(500);
        checkSilenceTimer.Elapsed += CheckForSilence;
        checkSilenceTimer.Start();

        Console.WriteLine("Listening for microphone input... Press Enter to exit.");
        Console.ReadLine();

        StopRecording();
        waveIn.StopRecording();
        waveIn.Dispose();
    }

    static void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        bool soundDetected = false;

        for (int index = 0; index < e.BytesRecorded; index += 2)
        {
            short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index]);
            float volume = Math.Abs(sample / 32768f);
            if (volume > silenceThreshold)
            {
                soundDetected = true;
                break;
            }
        }

        if (soundDetected)
        {
            if (writer == null)
            {
                string fileName = Path.Combine(outputFolder, $"Recording_{DateTime.Now:yyyyMMdd_HHmmss}.wav");
                writer = new WaveFileWriter(fileName, waveIn.WaveFormat);
                Console.WriteLine($"Started recording: {fileName}");
            }

            writer?.Write(e.Buffer, 0, e.BytesRecorded);
            lastSoundDetected = DateTime.Now;
        }
        else if (writer != null)
        {
            writer?.Write(e.Buffer, 0, e.BytesRecorded); // include quiet time before stopping
        }
    }

    static void CheckForSilence(object sender, ElapsedEventArgs e)
    {
        if (writer != null && (DateTime.Now - lastSoundDetected).TotalMilliseconds > silenceTimeoutMs)
        {
            StopRecording();
        }
    }

    static void StopRecording()
    {
        if (writer != null)
        {
            writer.Dispose();
            writer = null;
            Console.WriteLine("Stopped recording.");
        }
    }
}
