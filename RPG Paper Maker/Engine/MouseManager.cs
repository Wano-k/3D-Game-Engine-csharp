using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPG_Paper_Maker
{
    class MouseManager
    {
        private Point MousePosition = Point.Empty;
        private bool FirstLeftClick = false;
        private bool FirstRightClick = false;
        private bool FirstWheelClick = false;
        private bool OnLeftClick = false;
        private bool OnRightClick = false;
        private bool OnWheelClick = false;
        private bool UpLeftClick = true;
        private bool UpRightClick = true;
        private bool UpWheelClick = true;


        public void SetMouseDownStatus(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    FirstLeftClick = true;
                    OnLeftClick = true;
                    UpLeftClick = false;
                    break;
                case MouseButtons.Right:
                    FirstRightClick = true;
                    OnRightClick = true;
                    UpRightClick = false;
                    break;
                case MouseButtons.Middle:
                    FirstWheelClick = true;
                    OnWheelClick = true;
                    UpWheelClick = false;
                    break;
            }
        }

        public void SetMouseUpStatus(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    FirstLeftClick = true;
                    UpLeftClick = true;
                    OnLeftClick = false;
                    break;
                case MouseButtons.Right:
                    FirstRightClick = true;
                    UpRightClick = true;
                    OnRightClick = false;
                    break;
                case MouseButtons.Middle:
                    FirstWheelClick = true;
                    UpWheelClick = true;
                    OnWheelClick = false;
                    break;
            }
        }

        public Point GetPosition()
        {
            return MousePosition;
        }

        public void SetPosition(Point point)
        {
            MousePosition = point;
        }

        public void Update()
        {
            FirstLeftClick = false;
            FirstRightClick = false;
            FirstWheelClick = false;
        }

        public Boolean IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return OnLeftClick && FirstLeftClick;
                case MouseButtons.Right:
                    return OnRightClick && FirstRightClick;
                case MouseButtons.Middle:
                    return OnWheelClick && FirstWheelClick;
            }

            throw new Exception(button.ToString() + " is not managed.");
        }

        public Boolean IsButtonDownRepeat(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return OnLeftClick;
                case MouseButtons.Right:
                    return OnRightClick;
                case MouseButtons.Middle:
                    return OnWheelClick;
            }

            throw new Exception(button.ToString() + " is not managed.");
        }

        public Boolean IsButtonUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return UpLeftClick && FirstLeftClick;
                case MouseButtons.Right:
                    return UpRightClick && FirstRightClick;
                case MouseButtons.Middle:
                    return UpWheelClick && FirstWheelClick;
            }

            throw new Exception(button.ToString() + " is not managed.");
        }
    }
}
