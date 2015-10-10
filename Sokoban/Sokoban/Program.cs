using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SoundPlayer typewriter = SoundSingleton.getSoundPlayerInstance();
            typewriter.SoundLocation = @"Music\mainMusic.wav";
            typewriter.PlayLooping();

            Application.Run(new Menu());
        }
    }
}
