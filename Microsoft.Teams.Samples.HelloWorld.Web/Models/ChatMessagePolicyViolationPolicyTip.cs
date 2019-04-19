// <copyright file="ChatMessagePolicyViolationPolicyTip.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Chat message policy violation policy tip.
    /// </summary>
    [DataContract(Name = "chatMessagePolicyViolationPolicyTip")]
    public class ChatMessagePolicyViolationPolicyTip
    {
        /// <summary>
        /// Gets or sets the general text.
        /// </summary>
        [DataMember(Name = "generalText")]
        public string GeneralText { get; set; }

        /// <summary>
        /// Gets or sets the compliance url.
        /// </summary>
        [DataMember(Name = "complianceUrl")]
#pragma warning disable CA1056 // Uri properties should not be strings. Data transfer object model.
        public string ComplianceUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings. Data transfer object model.

        /// <summary>
        /// Gets or sets a list of matched condition descriptions.
        /// </summary>
        [DataMember(Name = "matchedConditionDescriptions")]
        public IEnumerable<string> MatchedConditionDescriptions { get; set; }
    }
}
