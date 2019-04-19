// <copyright file="ChatMessageMention.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Runtime.Serialization;
    

    /// <summary>
    /// Represents a mention in chat message.
    /// </summary>
    [DataContract(Name = "chatMessageMention")]
    public class ChatMessageMention
    {
        /// <summary>
        /// Gets or sets the identifier for the mention.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the mention text.
        /// </summary>
        [DataMember(Name = "mentionText")]
        public string MentionText { get; set; }

        /// <summary>
        /// Gets or sets the entity mentioned in the message.
        /// </summary>
        [DataMember(Name = "mentioned")]
        public IdentitySet Mentioned { get; set; }
    }
}
