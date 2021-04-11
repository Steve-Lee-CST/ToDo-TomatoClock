
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("capabilities")]
    public class Capability {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("name")]
        public String Name { get; set; }
        
        [Column("is_supported"), DefaultValue(0)]
        public Int64 IsSupported { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
    }
}

