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
            public EventTrigger Trigger;
            public NTree<EventCommand> CommandsTree;

            
            public class PageOptions
            {
                public bool MoveAnimation;
                public bool StopAnimation;
                public bool DirectionFix;
                public bool Through;
                public bool SetWithCamera;

                public PageOptions() : this(false, false, false, false, false)
                {

                }

                public PageOptions(bool moveAnimation, bool stopAnimation, bool directionFix, bool through, bool setWithCamera)
                {
                    MoveAnimation = moveAnimation;
                    StopAnimation = stopAnimation;
                    DirectionFix = directionFix;
                    Through = through;
                    SetWithCamera = setWithCamera;
                }

                public PageOptions CreateCopy()
                {
                    return new PageOptions(MoveAnimation, StopAnimation, DirectionFix, Through, SetWithCamera);
                }
            }

            public PageOptions Options;


            // -------------------------------------------------------------------
            // Constructors
            // -------------------------------------------------------------------

            public SystemEventPage() : this(SystemGraphic.GetDefaultEventGraphic(), DrawType.None, new PageOptions(), EventTrigger.ActionButton, GetDefaultTreeCommands())
            {

            }

            public SystemEventPage(SystemGraphic graphic, DrawType graphicDrawType, PageOptions options, EventTrigger trigger, NTree<EventCommand> commandsTree)
            {
                Graphic = graphic;
                GraphicDrawType = graphicDrawType;
                Options = options;
                Trigger = trigger;
                CommandsTree = commandsTree;
            }

            // -------------------------------------------------------------------
            // CreateCopy
            // -------------------------------------------------------------------

            public SystemEventPage CreateCopy()
            {
                NTree<EventCommand> tree = new NTree<EventCommand>(null);
                CopyTreeNode(tree, CommandsTree);

                return new SystemEventPage(Graphic.CreateCopy(), GraphicDrawType, Options.CreateCopy(), Trigger, tree);
            }

            // -------------------------------------------------------------------
            // CopyTreeNode
            // -------------------------------------------------------------------

            public void CopyTreeNode(NTree<EventCommand> tree, NTree<EventCommand> treeToCopy)
            {
                foreach (NTree<EventCommand> childToCopy in treeToCopy.GetChildren())
                {
                    CopyTreeNode(tree.AddChildData(childToCopy.Data.CreateCopy()), childToCopy);
                }
            }

            // -------------------------------------------------------------------
            // GetDefaultTreeCommands
            // -------------------------------------------------------------------

            public static NTree<EventCommand> GetDefaultTreeCommands()
            {
                NTree<EventCommand> tree = new NTree<EventCommand>(null);
                tree.AddChildData(new EventCommand());

                return tree;
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

        // -------------------------------------------------------------------
        // CreateNewPage
        // -------------------------------------------------------------------

        public void CreateNewPage(SystemEventPage page = null)
        {
            Pages.Insert(++CurrentPage, page == null ? new SystemEventPage() : page);
        }
    }
}
