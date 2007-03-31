// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using Gdk;
using Gtk;

namespace Psycho {

    public class UIManagerView : IView {

        private IModel Model;
        private IControl Control;

        public UIManager uiManager = new UIManager ();
        
        const string ui =
        "<ui>" +
        "  <menubar name='MenuBar'>" +
        "    <menu action='FileMenu'>" +
        "      <menuitem action='New'/>" +
        "      <menuitem action='Open'/>" +
        "      <menuitem action='Save'/>" +
        "      <menuitem action='SaveAs'/>" +
        "      <separator/>" +
        "      <menuitem action='Quit'/>" +
        "    </menu>" +
        "    <menu action='EditMenu'>" +
        "      <menuitem action='AddTopic'/>" +
        "      <menuitem action='AddSubtopic'/>" +
        "      <menuitem action='Delete'/>" +
        "      <menuitem action='Relation'/>" +
        "      <menuitem action='Callout'/>" +
        "      <menuitem action='Border'/>" +
        "      <separator/>" +
        "      <menuitem action='Copy'/>" +
        "      <menuitem action='Cut'/>" +
        "      <menuitem action='Paste'/>" +
        "    </menu>" +
        "    <menu action='HelpMenu'>" +
        "      <menuitem action='About'/>" +
        "    </menu>" +
        "  </menubar>" +
        "  <toolbar  name='ToolBar'>" +
        "    <toolitem name='new' action='New'/>" +
        "    <toolitem name='open' action='Open'/>" +
        "    <toolitem name='save' action='Save'/>" +
        "    <separator action='Sep1'/>" +
        "    <toolitem name='addtopic' action='AddTopic'/>" +
        "    <toolitem name='addsubtopic' action='AddSubtopic'/>" +
        "    <toolitem name='delete' action='Delete'/>" +
        "    <toolitem name='relation' action='Relation'/>" +
        "    <toolitem name='callout' action='Callout'/>" +
        "    <toolitem name='border' action='Border'/>" +
        "  </toolbar>" +
        "</ui>";

        public UIManagerView ()
        {
            //BuildIcons ();

            ActionEntry[] entries = new ActionEntry[] {
                new ActionEntry ("FileMenu", null, "_File", null, null, null),
                new ActionEntry ("EditMenu", null, "_Edit", null, null, null),
                new ActionEntry ("HelpMenu", null, "_Help", null, null, null),

                new ActionEntry ("New", Stock.New, "_New", "<control>N", "Create a new file", new EventHandler (ActionActivated)),
                new ActionEntry ("Open", Stock.Open, "_Open", "<control>O", "Open a file", new EventHandler (ActionActivated)),
                new ActionEntry ("Save", Stock.Save, "_Save", "<control>S", "Save current file", new EventHandler (ActionActivated)),
                new ActionEntry ("SaveAs", Stock.SaveAs, "Save _As", null, "Save to a file", new EventHandler (ActionActivated)),
                new ActionEntry ("Quit", Stock.Quit, "_Quit", "<control>Q", "Quit", new EventHandler (ActionActivated)),
                
                new ActionEntry ("AddTopic", "psycho-topic", "_Topic", "Return", "Create a new topic on a current level", new EventHandler (ActionActivated)),
                new ActionEntry ("AddSubtopic", "psycho-subtopic", "_Subtopic", "Insert", "Create a new topic as a child for currently selected topic", new EventHandler (ActionActivated)),
                new ActionEntry ("Delete", "psycho-delete", "_Delete", "Delete", "Delete currently selected topic", new EventHandler (ActionActivated)),
                new ActionEntry ("Relation", "psycho-relation", "_Relation", "", "Create a relation between two existing elements", new EventHandler (ActionActivated)),
                new ActionEntry ("Callout", "psycho-callout", "_Callout", "", "Create a callout topic for currently selected element", new EventHandler (ActionActivated)),
                new ActionEntry ("Border", "psycho-border", "_Border", "", "Add Border", new EventHandler (ActionActivated)),
                
                new ActionEntry ("Copy", Stock.Copy, "Copy", "<control>C", "Copy to clipboard", new EventHandler (ActionActivated)),
                new ActionEntry ("Cut", Stock.Cut, "Cut", "<control>X", "Cut to clipboard", new EventHandler (ActionActivated)),
                new ActionEntry ("Paste", Stock.Paste, "Paste", "<control>V", "Paste from clipboard", new EventHandler (ActionActivated)),
                
                new ActionEntry ("About", null, "_About", "<control>A", "About", new EventHandler (ActionActivated)),
            };

            ActionGroup actions = new ActionGroup ("group");
            actions.Add (entries);

            uiManager.InsertActionGroup (actions, 0);
            uiManager.AddUiFromString (ui);
            uiManager.AddTearoffs = true;
        }

