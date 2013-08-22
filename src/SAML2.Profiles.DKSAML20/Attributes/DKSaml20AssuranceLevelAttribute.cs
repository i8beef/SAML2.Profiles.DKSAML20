using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// DK SAML Profile AssuranceLevel attribute.
    /// </summary>
    public class DKSaml20AssuranceLevelAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string Name = "dk:gov:saml:attribute:AssuranceLevel";

        /// <summary>
        /// Creates an attribute with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="SamlAttribute"/>.</returns>
        public static SamlAttribute Create(string value)
        {
            return Create(Name, null, value);
        }
    }
}