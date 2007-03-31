using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Psycho {

    public interface ITopics : ICollection,
                               ICollection<ITopic>,
                               IEnumerable,
                               IEnumerable<ITopic>,
                               IList,
                               IList<ITopic> {
    }
}
