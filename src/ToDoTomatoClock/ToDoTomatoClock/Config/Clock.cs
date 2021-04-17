using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTomatoClock.Config
{
    public class Clock
    {
        [JsonProperty("minute")]
        public int Minute { get; set; } = 0;

        [JsonProperty("second")]
        public int Second { get; set; } = 0;

        public static List<Clock> GetDefaultClockList()
        {
            List<Clock> clocks = new List<Clock>();

            clocks.Add(new Clock() { Minute = 25, Second = 0 });
            clocks.Add(new Clock() { Minute = 5, Second = 0 });
            clocks.Add(new Clock() { Minute = 25, Second = 0 });
            clocks.Add(new Clock() { Minute = 5, Second = 0 });
            clocks.Add(new Clock() { Minute = 25, Second = 0 });
            clocks.Add(new Clock() { Minute = 5, Second = 0 });
            clocks.Add(new Clock() { Minute = 25, Second = 0 });
            clocks.Add(new Clock() { Minute = 15, Second = 0 });

            return clocks;
        }
    }
}
