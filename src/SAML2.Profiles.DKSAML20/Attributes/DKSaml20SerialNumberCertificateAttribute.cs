using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// DK SAML Profile SerialNumberCertificate attribute.
    /// </summary>
    public class DKSaml20SerialNumberCertificateAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string Name = "urn:oid:2.5.4.5";

        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FriendlyName = "serialNumber";

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