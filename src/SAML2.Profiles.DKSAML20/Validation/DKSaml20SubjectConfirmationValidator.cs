using SAML2.Profiles.DKSAML20;
using SAML2.Schema.Core;
using SAML2.Utils;
using SAML2.Validation;

namespace SAML2.Profiles.DKSAML20.Validation
{
    public class DKSAML20SubjectConfirmationValidator : ISaml20SubjectConfirmationValidator
    {
        public void ValidateSubjectConfirmation(SubjectConfirmation subjectConfirmation)
        {
            if (subjectConfirmation.Method == SubjectConfirmation.BEARER_METHOD)
            {
                if (subjectConfirmation.SubjectConfirmationData == null)
                    throw new DKSAML20FormatException("The DK-SAML 2.0 Profile requires that the bearer \"SubjectConfirmation\" element contains a \"SubjectConfirmationData\" element.");

                if (!Saml20Utils.ValidateRequiredString(subjectConfirmation.SubjectConfirmationData.Recipient))
                    throw new DKSAML20FormatException("The DK-SAML 2.0 Profile requires that the \"SubjectConfirmationData\" element contains the \"Recipient\" attribute.");

                if (!subjectConfirmation.SubjectConfirmationData.NotOnOrAfter.HasValue)
                    throw new DKSAML20FormatException("The DK-SAML 2.0 Profile requires that the \"SubjectConfirmationData\" element contains the \"NotOnOrAfter\" attribute.");

                if (subjectConfirmation.SubjectConfirmationData.NotBefore.HasValue)
                    throw new DKSAML20FormatException("The DK-SAML 2.0 Profile disallows the use of the \"NotBefore\" attribute of the \"SubjectConfirmationData\" element.");
            }
        }
    }
}