// <copyright file="ItemBody.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ContosoAirlines.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Item body. This is used to represent the <see cref="Messaging.ChatMessage"/> body.
    /// </summary>
    [DataContract(Name = "itemBody")]
    public class ItemBody
    {
        /// <summary>
        /// The content type pascal cased name.
        /// </summary>
        private const string ContentTypePascalCasedName = "ContentType";

        /// <summary>
        /// The content pascal cased name.
        /// </summary>
        private const string ContentPascalCasedName = "Content";

        /// <summary>
        /// The locking object. Used to ensure sync access to properties dictionary.
        /// </summary>
        private readonly object lockingObject = new object();

        /// <summary>
        /// The content type for the body.
        /// </summary>
        private BodyType contentType;

        /// <summary>
        /// The content for the body.
        /// </summary>
        private string content;

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        [DataMember(Name = "contentType")]
        public BodyType ContentType
        {
            get
            {
#pragma warning disable CS0618 // Type or member is obsolete
                // Extract the content from the dictionary and then delete the contents from dictionary to not run the same logic everytime.
                if (this.Properties.ContainsKey(ContentTypePascalCasedName))
                {
                    lock (this.lockingObject)
                    {
                        if (this.Properties.TryGetValue(ContentTypePascalCasedName, out object contentTypeFromDictionary))
                        {
                            if (Enum.TryParse<BodyType>(contentTypeFromDictionary.ToString(), ignoreCase: true, result: out BodyType bodyType))
                            {
                                this.contentType = bodyType;
                                this.Properties.Remove(ContentTypePascalCasedName);
                            }
                            else
                            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly - Used to throw correct exception when deserialization fails.
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations - Used to throw correct exception when deserialization fails.
                                throw new ArgumentOutOfRangeException(nameof(this.ContentType), $"{contentTypeFromDictionary} is not a valid body type");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations - Used to throw correct exception when deserialization fails.
#pragma warning restore CA2208 // Instantiate argument exceptions correctly - Used to throw correct exception when deserialization fails.
                            }
                        }
                    }
                }

                return this.contentType;
            }
            set => this.contentType = value;
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [DataMember(Name = "content")]
        public string Content
        {
            get
            {
                // Extract the content from the dictionary and then delete the contents from dictionary to not run the same logic everytime.
                if (this.Properties.ContainsKey(ContentPascalCasedName))
                {
                    lock (this.lockingObject)
                    {
                        if (this.Properties.TryGetValue(ContentPascalCasedName, out object contentFromDictionary))
                        {
                            if (contentFromDictionary == null)
                            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly - Used to throw correct exception when deserialization fails.
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations - Used to throw correct exception when deserialization fails.
                                throw new ArgumentNullException(nameof(this.Content));
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations - Used to throw correct exception when deserialization fails.
#pragma warning restore CA2208 // Instantiate argument exceptions correctly - Used to throw correct exception when deserialization fails.
                            }

                            // Why this cryptic code?
                            // When OData deserializer adds properties to OpenType it escapes them.
                            // E.g. "TESTING 123 \"BLEHHHH\" <attachment id=\"a58d8f843cd54694ba6fa1c6cc3f1eaf\"></attachment>"
                            // becomes TESTING 123 \\\"BLEHHHH\\\" <attachment id=\\\"a58d8f843cd54694ba6fa1c6cc3f1eaf\\\"></attachment>.
                            // This forces us to add special code to do transformation. Instead, we can get away with
                            // it by just converting the data back into json value and running json parse on it.
                            // The quotes around are removed at https://github.com/OData/WebApi/blob/3ec3a69fc557a9f6a0380ee21940c2998c02b394/src/Microsoft.AspNet.OData.Shared/Formatter/Deserialization/DeserializationHelpers.cs#L392
                            this.content = JValue.Parse("\"" + contentFromDictionary + "\"").ToString();
                            this.Properties.Remove(ContentPascalCasedName);
#pragma warning restore CS0618 // Type or member is obsolete
                        }
                    }
                }

                return this.content;
            }
            set => this.content = value;
        }

        /// <summary>
        /// Gets or sets the properties. This is just to get around the MS Graph limitation that
        /// Content and ContentType are only sent back in Pascal cashing.
        /// DO NOT ADD PROPERTIES TO THIS.
        /// </summary>
        [Obsolete("Not meant for application use, only used for deserialization of pascal cased properties.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}
