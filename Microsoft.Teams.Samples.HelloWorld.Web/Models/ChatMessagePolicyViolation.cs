// <copyright file="ChatMessagePolicyViolation.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Chat message policy violation.
    /// </summary>
    [DataContract(Name = "chatMessagePolicyViolation")]
    public class ChatMessagePolicyViolation
    {
        /// <summary>
        /// Gets or sets the DLP action.
        /// </summary>
        [DataMember(Name = "dlpAction")]
        public ChatMessagePolicyViolationDlpActionType DlpAction { get; set; }

        /// <summary>
        /// Gets or sets the justification text.
        /// </summary>
        [DataMember(Name = "justificationText")]
        public string JustificationText { get; set; }

        /// <summary>
        /// Gets or sets the policy tip.
        /// </summary>
        [DataMember(Name = "policyTip")]
        public ChatMessagePolicyViolationPolicyTip PolicyTip { get; set; }

        /// <summary>
        /// Gets or sets the user action.
        /// </summary>
        [DataMember(Name = "userAction")]
        public ChatMessagePolicyViolationUserActionType UserAction { get; set; }

        /// <summary>
        /// Gets or sets the verdict details.
        /// </summary>
        [DataMember(Name = "verdictDetails")]
        public ChatMessagePolicyViolationVerdictDetailsType VerdictDetails { get; set; }
    }
}
