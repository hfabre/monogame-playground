
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{

    class Animator
    {
        private Dictionary<string, AnimatedSprite> sprites = new Dictionary<string, AnimatedSprite>();
        private string currentAnimation;

        public Animator()
        {

        }

        public void AddAnimation(string key, AnimatedSprite animation)
        {
            this.sprites.Add(key, animation);
        }

        public void SetCurrentAnimation(string key)
        {
            this.currentAnimation = key;
        }

        public void Update(GameTime time)
        {
            this.sprites[this.currentAnimation].Update(time);
        }

        public void Draw(GameTime time, float x, float y, SpriteBatch spriteBatch)
        {
            this.sprites[this.currentAnimation].Draw(time, x, y, spriteBatch);
        }
    }
}