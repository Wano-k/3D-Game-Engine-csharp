using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemStatistics : SuperListItem
    {
        public static int MAX_STATISTICS = 999;
        public Dictionary<string, string> Names;
        public SystemGraphic Bar;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemStatistics(int id) : this(id, WANOK.GetDefaultNames(), new SystemGraphic(GraphicKind.Bar))
        {

        }

        public SystemStatistics(int id, Dictionary<string, string> names, SystemGraphic bar)
        {
            Id = id;
            Names = names;
            Bar = bar;
            SetName();
        }

        public void SetName()
        {
            Name = Names[WANOK.CurrentLang];
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            object[] newOptions = new object[Bar.Options.Length];
            newOptions[0] = new int[] { ((int[])Bar.Options[0])[0], ((int[])Bar.Options[0])[1], ((int[])Bar.Options[0])[2], ((int[])Bar.Options[0])[3] };
            SystemGraphic newGraphic = Bar.CreateCopy();
            newGraphic.Options = newOptions;

            return new SystemStatistics(Id, new Dictionary<string, string>(Names), newGraphic);
        }

        // -------------------------------------------------------------------
        // GetDefaultStatistics
        // -------------------------------------------------------------------

        public static List<SystemStatistics> GetDefaultStatistics()
        {
            List<SystemStatistics> list = new List<SystemStatistics>();
            list.Add(new SystemStatistics(1, WANOK.GetDefaultNames("HP"), new SystemGraphic(GraphicKind.Bar, new object[] { new int[] { 0, 0, 1, 1 } })));

            return list;
        }
    }
}
