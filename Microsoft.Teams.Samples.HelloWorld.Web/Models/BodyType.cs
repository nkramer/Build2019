// <copyright file="BodyType.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Item body type. This is used to represent the <see cref="Messaging.ChatMessage"/> body type.
    /// </summary>
    [DataContract(Name = "bodyType")]
    public enum BodyType
    {
        /// <summary>
        /// Plaintext content type.
        /// </summary>
        [EnumMember(Value = "text")]
        Text = 0,

        /// <summary>
        /// HTML content type.
        /// </summary>
        [EnumMember(Value = "html")]
        Html = 1,
    }
}
