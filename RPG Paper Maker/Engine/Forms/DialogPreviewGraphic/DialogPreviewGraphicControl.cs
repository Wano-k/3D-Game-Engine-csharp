using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker.Controls
{
    public class DialogPreviewGraphicControl
    {
        public SystemGraphic Model;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogPreviewGraphicControl(SystemGraphic graphic)
        {
            Model = graphic;
        }

        // -------------------------------------------------------------------
        // GetRTPFiles
        // -------------------------------------------------------------------

        public List<string> GetRTPFiles()
        {
            return SortArrayFiles(Directory.GetFiles(Model.GetRTPPath()));
        }

        // -------------------------------------------------------------------
        // GetLocalFiles
        // -------------------------------------------------------------------

        public List<string> GetLocalFiles()
        {
            return SortArrayFiles(Directory.GetFiles(Model.GetLocalPath()));
        }

        // -------------------------------------------------------------------
        // SortArrayFiles
        // -------------------------------------------------------------------

        public List<string> SortArrayFiles(string[] array)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < array.Length; i++)
            {
                if (new string[] { ".png", ".PNG", ".jpg", ".JPG", ".JPEG", ".bmp" }.Contains(Path.GetExtension(array[i])))
                {
                    list.Add(Path.GetFileName(array[i]));
                }
            }

            return list;
        }

        // -------------------------------------------------------------------
        // SetImageDatas
        // -------------------------------------------------------------------

        public void SetImageDatas(string name, bool isRTP)
        {
            Model.GraphicName = name;
            Model.IsRTP = isRTP;
        }
    }
}
