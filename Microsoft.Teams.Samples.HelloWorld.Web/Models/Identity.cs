// <copyright file="Identity.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// MS Graph identity.
    /// </summary>
    [DataContract(Name = "identity")]
    public class Identity
    {
        /// <summary>
        /// Gets or sets the identifier. In get this is
        /// User - AAD ID
        /// Bot - AAD Object ID
        /// Team - AAD Group Id
        /// Channel - Skype Thread MRI.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the additional properties. These are the workload specific properties.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}
