using System;
using SAML2.Schema.Core;
using SAML2.Schema.Protocol;
using SAML2.Utils;
using SAML2.Validation;

namespace SAML2.Profiles.DKSAML20.Validation
{
    /// <summary>
    /// DK SAML Profile Statement validator.
    /// </summary>
    public class DKSaml20StatementValidator : ISaml20StatementValidator
    {
        /// <summary>
        /// The attribute validator
        /// </summary>
        private ISaml20AttributeValidator _attributeValidator;

        #region Properties
        
        /// <summary>
        /// Gets the attribute validator.
        /// </summary>
        /// <value>The attribute validator.</value>
        public ISaml20AttributeValidator AttributeValidator
        {
            get
            {
                return _attributeValidator ?? (_attributeValidator = new DKSaml20AttributeValidator());
            }
        }

        #endregion

        #region Public functions 

        /// <summary>
        /// Validates the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">Thrown if a format error is detected.</exception>
        public void ValidateStatement(StatementAbstract statement)
        {
            if (statement is AuthzDecisionStatement)
            {
                ValidateAuthzDecisionStatement();
                return;
            }

            if (statement is AttributeStatement)
            {
                ValidateAttributeStatement(statement as AttributeStatement);
                return;
            }

            if (statement is AuthnStatement)
            {
                ValidateAuthnStatement(statement as AuthnStatement);
                return;
            }

            throw new DKSaml20FormatException(string.Format("The DK-SAML 2.0 profile does not allow unknown Statement type: \"{0}\"", statement.GetType()));
        }

        #endregion

        #region Private validation functions

        /// <summary>
        /// Validates the <c>AttributeStatement</c>.
        /// </summary>
        /// <param name="attributeStatement">The <c>AttributeStatement</c>.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">The DK-SAML 2.0 profile does not allow encrypted attributes.</exception>
        /// <exception cref="System.NotImplementedException">The DK-SAML 2.0 profile requires that the attributes are unencrypted.</exception>
        private void ValidateAttributeStatement(AttributeStatement attributeStatement)
        {
            foreach (var attribute in attributeStatement.Items)
            {
                if (attribute is EncryptedElement)
                {
                    throw new DKSaml20FormatException("The DK-SAML 2.0 profile does not allow encrypted attributes.");
                }

                if (!(attribute is SamlAttribute))
                {
                    throw new NotImplementedException(string.Format("Unable to handle attribute of type \"{0}\"", attribute.GetType().FullName));
                }

                AttributeValidator.ValidateAttribute((SamlAttribute)attribute);
            }
        }

        /// <summary>
        /// Validates the <c>AuthzDecisionStatement</c>.
        /// </summary>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">The DK-SAML 2.0 profile does not allow the <c>\AuthzDecisionStatement\</c> element.</exception>
        private void ValidateAuthzDecisionStatement()
        {
            throw new DKSaml20FormatException("The DK-SAML 2.0 profile does not allow the \"AuthzDecisionStatement\" element.");
        }

        /// <summary>
        /// Validates the <c>AuthnStatement</c>.
        /// </summary>
        /// <param name="authnStatement">The <c>AuthnStatement</c>.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">The DK-SAML 2.0 profile requires that the <c>\AuthnStatement\</c> element contains the <c>\SessionIndex\</c> attribute.</exception>
        private void ValidateAuthnStatement(AuthnStatement authnStatement)
        {
            if (!Saml20Utils.ValidateRequiredString(authnStatement.SessionIndex))
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires that the \"AuthnStatement\" element contains the \"SessionIndex\" attribute.");
            }
        }

        #endregion
    }
}