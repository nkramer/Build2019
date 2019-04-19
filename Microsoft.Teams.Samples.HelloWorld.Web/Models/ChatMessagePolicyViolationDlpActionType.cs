// <copyright file="ChatMessagePolicyViolationDlpActionType.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Chat message policy violation DLP action type.
    /// </summary>
    [Flags]
    [DataContract(Name = "chatMessagePolicyViolationDlpActionType")]
#pragma warning disable CA1714 // Flags enums should have plural names - Pending WI#416267
    public enum ChatMessagePolicyViolationDlpActionType
#pragma warning restore CA1714 // Flags enums should have plural names - Pending WI#416267
    {
        /// <summary>
        /// Default value.​
        /// </summary>
        [EnumMember(Value = "none")]
        None = 0x0,

        /// <summary>
        /// Sender is notified that the message is violating one of the policies set by tenant admin​.
        /// </summary>
        [EnumMember(Value = "notifySender")]
        NotifySender = 0x1,

        /// <summary>
        /// Message is blocked to sender and recipients of message​.
        /// </summary>
        [EnumMember(Value = "blockAccess")]
        BlockAccess = 0x2,

        /// <summary>
        /// Message is blocked to external users.​
        /// </summary>
        [EnumMember(Value = "blockAccessExternal")]
        BlockAccessExternal = 0x4,
    }
}
