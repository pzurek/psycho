using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Psycho
{
    public interface IPsychoModel
    {
        #region properties
        Topic CurrentTopic { get; set; }
        Topic CentralTopic { get; set; }
        int CurrentLevel { get; set; }
        #endregion

        #region methods
        void AddTopic ();
        void AddSubtopic ();
        void Delete();
        void SetCurrent(string paramGuid);
        #endregion

        #region Observer implementation
        void AddObserver(IPsychoView paramView);
        void RemoveObserver(IPsychoView paramView);
        void NotifyObservers();
        #endregion
    }
}
