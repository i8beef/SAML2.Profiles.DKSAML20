using System;
using SAML2.Schema.Core;
using SAML2.Schema.Protocol;
using SAML2.Validation;

namespace SAML2.Profiles.DKSAML20.Validation
{
    /// <summary>
    /// DK SAML Profile Attribute validator.
    /// </summary>
    public class DKSaml20AttributeValidator : ISaml20AttributeValidator
    {
        /// <summary>
        /// Validates the attribute.
        /// </summary>
        /// <param name="samlAttribute">The SAML attribute.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">
        /// The DK-SAML 2.0 profile requires that an attribute <c>\Name\</c> is an URI.
        /// or
        /// The DK-SAML 2.0 profile requires that all attribute values are of type <c>\xs:string\</c>.
        /// </exception>
        public void ValidateAttribute(SamlAttribute samlAttribute)
        {
            if (!Uri.IsWellFormedUriString(samlAttribute.Name, UriKind.Absolute))
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires that an attributes \"Name\" is an URI.");
            }

            if (samlAttribute.AttributeValue == null)
            {
                return;
            }

            foreach (object val in samlAttribute.AttributeValue)
            {
                if (val is string)
                {
                    continue;
                }

                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires that all attribute values are of type \"xs:string\".");
            }
        }

        /// <summary>
        /// Validates the encrypted attribute.
        /// </summary>
        /// <param name="encryptedElement">The encrypted element.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">The DK-SAML 2.0 profile does not support the EncryptedAttribute element</exception>
        public void ValidateEncryptedAttribute(EncryptedElement encryptedElement)
        {
            throw new DKSaml20FormatException("The DK-SAML 2.0 profile does not support the EncryptedAttribute element");
        }
    }
}