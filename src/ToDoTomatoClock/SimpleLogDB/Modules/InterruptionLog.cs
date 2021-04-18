using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleLogDB.Modules
{
    [Table("interruption_logs")]
    public class InterruptionLog
    {
        [Column("_id")]
        public Int64 Id { get; set; }

        [Column("start_time")]
        public String StartTime { get; set; }

        [Column("end_time")]
        public String EndTime { get; set; }

        [Column("remark")]
        public String Remark { get; set; }

        public TomatoClockLog TomatoClockLog { get; set; }
    }
}
