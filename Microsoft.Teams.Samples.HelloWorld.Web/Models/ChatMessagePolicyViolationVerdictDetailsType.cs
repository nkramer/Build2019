// <copyright file="ChatMessagePolicyViolationVerdictDetailsType.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Chat message policy violation verdict details type.
    /// </summary>
    [Flags]
    [DataContract(Name = "chatMessagePolicyViolationVerdictDetailsType")]
#pragma warning disable CA1714 // Flags enums should have plural names - Pending WI#416267
    public enum ChatMessagePolicyViolationVerdictDetailsType
#pragma warning restore CA1714 // Flags enums should have plural names - Pending WI#416267
    {
        /// <summary>
        /// Do not allow any override​.
        /// </summary>
        [EnumMember(Value = "none")]
        None = 0x0,

        /// <summary>
        /// Allow false positive override​.
        /// </summary>
        [EnumMember(Value = "allowFalsePositiveOverride")]
        AllowFalsePositiveOverride = 0x1,

        /// <summary>
        /// Allow override without justification​.
        /// </summary>
        [EnumMember(Value = "allowOverrideWithoutJustification")]
        AllowOverrideWithoutJustification = 0x2,

        /// <summary>
        /// Allow override with justification​.
        /// </summary>
        [EnumMember(Value = "allowOverrideWithJustification")]
        AllowOverrideWithJustification = 0x4,
    }
}