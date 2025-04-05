# 🎤 Voice-Activated Recorder (.NET)

This is a .NET console app that automatically records from your microphone when sound is detected and stops when silent. It saves the output as timestamped MP3 files.

## 📦 Requirements

- .NET 6 or later
- NAudio
- NAudio.Lame

## 🔧 Setup

1. Clone the repo or copy the files.
2. Run the following:
   ```bash
   dotnet add package NAudio
   dotnet add package NAudio.Lame
   dotnet run
