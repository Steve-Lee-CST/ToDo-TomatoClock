
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("steps")]
    public class Step {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("local_id")]
        public String LocalId { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("task_local_id")]
        public String TaskLocalId { get; set; }
        
        [Column("task_local_id_changed"), DefaultValue(0)]
        public Int64 TaskLocalIdChanged { get; set; }
        
        [Column("completed"), DefaultValue(0)]
        public Int64 Completed { get; set; }
        
        [Column("completed_changed"), DefaultValue(0)]
        public Int64 CompletedChanged { get; set; }
        
        [Column("subject")]
        public String Subject { get; set; }
        
        [Column("subject_changed"), DefaultValue(0)]
        public Int64 SubjectChanged { get; set; }
        
        [Column("position"), DefaultValue(STRFTIME("%Y-%m-%dT%H:%M:%S","now"))]
        public String Position { get; set; }
        
        [Column("position_changed"), DefaultValue(0)]
        public Int64 PositionChanged { get; set; }
        
        [Column("created_datetime")]
        public String CreatedDatetime { get; set; }
        
        [Column("deleted"), DefaultValue(0)]
        public Int64 Deleted { get; set; }
        
    }
}

