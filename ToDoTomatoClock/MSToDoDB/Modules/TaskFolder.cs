
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("task_folders")]
    public class TaskFolder {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("online_id")]
        public String OnlineId { get; set; }
        
        [Column("local_id")]
        public String LocalId { get; set; }
        
        [Column("change_key")]
        public String ChangeKey { get; set; }
        
        [Column("is_default"), DefaultValue(0)]
        public Int64 IsDefault { get; set; }
        
        [Column("deleted"), DefaultValue(0)]
        public Int64 Deleted { get; set; }
        
        [Column("delta_link")]
        public String DeltaLink { get; set; }
        
        [Column("group_local_id")]
        public String GroupLocalId { get; set; }
        
        [Column("group_local_id_changed"), DefaultValue(0)]
        public Int64 GroupLocalIdChanged { get; set; }
        
        [Column("delete_after_sync"), DefaultValue(0)]
        public Int64 DeleteAfterSync { get; set; }
        
        [Column("name")]
        public String Name { get; set; }
        
        [Column("name_changed"), DefaultValue(0)]
        public Int64 NameChanged { get; set; }
        
        [Column("position")]
        public String Position { get; set; }
        
        [Column("position_changed"), DefaultValue(0)]
        public Int64 PositionChanged { get; set; }
        
        [Column("show_completed_tasks"), DefaultValue(1)]
        public Int64 ShowCompletedTasks { get; set; }
        
        [Column("show_completed_tasks_changed"), DefaultValue(0)]
        public Int64 ShowCompletedTasksChanged { get; set; }
        
        [Column("sorting_order"), DefaultValue("STORED_POSITION")]
        public String SortingOrder { get; set; }
        
        [Column("sorting_order_changed"), DefaultValue(0)]
        public Int64 SortingOrderChanged { get; set; }
        
        [Column("sorting_direction"), DefaultValue("DESCENDING")]
        public String SortingDirection { get; set; }
        
        [Column("sorting_direction_changed"), DefaultValue(0)]
        public Int64 SortingDirectionChanged { get; set; }
        
        [Column("theme_background"), DefaultValue("mountain")]
        public String ThemeBackground { get; set; }
        
        [Column("theme_background_changed"), DefaultValue(0)]
        public Int64 ThemeBackgroundChanged { get; set; }
        
        [Column("theme_color"), DefaultValue("dark_blue")]
        public String ThemeColor { get; set; }
        
        [Column("theme_color_changed"), DefaultValue(0)]
        public Int64 ThemeColorChanged { get; set; }
        
        [Column("is_owner"), DefaultValue(1)]
        public Int64 IsOwner { get; set; }
        
        [Column("sync_update_required"), DefaultValue(0)]
        public Int64 SyncUpdateRequired { get; set; }
        
        [Column("sharing_link")]
        public String SharingLink { get; set; }
        
        [Column("is_folder_shared"), DefaultValue(0)]
        public Int64 IsFolderShared { get; set; }
        
        [Column("folder_type")]
        public String FolderType { get; set; }
        
        [Column("sync_status"), DefaultValue("Synced")]
        public String SyncStatus { get; set; }
        
        [Column("sharing_status"), DefaultValue("NotShared")]
        public String SharingStatus { get; set; }
        
        [Column("sharing_status_changed"), DefaultValue(0)]
        public Int64 SharingStatusChanged { get; set; }
        
        [Column("is_cross_tenant"), DefaultValue(0)]
        public Int64 IsCrossTenant { get; set; }

        // extra-datastructure
        // tree: (Group-)TaskFolder-Task-Step
        [NotMapped]
        public Group Parent { get; set; }

        [NotMapped]
        public List<Task> Childs { get; set; } = new List<Task>();
    }
}

