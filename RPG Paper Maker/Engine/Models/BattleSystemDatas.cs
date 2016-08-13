using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class BattleSystemDatas
    {
        public static int MAX_WEAPONS_KIND = 9999;
        public static int MAX_ARMORS_KIND = 9999;

        // ListBoxes
        public List<SystemElement> Elements = new List<SystemElement>();
        public List<SystemStatistics> Statistics = new List<SystemStatistics>();
        public List<SuperListItemName> WeaponsKind = new List<SuperListItemName>();
        public List<SuperListItemName> ArmorsKind = new List<SuperListItemName>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public BattleSystemDatas()
        {
            // Get defaults lists
            Elements = SystemElement.GetDefaultElements();
            Statistics = SystemStatistics.GetDefaultStatistics();

            // Weapons kind
            WeaponsKind.Add(new SuperListItemName(1, WANOK.GetDefaultNames("Sword")));
            WeaponsKind.Add(new SuperListItemName(2, WANOK.GetDefaultNames("Axe")));
            WeaponsKind.Add(new SuperListItemName(3, WANOK.GetDefaultNames("Spear")));
            WeaponsKind.Add(new SuperListItemName(4, WANOK.GetDefaultNames("Staff")));
            WeaponsKind.Add(new SuperListItemName(5, WANOK.GetDefaultNames("Bow")));
            WeaponsKind.Add(new SuperListItemName(6, WANOK.GetDefaultNames("Firearm")));

            // Armors kind
            ArmorsKind.Add(new SuperListItemName(1, WANOK.GetDefaultNames("Helmet")));
            ArmorsKind.Add(new SuperListItemName(2, WANOK.GetDefaultNames("Cap")));
            ArmorsKind.Add(new SuperListItemName(3, WANOK.GetDefaultNames("Mail")));
            ArmorsKind.Add(new SuperListItemName(4, WANOK.GetDefaultNames("Vest")));
            ArmorsKind.Add(new SuperListItemName(5, WANOK.GetDefaultNames("Vambraces")));
            ArmorsKind.Add(new SuperListItemName(6, WANOK.GetDefaultNames("Guards")));
            ArmorsKind.Add(new SuperListItemName(7, WANOK.GetDefaultNames("Greaves")));
            ArmorsKind.Add(new SuperListItemName(8, WANOK.GetDefaultNames("Leggings")));
        }
    }
}
