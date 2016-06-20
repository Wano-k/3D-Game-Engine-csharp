using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class CursorEditor
    {
        public Vector3 Position;
        private Square Square;
        private int Frame = 0, FrameTick = 0, CursorWait = 0;
        private const int CursorWaitDuration = 3, FrameDuration = 100;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public CursorEditor(GraphicsDevice device)
        {
            Square = new Square(device, MapEditor.TexCursor, new int[] { 0, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE });

            // Position and size
            Position = new Vector3(0,0,0);
        }

        // -------------------------------------------------------------------
        // Reset
        // -------------------------------------------------------------------

        public void Reset()
        {
            Position = new Vector3(0, 0, 0);
        }

        // -------------------------------------------------------------------
        // GetX
        // -------------------------------------------------------------------

        public int GetX()
        {
            return (int)((Position.X + 1) / WANOK.SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // GetZ
        // -------------------------------------------------------------------

        public int GetZ()
        {
            return (int)((Position.Z + 1) / WANOK.SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // GetPortion
        // -------------------------------------------------------------------

        public int[] GetPortion()
        {
            return new int[] { GetX() / WANOK.PORTION_SIZE, GetZ() / WANOK.PORTION_SIZE };
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(GameTime gameTime, Camera camera, MapInfos map)
        {
            double angle = camera.HorizontalAngle;
            int x = GetX(), z = GetZ(), x_plus, z_plus;

            if (WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveUp)
                || WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveDown)
                || WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveLeft)
                || WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveRight))
            {
                CursorWait = CursorWaitDuration;
            }

            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveUp, CursorWait)) // Up
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos(angle * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin(angle * Math.PI / 180.0))));
                if ((z > 0 && z_plus < 0) || (z < map.Height-1 && z_plus > 0)) Position.Z += z_plus;
                if (z_plus == 0 && ((x > 0 && x_plus < 0) || (x < map.Width-1 && x_plus > 0))) Position.X += x_plus;
            }  
            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveDown, CursorWait)) // Down
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos(angle * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin(angle * Math.PI / 180.0))));
                if ((z < map.Height - 1 && z_plus < 0) || (z > 0 && z_plus > 0)) Position.Z -= z_plus;
                if (z_plus == 0 && ((x < map.Width-1 && x_plus < 0) || (x > 0 && x_plus > 0))) Position.X -= x_plus;
            }
            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveLeft, CursorWait)) // Left
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos((angle - 90.0) * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin((angle - 90.0) * Math.PI / 180.0))));
                if ((x > 0 && x_plus < 0) || (x < map.Width-1 && x_plus > 0)) Position.X += x_plus;
                if (x_plus == 0 && ((z > 0 && z_plus < 0) || (z < map.Height-1 && z_plus > 0))) Position.Z += z_plus;
            }
            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveRight, CursorWait)) // Right
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos((angle - 90.0) * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin((angle - 90.0) * Math.PI / 180.0))));
                if ((x < map.Width - 1 && x_plus < 0) || (x > 0 && x_plus > 0)) Position.X -= x_plus;
                if (x_plus == 0 && ((z < map.Height-1 && z_plus < 0) || (z > 0 && z_plus > 0))) Position.Z -= z_plus;
            }
              
            // Frames update  
            FrameTick += gameTime.ElapsedGameTime.Milliseconds;
            if (FrameTick >= FrameDuration)
            {
                Frame += 1;
                if (Frame > 3) Frame = 0;
                FrameTick = 0;
            }

            if (CursorWait > 0) CursorWait--;
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, GameTime gameTime, BasicEffect effect)
        {
            Square.CreateTex(new int[] { WANOK.BASIC_SQUARE_SIZE * Frame, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE }, MapEditor.TexCursor);
            Square.Draw(device, gameTime, effect, MapEditor.TexCursor, Position);
        }
    }
}
