using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ToDoTomatoClock.Tools
{
    public class MsgToken : IEquatable<MsgToken>
    {
        public string Token { get; private set; }
        public MsgToken(string sender, string receiver, string action)
        {
            Token = string.Format(
                "MsgToken_{0}_To_{1}_On_{2}",
                sender, receiver, action);
        }

        public bool Equals([AllowNull] MsgToken other)
        {
            return Token.Equals(other.Token);
        }

        public static MsgToken Create(string sender, string receiver, string action) => new MsgToken(sender, receiver, action);
    }
}