        static void BuildIcons ()
        {
            Gdk.Pixbuf topicIcon = Gdk.Pixbuf.LoadFromResource ("psycho-topic.png");
            Gdk.Pixbuf subtopicIcon = Gdk.Pixbuf.LoadFromResource ("psycho-subtopic.png");
            Gdk.Pixbuf deleteIcon = Gdk.Pixbuf.LoadFromResource ("psycho-delete.png");

            IconFactory factory = new IconFactory ();
            factory.Add ("psycho-topic", new IconSet (topicIcon));
            factory.Add ("psycho-subtopic", new IconSet (subtopicIcon));
            factory.Add ("psycho-delete", new IconSet (deleteIcon));
            factory.AddDefault ();

            StockManager.Add (new StockItem ("psycho-topic", "Add Topic", 0, Gdk.ModifierType.Button1Mask, ""));
            StockManager.Add (new StockItem ("psycho-subtopic", "Add Subtopic", 0, Gdk.ModifierType.Button1Mask, ""));
            StockManager.Add (new StockItem ("psycho-delete", "Delete Topic", 0, Gdk.ModifierType.Button1Mask, ""));
        }

        private void ActionActivated (object sender, EventArgs args)
        {
            Action action = sender as Action;
            Console.WriteLine ("Action \"{0}\" activated", action.AccelPath);
            
            switch (action.Name) {
            case "AddTopic":
                AddTopic ();
                return;
            case "AddSubtopic":
                AddSubtopic ();
                return;
            case "Delete":
                DeleteTopic ();
                return;
            default: break;
            }
        }

        #region IView Members

        public void WireUp (IControl paramControl, IModel paramModel)
        {
            if (Model != null) {
                Model.RemoveObserver (this);
            }

            Model = paramModel;
            Control = paramControl;

            Control.SetModel (Model);
            Control.SetView (this);
            Model.AddObserver (this);
            Update (Model);
        }

        public void CheckButtonsLegal ()
        {
            if (Model.CurrentTopic == Model.CentralTopic) {
                DisableAddSibling ();
                DisableDelete ();
            }
            else {
                EnableAddSibling ();
                EnableDelete ();
            }
        }

        public void Update (IModel paramModel)
        {
            CheckButtonsLegal ();
        }

        public void AddTopic ()
        {
            Control.RequestAddTopic ();
        }

        public void AddSubtopic ()
        {
            Control.RequestAddSubtopic ();
        }

        public void DeleteTopic ()
        {
            Control.RequestDelete ();
        }

        public void ExpandTopic (string paramGuid, bool isExpanded)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void EditTitle (string Title)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void SetCurrentTopic ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void TriggerEdit (bool editPending)
        {
        }

        public void DisableAddSibling ()
        {
        }

        public void DisableDelete ()
        {
            if (uiManager.GetAction ("<Actions>/group/Delete") != null) {
                Action deleteAction = uiManager.GetAction ("<Actions>/group/Delete");
                deleteAction.Sensitive = false;
            }
                
        }

        public void EnableAddSibling ()
        {
        }

        public void EnableDelete ()
        {
        }

        public void CommitChange (Topic paramTopic)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        #endregion
    }
}
