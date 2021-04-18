using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleLogDB.Modules
{
    [Table("simple_task_folders")]
    public class SimpleTaskFolder
    {
        [Column("_id")]
        public Int64 Id { get; set; }

        [Column("online_id")]
        public String OnlineId { get; set; }

        [Column("local_id")]
        public String LocalId { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("group_local_id")]
        public String GroupLocalId { get; set; }

        [Column("group_online_id")]
        public String GroupOnlineId { get; set; }
    }
}
