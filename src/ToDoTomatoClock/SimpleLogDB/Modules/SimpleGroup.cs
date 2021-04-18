using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleLogDB.Modules
{
    [Table("simple_groups")]
    public class SimpleGroup
    {
        [Column("_id")]
        public Int64 Id { get; set; }

        [Column("online_id")]
        public String OnlineId { get; set; }

        [Column("local_id")]
        public String LocalId { get; set; }

        [Column("name")]
        public String Name { get; set; }
    }
}
