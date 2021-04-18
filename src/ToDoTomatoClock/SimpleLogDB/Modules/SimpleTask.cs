using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleLogDB.Modules
{
    [Table("simple_tasks")]
    public class SimpleTask
    {
        [Column("_id")]
        public Int64 Id { get; set; }

        [Column("online_id")]
        public String OnlineId { get; set; }

        [Column("local_id")]
        public String LocalId { get; set; }

        [Column("subject")]
        public String Subject { get; set; }

        [Column("original_body_content")]
        public String OriginalBodyContent { get; set; }

        [Column("task_folder_local_id")]
        public String TaskFolderLocalId { get; set; }

        [Column("task_folder_online_id")]
        public String TaskFolderOnlineId { get; set; }

        [Column("remark")]
        public String Remark { get; set; }
    }
}
