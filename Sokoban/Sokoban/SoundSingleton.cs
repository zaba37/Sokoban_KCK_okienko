using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    static class SoundSingleton
    {
        private static SoundPlayer soundPlayer = null;

        public static SoundPlayer getSoundPlayerInstance()
        {
            if (soundPlayer == null)
            {
                soundPlayer = new SoundPlayer();
                return soundPlayer;
            }
            else
            {
                return soundPlayer;
            }
        }
    }
}
