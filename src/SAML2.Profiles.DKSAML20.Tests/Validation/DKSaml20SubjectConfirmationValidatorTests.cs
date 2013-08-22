using System;
using NUnit.Framework;
using SAML2.Profiles.DKSAML20.Validation;
using SAML2.Schema.Core;

namespace SAML2.Profiles.DKSAML20.Tests.Validation
{
    /// <summary>
    /// <see cref="DKSaml20SubjectConfirmationValidator"/> tests.
    /// </summary>
    [TestFixture]
    public class DKSaml20SubjectConfirmationValidatorTests
    {
        /// <summary>
        /// ValidateSubject method tests.
        /// </summary>
        [TestFixture]
        public class ValidateSubjectMethod
        {
            /// <summary>
            /// Throws exception when NotBefore element is present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 Profile disallows the use of the \"NotBefore\" attribute of the \"SubjectConfirmationData\" element.")]
            public void ThrowsWhenNotBeforeElementIsPresent()
            {
                // Arrange
                var validator = new DKSaml20SubjectConfirmationValidator();

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var subjectConfirmation = (SubjectConfirmation)Array.Find(saml20Assertion.Subject.Items, item => item is SubjectConfirmation);
                subjectConfirmation.SubjectConfirmationData.NotOnOrAfter = DateTime.UtcNow;
                subjectConfirmation.SubjectConfirmationData.NotBefore = DateTime.UtcNow.Subtract(new TimeSpan(5, 0, 0, 0));

                // Act
                validator.ValidateSubjectConfirmation(subjectConfirmation);
            }

            /// <summary>
            /// Throws exception when NotOnOrAfter element is not present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 Profile requires that the \"SubjectConfirmationData\" element contains the \"NotOnOrAfter\" attribute.")]
            public void ThrowsWhenNotOnOrAfterElementIsNotPresent()
            {
                // Arrange
                var validator = new DKSaml20SubjectConfirmationValidator();

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var subjectConfirmation = (SubjectConfirmation)Array.Find(saml20Assertion.Subject.Items, item => item is SubjectConfirmation);
                subjectConfirmation.SubjectConfirmationData.NotOnOrAfter = null;

                // Act
                validator.ValidateSubjectConfirmation(subjectConfirmation);
            }

            /// <summary>
            /// Throws exception when recipient element is not present.
            /// </summary>
            [Test]
            [ExpectedException(typeof(DKSaml20FormatException), ExpectedMessage = "The DK-SAML 2.0 Profile requires that the \"SubjectConfirmationData\" element contains the \"Recipient\" attribute.")]
            public void ThrowsWhenRecipientElementIsNotPresent()
            {
                // Arrange
                var validator = new DKSaml20SubjectConfirmationValidator();

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var subjectConfirmation = (SubjectConfirmation)Array.Find(saml20Assertion.Subject.Items, item => item is SubjectConfirmation);
                subjectConfirmation.SubjectConfirmationData.NotOnOrAfter = DateTime.UtcNow;
                subjectConfirmation.SubjectConfirmationData.NotBefore = null;
                subjectConfirmation.SubjectConfirmationData.Recipient = null;

                // Act
                validator.ValidateSubjectConfirmation(subjectConfirmation);
            }

            /// <summary>
            /// Verifies that a valid subject confirmation can be validated.
            /// </summary>
            [Test]
            public void ValidatesSubjectConfirmation()
            {
                // Arrange
                var validator = new DKSaml20SubjectConfirmationValidator();

                var saml20Assertion = AssertionUtil.GetBasicAssertion();
                var subjectConfirmation = (SubjectConfirmation)Array.Find(saml20Assertion.Subject.Items, item => item is SubjectConfirmation);

                // Act
                validator.ValidateSubjectConfirmation(subjectConfirmation);
            }
        }
    }
}
