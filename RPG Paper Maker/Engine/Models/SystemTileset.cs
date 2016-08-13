using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemTileset : SuperListItem
    {
        public static int MAX_TILESETS = 9999;
        public SystemGraphic Graphic;
        public Collision Collision;
        public List<int> Autotiles = new List<int>();
        public List<int> Reliefs = new List<int>();
        public List<object[]> ReliefTop = new List<object[]>();


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemTileset(int id) : this(id, "", new SystemGraphic(WANOK.NONE_IMAGE_STRING, true, GraphicKind.Tileset), new Collision(), new List<int>(), new List<int>(), new List<object[]>())
        {
            
        }

        public SystemTileset(int id, string n, SystemGraphic graphic, Collision collision, List<int> autotiles, List<int> reliefs, List<object[]> reliefTop)
        {
            Id = id;
            Name = n;
            Graphic = graphic;
            Collision = collision;
            Autotiles = autotiles;
            Reliefs = reliefs;
            ReliefTop = reliefTop;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new SystemTileset(Id, Name, Graphic.CreateCopy(), Collision.CreateCopy(), new List<int>(Autotiles), new List<int>(Reliefs), CreateReliefTopCopy());
        }

        public List<object[]> CreateReliefTopCopy()
        {
            List<object[]> list = new List<object[]>();
            for (int i = 0; i < ReliefTop.Count; i++)
            {
                object[] obj = new object[2];
                obj[0] = ReliefTop[i][0];
                int[] texture;
                if (ReliefTop[i][1] == null)
                {
                    texture = null;
                }
                else
                {
                    texture = new int[((int[])ReliefTop[i][1]).Length];
                    for (int j = 0; j < texture.Length; j++)
                    {
                        texture[j] = ((int[])ReliefTop[i][1])[j];
                    }
                }
                obj[1] = texture;
                list.Add(obj);
            }

            return list;
        }

        // -------------------------------------------------------------------
        // GetDefaultTilesets
        // -------------------------------------------------------------------

        public static List<SystemTileset> GetDefaultTilesets()
        {
            List<SystemTileset> list = new List<SystemTileset>();
            list.Add(new SystemTileset(1, "Plains", new SystemGraphic("plains.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8, 8)), new List<int>(new int[] { 1, 2 }), new List<int>(new int[] { 1 }), new List<object[]> {
                new object[] { DrawType.Floors, new int[] { 0, 0, 1, 1 } } }));
            list.Add(new SystemTileset(2, "Beach", new SystemGraphic("beach.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8, 8)), new List<int>(new int[] { }), new List<int>(new int[] { 1, 2 }), new List<object[]> {
                new object[] { DrawType.Floors, new int[] { 4, 0, 1, 1 } },
                new object[] { DrawType.Floors, new int[] { 0, 0, 1, 1 } } }));
            list.Add(new SystemTileset(3, "Halloween", new SystemGraphic("halloween.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8, 8)), new List<int>(), new List<int>(new int[] { 3 }), new List<object[]> {
                new object[] { DrawType.Floors, new int[] { 0, 0, 1, 1 } } }));

            return list;
        }
    }
}
