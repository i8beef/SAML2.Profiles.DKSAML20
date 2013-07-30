using System;
using SAML2.Profiles.DKSAML20;
using SAML2.Schema.Core;
using SAML2.Schema.Protocol;
using SAML2.Utils;

namespace SAML2.Profiles.DKSAML20.Validation
{
    internal class DKSAML20StatementValidator : ISaml20StatementValidator
    {

        #region Properties

        private ISaml20AttributeValidator _attributeValidator;

        public ISaml20AttributeValidator AttributeValidator
        {
            get
            {
                if (_attributeValidator == null)
                    _attributeValidator = new DKSAML20AttributeValidator();
                return _attributeValidator;
            }
        }

        #endregion

        #region Public functions 

        public void ValidateStatement(StatementAbstract statement)
        {
            if(statement is AuthzDecisionStatement)
            {
                ValidateAuthzDecisionStatement();
                return;
            }

            if(statement is AttributeStatement)
            {
                ValidateAttributeStatement(statement as AttributeStatement);
                return;
            }

            if(statement is AuthnStatement)
            {
                ValidateAuthnStatement(statement as AuthnStatement);
                return;
            }

            throw new DKSAML20FormatException(
                string.Format("The DK-SAML 2.0 profile does not allow unknown Statement type: \"{0}\"", statement.GetType()));
        }

        #endregion

        #region Private validation functions

        private void ValidateAuthzDecisionStatement()
        {
            throw new DKSAML20FormatException(
                "The DK-SAML 2.0 profile does not allow the \"AuthzDecisionStatement\" element.");
        }

        private void ValidateAuthnStatement(AuthnStatement authnStatement)
        {
            if (!Saml20Utils.ValidateRequiredString(authnStatement.SessionIndex))
                throw new DKSAML20FormatException(
                    "The DK-SAML 2.0 profile requires that the \"AuthnStatement\" element contains the \"SessionIndex\" attribute.");
        }

        private void ValidateAttributeStatement(AttributeStatement attributeStatement)
        {
            foreach (object attribute in attributeStatement.Items)
            {
                if (attribute is EncryptedElement)
                    throw new DKSAML20FormatException("The DK-SAML 2.0 profile does not allow encrypted attributes.");

                if (!(attribute is SamlAttribute))
                    throw new NotImplementedException(string.Format("Unable to handle attribute of type \"{0}\"", attribute.GetType().FullName));

                AttributeValidator.ValidateAttribute((SamlAttribute) attribute);
            }
        }

        #endregion
    }
}