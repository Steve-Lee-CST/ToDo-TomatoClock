
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{
    [Table("folder_import_metadata")]
    public class FolderImportMetadata {
        
        [Column("_id")]
        public Int64 Id { get; set; }
        
        [Column("folder_local_id")]
        public String FolderLocalId { get; set; }
        
        [Column("wunderlist_id")]
        public String WunderlistId { get; set; }
        
        [Column("was_shared"), DefaultValue(0)]
        public Int64 WasShared { get; set; }
        
        [Column("members")]
        public String Members { get; set; }
        
        [Column("dismissed"), DefaultValue(0)]
        public Int64 Dismissed { get; set; }
        
        [Column("dismissed_changed"), DefaultValue(0)]
        public Int64 DismissedChanged { get; set; }
        
    }
}

