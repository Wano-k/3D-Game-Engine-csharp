using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemGraphic
    {
        public string GraphicName;
        public bool IsRTP;
        public GraphicKind GraphicKind;
        public object[] Options;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemGraphic(GraphicKind graphicKind, object[] options = null) : this(WANOK.NONE_IMAGE_STRING, true, graphicKind, options)
        {

        }

        public SystemGraphic(string graphicName, bool isRTP, GraphicKind graphicKind, object[] options = null)
        {
            GraphicName = graphicName;
            IsRTP = isRTP;
            GraphicKind = graphicKind;
            Options = options;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public SystemGraphic CreateCopy()
        {
            object[] options = new object[Options.Length];
            for (int i = 0; i < Options.Length; i++)
            {
                options[i] = Options[i];
            }

            return new SystemGraphic(GraphicName, IsRTP, GraphicKind, options);
        }

        // -------------------------------------------------------------------
        // IsNone
        // -------------------------------------------------------------------

        public bool IsNone()
        {
            return GraphicName == WANOK.NONE_IMAGE_STRING;
        }

        // -------------------------------------------------------------------
        // GetGraphicPath
        // -------------------------------------------------------------------

        public string GetGraphicPath()
        {
            if (IsNone()) return null;
            return IsRTP ? GetRTPPath(GraphicName) : GetLocalPath(GraphicName);
        }

        // -------------------------------------------------------------------
        // GetRTPPath
        // -------------------------------------------------------------------

        public string GetRTPPath()
        {
            return Path.Combine(WANOK.Game.System.PathRTP, GetRessourcesPath());
        }

        public string GetRTPPath(string fileName)
        {
            return Path.Combine(GetRTPPath(), fileName);
        }

        // -------------------------------------------------------------------
        // GetLocalPath
        // -------------------------------------------------------------------

        public string GetLocalPath()
        {
            return Path.Combine(WANOK.CurrentDir, GetRessourcesPath());
        }

        public string GetLocalPath(string fileName)
        {
            return Path.Combine(GetLocalPath(), fileName);
        }

        // -------------------------------------------------------------------
        // GetRessourcesPath
        // -------------------------------------------------------------------

        public string GetRessourcesPath()
        {
            switch (GraphicKind)
            {
                case GraphicKind.Tileset:
                    return Path.Combine("Content", "Pictures", "Textures2D", "Tilesets");
                case GraphicKind.Autotile:
                    return Path.Combine("Content", "Pictures", "Textures2D", "Autotiles");
                case GraphicKind.Character:
                    return Path.Combine("Content", "Pictures", "Textures2D", "Characters");
                case GraphicKind.Relief:
                    return Path.Combine("Content", "Pictures", "Textures2D", "Reliefs");
                case GraphicKind.Other:
                    return Path.Combine("Content", "Pictures", "UI", "Others");
                case GraphicKind.Icon:
                    return Path.Combine("Content", "Pictures", "UI", "Icons");
                case GraphicKind.Bar:
                    return Path.Combine("Content", "Pictures", "UI", "Bars");
                default:
                    return "";
            }
        }

        // -------------------------------------------------------------------
        // LoadImage
        // -------------------------------------------------------------------

        public Image LoadImage()
        {
            string path = "";
            try
            {
                path = GetGraphicPath();
                if (path == null) return Properties.Resources.none;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return Image.FromStream(stream);
                }
            }
            catch {
                MessageBox.Show("Could not load " + path, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Properties.Resources.none;
            }
        }

        // -------------------------------------------------------------------
        // LoadTexture
        // -------------------------------------------------------------------

        public Texture2D LoadTexture(GraphicsDevice device)
        {
            string path = "";
            try
            {
                path = GetGraphicPath();
                if (path == null) return MapEditor.TexNone;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return Texture2D.FromStream(device, stream);
                }
            }
            catch
            {
                MessageBox.Show("Could not load " + path, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return MapEditor.TexNone;
            }
        }
    }
}
