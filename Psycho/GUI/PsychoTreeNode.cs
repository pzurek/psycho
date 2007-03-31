
using System;
using Gtk;

namespace Psycho
{
	[Gtk.TreeNode]
	public class PsychoTreeNode : TreeNode
	{
		private string guid;
		private string title;
        private string parentGuid;
        public new PsychoTreeNode Parent;

        public PsychoTreeNode(string paramTitle, string paramGuid, string parentGuid)
            : base()
		{
			this.title = paramTitle;
			this.guid = paramGuid;
            if (Parent != null)
                this.parentGuid = this.Parent.guid;
            else
                this.parentGuid = "No parent for central topic";
  		}

        [Gtk.TreeNodeValue (Column=0)]
        public string Title
        {
        	get { return title; }
        }

        [Gtk.TreeNodeValue (Column=1)]
        public string GUID
        {
        	get { return guid; }
        }

        [Gtk.TreeNodeValue(Column = 2)]
        public string parentGUID
        {
            get { return this.Parent.guid; }
        }
    }
}
