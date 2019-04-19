// <copyright file="ChatMessageReaction.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Runtime.Serialization;
    

    /// <summary>
    /// Chat message reaction.
    /// </summary>
    [DataContract(Name = "chatMessageReaction")]
    public class ChatMessageReaction
    {
        /// <summary>
        /// Gets or sets the type of reaction.
        /// </summary>
        [DataMember(Name = "reactionType", IsRequired = true)]
        public string ReactionType { get; set; }

        /// <summary>
        /// Gets or sets the time when the reaction was created.
        /// </summary>
        [DataMember(Name = "createdDateTime", IsRequired = true)]
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the user who gave the reaction.
        /// </summary>
        [DataMember(Name = "user", IsRequired = true)]
        public IdentitySet User { get; set; }
    }
}
