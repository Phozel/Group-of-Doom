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

namespace Shard
{
    public class SoundSDL : Sound
    {
        private static byte[] mixingBuffer = new byte[44100 * 4]; // 1 sec buffer (44100 samples * 4 bytes per sample)

        uint auDev;
        SDL.SDL_AudioSpec have, want;
        public SoundSDL()
        {
            auDev = SDL.SDL_OpenAudioDevice(IntPtr.Zero, 0, ref have, out want, 0);
        }

        public override void playSound(string file, int volume)
        {
            //SDL.SDL_AudioSpec have;
            uint length;
            IntPtr buffer;

            file = Bootstrap.getAssetManager().getAssetPath(file);

            SDL.SDL_LoadWAV(file, out have, out buffer, out length);
            Console.WriteLine($"WAV Loaded - Sample Rate: {have.freq}, Format: {have.format}, Channels: {have.channels}, Length: {length}");

            if (have.freq != 44100)
            {
                Console.WriteLine("Warning: WAV file is not 44100Hz, playback may be distorted.");
            }

            int safeLength = (int)Math.Min(length, mixingBuffer.Length);

            SDL.SDL_ClearQueuedAudio(auDev);
            IntPtr mixedBuffer = Marshal.AllocHGlobal(safeLength);
            Array.Clear(mixingBuffer, 0, safeLength);
            Marshal.Copy(mixingBuffer, 0, mixedBuffer, safeLength); // Copy existing buffer
            SDL.SDL_MixAudioFormat(mixedBuffer, buffer, have.format, (uint)safeLength, volume); // Mixing sounds and adjusting volume
            Marshal.Copy(mixedBuffer, mixingBuffer, 0, safeLength);

            if (SDL.SDL_GetQueuedAudioSize(auDev) > 44100 * 4)    // Limit queue size
            {
                Console.WriteLine("Audio queue too large! Clearing...");
                SDL.SDL_ClearQueuedAudio(auDev);
            }

            SDL.SDL_QueueAudio(auDev, mixedBuffer, (uint)safeLength);
            Console.WriteLine($"Queued Audio Size: {SDL.SDL_GetQueuedAudioSize(auDev)}");
            SDL.SDL_PauseAudioDevice(auDev, 0);

            SDL.SDL_FreeWAV(buffer);
            Marshal.FreeHGlobal((int)length);
        }

        // old version of SoundBeep
        public override void playSound2(string file)
        {
            SDL.SDL_AudioSpec have, want;
            uint length, dev;
            IntPtr buffer;

            file = Bootstrap.getAssetManager().getAssetPath(file);

            SDL.SDL_LoadWAV(file, out have, out buffer, out length);

            dev = SDL.SDL_OpenAudioDevice(IntPtr.Zero, 0, ref have, out want, 0);

            if (SDL.SDL_GetQueuedAudioSize(dev) >= 15)
            {
                SDL.SDL_CloseAudioDevice(dev);
                dev = SDL.SDL_OpenAudioDevice(IntPtr.Zero, 0, ref have, out want, 0);
            }

            int success = SDL.SDL_QueueAudio(dev, buffer, length);
            SDL.SDL_PauseAudioDevice(dev, 0);

            SDL.SDL_FreeWAV(buffer);

        }

    }
}