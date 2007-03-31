using System;
using System.Collections.Generic;
using System.Text;

namespace Psycho
{
    public interface IPsychoControl
    {
        void RequestAddTopic();
        void RequestAddSubtopic();
        void RequestDelete();
        void RequestSetTitle(string title);
        void RequestSetCurrent(string paramGuid);

        void SetModel(IPsychoModel paramModel);
        void SetView(IPsychoView paramView);
    }
}
