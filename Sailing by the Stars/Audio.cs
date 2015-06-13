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
        private SoundEffect menuBGM;
        private SoundEffect gameBGM;
        private SoundEffect laserFX;
        private SoundEffect collideFX;
        private SoundEffect explosionFX;
        private SoundEffect hitExplosionFX;

        private SoundEffectInstance menuBGMInstance;
        private SoundEffectInstance gameBGMInstance;
        private SoundEffectInstance laserFXInstance;
        private SoundEffectInstance collideFXInstance;
        private SoundEffectInstance explosionFXInstance;
        private SoundEffectInstance hitExplosionFXInstance;

        public Audio(MainGame game)
        {
            //load sound
            menuBGM = game.Content.Load<SoundEffect>("Audio/menuBGM.wav");
            gameBGM = game.Content.Load<SoundEffect>("Audio/gameBGM.wav");
            laserFX = game.Content.Load<SoundEffect>("Audio/laserFX.wav");
            collideFX = game.Content.Load<SoundEffect>("Audio/collideFX.wav");
            explosionFX = game.Content.Load<SoundEffect>("Audio/explosionFX.wav");
            hitExplosionFX = game.Content.Load<SoundEffect>("Audio/hitExplosionFX.wav");


            //create instances
            menuBGMInstance = menuBGM.CreateInstance();
            gameBGMInstance = gameBGM.CreateInstance();
            laserFXInstance = laserFX.CreateInstance();
            collideFXInstance = collideFX.CreateInstance();
            explosionFXInstance = explosionFX.CreateInstance();
            hitExplosionFXInstance = hitExplosionFX.CreateInstance();

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

        internal void playHitExplosionFX()
        {
            hitExplosionFXInstance.IsLooped = false;
            hitExplosionFXInstance.Play();
        }

        internal void stopHitExplosionFX()
        {
            hitExplosionFXInstance.Stop();
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
