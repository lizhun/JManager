using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace JiraManager.Entities
{


    public class JiraPageInfo:ICloneable
    {
        public IList<StatusChangeItem> _statuslogs;

        public StatusChangeItem fireLog { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string LastestStatus { get; set; }


        public string JiraNumber { get; set; }

        public IList<StatusChangeItem> StatusLogs
        {
            get
            {
                if (this._statuslogs == null)
                {
                    this._statuslogs = new List<StatusChangeItem>();
                }
                return this._statuslogs;
            }
            set
            {
                this._statuslogs = value;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

