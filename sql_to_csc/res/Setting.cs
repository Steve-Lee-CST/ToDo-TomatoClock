
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("settings")]
    public class Setting {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("key")]
        public String Key { get; set; }
        
        [Column("value")]
        public String Value { get; set; }
        
        [Column("value_changed"), DefaultValue(0)]
        public Int64 ValueChanged { get; set; }
        
    }
}

