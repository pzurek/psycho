using System;
using System.Collections.Generic;
using System.Text;

namespace Psycho
{
    public interface IPsychoView
    {
        #region Observer implementation
        void Update(IPsychoModel paramModel);
        void AddTopic();
        void AddSubtopic();
        void Delete();
        //void SetTopicTitle(string Title);
        void SetCurrentTopic();
        void DisableAddSibling();
        void EnableAddSibling();
        #endregion
    }
}
