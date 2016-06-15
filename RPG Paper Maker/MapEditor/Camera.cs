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
    class Camera
    {
        public Vector3 Position;
        private Vector3 Target;
        private Vector3 UpVector = Vector3.Up;
        public Matrix Projection;
        public Matrix View { get { return Matrix.CreateLookAt(Position, Target, UpVector); } }
        public Matrix World;
        public double HorizontalAngle = -90.0, TargetAngle = -90.0, VerticalAngle = 0.0, Distance = 200.0, Height = 100.0;
        public int RotateVelocity = 180;
        private double RotateSteps = 90.0, RotateTick = 0.0;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Camera(GraphicsDevice GraphicsDevice)
        {
            Position = Vector3.Zero;
            Target = Vector3.Zero;
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1.0f, 10000.0f);
            World = Matrix.Identity;
        }

        // -------------------------------------------------------------------
        // ReLoadMap
        // -------------------------------------------------------------------

        public void ReLoadMap()
        {
            Position = Vector3.Zero;
            Target = Vector3.Zero;
            HorizontalAngle = -90.0;
            TargetAngle = -90.0;
            VerticalAngle = 0.0;
            Distance = 200.0;
            Height = 100.0;
        }

        // -------------------------------------------------------------------
        // ZoomPlus
        // -------------------------------------------------------------------

        public void ZoomPlus()
        {
            if (Position.Y >= 0)
            {
                double dist = Distance / Height;
                Distance -= dist * 20;
                if (Distance < dist * 20) Distance = dist * 20;
                Height -= 20;
                if (Height < 20) Height = 20;
            }
            else
            {
                double dist = Distance / -Height;
                Distance -= dist * 20;
                if (Distance < dist * 20) Distance = dist * 20;
                Height += 20;
                if (Height >= -20) Height = -20;
            }
        }

        // -------------------------------------------------------------------
        // ZoomLess
        // -------------------------------------------------------------------

        public void ZoomLess()
        {
            if (Position.Y >= 0)
            {
                double dist = Distance / Height;
                Distance += dist * 20;
                Height += 20;
            }
            else
            {
                double dist = Distance / -Height;
                Distance += dist * 20;
                Height -= 20;
            }
        }

        // -------------------------------------------------------------------
        // SetAngleH
        // -------------------------------------------------------------------

        public void SetAngleH(int angle)
        {
            HorizontalAngle += angle;
            TargetAngle += angle;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(GameTime gameTime, CursorEditor cursor, Point mouseBeforeUpdate)
        {
            // Zoom
            if (WANOK.MapMouseManager.IsWheelUp())
            {
                ZoomPlus();
            }
            else if (WANOK.MapMouseManager.IsWheelDown())
            {
                ZoomLess();
            }

            // Wheel Rotation
            if (WANOK.MapMouseManager.IsButtonDownRepeat(System.Windows.Forms.MouseButtons.Middle))
            {
                Height += (WANOK.MapMouseManager.GetPosition().Y - mouseBeforeUpdate.Y) * 2;
                SetAngleH((WANOK.MapMouseManager.GetPosition().X - mouseBeforeUpdate.X) / 2);
            }

            // Horizontal angle
            if (TargetAngle != HorizontalAngle)
            {
                float speed = (float)(RotateVelocity * (gameTime.ElapsedGameTime.Milliseconds) / 1000.0);
                if (TargetAngle > HorizontalAngle)
                {
                    HorizontalAngle += speed;
                    if (HorizontalAngle > TargetAngle) HorizontalAngle = TargetAngle;
                }
                else if (TargetAngle < HorizontalAngle){
                    HorizontalAngle -= speed;
                    if (HorizontalAngle < TargetAngle) HorizontalAngle = TargetAngle;
                }
            }
            if (HorizontalAngle >= 270.0 || HorizontalAngle <= -450.0)
            {
                HorizontalAngle = -90.0;
                TargetAngle = -90.0;
            }

            // Keyboard 
            if (TargetAngle == HorizontalAngle)
            {
                if (WANOK.KeyboardManager.IsButtonDownRepeat(Keys.Left))
                {
                    TargetAngle -= RotateSteps;
                }
                else if (WANOK.KeyboardManager.IsButtonDownRepeat(Keys.Right))
                {
                    TargetAngle += RotateSteps;
                }
            }

            // Updating camera according to hero position
            Target.X = cursor.Position.X + (WANOK.SQUARE_SIZE/2);
            Target.Y = cursor.Position.Y;
            Target.Z = cursor.Position.Z;

            // Camera position
            Position.X = Target.X - (float)(Distance * Math.Cos(HorizontalAngle * Math.PI / 180.0));
            Position.Y = Target.Y - (float)(Distance * Math.Sin(VerticalAngle * Math.PI / 180.0)) + (float)Height;
            Position.Z = Target.Z - (float)(Distance * Math.Sin(HorizontalAngle * Math.PI / 180.0));

            // Rotate tick update
            RotateTick = gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
