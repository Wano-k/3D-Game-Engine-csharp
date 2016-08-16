using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RPG_Paper_Maker
{
    public partial class GraphicControl : UserControl
    {
        public GraphicPanel panel = new GraphicPanel();
        public delegate void EventHandlerGraphic(SystemGraphic graphic);
        public event EventHandlerGraphic ClosingDialogEvent;


        // -------------------------------------------------------------------
        // GRAPHIC PANEL CLASS
        // -------------------------------------------------------------------

        #region class GraphicPanel

        public class GraphicPanel : Panel
        {
            public SystemGraphic Graphic;
            public Image Image = null;
            public Pen BorderPen = new Pen(Color.LightGray);
            public int Frame = 0;
            public bool Animated = true;


            // -------------------------------------------------------------------
            // Constructor
            // -------------------------------------------------------------------

            public GraphicPanel()
            {
                BorderPen.DashStyle = DashStyle.Dash;
                BorderPen.Width = 3;
            }

            // -------------------------------------------------------------------
            // OnPaint
            // -------------------------------------------------------------------

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

                base.OnPaint(e);

                if (Image != null)
                {
                    int frames = (int)Graphic.Options[0];
                    int rows = (int)Graphic.Options[1] == 0 ? 4 : 8;
                    Size size = new Size((int)((Image.Size.Width / frames) * WANOK.RELATION_SIZE), (int)((Image.Size.Height / rows) * WANOK.RELATION_SIZE));
                    int offset = Frame % 2 == 0 ? 0 : 1;
                    Point point = new Point((Size.Width - size.Width) / 2, ((Size.Height - size.Height) / 2) + offset);
                    int index = (int)Graphic.Options[2];
                    Size frameSize = new Size(Image.Size.Width / frames, Image.Size.Height / rows);
                    Point framePoint = Animated ? new Point(Frame * frameSize.Width, (index / frames) * frameSize.Height) : new Point((index % frames) * frameSize.Width, (index / frames) * frameSize.Height);
                    e.Graphics.DrawImage(Image, new Rectangle(point, size), new Rectangle(framePoint, frameSize), GraphicsUnit.Pixel);
                }

                if (Focused) e.Graphics.DrawRectangle(BorderPen, new Rectangle(ClientRectangle.X + 5, ClientRectangle.Y + 5, ClientRectangle.Width - 11, ClientRectangle.Height - 11));
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public GraphicControl()
        {
            InitializeComponent();

            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(panel, 0, 1);

            // Combobox draw type
            foreach (DrawType drawtype in Enum.GetValues(typeof(DrawType)))
            {
                comboBox1.Items.Add(WANOK.DrawTypeToString(drawtype));
            }

            // Event
            panel.Click += panel_Click;
            panel.DoubleClick += Panel_DoubleClick;
            panel.LostFocus += Panel_LostFocus;
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters(SystemGraphic graphic)
        {
            panel.Graphic = graphic;
        }

        // -------------------------------------------------------------------
        // GetComboBox
        // -------------------------------------------------------------------

        public ComboBox GetComboBox()
        {
            return comboBox1;
        }

        // -------------------------------------------------------------------
        // ResetGraphics
        // -------------------------------------------------------------------

        public void ResetGraphics()
        {
            panel.Image = null;
            panel.Frame = 0;
            panel.Graphic = SystemGraphic.GetDefaultEventGraphic();
            panel.Invalidate();
        }

        // -------------------------------------------------------------------
        // OpenSpriteDialog
        // -------------------------------------------------------------------

        public void OpenSpriteDialog()
        {
            DialogPreviewGraphicSelectFrame dialog = new DialogPreviewGraphicSelectFrame(panel.Graphic.CreateCopy(), OptionsKind.CharacterSelection);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                panel.Graphic = dialog.GetGraphic();
                if (panel.Graphic.IsNone())
                {
                    comboBox1.SelectedIndex = (int)DrawType.None;
                    ResetGraphics();
                }
                else
                {
                    panel.Image = panel.Graphic.LoadImage();
                    panel.Refresh();
                }
                var eventSubscribers = ClosingDialogEvent;
                if (eventSubscribers != null) eventSubscribers(panel.Graphic);
            }
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void panel_Click(object sender, EventArgs e)
        {
            panel.Focus();
            panel.Refresh();
        }

        private void Panel_DoubleClick(object sender, EventArgs e)
        {
            switch ((DrawType)comboBox1.SelectedIndex)
            {
                case DrawType.None:
                    comboBox1.SelectedIndex = (int)DrawType.FaceSprite;
                    OpenSpriteDialog();
                    break;
                case DrawType.Floors:
                    break;
                case DrawType.Autotiles:
                    break;
                case DrawType.FaceSprite:
                    OpenSpriteDialog();
                    break;
                case DrawType.FixSprite:
                    OpenSpriteDialog();
                    break;
                case DrawType.DoubleSprite:
                    OpenSpriteDialog();
                    break;
                case DrawType.QuadraSprite:
                    OpenSpriteDialog();
                    break;
                case DrawType.OnFloorSprite:
                    OpenSpriteDialog();
                    break;
                case DrawType.Montains:
                    break;
                default:
                    break;
            }
        }

        private void Panel_LostFocus(object sender, EventArgs e)
        {
            panel.Refresh();
        }
    }
}
