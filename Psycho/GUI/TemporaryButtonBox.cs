
using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;

namespace Psycho
{
	///<summary>
	///Temporary view.
	///Buttons creating and deleting topics
	/// and a nodeview to show them and select the current.
	///</summary>
	public class TemporaryButtonBox : VBox , IPsychoView
	{
		private IPsychoModel Model;
		private IPsychoControl Control;
		
		public NodeStore store = new NodeStore(typeof (PsychoTreeNode));
		public NodeView outlineView = new NodeView();
        private PsychoTreeNode centralNode = new PsychoTreeNode("" , "", "");
        private PsychoTreeNode selectedNode = new PsychoTreeNode("" , "", "");
  
		Button addSiblingButton = new Button();
	    Button addChildButton = new Button();
		Button deleteButton = new Button();
	    
		public TemporaryButtonBox() : base()
		{
			this.Homogeneous = false;
			this.BorderWidth = 6;

			Entry titleEntry = new Entry();
			HButtonBox buttonBox = new HButtonBox();
            ScrolledWindow outlineContainer = new ScrolledWindow();
            
			outlineView.NodeStore = (store);
            outlineView.NodeSelection.Mode = SelectionMode.Single;
            outlineView.AppendColumn("Title", new CellRendererText(), "text", 0);
            outlineView.AppendColumn("GUID", new CellRendererText(), "text", 1);
            outlineView.ShowAll();
            outlineView.Selection.Changed += new System.EventHandler (OnSelectionChanged);

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
		
		public void WireUp(IPsychoControl paramControl, IPsychoModel paramModel)
		{
			if(Model != null)
			{
				Model.RemoveObserver(this);
			}

			Model = paramModel;
			Control = paramControl;

			Control.SetModel(Model);
			Control.SetView(this);
			Model.AddObserver(this);
            this.Update(Model);
		}
		
		private void btnAddChild_Click(object sender, System.EventArgs e)
		{
			Console.WriteLine("Add Child Button clicked");
			AddSubtopic();
		}
		
		public void AddSubtopic()
		{
			this.Control.RequestAddSubtopic();
		}
		
		private void btnAddSibling_Click(object sender, System.EventArgs e)
		{
			Console.WriteLine("Add Sibling Button clicked");
			AddTopic();
		}
		
		public void AddTopic()
		{
			this.Control.RequestAddTopic();
		}
		
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			Console.WriteLine("Delete Button clicked");
			Delete();
		}
		
		public void Delete()
		{
			this.Control.RequestDelete();
		}
		
		public void Update(IPsychoModel paramModel)
		{
            store.Clear();
            centralNode = new PsychoTreeNode(paramModel.CentralTopic.Title, paramModel.CentralTopic.GUID, "Central topic");
            store.AddNode(centralNode);
            this.AddNodesRecursively(/*centralNode, paramModel.CentralTopic*/);
            this.outlineView.ExpandAll();
            this.SelectNodeByGUID(paramModel.CurrentTopic.GUID);
		}

        private void AddNodesRecursively(/*PsychoTreeNode paramNode, Topic paramTopic*/)
        {
            PsychoTreeNode currentNode = (this.centralNode);
            PsychoTreeNode currentChild = new PsychoTreeNode("", "", "");

            Queue<Topic> remaining = new Queue<Topic>();
            remaining.Enqueue(this.Model.CentralTopic);
            while (remaining.Count > 0)
            {
                Topic topic = remaining.Dequeue();
                foreach (Topic child in topic.Subtopics)
                {
                    remaining.Enqueue(child);
                    PsychoTreeNode newNode = new PsychoTreeNode(child.Title, child.GUID, child.Parent.GUID);
                    currentNode.AddChild(newNode);
                    currentChild = newNode;
                }
                currentNode = currentChild;
            }
        }

        public void SelectNodeByGUID(string paramGuid)
        {
            foreach (PsychoTreeNode node in this.outlineView)
            {
                this.outlineView.NodeSelection.SelectNode(node);
                Console.WriteLine("Node found :" + node.GUID);
                if (node.GUID == paramGuid) break;
            }
        }

		public void SetCurrentTopic()
		{
            this.Control.RequestSetCurrent(selectedNode.GUID);
            Console.WriteLine("Slection request sent to controller");
		}
		
        void OnSelectionChanged (object sender, System.EventArgs args)
        {
	       	selectedNode = checked((Psycho.PsychoTreeNode)outlineView.NodeSelection.SelectedNode);
            Console.WriteLine("Selection changed in the view: " + selectedNode.GUID);
            SetCurrentTopic();
        }
		
		public void DisableAddSibling()
		{
			addSiblingButton.Visible = (false);
		}
		
		public void EnableAddSibling()
		{
			addSiblingButton.Visible = (true);
		}
	}
}
