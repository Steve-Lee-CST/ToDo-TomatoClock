using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleLogDB.Modules
{
    [Table("tomato_clock_logs")]
    public class TomatoClockLog
    {
        [Column("_id")]
        public Int64 Id { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Column("end_time")]
        public DateTime EndTime { get; set; }

        [Column("total_second")]
        public Int64 TotalSecond { get; set; }

        [Column("remark")]
        public String Remark { get; set; }

        public SimpleTask SimpleTask { get; set; }
    }
}
