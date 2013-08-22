using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// DK SAML Profile Title attribute.
    /// </summary>
    public class DKSaml20TitleAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string Name = "urn:oid:2.5.4.12";

        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FriendlyName = "title";

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