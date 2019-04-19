// <copyright file="ChatMessageType.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Type of message.
    /// </summary>
    [DataContract(Name = "chatMessageType")]
    public enum ChatMessageType
    {
        /// <summary>
        /// The regular message.
        /// </summary>
        [EnumMember(Value = "message")]
        Message = 0,

        /// <summary>
        /// The chat event.
        /// </summary>
        [EnumMember(Value = "chatEvent")]
        ChatEvent = 1,

        /// <summary>
        /// The typing message.
        /// </summary>
        [EnumMember(Value = "typing")]
        Typing = 2,
    }
}
