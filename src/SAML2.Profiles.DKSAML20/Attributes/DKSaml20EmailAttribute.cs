using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// DK SAML Profile Email attribute.
    /// </summary>
    public class DKSaml20EmailAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string Name = "urn:oid:0.9.2342.19200300.100.1.3";

        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FriendlyName = "email";

        /// <summary>
        /// Creates an attribute with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="SamlAttribute"/>.</returns>
        public static SamlAttribute Create(string value)
        {
            return Create(Name, FriendlyName, value);
        }
    }
}