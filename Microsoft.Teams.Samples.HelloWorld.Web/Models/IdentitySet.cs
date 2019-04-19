// <copyright file="IdentitySet.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Identity set.
    /// Collection of identities for a specific action (From etc.).
    /// In most cases only one of the property is filled, rest are nulls thus representing the entity.
    /// </summary>
    [DataContract(Name = "identitySet")]
    public class IdentitySet
    {
        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        [DataMember(Name = "application")]
        public Identity Application { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        [DataMember(Name = "device")]
        public Identity Device { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [DataMember(Name = "user")]
        public Identity User { get; set; }

        /// <summary>
        /// Gets or sets the conversation.
        /// </summary>
        [DataMember(Name = "conversation")]
        public Identity Conversation { get; set; }

        /// <summary>
        /// Gets or sets the additional properties. These are the workload specific properties.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}
