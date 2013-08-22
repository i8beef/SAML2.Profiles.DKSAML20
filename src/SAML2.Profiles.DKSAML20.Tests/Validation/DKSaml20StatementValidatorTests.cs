using System;
using System.Collections.Generic;
using NUnit.Framework;
using SAML2.Profiles.DKSAML20.Validation;
using SAML2.Schema.Core;
using SAML2.Schema.Protocol;
using SAML2.Schema.XEnc;

namespace SAML2.Profiles.DKSAML20.Tests.Validation
{
    /// <summary>
    /// <see cref="DKSaml20StatementValidator"/> tests.
    /// </summary>
    [TestFixture]
    public class DKSaml20StatementValidatorTests
    {
        /// <summary>
        /// ValidateConditions method tests.
        /// </summary>
        [TestFixture]
        public class ValidateAttributeStatementMethod
        {
            /// <summary>
            /// Test that EncryptedData element with the correct Type value is disallowed by the DK SAML 2.0 validation
            /// </summary>        
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 profile does not allow encrypted attributes.")]
            public void ThrowsExceptionWhenAttributeStatementIsEncrypted()
            {
                // Arrange
                var validator = new DKSaml20StatementValidator();
                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var statements = new List<StatementAbstract>(saml20Assertion.Items);
                var sas = GetAttributeStatement(statements);
                var attributes = new List<object>(sas.Items);
                var ee = new EncryptedElement { EncryptedData = new EncryptedData { Type = Saml20Constants.Xenc + "Element" } };

                attributes.Add(ee);
                sas.Items = attributes.ToArray();

                // Act
                validator.ValidateStatement(sas);
            }

            /// <summary>
            /// Convenience method for extracting the list of Attributes from the assertion.
            /// </summary>
            /// <param name="statements">The statements.</param>
            /// <returns>The <see cref="AttributeStatement"/>.</returns>
            private static AttributeStatement GetAttributeStatement(List<StatementAbstract> statements)
            {
                return (AttributeStatement)statements.Find(ssa => ssa is AttributeStatement);
            }
        }

        /// <summary>
        /// ValidateAuthnStatement method tests.
        /// </summary>
        [TestFixture]
        public class ValidateAuthnStatementMethod
        {
            /// <summary>
            /// Throws exception when session index element is not present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 profile requires that the \"AuthnStatement\" element contains the \"SessionIndex\" attribute.")]
            public void ThrowsWhenSessionIndexElementIsNotPresent()
            {
                // Arrange
                var validator = new DKSaml20StatementValidator();

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var authnStatement = (AuthnStatement)Array.Find(saml20Assertion.Items, stmnt => stmnt is AuthnStatement);

                authnStatement.SessionIndex = null;

                // Act
                validator.ValidateStatement(authnStatement);
            }
        }
    }
}
