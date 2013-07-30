using System;
using SAML2.Profiles.DKSAML20;
using SAML2.Schema.Core;
using SAML2.Schema.Protocol;

namespace SAML2.Profiles.DKSAML20.Validation
{
    internal class DKSAML20AttributeValidator : ISaml20AttributeValidator
    {
        public void ValidateAttribute(SamlAttribute samlAttribute)
        {            
            if (!Uri.IsWellFormedUriString(samlAttribute.Name, UriKind.Absolute))
                throw new DKSAML20FormatException("The DK-SAML 2.0 profile requires that an attribute's \"Name\" is an URI.");

            if (samlAttribute.AttributeValue == null)
                return;

            foreach (object val in samlAttribute.AttributeValue)
            {
                if (val is string)
                    continue;

                throw new DKSAML20FormatException("The DK-SAML 2.0 profile requires that all attribute values are of type \"xs:string\".");
            }
        }

        public void ValidateEncryptedAttribute(EncryptedElement encryptedElement)
        {
            throw new DKSAML20FormatException("The DK-SAML 2.0 profile does not support the EncryptedAttribute element");
        }
    }
}