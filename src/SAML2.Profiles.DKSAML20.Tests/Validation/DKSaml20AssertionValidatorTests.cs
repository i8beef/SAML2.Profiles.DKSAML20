using System.Collections.Generic;
using NUnit.Framework;
using SAML2.Profiles.DKSAML20.Validation;
using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Tests.Validation
{
    /// <summary>
    /// <see cref="DKSaml20AssertionValidator"/> tests.
    /// </summary>
    [TestFixture]
    public class DKSaml20AssertionValidatorTests
    {
        /// <summary>
        /// ValidateConditions method tests.
        /// </summary>
        [TestFixture]
        public class ValidateConditionsMethod
        {
            /// <summary>
            /// Verify the rules for the &lt;Conditions&gt; element, which are outlined in section 7.1.5 of [DKSAML]
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 profile requires that an \"AudienceRestriction\" element is present on the saml20Assertion.")]
            public void ThrowsExceptionWhenConditionsAreInvalid()
            {
                // Arrange
                var validator = new DKSaml20AssertionValidator(AssertionUtil.GetAudiences(), false);

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var conditions = new List<ConditionAbstract>(saml20Assertion.Conditions.Items);

                var index = conditions.FindIndex(cond => cond is AudienceRestriction);
                conditions.RemoveAt(index);

                // Add another condition to avoid an empty list of conditions.
                conditions.Add(new OneTimeUse());
                saml20Assertion.Conditions.Items = conditions;

                // Act
                validator.ValidateAssertion(saml20Assertion);
            }
        }

        /// <summary>
        /// ValidateIssuerElement method tests.
        /// </summary>
        [TestFixture]
        public class ValidateIssuerElementMethod
        {
            /// <summary>
            /// Determines whether this instance can validate name unique identifier element information quirks mode.
            /// </summary>
            [Test]
            public void CanValidateNameIdElementInQuirksMode()
            {
                // Arrange
                var quirksModeValidator = new DKSaml20AssertionValidator(AssertionUtil.GetAudiences(), true);

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                saml20Assertion.Issuer = new NameId { Value = "http://safewhere.net", Format = "http://example.com" };

                // Act
                quirksModeValidator.ValidateAssertion(saml20Assertion);
            }
        }

        /// <summary>
        /// ValidateStatements method tests.
        /// </summary>
        [TestFixture]
        public class ValidateStatementsMethod 
        {
            /// <summary>
            /// Throws exception when attribute statement has invalid statement.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 profile requires exactly one \"AuthnStatement\" element and one \"AttributeStatement\" element.")]
            public void ThrowsExceptionWhenAttributeStatementHasInvalidStatementType()
            {
                // Arrange
                var validator = new DKSaml20AssertionValidator(AssertionUtil.GetAudiences(), false);

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var authzDecisionStatement = new AuthzDecisionStatement
                                                 {
                                                     Decision = DecisionType.Permit,
                                                     Resource = "http://safewhere.net",
                                                     Action = new[] { new Action() }
                                                 };
                authzDecisionStatement.Action[0].Namespace = "http://actionns.com";
                authzDecisionStatement.Action[0].Value = "value";

                var statements = new List<StatementAbstract>(saml20Assertion.Items) { authzDecisionStatement };
                saml20Assertion.Items = statements.ToArray();

                // Act
                validator.ValidateAssertion(saml20Assertion);
            }

            /// <summary>
            /// Throws exception when attribute statement is not present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 profile requires exactly one \"AuthnStatement\" element and one \"AttributeStatement\" element.")]
            public void ThrowsExceptionWhenAttributeStatementIsNotPresent()
            {
                // Arrange
                var validator = new DKSaml20AssertionValidator(AssertionUtil.GetAudiences(), false);

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var statements = new List<StatementAbstract>(saml20Assertion.Items);
                statements.RemoveAll(stmnt => stmnt is AttributeStatement);
                saml20Assertion.Items = statements.ToArray();

                // Act
                validator.ValidateAssertion(saml20Assertion);
            }

            /// <summary>
            /// Throws exception when authn statement is not present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 profile requires exactly one \"AuthnStatement\" element and one \"AttributeStatement\" element.")]
            public void ThrowsExceptionWhenAuthnStatementIsNotPresent()
            {
                // Arrange
                var validator = new DKSaml20AssertionValidator(AssertionUtil.GetAudiences(), false);

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var statements = new List<StatementAbstract>(saml20Assertion.Items);
                statements.RemoveAll(stmnt => stmnt is AuthnStatement);
                saml20Assertion.Items = statements.ToArray();

                // Act
                validator.ValidateAssertion(saml20Assertion);
            }
        }
    }
}
