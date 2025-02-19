/*
*
*   A very simple implementation of a very simple sound system.
*   @author Michael Heron
*   @version 1.0
*   
*/

using SDL2;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static SDL2.SDL_mixer;

namespace Shard
{
    public class SoundSDL : Sound
    {

        int frequency = 44100;
        ushort format = SDL.AUDIO_S16SYS;
        int channels = 2;
        int chunksize = 2048;
        public SoundSDL()
        {
            if (SDL_mixer.Mix_OpenAudio(frequency, format, channels, chunksize) != 0)
            {
                Console.WriteLine($"Failed to open Audio Device: {SDL.SDL_GetError()}");
                return;
            }
            Console.WriteLine($"Number of available Audio Channels: {SDL_mixer.Mix_AllocateChannels(-1)}");
            Console.WriteLine(frequency + " " + format + " " + channels + " " + chunksize);
        }

        public override void playSound(string file, int volume)
        {
            file = Bootstrap.getAssetManager().getAssetPath(file);

            IntPtr chunk = SDL_mixer.Mix_LoadWAV(file);
            if (chunk == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to load WAV: {SDL.SDL_GetError()}");
                return;
            }
            
            // Find length of sound
            SDL_mixer.MIX_Chunk soundChunk = Marshal.PtrToStructure<SDL_mixer.MIX_Chunk>(chunk);        // Get chunk length
            int chunkSize = (int)soundChunk.alen;                                                       // Get the size of the audio data in bytes
            int bytesPerSample = 2;                                                                     // PCM s16le means 16-bit = 2 bytes per sample
            int chunkFrequency, chunkChannels;
            ushort chunkFormat;
            SDL_mixer.Mix_QuerySpec(out chunkFrequency, out chunkFormat, out chunkChannels);            // Get chunk specs
            double lengthsec = (double)chunkSize / (chunkFrequency * chunkChannels * bytesPerSample);   // Get length in seconds
            int length = (int)(lengthsec * 1000);                                                       // Convert to milliseconds

            Console.WriteLine($"Sound duration: {length} ms");


            SDL_mixer.Mix_VolumeChunk(chunk, volume);   // Set volume
            SDL_mixer.Mix_PlayChannel(-1, chunk, 0);    // Play sound on the first available channel

            // Free the sound after it has finished playing
            Task.Run(() =>
            {
                Thread.Sleep(length);
                SDL_mixer.Mix_FreeChunk(chunk);
            });
        }


        public override void playMusic(string file, int volume)
        {
            file = Bootstrap.getAssetManager().getAssetPath(file);

            IntPtr music = SDL_mixer.Mix_LoadMUS(file);
            if (music == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to load music: {SDL.SDL_GetError()}");
                return;
            }

            // Play music with infinite looping (-1 means loop forever)
            SDL_mixer.Mix_PlayMusic(music, -1);
        }

        public override void stopMusic()
        {
            SDL_mixer.Mix_HaltMusic();
        }

    }
}