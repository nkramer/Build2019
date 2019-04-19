// <copyright file="ChatMessagePolicyViolationUserActionType.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Chat message policy violation user action type.
    /// </summary>
    [Flags]
    [DataContract(Name = "chatMessagePolicyViolationUserActionType")]
#pragma warning disable CA1714 // Flags enums should have plural names - Pending WI#416267
    public enum ChatMessagePolicyViolationUserActionType
#pragma warning restore CA1714 // Flags enums should have plural names - Pending WI#416267
    {
        /// <summary>
        /// Default value.​
        /// </summary>
        [EnumMember(Value = "none")]
        None = 0x0,

        /// <summary>
        /// Sender has overridden the message.
        /// </summary>
        [EnumMember(Value = "override")]
        Override = 0x1,

        /// <summary>
        /// Sender has marked the message as false positive​.
        /// </summary>
        [EnumMember(Value = "reportFalsePositive")]
        ReportFalsePositive = 0x2,
    }
}