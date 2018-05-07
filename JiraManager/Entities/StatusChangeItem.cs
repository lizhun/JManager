
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace JiraManager.Entities
{
    public class StatusChangeItem
    {

        public string JiraNumber { get; set; }
        public DateTime ActionDateTime { get; set; }

        public string NewValue { get; set; }

        public string NewValueUpper => NewValue?.ToUpper();

        public string OldValue { get; set; }

        public string OldValueUpper => OldValueUpper?.ToUpper();
    }
}

