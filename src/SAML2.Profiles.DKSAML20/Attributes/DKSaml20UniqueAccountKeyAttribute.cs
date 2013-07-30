using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class DKSAML20UniqueAccountKeyAttribute : DKSAML20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string NAME = "dk:gov:saml:attribute:UniqueAccountKey";

        /// <summary>
        /// Creates an attribute with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SamlAttribute Create(string value)
        {
            return Create(NAME, null, value);
        }
    }
}