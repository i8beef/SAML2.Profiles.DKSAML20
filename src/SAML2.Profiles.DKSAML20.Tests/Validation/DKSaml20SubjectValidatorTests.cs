using System;
using NUnit.Framework;
using SAML2.Profiles.DKSAML20.Validation;
using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Tests.Validation
{
    /// <summary>
    /// <see cref="DKSaml20SubjectValidator"/> tests.
    /// </summary>
    [TestFixture]
    public class DKSaml20SubjectValidatorTests
    {
        /// <summary>
        /// ValidateSubject method tests.
        /// </summary>
        [TestFixture]
        public class ValidateSubjectMethod
        {
            /// <summary>
            /// Throws exception when subject confirmation element is not present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 Profile requires that a bearer \"SubjectConfirmation\" element is present.")]
            public void ThrowsWhenSubjectConfirmationElementIsNotPresent()
            {
                // Arrange
                var validator = new DKSaml20SubjectValidator();

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var subjectConfirmation = (SubjectConfirmation)Array.Find(saml20Assertion.Subject.Items, item => item is SubjectConfirmation);
                subjectConfirmation.Method = "http://example.com";

                // Act
                validator.ValidateSubject(saml20Assertion.Subject);
            }
        }
    }
}
