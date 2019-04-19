// <copyright file="ChatMessageHostedImage.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a image in chat message.
    /// </summary>
    [DataContract(Name = "chatMessageHostedImage")]
    public class ChatMessageHostedImage
    {
        /// <summary>
        /// Gets or sets the identifier for the image.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        [DataMember(Name = "url")]
        public string ImagePath { get; set; }
    }
}
