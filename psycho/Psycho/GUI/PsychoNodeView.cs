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
using Gtk;

namespace Psycho {

    public partial class PsychoNodeView : NodeView {

        public PsychoNodeView() : base() {

            //TreeViewColumn titleColumn = new TreeViewColumn();
            //titleColumn.Title = "Topic title";
            //CellRendererText titleCell = new CellRendererText();
            //titleColumn.PackStart(titleCell, true);

            //titleCell.Editable = true;
            //titleCell.Edited += new EditedHandler(titleCell_Edited);

//            AppendColumn(titleColumn);

            AppendColumn("Title", new CellRendererText(), "text", 0);
            AppendColumn("GUID", new CellRendererText(), "text", 1);

            Selection.Changed += new System.EventHandler(OnSelectionChanged);
            RowCollapsed += new RowCollapsedHandler(outlineView_RowCollapsed);
            RowExpanded += new RowExpandedHandler(outlineView_RowExpanded);
            KeyPressEvent += new KeyPressEventHandler(outlineView_KeyPressEvent);
            ExpanderColumn.Expand = true;

            NodeSelection.Mode = SelectionMode.Single;
            ShowAll();
        }

        void outlineView_KeyPressEvent(object o, KeyPressEventArgs args)
        {
            string key = args.Event.Key.ToString();
            Console.WriteLine(key);
        }

        void OnSelectionChanged(object sender, System.EventArgs args)
        {
            if (NodeSelection.SelectedNode != null) {
                selectedNode = checked((Psycho.PsychoTreeNode) NodeSelection.SelectedNode);
                Console.WriteLine("Selection changed in the view: " + selectedNode.GUID);
                SetCurrentTopic();
            }
        }

        private void titleCell_Edited(object sender, Gtk.EditedArgs args)
        {
            EditTitle(args.NewText);
        }

        private void outlineView_RowCollapsed(object sender, Gtk.RowCollapsedArgs args)
        {
            TreePath path = args.Path;
            PsychoTreeNode node = checked((Psycho.PsychoTreeNode) store.GetNode(path));
            ExpandTopic(node.GUID, false);
        }

        private void outlineView_RowExpanded(object sender, Gtk.RowExpandedArgs args)
        {
            TreePath path = args.Path;
            PsychoTreeNode node = checked((Psycho.PsychoTreeNode) store.GetNode(path));
            ExpandTopic(node.GUID, true);
        }
    }
}
