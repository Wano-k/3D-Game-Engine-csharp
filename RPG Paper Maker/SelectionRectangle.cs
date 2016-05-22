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
        public int X;
        public int Y;
        public int Width;
        public int Height;
        protected Texture2D BorderTopLeft;
        protected Texture2D BorderTop;
        protected int RealX, RealY;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SelectionRectangle(GraphicsDevice GraphicsDevice, Texture2D image, int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RealX = 0;
            RealY = 0;

            // Loading borders
            BorderTopLeft = WANOK.GetSubImage(GraphicsDevice, image, new Rectangle(0, 0, BORDER_SIZE, BORDER_SIZE));
            BorderTop = WANOK.GetSubImage(GraphicsDevice, image, new Rectangle(BORDER_SIZE, 0, 1, BORDER_SIZE));
        }

        // -------------------------------------------------------------------
        // SetRectangle
        // -------------------------------------------------------------------

        public void SetRectangle(int x, int y, int width, int height)
        {
            X = (x / WANOK.BASIC_SQUARE_SIZE) * WANOK.BASIC_SQUARE_SIZE;
            Y = (y / WANOK.BASIC_SQUARE_SIZE) * WANOK.BASIC_SQUARE_SIZE;
            Width = width * WANOK.BASIC_SQUARE_SIZE;
            Height = height * WANOK.BASIC_SQUARE_SIZE;
        }

        // -------------------------------------------------------------------
        // SetRealPosition
        // -------------------------------------------------------------------

        public void SetRealPosition()
        {
            X = RealX;
            Y = RealY;
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(SpriteBatch spriteBatch)
        {
            // Setting coords
            int x = X;
            int x_width = 0;

            if (Width < 0)
            {
                x += Width + WANOK.BASIC_SQUARE_SIZE;
                Width = -Width;
                x_width = Width - WANOK.BASIC_SQUARE_SIZE;
            }

            int y = Y;
            int y_height = 0;

            if (Height < 0)
            {
                y += Height + WANOK.BASIC_SQUARE_SIZE;
                Height = -Height;
                y_height = Height - WANOK.BASIC_SQUARE_SIZE;
            }

            // Left-Top
            spriteBatch.Draw(BorderTopLeft, new Vector2(x + BORDER_SIZE, y + BORDER_SIZE), null, Color.White, 0, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);
            // Right-Top
            spriteBatch.Draw(BorderTopLeft, new Vector2(x + Width - BORDER_SIZE, y + BORDER_SIZE), null, Color.White, (float)Math.PI/2, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);
            // Right-Bot
            spriteBatch.Draw(BorderTopLeft, new Vector2(x + Width - BORDER_SIZE, y + Height - BORDER_SIZE), null, Color.White, (float)Math.PI, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);
            // Left-Bot
            spriteBatch.Draw(BorderTopLeft, new Vector2(x + BORDER_SIZE, y + Height - BORDER_SIZE), null, Color.White, (float)Math.PI*1.5f, new Vector2(BORDER_SIZE, BORDER_SIZE), 1.0f, SpriteEffects.None, 0);

            // Top
            spriteBatch.Draw(BorderTop, new Vector2(x + (BORDER_SIZE*(1 + Width-(BORDER_SIZE * 2))), y + BORDER_SIZE), null, Color.White, 0, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(Width - (BORDER_SIZE*2), 1), SpriteEffects.None, 0);
            // Right
            spriteBatch.Draw(BorderTop, new Vector2(x + Width - BORDER_SIZE, y + (BORDER_SIZE*(1 + Height - (BORDER_SIZE * 2)))), null, Color.White, (float)Math.PI / 2, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(Height - (BORDER_SIZE * 2), 1), SpriteEffects.None, 0);
            // Bot
            spriteBatch.Draw(BorderTop, new Vector2(x + Width - (BORDER_SIZE* (1 + Width - (BORDER_SIZE * 2))), y + Height - BORDER_SIZE), null, Color.White, (float)Math.PI, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(Width - (BORDER_SIZE * 2), 1), SpriteEffects.None, 0);
            // Left
            spriteBatch.Draw(BorderTop, new Vector2(x + BORDER_SIZE, y + Height - (BORDER_SIZE * (1 + Height - (BORDER_SIZE * 2)))), null, Color.White, (float)Math.PI * 1.5f, new Vector2(BORDER_SIZE, BORDER_SIZE), new Vector2(Height - (BORDER_SIZE * 2), 1), SpriteEffects.None, 0);

            RealX = X - x_width;
            RealY = Y - y_height;
        }
    }
}
