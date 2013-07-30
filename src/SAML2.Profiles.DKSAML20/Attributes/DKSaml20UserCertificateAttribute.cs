using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class DKSAML20UserCertificateAttribute : DKSAML20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string NAME = "urn:oid:1.3.6.1.4.1.1466.115.121.1.8";
        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FRIENDLYNAME = "userCertificate";

        /// <summary>
        /// Creates an attribute with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SamlAttribute Create(string value)
        {
            return Create(NAME, FRIENDLYNAME, value);
        }
    }
}