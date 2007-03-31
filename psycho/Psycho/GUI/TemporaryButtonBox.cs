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
using Gdk;

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

        private NodeStore store = new NodeStore(typeof(PsychoTreeNode));
        private PsychoNodeView outlineView = new PsychoNodeView();

        Entry titleEntry = new Entry();
        Button addSiblingButton = new Button();
        Button addChildButton = new Button();
        Button deleteButton = new Button();
        #endregion

        public TemporaryButtonBox() : base() {

            this.Homogeneous = false;
            this.BorderWidth = 6;

            HButtonBox buttonBox = new HButtonBox();
            ScrolledWindow outlineContainer = new ScrolledWindow();

            outlineView.NodeStore = (store);
            outlineView.NodeSelection.Mode = SelectionMode.Single;
            outlineView.ShowAll();

            outlineContainer.Add(outlineView);

            titleEntry.EditingDone += new EventHandler(titleEntry_EditingDone);
            Console.WriteLine("Title editing done");

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

        public void WireUp(IPsychoControl paramControl, IPsychoModel paramModel)
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

        private void titleEntry_EditingDone(object sender, System.EventArgs args)
        {
            EditTitle(titleEntry.Text);
        }

        public void EditTitle(string paramString)
        {
            Control.RequestSetTitle(paramString);
        }

        private void btnAddChild_Click(object sender, System.EventArgs args)
        {
            Console.WriteLine("Add Child Button clicked");
            AddSubtopic();
        }

        public void AddSubtopic()
        {
            Control.RequestAddSubtopic();
        }

        private void btnAddSibling_Click(object sender, System.EventArgs args)
        {
            Console.WriteLine("Add Sibling Button clicked");
            AddTopic();
        }

        public void AddTopic()
        {
            Control.RequestAddTopic();
        }

        private void btnDelete_Click(object sender, System.EventArgs args)
        {
            Console.WriteLine("Delete Button clicked");
            DeleteTopic();
        }

        public void DeleteTopic()
        {
            Control.RequestDelete();
        }

        public void SetCurrentTopic()
        {
            Control.RequestSetCurrent(outlineView.selectedNode.GUID);
        }

        public void Update(IPsychoModel paramModel)
        {
            store.Clear();
            outlineView.centralNode = new PsychoTreeNode(paramModel.CentralTopic.Title, paramModel.CentralTopic.GUID);
            store.AddNode(outlineView.centralNode);
            AddNodesRecursively(outlineView.centralNode, paramModel.CentralTopic);
            outlineView.ExpandAll();
            titleEntry.Text = paramModel.CurrentTopic.Title;
        }

        public void DisableAddSibling()
        {
            addSiblingButton.Sensitive = (false);
        }

        public void EnableAddSibling()
        {
            addSiblingButton.Sensitive = (true);
        }

        public void ExpandTopic(string paramGuid, bool isExpanded)
        {
            Control.RequestExpand(paramGuid, isExpanded);
        }
    }
}
