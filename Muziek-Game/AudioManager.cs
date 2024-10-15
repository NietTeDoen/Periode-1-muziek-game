using System.Windows.Media;
using System;

public class AudioManager
{

    private static MediaPlayer _mediaPlayer;
    public AudioManager()
    {
        if (_mediaPlayer == null)
        {
            _mediaPlayer = new MediaPlayer();
        }
    }
    public void PlayMedia(string filepath)
    {
        _mediaPlayer.Open(new Uri(filepath));
        _mediaPlayer.Play();
    }

    public void StopMedia()
    {
        _mediaPlayer.Stop();
    }

    public void PauseMedia()
    {
        _mediaPlayer.Pause();
    }

    public void LoadAbba()
    {
        string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "muziek", "ABBA.mp3");
        PlayMedia(musicPath);
    }

    // Zorg ervoor dat er slechts één MediaPlayer is
    public static MediaPlayer GetMediaPlayer()
    {
        if (_mediaPlayer == null)
        {
            _mediaPlayer = new MediaPlayer();
        }

        // Gebruik een reguliere bestands-URI
        string musicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "muziek", "ABBA.mp3");
        Uri musicUri = new Uri(musicPath);

        _mediaPlayer.Volume = 0.5; // Maximaal volume
        _mediaPlayer.Open(musicUri);
        _mediaPlayer.Play();
        
        return _mediaPlayer; 

    }

    public void VolumeChange(double newVolume)
    {
        if (_mediaPlayer != null)
        {
            _mediaPlayer.Volume = newVolume;
        }
    }
}   


