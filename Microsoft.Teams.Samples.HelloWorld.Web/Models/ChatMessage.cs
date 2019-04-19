// <copyright file="ChatMessage.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// EDM representing the Chat Service (Teams) Message.
    /// </summary>
    [DataContract(Name = "chatMessage")]
    public class ChatMessage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the reply to identifier.
        /// </summary>
        [DataMember(Name = "replyToId")]
        public string ReplyToId { get; set; }

        /// <summary>
        /// Gets or sets from. The sender of the message.
        /// </summary>
        [DataMember(Name = "from")]
        public IdentitySet From { get; set; }

        /// <summary>
        /// Gets or sets the etag for the message. This changes when message is updated.
        /// </summary>
        [DataMember(Name = "etag")]
        public string Etag { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        [DataMember(Name = "messageType", IsRequired = true)]
        public ChatMessageType MessageType { get; set; } = ChatMessageType.Message;

        /// <summary>
        /// Gets or sets the created time. This is in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the last modified time. This is in ISO 8601 format.
        /// </summary>
        [DataMember(Name = "lastModifiedDateTime")]
        public DateTimeOffset? LastModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the deletion time of this <see cref="ChatMessage"/>.
        /// </summary>
        [DataMember(Name = "deletedDateTime")]
        public DateTimeOffset? DeletedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the subject of the message. This is only present in the root message.
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body of the message. This specified the body as well as declares the kind of body it is.
        /// </summary>
        [DataMember(Name = "body", IsRequired = true)]
        public ItemBody Body { get; set; }

        /// <summary>
        /// Gets or sets the summary of the message.
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the attachments that are part of the message. This includes cards etc.
        /// </summary>
        [DataMember(Name = "attachments")]
        public IEnumerable<ChatMessageAttachment> Attachments { get; set; } = new List<ChatMessageAttachment>();

        /// <summary>
        /// Gets or sets the mentions. This lists the entities (users, bots etc.) that were mentioned in the message.
        /// </summary>
        [DataMember(Name = "mentions")]
        public IEnumerable<ChatMessageMention> Mentions { get; set; } = new List<ChatMessageMention>();

        /// <summary>
        /// Gets or sets the value indicating importance of message.
        /// </summary>
        [DataMember(Name = "importance", IsRequired = true)]
        public ChatMessageImportance Importance { get; set; } = ChatMessageImportance.Normal;

        /// <summary>
        /// Gets or sets the policy violation.
        /// </summary>
        [DataMember(Name = "policyViolation")]
        public ChatMessagePolicyViolation PolicyViolation { get; set; }

        /// <summary>
        /// Gets or sets the reactions. This includes likes etc. for the message.
        /// </summary>
        [DataMember(Name = "reactions")]
        public IEnumerable<ChatMessageReaction> Reactions { get; set; } = new List<ChatMessageReaction>();

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        [DataMember(Name = "locale", IsRequired = true)]
        public string Locale { get; set; } = "en-us";

        /// <summary>
        /// Gets or sets the replies for this message.
        /// </summary>
        
        [DataMember(Name = "replies")]
        public IEnumerable<ChatMessage> Replies { get; set; }

        /// <summary>
        /// Gets or sets the hosted images that are part of the message.
        /// </summary>
        [DataMember(Name = "hostedImages")]
        public IEnumerable<ChatMessageHostedImage> HostedImages { get; set; }
    }
}
