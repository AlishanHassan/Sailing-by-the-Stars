using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sailing_by_the_Stars
{
    class Audio
    {
        private SoundEffect laserFX;
        public SoundEffect collideFX;
        public SoundEffect explosionFX;
        public SoundEffect menuBGM;
        public SoundEffect gameBGM;

        public Audio(MainGame game)
        {
            laserFX = game.Content.Load<SoundEffect>("laserFX");
        }

        internal void playLaserFX()
        {
            //laserFX.Play(); // no audio hardware exception: OpenAl driver not installed

            /*To use the NullDevice for sound, run this line somewhere before initializing your Game:
             Environment.SetEnvironmentVariable("FNA_AUDIO_DISABLE_SOUND", "1");
            */
        }

    }
}
