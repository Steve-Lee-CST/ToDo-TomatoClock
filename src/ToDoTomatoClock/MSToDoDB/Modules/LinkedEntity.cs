
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("linked_entities")]
    public class LinkedEntity {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("local_id")]
        public String LocalId { get; set; }
        
        [Column("web_link")]
        public String WebLink { get; set; }
        
        [Column("entity_type")]
        public String EntityType { get; set; }
        
        [Column("entity_subtype")]
        public String EntitySubtype { get; set; }
        
        [Column("display_name")]
        public String DisplayName { get; set; }
        
        [Column("preview")]
        public String Preview { get; set; }
        
        [Column("application_name")]
        public String ApplicationName { get; set; }
        
        [Column("client_state")]
        public String ClientState { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("task_local_id")]
        public String TaskLocalId { get; set; }
        
        [Column("position")]
        public String Position { get; set; }
        
        [Column("position_changed"), DefaultValue(0)]
        public Int64 PositionChanged { get; set; }
        
        [Column("deleted"), DefaultValue(0)]
        public Int64 Deleted { get; set; }
        
    }
}

