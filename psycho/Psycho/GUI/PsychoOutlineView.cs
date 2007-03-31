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

        private TreeStore store = new TreeStore(typeof(PsychoTreeNode));
        private TreeView outlineView = new TreeView();

        //private TreeIter centralNode;
        private TreeIter selectedNode;
        private Topic selectedTopic;

        public PsychoOutlineView ()
            : base()
        {
            TreeViewColumn titleColumn = new TreeViewColumn();
            titleColumn.Title = "Topic title";
            CellRendererText titleCell = new CellRendererText();
            titleCell.Editable = true;
            titleColumn.PackStart(titleCell, true);
            titleColumn.AddAttribute(titleCell, "text", 0);
            titleColumn.SetCellDataFunc(titleCell, new Gtk.TreeCellDataFunc(RenderTitle));

            TreeViewColumn guidColumn = new TreeViewColumn();
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
            outlineView.KeyPressEvent += new KeyPressEventHandler(outlineView_KeyPressEvent);

            outlineView.ExpanderColumn.Expand = true;
            //outlineView.ExpandAll();
            this.VscrollbarPolicy = PolicyType.Always;
            Add(outlineView);
            ShowAll();
        }

        void outlineView_Focused (object sender, FocusedArgs args)
        {
            Update(Model);
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
        }

        public void AddTopic ()
        {

        }

        public void AddSubtopic ()
        {
        }

        public void DeleteTopic ()
        {
        }

        public void ExpandTopic (string paramGuid, bool isExpanded)
        {
            this.Control.RequestExpand(paramGuid, isExpanded);
        }

        public void EditTitle (string Title)
        {
        }

        public void SetCurrentTopic ()
        {
            this.Control.RequestSetCurrent(selectedTopic.GUID);
            Console.WriteLine("Slection request: " + selectedTopic.GUID);
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
                if (paramTopic.IsExpanded == true) outlineView.ExpandRow(paramStore.GetPath(paramParent), true);
                AddNodesRecursively(paramStore, kid, child);
            }
        }

        public void SelectNodeByGUID (string paramGuid)
        {

        }

        void outlineView_KeyPressEvent (object sender, KeyPressEventArgs args)
        {
            string key = args.Event.Key.ToString();
            Console.WriteLine(key);
        }

        void OnSelectionChanged (object sender, System.EventArgs args)
        {
            TreeModel model;

            if (((TreeSelection) sender).GetSelected(out model, out selectedNode)) {
                selectedTopic = (Topic) model.GetValue(selectedNode, 0);
                SetCurrentTopic();
            }
        }

        private void titleCell_Edited (object sender, Gtk.EditedArgs args)
        {
            EditTitle(args.NewText);
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
