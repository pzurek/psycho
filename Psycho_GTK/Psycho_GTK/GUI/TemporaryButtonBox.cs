
using System;
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
		private PsychoTreeNode selectedNode;
  
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
			paramModel.CentralTopic.ForEach(delegate (Topic topic)
			{
				PsychoTreeNode node = new PsychoTreeNode(topic.Title, topic.GUID);
			    this.store.AddNode(node);
			} );
//            this.outlineView.Realize();
		}
		
		public void SetCurrentTopic()
		{
            this.Control.RequestSetCurrent(selectedNode.GUID);
		}
		
        void OnSelectionChanged (object sender, System.EventArgs args)
        {
	       	selectedNode = checked((Psycho.PsychoTreeNode)outlineView.NodeSelection.SelectedNode);
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
