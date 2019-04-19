// <copyright file="AADUserConversationMember.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Runtime.Serialization;
    

    /// <summary>
    /// The AAD Conversation Member.
    /// </summary>
    [DataContract(Name = "aadUserConversationMember")]
    public class AADUserConversationMember : ConversationMember
    {
        /// <summary>
        /// Gets or sets the User's Identifier.
        /// </summary>
        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the User's email.
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the User's Identifier.
        /// </summary>
        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
