using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemRelief : ComboxBoxSpecialTilesetItem
    {
        public static int MAX_RELIEFS = 9999;
        public Collision Collision;
        public DrawType TopDrawType;
        public object[] TopTexture;


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemRelief(int id) : this(id, "", new SystemGraphic(GraphicKind.Relief), new Collision(), DrawType.None, null)
        {

        }

        public SystemRelief(int id, string n, SystemGraphic graphic, Collision collision, DrawType topDrawtype, object[] topTexture)
        {
            Id = id;
            Name = n;
            Graphic = graphic;
            Collision = collision;
            TopDrawType = topDrawtype;
            TopTexture = topTexture;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            object[] topTextureCopy = null;
            if (TopTexture != null)
            {
                topTextureCopy = new object[TopTexture.Length];
                for (int i = 0; i < TopTexture.Length; i++)
                {
                    topTextureCopy[i] = TopTexture[i];
                }
            }
            
            return new SystemRelief(Id, Name, Graphic.CreateCopy(), Collision.CreateCopy(), TopDrawType, topTextureCopy);
        }

        // -------------------------------------------------------------------
        // GetDefaultReliefs
        // -------------------------------------------------------------------

        public static List<SystemRelief> GetDefaultReliefs()
        {
            List<SystemRelief> list = new List<SystemRelief>();
            list.Add(new SystemRelief(1, "Grass", new SystemGraphic("grass.png", true, GraphicKind.Relief), new Collision(), DrawType.Floors, new object[] { 0, 0, 1, 1 }));
            list.Add(new SystemRelief(2, "Halloween", new SystemGraphic("halloween.png", true, GraphicKind.Relief), new Collision(), DrawType.Floors, new object[] { 0, 0, 1, 1 }));

            return list;
        }
    }
}
