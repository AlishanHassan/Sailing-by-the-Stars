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
        public SoundEffect menuBGM;
        private SoundEffect gameBGM;
        private SoundEffect laserFX;
        public SoundEffect collideFX;
        public SoundEffect explosionFX;

        private SoundEffectInstance menuBGMInstance;
        private SoundEffectInstance gameBGMInstance;
        private SoundEffectInstance laserFXInstance;
        private SoundEffectInstance collideFXInstance;
        private SoundEffectInstance explosionFXInstance;

        public Audio(MainGame game)
        {
            //load sound
            menuBGM = game.Content.Load<SoundEffect>("menuBGM.wav");
            gameBGM = game.Content.Load<SoundEffect>("gameBGM.wav");   
            laserFX = game.Content.Load<SoundEffect>("laserFX.wav");
            collideFX = game.Content.Load<SoundEffect>("collideFX.wav");
            explosionFX = game.Content.Load<SoundEffect>("explosionFX.wav");


            //create instances
            menuBGMInstance = menuBGM.CreateInstance();
            gameBGMInstance = gameBGM.CreateInstance();
            laserFXInstance = laserFX.CreateInstance();
            collideFXInstance = collideFX.CreateInstance();
            explosionFXInstance = explosionFX.CreateInstance();

        }

        internal void stopBGM()
        {           
            gameBGMInstance.Stop();
            menuBGMInstance.Stop();
        }

        internal void playMenuBGM()
        {
            menuBGMInstance.IsLooped = true;
            menuBGMInstance.Play();
        }

        internal void playGameBGM()
        {
            gameBGMInstance.IsLooped = true;
            gameBGMInstance.Play();
        }

        internal void pauseGameBGM()
        {
            gameBGMInstance.Pause();
        }
      
        internal void resumeGameBGM()
        {
            gameBGMInstance.Resume();
        }

        internal void playLaserFX()
        {
            laserFXInstance.IsLooped = false;
            laserFXInstance.Play();
        }
        
        internal void stopLaserFX()
        {
            laserFXInstance.Stop();
        }

        internal void playCollideFX()
        {
            collideFXInstance.IsLooped = false;
            collideFXInstance.Play();
        }

        internal void stopCollideFX()
        {
            collideFXInstance.Stop();
        }

        internal void playExplosionFX()
        {
            explosionFXInstance.IsLooped = false;
            explosionFXInstance.Play();
        }

        internal void stopExplosionFX()
        {
            explosionFXInstance.Stop();
        }


    }
}
