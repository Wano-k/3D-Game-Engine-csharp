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


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemGraphic(string graphicName, bool isRTP, GraphicKind graphicKind)
        {
            GraphicName = graphicName;
            IsRTP = isRTP;
            GraphicKind = graphicKind;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public SystemGraphic CreateCopy()
        {
            return new SystemGraphic(GraphicName, IsRTP, GraphicKind);
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
            return IsRTP ? GetRTPPath(GraphicName) : GetLocalPath(GraphicName);
        }

        // -------------------------------------------------------------------
        // GetRTPPath
        // -------------------------------------------------------------------

        public string GetRTPPath()
        {
            return Path.Combine(WANOK.SystemDatas.PathRTP, GetRessourcesPath());
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
                case GraphicKind.Picture:
                    return "";
                case GraphicKind.Tileset:
                    return Path.Combine("Content", "Pictures", "Textures2D", "Tilesets");
                case GraphicKind.Autotile:
                    return Path.Combine("Content", "Pictures", "Textures2D", "Autotiles");
                default:
                    return "";
            }
        }

        // -------------------------------------------------------------------
        // LoadImage
        // -------------------------------------------------------------------

        public Image LoadImage()
        {
            string path = GetGraphicPath();
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return Image.FromStream(stream);
                }
            }
            catch {
                MessageBox.Show("Could not load " + path, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
