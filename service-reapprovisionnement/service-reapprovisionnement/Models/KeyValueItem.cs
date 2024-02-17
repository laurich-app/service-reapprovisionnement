using System;

namespace Reapprovisionnement.Models {

public class KeyValueItem
    {
        public string LockIndex { get; set; }
        public string Key { get; set; }
        public int Flags { get; set; }
        public string Value { get; set; }
        public int CreateIndex { get; set; }
        public int ModifyIndex { get; set; }
    }
}