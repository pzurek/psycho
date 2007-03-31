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
using System.Xml;
using Gtk;

namespace Psycho {

    public class XMLView : IView {

        public TextView preview;

        public XMLView ()
        {

        }

        #region IView Members

        public void Update (IModel paramModel)
        {

        }

        public void WireUp (IControl paramControl, IModel paramModel)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void AddTopic ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void AddSubtopic ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void DeleteTopic ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void ExpandTopic (string paramGuid, bool isExpanded)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void EditTitle (string Title)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void SetCurrentTopic ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void TriggerEdit (bool editPending)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void DisableAddSibling ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void DisableDelete ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void EnableAddSibling ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public void EnableDelete ()
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        #endregion
    }
}
