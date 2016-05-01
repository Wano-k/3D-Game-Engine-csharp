using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class SelectionRectangle
    {
        int BORDER_SIZE = 4;
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        protected Texture2D BorderTopLeft;
        protected Texture2D BorderTop;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SelectionRectangle(GraphicsDevice GraphicsDevice, Texture2D image, int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            // Loading borders
            this.BorderTopLeft = WANOK.GetSubImage(GraphicsDevice, image, new Rectangle(0, 0, BORDER_SIZE, BORDER_SIZE));
            this.BorderTop = WANOK.GetSubImage(GraphicsDevice, image, new Rectangle(BORDER_SIZE, 0, 1, BORDER_SIZE));
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(SpriteBatch spriteBatch)
        {
            // Left-Top
            spriteBatch.Draw(this.BorderTopLeft, new Vector2(this.X + BORDER_SIZE, this.Y + BORDER_SIZE), null, Color.White, 0, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);
            // Right-Top
            spriteBatch.Draw(this.BorderTopLeft, new Vector2(this.X + this.Width - BORDER_SIZE, this.Y + BORDER_SIZE), null, Color.White, (float)Math.PI/2, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);
            // Right-Bot
            spriteBatch.Draw(this.BorderTopLeft, new Vector2(this.X + this.Width - BORDER_SIZE, this.Y + this.Height - BORDER_SIZE), null, Color.White, (float)Math.PI, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);
            // Left-Bot
            spriteBatch.Draw(this.BorderTopLeft, new Vector2(this.X + BORDER_SIZE, this.Y + this.Height - BORDER_SIZE), null, Color.White, (float)Math.PI*1.5f, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);

            // Top
            spriteBatch.Draw(this.BorderTop, new Vector2(this.X + (BORDER_SIZE*(1+this.Width-(BORDER_SIZE*2))), this.Y + BORDER_SIZE), null, Color.White, 0, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(this.Width - (BORDER_SIZE*2), 1), SpriteEffects.None, 0);
            // Right
            spriteBatch.Draw(this.BorderTop, new Vector2(this.X + this.Width - BORDER_SIZE, this.Y + (BORDER_SIZE*(1 + this.Height - (BORDER_SIZE * 2)))), null, Color.White, (float)Math.PI / 2, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(this.Height - (BORDER_SIZE * 2), 1), SpriteEffects.None, 0);
            // Bot
            spriteBatch.Draw(this.BorderTop, new Vector2(this.X + this.Width - (BORDER_SIZE* (1 + this.Width - (BORDER_SIZE * 2))), this.Y + this.Height - BORDER_SIZE), null, Color.White, (float)Math.PI, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(this.Width - (BORDER_SIZE * 2), 1), SpriteEffects.None, 0);
            // Left
            spriteBatch.Draw(this.BorderTop, new Vector2(this.X + BORDER_SIZE, this.Y + this.Height - (BORDER_SIZE * (1 + this.Height - (BORDER_SIZE * 2)))), null, Color.White, (float)Math.PI * 1.5f, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(this.Height - (BORDER_SIZE * 2), 1), SpriteEffects.None, 0);
        }
    }
}
