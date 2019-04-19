// <copyright file="ChatMessageImportance.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the importance of message.
    /// </summary>
    [DataContract(Name = "chatMessageImportance")]
    public enum ChatMessageImportance
    {
        /// <summary>
        /// Message was sent with normal importance.
        /// </summary>
        [EnumMember(Value = "normal")]
        Normal = 0,

        /// <summary>
        /// Message was sent with high importance.
        /// </summary>
        [EnumMember(Value = "high")]
        High = 1,
    }
}
