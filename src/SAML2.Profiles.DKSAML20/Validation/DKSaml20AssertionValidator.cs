using System.Collections.Generic;
using SAML2.Schema.Core;
using SAML2.Validation;

namespace SAML2.Profiles.DKSAML20.Validation
{
    using System;

    /// <summary>
    /// DK-SAML Profile Assertion validator.
    /// </summary>
    public class DKSaml20AssertionValidator : Saml20AssertionValidator
    {
        /// <summary>
        /// The statement validator
        /// </summary>
        private ISaml20StatementValidator _statementValidator;

        /// <summary>
        /// The subject validator
        /// </summary>
        private ISaml20SubjectValidator _subjectValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DKSaml20AssertionValidator"/> class.
        /// </summary>
        /// <param name="allowedAudienceUris">The allowed audience uris.</param>
        /// <param name="quirksMode">if set to <c>true</c> [quirks mode].</param>
        public DKSaml20AssertionValidator(List<string> allowedAudienceUris, bool quirksMode)
            : base(allowedAudienceUris, quirksMode)
        { }

        #region Properties

        /// <summary>
        /// Gets the subject validator.
        /// </summary>
        /// <value>The subject validator.</value>
        private ISaml20SubjectValidator SubjectValidator
        {
            get
            {
                return _subjectValidator ?? (_subjectValidator = new DKSaml20SubjectValidator());
            }
        }

        /// <summary>
        /// Gets the statement validator.
        /// </summary>
        /// <value>The statement validator.</value>
        private ISaml20StatementValidator StatementValidator
        {
            get
            {
                return _statementValidator ?? (_statementValidator = new DKSaml20StatementValidator());
            }
        }

        #endregion

        /// <summary>
        /// Validates the <c>saml20Assertion</c> to make sure it conforms to the DK-SAML 2.0 profile.
        /// </summary>
        /// <param name="assertion">The assertion to validate.</param>
        public override void ValidateAssertion(Assertion assertion)
        {
            base.ValidateAssertion(assertion);

            ValidateIssuerElement(assertion);
            ValidateStatements(assertion);
            ValidateSubject(assertion);
            ValidateConditions(assertion); 
        }

        /// <summary>
        /// Throws a <see cref="DKSaml20FormatException"/> containing an error message saying that an Issuer-element cannot have
        /// attributes in the DK-SAML 2.0 profile.
        /// </summary>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">The DK-SAML 2.0 Profile does not allow the <c>\Issuer\</c> element to have any attributes.</exception>
        private static void ThrowIssuerNotEntity()
        {
            throw new DKSaml20FormatException("The DK-SAML 2.0 Profile does not allow the \"Issuer\" element to have any attributes.");
        }

        /// <summary>
        /// Validates the <c>saml20Assertion</c>'s list of conditions.
        /// </summary>
        /// <param name="saml20Assertion">The <c>saml20Assertion</c>.</param>
        /// <exception cref="DKSaml20FormatException">
        /// The DK-SAML 2.0 profile requires that an <c>\AudienceRestriction\</c> element contains the service provider's unique identifier in an <c>\Audience\</c> element.
        /// or
        /// The DK-SAML 2.0 profile requires that an <c>\AudienceRestriction\</c> element is present on the <c>saml20Assertion</c>.
        /// </exception>
        private void ValidateConditions(Assertion saml20Assertion)
        {
            var audienceRestrictionPresent = false;
            foreach (var condition in saml20Assertion.Conditions.Items)
            {
                if (condition is AudienceRestriction)
                {
                    audienceRestrictionPresent = true;
                    var audienceRestriction = (AudienceRestriction)condition;
                    if (audienceRestriction.Audience == null || audienceRestriction.Audience.Count == 0)
                    {
                        throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires that an \"AudienceRestriction\" element contains the service provider's unique identifier in an \"Audience\" element.");
                    }
                }
            }

            if (!audienceRestrictionPresent)
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires that an \"AudienceRestriction\" element is present on the saml20Assertion.");
            }
        }

        /// <summary>
        /// Ensures that a "subject" is present in the <c>saml20Assertion</c>, and validates the subject.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <exception cref="DKSaml20FormatException">The DK-SAML 2.0 profile requires that a <c>\Subject\</c> element is present in the <c>saml20Assertion</c>.</exception>
        private void ValidateSubject(Assertion assertion)
        {
            if (assertion.Subject == null)
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires that a \"Subject\" element is present in the saml20Assertion.");
            }

            SubjectValidator.ValidateSubject(assertion.Subject);
        }

        /// <summary>
        /// Ensures that there are no <c>AuthzdecisionStatement</c> in the DK-SAML 2.0 assertion.
        /// </summary>
        /// <remarks>
        /// TODO If no attributes are requested, the assertion will not contain an AttributeStatement instance. Rethink this validation.
        /// </remarks>
        /// <param name="assertion">The assertion.</param>
        /// <exception cref="DKSaml20FormatException">
        /// The DK-SAML 2.0 profile requires exactly one <c>\AuthnStatement\</c> element and one <c>\AttributeStatement\</c> element.
        /// or
        /// The DK-SAML 2.0 profile requires exactly one <c>\AuthnStatement\</c> element and one <c>\AttributeStatement\</c> element.
        /// </exception>
        private void ValidateStatements(Assertion assertion)
        {
            // Check that the number of statements is correct.
            if (assertion.Items.Length != 2)
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires exactly one \"AuthnStatement\" element and one \"AttributeStatement\" element.");
            }

            // Check if it is the correct statements.            
            var authnStatementPresent = false;
            var attributeStatementPresent = false;
            foreach (StatementAbstract statement in assertion.Items)
            {
                StatementValidator.ValidateStatement(statement);

                if (statement is AuthnStatement)
                {
                    authnStatementPresent = true;
                }

                if (statement is AttributeStatement)
                {
                    attributeStatementPresent = true;
                }
            }

            if (!(authnStatementPresent && attributeStatementPresent))
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 profile requires exactly one \"AuthnStatement\" element and one \"AttributeStatement\" element.");
            }            
        }

        /// <summary>
        /// Checks that the signature element does not contain any attributes, as ordered in the DK-SAML 2.0 profile.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">Assertion MUST contain an issuer in the DK-SAML 2.0 profile.</exception>
        private void ValidateIssuerElement(Assertion assertion)
        {
            if (assertion == null)
            {
                throw new ArgumentNullException("assertion");
            }

            // KBP 01-09-2008: Removed validation of attributes on Issuer element due to future changes DK-SAML profile.
            if (assertion.Issuer == null)
            {
                throw new DKSaml20FormatException("Assertion MUST contain an issuer in the DK-SAML 2.0 profile.");
            }
        }
    }
}
