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

        public PsychoOutlineView()
            : base() { }

            private IPsychoModel Model;
            private IPsychoControl Control;

            private NodeStore store = new NodeStore(typeof(PsychoTreeNode));
            private NodeView outlineView = new NodeView();

            public void Update(IPsychoModel paramModel)
        {
            store.Clear();
            centralNode = new PsychoTreeNode(paramModel.CentralTopic.Title, paramModel.CentralTopic.GUID);
            store.AddNode(centralNode);
            AddNodesRecursively(centralNode, paramModel.CentralTopic);
            ExpandAll();
            ListAllNodes();
            Realize();
            Show();
        }

        public void AddTopic()
        {
        }

        public void AddSubtopic()
        {
        }

        public void DeleteTopic()
        {
        }

        public void ExpandTopic(string paramGuid, bool isExpanded)
        {
        }

        public void EditTitle(string Title)
        {
        }

        public void SetCurrentTopic()
        {
        }

        public void DisableAddSibling()
        {
        }

        public void EnableAddSibling()
        {
        }

        public void DisableDelete()
        {
        }

        public void EnableDelete()
        {
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

        private void AddNodesRecursively(PsychoTreeNode paramNode, Topic paramTopic)
        {
            foreach (Topic child in paramTopic.Subtopics) {
                PsychoTreeNode newNode = new PsychoTreeNode(child.Title, child.GUID);
                paramNode.AddChild(newNode);
                newNode.Parent = paramNode;
                AddNodesRecursively(newNode, child);
            }
        }

        public void SelectNodeByGUID(string paramGuid)
        {
            foreach (PsychoTreeNode node in this) {
                this.NodeSelection.SelectNode(node);
                Console.WriteLine("Node found :" + node.GUID);
                if (node.GUID == paramGuid) break;
            }
        }

        private void ListAllNodes()
        {
            foreach (PsychoTreeNode node in store)
                Console.WriteLine(node.Title);
        }
    }
}
