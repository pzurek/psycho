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

using System.Collections.Generic;
using System.Text;

namespace Psycho {
    class MindControl : IPsychoControl {
        private IPsychoModel Model;
        private IPsychoView View;

        public MindControl(IPsychoModel paramModel, IPsychoView paramView)
        {
            Model = paramModel;
            View = paramView;
        }

        #region IPsychoControl Members

        public void SetModel(IPsychoModel paramModel)
        {
            Model = paramModel;
        }

        public void SetView(IPsychoView paramView)
        {
            View = paramView;
        }

        public void RequestSetCurrent(string paramGuid)
        {
            if (Model != null)
                Model.SetCurrent(paramGuid, Model.CentralTopic);
        }

        public void RequestAddSubtopic()
        {
            if (Model != null) 
                Model.CreateSubtopic();
        }

        public void RequestAddTopic()
        {
            if (Model != null) 
                Model.CreateTopic();
        }

        public void RequestDelete()
        {
            if (Model != null)
                Model.DeleteTopic();
        }

        public void RequestSetTitle(string paramTitle)
        {
            if (Model != null) 
                Model.SetTitle (paramTitle);
        }

        public void RequestExpand(string paramGuid, bool isExpanded)
        {
            if (Model != null) 
                Model.ExpandTopic(paramGuid, isExpanded);
        }
        #endregion
    }
}
