using System;

namespace GSI.Core
{
    public class ViewModel
    {
        public string   File          { get; set; }
        public string   Id            { get; set; }
        public string   Title         { get; set; }
        public string   CollectorName { get; set; }
        public string   Type          { get; set; }
        public float    Quantity      { get; set; }
        public float    Uncertainty   { get; set; }
        public string   Units         { get; set; }
        public float    Geometry      { get; set; }
        public string   BuildUpType   { get; set; }
        public DateTime BeginDate     { get; set; }
        public DateTime EndDate       { get; set; }
        public string   Description   { get; set; }
        public bool     ReadSuccess   { get; set; }
        public string   ErrorMessage  { get; set; }
    }
}
