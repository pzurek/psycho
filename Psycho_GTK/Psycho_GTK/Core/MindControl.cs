using System;
using System.Collections.Generic;
using System.Text;

namespace Psycho
{
    class MindControl : IPsychoControl
    {
        private IPsychoModel Model;
        private IPsychoView View;

        public MindControl(IPsychoModel paramModel, IPsychoView paramView)
        {
            this.Model = paramModel;
            this.View = paramView;
            CheckAddSiblingButtonLegal();
        }

        #region IPsychoControl Members

        public void SetModel(IPsychoModel paramModel)
        {
            this.Model = paramModel;
        }

        public void SetView(IPsychoView paramView)
        {
            this.View = paramView;
        }
	
		public void RequestSetCurrent(string paramGuid)
		{
	    	this.Model.SetCurrent(paramGuid);
		}
        
		public void RequestAddSubtopic()
        {	
			if (Model != null)
            {
            	Model.AddSubtopic();
                CheckAddSiblingButtonLegal();
			}
        }

        public void RequestAddTopic()
        {
			if (Model != null)
			{
	            Model.AddTopic();
                CheckAddSiblingButtonLegal();
			}
        }

        public void RequestDelete()
        {
			if (Model != null)
			{
            	Model.CurrentTopic.Delete();
                CheckAddSiblingButtonLegal();
			}
        }

        public void RequestSetTitle(string paramTitle)
        {
			if (Model != null)
			{
	            Model.CurrentTopic.Title = (paramTitle);
			}
        }
        #endregion
    	
		public void CheckAddSiblingButtonLegal()
		{
	    	if(Model.CurrentTopic == Model.CentralTopic)
	    	{ 
				View.DisableAddSibling();
	    	}
	    	else 
	    	{
				View.EnableAddSibling();
	    	}
		}
    }
}
