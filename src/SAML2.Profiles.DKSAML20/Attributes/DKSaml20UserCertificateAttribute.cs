using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// DK SAML Profile UserCertificateAttribute attribute.
    /// </summary>
    public class DKSaml20UserCertificateAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string Name = "urn:oid:1.3.6.1.4.1.1466.115.121.1.8";

        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FriendlyName = "userCertificate";

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