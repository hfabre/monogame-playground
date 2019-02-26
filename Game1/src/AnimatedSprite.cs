using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class AnimatedSprite
    {
        private List<Sprite> sprites = new List<Sprite>();
        public Point FrameSize { get; set; }
        public int nbFrames = 0;
        public int frameRate { get; set; }
        public bool repeat { get; set; }
        protected int currentFrame = 0;
        protected bool finishedAnimation = false;
        protected double timeBetweenFrame = 64; // 60 fps 

        protected double lastFrameUpdatedTime = 0;

        public AnimatedSprite(String[] assets, float scale, bool repeat, bool flip, float offsetX = 0, float offsetY = 0)
        {
            for (int i = 0; i < assets.Length; i++)
            {
                this.sprites.Add(new Sprite(assets[i], scale, flip, offsetX, offsetY));
            }

            this.nbFrames = this.sprites.Count;
            this.repeat = repeat;
        }

        public void Update(GameTime time)
        {
            if (finishedAnimation) return;
            this.lastFrameUpdatedTime += time.ElapsedGameTime.Milliseconds;
            if (this.lastFrameUpdatedTime > this.timeBetweenFrame)
            {
                this.lastFrameUpdatedTime = 0;
                if (this.repeat)
                {
                    this.currentFrame++;
                    if (this.currentFrame >= this.nbFrames)
                    {
                        this.currentFrame = 0;
                    }
                }
                else
                {
                    this.currentFrame++;
                    if (this.currentFrame >= this.nbFrames)
                    {
                        this.currentFrame = 0;
                        if (this.currentFrame >= this.nbFrames)
                        {
                            this.finishedAnimation = true;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime time, float x, float y, SpriteBatch spriteBatch)
        {
            this.sprites[currentFrame].Draw(x, y, spriteBatch);
        }
    }
}
