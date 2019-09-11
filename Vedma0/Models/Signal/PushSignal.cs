using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Signal
{
    public class PushSignal
    {
        public ResultSignal ResultSignal { get; set; }
        public SignalPriority SignalPriority { get; set; }
        public LinkData LinkData { get; set; }

    }
    public enum SignalPriority
    {
        high,
        medium,
        low
    }
    public class LinkData
    {
        public Category Category { get; set; }
        public long? Id { get; set; }
    }
    public enum Category
    {
        main,
        contact,
        map,
        diary,
        news,
        abilities
    }
}
