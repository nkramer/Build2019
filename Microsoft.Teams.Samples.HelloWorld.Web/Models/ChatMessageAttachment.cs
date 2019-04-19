// <copyright file="ChatMessageAttachment.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Describes a message attachment like cards, images etc.
    /// </summary>
    [DataContract(Name = "chatMessageAttachment")]
    public class ChatMessageAttachment
    {
        /// <summary>
        /// Gets or sets the identifier for the attachment. This is unique in scope of message only.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the content. This can be something like reference, file, image/imageType, application/cardType etc.
        /// </summary>
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the content URL.
        /// </summary>
        [DataMember(Name = "contentUrl")]
#pragma warning disable CA1056 // Uri properties should not be strings. Data transfer object model.
        public string ContentUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings. Data transfer object model.

        /// <summary>
        /// Gets or sets the content. This and <see cref="ContentUrl"/> are mutually exclusive.
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL.
        /// </summary>
        /// <remarks>
        /// URL to a thumbnail image that the channel can use if it supports using an alternative, smaller form of content or contentUrl.
        /// For example, if you set contentType to application/word and set contentUrl to the location of the Word document, you might include a
        /// thumbnail image that represents the document. The channel could display the thumbnail image instead of the document. When the user
        /// clicks the image, the channel would open the document.
        /// </remarks>
        [DataMember(Name = "thumbnailUrl")]
#pragma warning disable CA1056 // Uri properties should not be strings. Data transfer object model.
        public string ThumbnailUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings. Data transfer object model.
    }
}
