using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessStructure.Vms.JShoping.ImExPrice.BAL.Models
{
    public class ExtraFieldsValuesModel
    {
        public int Id { get; internal set; }
        public int FieldId { get; internal set; }
        public int Ordering { get; internal set; }
        public string NameRu { get; internal set; }
    }
}
