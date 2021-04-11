
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("members")]
    public class Member {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("display_name")]
        public String DisplayName { get; set; }
        
        [Column("avatar_url")]
        public String AvatarUrl { get; set; }
        
        [Column("task_folder_local_id")]
        public String TaskFolderLocalId { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("owner"), DefaultValue(0)]
        public Int64 Owner { get; set; }
        
    }
}

