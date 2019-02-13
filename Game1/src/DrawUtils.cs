using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class DrawUtils
    {
        public static void DrawEmptyRectangle(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y, int width, int height, int bw = 2)
        {
            Texture2D t = GraphicsEngine.GetInstance().textures["blank"];

            spriteBatch.Draw(t, new Rectangle(x, y, bw, height), Color.LightGreen); // Left
            spriteBatch.Draw(t, new Rectangle(x + width, y, bw, height), Color.LightGreen); // Right
            spriteBatch.Draw(t, new Rectangle(x, y, width, bw), Color.LightGreen); // Top
            spriteBatch.Draw(t, new Rectangle(x, y + height, width, bw), Color.LightGreen); // Bottom
        }
    }
}
