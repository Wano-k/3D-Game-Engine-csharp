using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemEvent
    {
        public string Name;
        public List<SystemEventPage> Pages;

        [NonSerialized()]
        public int CurrentPage = 0;


        // -------------------------------------------------------------------
        // SYSTEM EVENT PAGE
        // -------------------------------------------------------------------

        #region Event page 

        public class SystemEventPage
        {
            public SystemGraphic Graphic;
            public DrawType GraphicDrawType;


            // -------------------------------------------------------------------
            // Constructors
            // -------------------------------------------------------------------

            public SystemEventPage() : this(SystemGraphic.GetDefaultEventGraphic(), DrawType.None)
            {

            }

            public SystemEventPage(SystemGraphic graphic, DrawType graphicDrawType)
            {
                Graphic = graphic;
                GraphicDrawType = graphicDrawType;
            }

            // -------------------------------------------------------------------
            // CreateCopy
            // -------------------------------------------------------------------

            public SystemEventPage CreateCopy()
            {
                return new SystemEventPage(Graphic.CreateCopy(), GraphicDrawType);
            }
        }

        #endregion


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemEvent(string name) : this(name, new List<SystemEventPage>(new SystemEventPage[] { new SystemEventPage() }))
        {

        }

        public SystemEvent(string name, List<SystemEventPage> pages)
        {
            Name = name;
            Pages = pages;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public SystemEvent CreateCopy()
        {
            List<SystemEventPage> pages = new List<SystemEventPage>();
            for (int i = 0; i < Pages.Count; i++)
            {
                pages.Add(Pages[i].CreateCopy());
            }

            return new SystemEvent(Name, pages);
        }
    }
}
