using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// DK SAML Profile PostalAddress attribute.
    /// </summary>
    public class DKSaml20PostalAddressAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string Name = "urn:oid:2.5.4.16";

        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FriendlyName = "postalAddress";

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