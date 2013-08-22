using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Attributes
{
    /// <summary>
    /// The base class for all DK SAML 2.0 attributes.
    /// </summary>
    public abstract class DKSaml20Attribute 
    {
        /// <summary>
        /// Creates a <see cref="SamlAttribute"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="friendlyName">Friendly name.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The <see cref="SamlAttribute"/>.</returns>
        protected static SamlAttribute Create(string name, string friendlyName, string value)
        {
            var att = new SamlAttribute
                          {
                              NameFormat = SamlAttribute.NameformatUri,
                              Name = name,
                              FriendlyName = friendlyName,
                              AttributeValue = new[] { value }
                          };

            return att;   
        }
    }
}