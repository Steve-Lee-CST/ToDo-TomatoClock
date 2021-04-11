
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("assignments")]
    public class Assignment {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("local_id")]
        public String LocalId { get; set; }
        
        [Column("task_local_id")]
        public String TaskLocalId { get; set; }
        
        [Column("deleted"), DefaultValue(0)]
        public Int64 Deleted { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("assignee_id")]
        public String AssigneeId { get; set; }
        
        [Column("assignee_id_type")]
        public String AssigneeIdType { get; set; }
        
        [Column("assignee_display_name")]
        public String AssigneeDisplayName { get; set; }
        
        [Column("assignee_avatar_url")]
        public String AssigneeAvatarUrl { get; set; }
        
        [Column("assigner_id")]
        public String AssignerId { get; set; }
        
        [Column("assignment_source")]
        public String AssignmentSource { get; set; }
        
        [Column("assigned_datetime")]
        public String AssignedDatetime { get; set; }
        
        [Column("position"), DefaultValue(STRFTIME("%Y-%m-%dT%H:%M:%S","now"))]
        public String Position { get; set; }
        
    }
}

