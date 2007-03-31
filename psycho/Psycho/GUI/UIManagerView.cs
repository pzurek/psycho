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
using Gtk;

namespace Psycho {
    public class UIManagerView : IPsychoView {

        private IPsychoModel Model;
        private IPsychoControl Control;

        public UIManager uiManager = new UIManager();

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
        "    <toolitem name='open' action='Open'/>" +
        "    <toolitem name='quit' action='Quit'/>" +
        "    <separator action='Sep1'/>" +
        "  </toolbar>" +
        "</ui>";

        public UIManagerView ()
        {
            ActionEntry[] entries = new ActionEntry[] {
                new ActionEntry ("FileMenu", null, "_File", null, null, null),
                new ActionEntry ("EditMenu", null, "_Edit", null, null, null),
                new ActionEntry ("HelpMenu", null, "_Help", null, null, null),

                new ActionEntry ("New", Stock.New, "_New", "<control>N", "Create a new file", new EventHandler (ActionActivated)),
                new ActionEntry ("Open", Stock.Open, "_Open", "<control>O", "Open a file", new EventHandler (ActionActivated)),
                new ActionEntry ("Save", Stock.Save, "_Save", "<control>S", "Save current file", new EventHandler (ActionActivated)),
                new ActionEntry ("SaveAs", Stock.SaveAs, "Save _As", null, "Save to a file", new EventHandler (ActionActivated)),
                new ActionEntry ("Quit", Stock.Quit, "_Quit", "<control>Q", "Quit", new EventHandler (ActionActivated)),
                
                new ActionEntry ("AddTopic", Stock.Add, "Add _Topic", "Return", "Add Topic", new EventHandler (ActionActivated)),
                new ActionEntry ("AddSubtopic", Stock.GoDown, "Add _Subtopic", "Insert", "Add Subtopic", new EventHandler (ActionActivated)),
                new ActionEntry ("Delete", Stock.Delete, "_Delete", "Delete", "Add Topic", new EventHandler (ActionActivated)),
                
                new ActionEntry ("Copy", Stock.Copy, "Copy", "<control>C", "Copy to clipboard", new EventHandler (ActionActivated)),
                new ActionEntry ("Cut", Stock.Cut, "Cut", "<control>X", "Cut to clipboard", new EventHandler (ActionActivated)),
                new ActionEntry ("Paste", Stock.Paste, "Paste", "<control>V", "Paste from clipboard", new EventHandler (ActionActivated)),
                
                new ActionEntry ("About", null, "_About", "<control>A", "About", new EventHandler (ActionActivated)),
            };

            ActionGroup actions = new ActionGroup("group");
            actions.Add(entries);

            uiManager.InsertActionGroup(actions, 0);
            uiManager.AddUiFromString(ui);
        }

        private void ActionActivated (object sender, EventArgs a)
        {
            Action action = sender as Action;
            Console.WriteLine("Action \"{0}\" activated", action.Name);
        }

        #region IPsychoView Members

        public void WireUp (IPsychoControl paramControl, IPsychoModel paramModel)
        {
            if (Model != null) {
                Model.RemoveObserver(this);
            }

            Model = paramModel;
            Control = paramControl;

            Control.SetModel(Model);
            Control.SetView(this);
            Model.AddObserver(this);
            Update(Model);
        }

        public void Update (IPsychoModel paramModel)
        {
        }

        public void AddTopic ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddSubtopic ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DeleteTopic ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ExpandTopic (string paramGuid, bool isExpanded)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void EditTitle (string Title)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetCurrentTopic ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DisableAddSibling ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DisableDelete ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void EnableAddSibling ()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void EnableDelete ()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion
    }
}