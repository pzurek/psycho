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
using System.Collections;
using System.Collections.Generic;
using Gtk;

namespace Psycho {
    ///<summary>
    ///Temporary view.
    ///Buttons creating and deleting topics
    /// and a nodeview to show them and select the current.
    ///</summary>
    public class TemporaryButtonBox : VBox, IPsychoView {
        #region fields
        private IPsychoModel Model;
        private IPsychoControl Control;

        public NodeStore store = new NodeStore(typeof(PsychoTreeNode));
        public NodeView outlineView = new NodeView();
        private PsychoTreeNode centralNode = new PsychoTreeNode("", "");
        private PsychoTreeNode selectedNode = new PsychoTreeNode("", "");

        Entry titleEntry = new Entry();
        Button addSiblingButton = new Button();
        Button addChildButton = new Button();
        Button deleteButton = new Button();
        #endregion

        public TemporaryButtonBox () : base()
        {
            this.Homogeneous = false;
            this.BorderWidth = 6;

            HButtonBox buttonBox = new HButtonBox();
            ScrolledWindow outlineContainer = new ScrolledWindow();

            outlineView.NodeStore = (store);
            outlineView.NodeSelection.Mode = SelectionMode.Single;
            outlineView.AppendColumn("Title", new CellRendererText(), "text", 0);
            outlineView.AppendColumn("GUID", new CellRendererText(), "text", 1);
            outlineView.ShowAll();
            outlineView.Selection.Changed += new System.EventHandler(OnSelectionChanged);
            outlineView.ExpanderColumn.Expand = true;
            outlineContainer.Add(outlineView);

            addSiblingButton.Label = ("Add Sibling");
            addSiblingButton.Clicked += new EventHandler(btnAddSibling_Click);

            addChildButton.Label = ("Add Child");
            addChildButton.Clicked += new EventHandler(btnAddChild_Click);
            Console.WriteLine("Add Child Button");
            deleteButton.Label = ("Delete");
            deleteButton.Clicked += new EventHandler(btnDelete_Click);
            Console.WriteLine("Delete Button");

            buttonBox.Homogeneous = true;
            buttonBox.Layout = (Gtk.ButtonBoxStyle.End);
            buttonBox.Spacing = 6;
            buttonBox.PackStart(addSiblingButton, false, true, 6);
            buttonBox.PackStart(addChildButton, false, true, 6);
            buttonBox.PackStart(deleteButton, false, true, 6);

            this.PackStart(titleEntry, false, false, 6);
            this.PackStart(buttonBox, false, false, 6);
            this.PackStart(outlineContainer, true, true, 6);
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
            this.Update(Model);
        }

        private void btnAddChild_Click (object sender, System.EventArgs e)
        {
            Console.WriteLine("Add Child Button clicked");
            AddSubtopic();
        }

        public void AddSubtopic ()
        {
            this.Control.RequestAddSubtopic();
        }

        private void btnAddSibling_Click (object sender, System.EventArgs e)
        {
            Console.WriteLine("Add Sibling Button clicked");
            AddTopic();
        }

        public void AddTopic ()
        {
            this.Control.RequestAddTopic();
        }

        private void btnDelete_Click (object sender, System.EventArgs e)
        {
            Console.WriteLine("Delete Button clicked");
            DeleteTopic();
        }

        public void DeleteTopic ()
        {
            this.Control.RequestDelete();
        }

        public void Update (IPsychoModel paramModel)
        {
            store.Clear();
            centralNode = new PsychoTreeNode(paramModel.CentralTopic.Title, paramModel.CentralTopic.GUID);
            store.AddNode(centralNode);
            AddNodesRecursively(centralNode, paramModel.CentralTopic);
            outlineView.ExpandAll();
            titleEntry.Text = paramModel.CurrentTopic.Title;
        }

        private void AddNodesRecursively (PsychoTreeNode paramNode, Topic paramTopic)
        {
            foreach (Topic child in paramTopic.Subtopics) {
                PsychoTreeNode newNode = new PsychoTreeNode(child.Title, child.GUID);
                paramNode.AddChild(newNode);
                newNode.Parent = paramNode;
                AddNodesRecursively(newNode, child);
            }
        }

        public void SelectNodeByGUID (string paramGuid)
        {
            foreach (PsychoTreeNode node in outlineView) {
                this.outlineView.NodeSelection.SelectNode(node);
                Console.WriteLine("Node found :" + node.GUID);
                if (node.GUID == paramGuid) break;
            }
        }

        public void SetCurrentTopic ()
        {
            this.Control.RequestSetCurrent(selectedNode.GUID);
        }

        void OnSelectionChanged (object sender, System.EventArgs args)
        {
            if (this.outlineView.NodeSelection.SelectedNode != null) {
                selectedNode = checked((Psycho.PsychoTreeNode) outlineView.NodeSelection.SelectedNode);
                Console.WriteLine("Selection changed in the view: " + selectedNode.GUID);
                SetCurrentTopic();
            }
        }

        public void DisableAddSibling ()
        {
            addSiblingButton.Visible = (false);
        }

        public void EnableAddSibling ()
        {
            addSiblingButton.Visible = (true);
        }
    }
}
