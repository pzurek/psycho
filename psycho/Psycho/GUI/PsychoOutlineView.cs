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
using System.Collections.Generic;
using System.Text;
using Gtk;

namespace Psycho {

    public class PsychoOutlineView : ScrolledWindow, IPsychoView {

        private IPsychoModel Model;
        private IPsychoControl Control;

        private TreeStore store = new TreeStore(typeof(Topic));
        private TreeView outlineView = new TreeView();

        private TreeIter selectedNode;
        private Topic selectedTopic;

        //private Topic SelectedTopic {
        //    get {
        //        TreeModel model;
        //        Topic selected;
        //        if (outlineView.Selection.GetSelected(out model, out selectedNode)) {
        //            selected = (Topic) model.GetValue(selectedNode, 0);
        //            return selected;
        //        }
        //    }
        //}

        private bool isEdited;
        private TreeViewColumn titleColumn = new TreeViewColumn();
        private TreeViewColumn guidColumn = new TreeViewColumn();

        public PsychoOutlineView ()
            : base()
        {
            titleColumn.Title = "Topic title";
            CellRendererText titleCell = new CellRendererText();
            titleCell.Editable = true;
            titleCell.Edited += new EditedHandler(titleCell_Edited);
            titleCell.EditingStarted += new EditingStartedHandler(titleCell_EditingStarted);
            titleCell.EditingCanceled += new EventHandler(titleCell_EditingCanceled);
            titleColumn.PackStart(titleCell, true);
            titleColumn.AddAttribute(titleCell, "text", 0);
            titleColumn.SetCellDataFunc(titleCell, new Gtk.TreeCellDataFunc(RenderTitle));

            guidColumn.Title = "Topic GUID";
            CellRendererText guidCell = new CellRendererText();
            guidColumn.PackStart(guidCell, false);
            guidColumn.AddAttribute(guidCell, "text", 1);
            guidColumn.SetCellDataFunc(guidCell, new Gtk.TreeCellDataFunc(RenderGuid));

            outlineView.Model = store;
            outlineView.AppendColumn(titleColumn);
            outlineView.AppendColumn(guidColumn);

            outlineView.Selection.Changed += new System.EventHandler(OnSelectionChanged);
            outlineView.Focused += new FocusedHandler(outlineView_Focused);
            outlineView.RowCollapsed += new RowCollapsedHandler(outlineView_RowCollapsed);
            outlineView.RowExpanded += new RowExpandedHandler(outlineView_RowExpanded);
            outlineView.KeyReleaseEvent += new KeyReleaseEventHandler(outlineView_KeyReleaseEvent);
            outlineView.RowActivated += new RowActivatedHandler(outlineView_RowActivated);

            outlineView.ExpanderColumn.Expand = true;
            this.VscrollbarPolicy = PolicyType.Always;
            Add(outlineView);
            ShowAll();
        }

        void titleCell_EditingCanceled (object sender, EventArgs args)
        {
            isEdited = false;
        }

        void titleCell_EditingStarted (object sender, EditingStartedArgs args)
        {
            isEdited = true;
        }

        void outlineView_KeyReleaseEvent (object sender, KeyReleaseEventArgs args)
        {
            string key = args.Event.Key.ToString();
            Console.WriteLine(key);
            switch (key) {
            case "Return":
            if (isEdited) return;
            else {
                AddTopic();
                return;
            }
            case "Insert":
            AddSubtopic();
            return;
            case "Delete":
            DeleteTopic();
            return;
            default: break;
            }
        }

        void outlineView_RowActivated (object sender, RowActivatedArgs args)
        {
            //store = (TreeStore) outlineView.Model;
            //TreeIter iter;
            //store.GetIter(out iter, args.Path);
            //Topic iterTopic = (Topic) store.GetValue(iter, 0);
            Console.WriteLine("Row activated");
        }

        void outlineView_Focused (object sender, FocusedArgs args)
        {
            Console.WriteLine("OutlineView Focused");
        }

        private void RenderTitle (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            Topic topic = (Topic) model.GetValue(iter, 0);
            (cell as CellRendererText).Text = topic.Title;
        }

        private void RenderGuid (TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            Topic topic = (Topic) model.GetValue(iter, 0);
            (cell as CellRendererText).Text = topic.GUID;
        }

        public void Update (IPsychoModel paramModel)
        {
            store.Clear();
            TreeIter centralNode = store.AppendValues(paramModel.CentralTopic);
            AddNodesRecursively(store, centralNode, paramModel.CentralTopic);
            outlineView.ExpandAll();
            outlineView.ScrollToCell(store.GetPath(selectedNode), titleColumn, true, 0, 0);
        }

        public void AddTopic ()
        {
            Control.RequestAddTopic();
        }

        public void AddSubtopic ()
        {
            Control.RequestAddSubtopic();
        }

        public void DeleteTopic ()
        {
            Control.RequestDelete();
        }

        public void ExpandTopic (string paramGuid, bool isExpanded)
        {
            Control.RequestExpand(paramGuid, isExpanded);
        }

        public void EditTitle (string Title)
        {
            Control.RequestSetTitle(Title);
        }

        public void SetCurrentTopic ()
        {
            Control.RequestSetCurrent(selectedTopic.GUID);
        }

        public void DisableAddSibling ()
        {
        }

        public void EnableAddSibling ()
        {
        }

        public void DisableDelete ()
        {
        }

        public void EnableDelete ()
        {
        }

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

        private void AddNodesRecursively (TreeStore paramStore, TreeIter paramParent, Topic paramTopic)
        {

            foreach (Topic child in paramTopic.Subtopics) {
                TreeIter kid = paramStore.AppendValues(paramParent, child);

                //if (paramTopic.IsExpanded) {
                //    outlineView.ExpandRow(paramStore.GetPath(paramParent), true);
                //}
                //else
                //    outlineView.ExpandRow(paramStore.GetPath(paramParent), false);

                if (Model.CurrentTopic == child) {
                    selectedNode = kid;
                    Console.WriteLine("Current iter set to: " + child.Title);
                }

                AddNodesRecursively(paramStore, kid, child);
            }
        }

        public void SelectNodeByGUID (string paramGuid)
        {

        }

        void OnSelectionChanged (object sender, System.EventArgs args)
        {
            TreeModel model;

            if (((TreeSelection) sender).GetSelected(out model, out selectedNode))
                selectedTopic = (Topic) model.GetValue(selectedNode, 0);
            if (selectedTopic != Model.CurrentTopic) SetCurrentTopic();

        }

        private void titleCell_Edited (object sender, Gtk.EditedArgs args)
        {
            string titleText = args.NewText;
            EditTitle(titleText);
        }

        private void outlineView_RowCollapsed (object sender, Gtk.RowCollapsedArgs args)
        {
            TreeIter expanded = args.Iter;
            Topic expandedTopic = (Topic) store.GetValue(expanded, 0);
            ExpandTopic(expandedTopic.GUID, false);
        }

        private void outlineView_RowExpanded (object sender, Gtk.RowExpandedArgs args)
        {
            TreeIter expanded = args.Iter;
            Topic expandedTopic = (Topic) store.GetValue(expanded, 0);
            ExpandTopic(expandedTopic.GUID, true);
        }
    }
}
