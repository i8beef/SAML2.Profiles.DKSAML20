using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class DKSAML20SurnameAttribute : DKSAML20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string NAME = "urn:oid:2.5.4.4";
        /// <summary>
        /// Friendly name
        /// </summary>
        public const string FRIENDLYNAME = "surName";

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