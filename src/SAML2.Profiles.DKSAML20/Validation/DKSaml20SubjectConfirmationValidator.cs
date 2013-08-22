using SAML2.Schema.Core;
using SAML2.Utils;
using SAML2.Validation;

namespace SAML2.Profiles.DKSAML20.Validation
{
    /// <summary>
    /// DK SAML Profile Subject Confirmation validator.
    /// </summary>
    public class DKSaml20SubjectConfirmationValidator : ISaml20SubjectConfirmationValidator
    {
        /// <summary>
        /// Validates the subject confirmation.
        /// </summary>
        /// <param name="subjectConfirmation">The subject confirmation.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">
        /// The DK-SAML 2.0 Profile requires that the bearer \SubjectConfirmation\ element contains a \SubjectConfirmationData\ element.
        /// or
        /// The DK-SAML 2.0 Profile requires that the \SubjectConfirmationData\ element contains the \Recipient\ attribute.
        /// or
        /// The DK-SAML 2.0 Profile requires that the \SubjectConfirmationData\ element contains the \NotOnOrAfter\ attribute.
        /// or
        /// The DK-SAML 2.0 Profile disallows the use of the \NotBefore\ attribute of the \SubjectConfirmationData\ element.
        /// </exception>
        public void ValidateSubjectConfirmation(SubjectConfirmation subjectConfirmation)
        {
            if (subjectConfirmation.Method == SubjectConfirmation.BearerMethod)
            {
                if (subjectConfirmation.SubjectConfirmationData == null)
                {
                    throw new DKSaml20FormatException("The DK-SAML 2.0 Profile requires that the bearer \"SubjectConfirmation\" element contains a \"SubjectConfirmationData\" element.");
                }

                if (!Saml20Utils.ValidateRequiredString(subjectConfirmation.SubjectConfirmationData.Recipient))
                {
                    throw new DKSaml20FormatException("The DK-SAML 2.0 Profile requires that the \"SubjectConfirmationData\" element contains the \"Recipient\" attribute.");
                }

                if (!subjectConfirmation.SubjectConfirmationData.NotOnOrAfter.HasValue)
                {
                    throw new DKSaml20FormatException("The DK-SAML 2.0 Profile requires that the \"SubjectConfirmationData\" element contains the \"NotOnOrAfter\" attribute.");
                }

                if (subjectConfirmation.SubjectConfirmationData.NotBefore.HasValue)
                {
                    throw new DKSaml20FormatException("The DK-SAML 2.0 Profile disallows the use of the \"NotBefore\" attribute of the \"SubjectConfirmationData\" element.");
                }
            }
        }
    }
}