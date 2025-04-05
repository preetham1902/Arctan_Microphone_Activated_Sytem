# Arctan_Microphone_Activated_Sytem


# 🎤 Voice-Activated Recorder (.NET)

This is a .NET console app that automatically records from your microphone when sound is detected and stops when silent. It saves the output as timestamped MP3 files.

## 📦 Requirements

- .NET 6 or later
- NAudio
- NAudio.Lame

## 🔧 Setup

1. Clone the repo or extract the ZIP.
2. Run the following:
   ```bash
   dotnet add package NAudio
   dotnet add package NAudio.Lame
   dotnet run
   ```

## 🎯 Features

- Voice-activated recording (starts/stops based on volume)
- Records in MP3 format
- Saves to `/Recordings` with timestamped names

## 📁 Output Example

```
/Recordings/
  ├── Recording_20250405_151012.mp3
  ├── Recording_20250405_151139.mp3
```

## ⚠️ Notes

- Make sure your input device is set correctly in your OS.
- Silence timeout is set to 2 seconds (change in `silenceTimeoutMs`).
