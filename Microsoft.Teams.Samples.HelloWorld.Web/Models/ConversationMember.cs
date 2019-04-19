// <copyright file="ConversationMember.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Abstract base class for Conversation Membership.
    /// </summary>
    [DataContract(Name = "conversationMember")]
    public abstract class ConversationMember
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the member's roles.
        /// </summary>
        [DataMember(Name = "roles")]
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// Gets or sets the display name for the member.
        /// </summary>
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }
}
