using Application.Abstraction.Interfaces.Repositories.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.Utilities
{
    public class EventIncrementer : IEventIncrementer
    {
        private static readonly Lazy<EventIncrementer> Lazy =
             new Lazy<EventIncrementer>(() =>
                 new EventIncrementer());

        public EventIncrementer Instance => Lazy.Value;

        public string EventSequence(string value)
        {
            return StringHelpers.IncrementAlphaNumericValue(value);
        }
    }
}
