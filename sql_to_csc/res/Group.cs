
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("groups")]
    public class Group {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("local_id")]
        public String LocalId { get; set; }
        
        [Column("change_key")]
        public String ChangeKey { get; set; }
        
        [Column("deleted"), DefaultValue(0)]
        public Int64 Deleted { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("name")]
        public String Name { get; set; }
        
        [Column("name_changed"), DefaultValue(0)]
        public Int64 NameChanged { get; set; }
        
        [Column("position"), DefaultValue(STRFTIME("%Y-%m-%dT%H:%M:%S","now"))]
        public String Position { get; set; }
        
        [Column("position_changed"), DefaultValue(0)]
        public Int64 PositionChanged { get; set; }
        
        [Column("is_expanded"), DefaultValue(0)]
        public Int64 IsExpanded { get; set; }
        
    }
}

