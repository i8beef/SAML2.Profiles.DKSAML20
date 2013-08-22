using SAML2.Schema.Core;
using SAML2.Validation;

namespace SAML2.Profiles.DKSAML20.Validation
{
    /// <summary>
    /// DK SAML Profile Subject validator.
    /// </summary>
    public class DKSaml20SubjectValidator : ISaml20SubjectValidator
    {
        /// <summary>
        /// The subject confirmation validator
        /// </summary>
        private ISaml20SubjectConfirmationValidator _subjectConfirmationValidator;

        #region Properties

        /// <summary>
        /// Gets the subject confirmation validator.
        /// </summary>
        /// <value>The subject confirmation validator.</value>
        private ISaml20SubjectConfirmationValidator SubjectConfirmationValidator
        {
            get
            {
                return _subjectConfirmationValidator
                       ?? (_subjectConfirmationValidator = new DKSaml20SubjectConfirmationValidator());
            }
        }

        #endregion

        /// <summary>
        /// Validates the subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <exception cref="SAML2.Profiles.DKSAML20.DKSaml20FormatException">
        /// The DK-SAML 2.0 Profile requires at least one <c>\SubjectConfirmation\</c> element within the <c>\Subject\</c> element.
        /// or
        /// The DK-SAML 2.0 Profile requires that a bearer <c>\SubjectConfirmation\</c> element is present.
        /// </exception>
        public void ValidateSubject(Subject subject)
        {
            if (subject.Items == null || subject.Items.Length == 0)
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 Profile requires at least one \"SubjectConfirmation\" element within the \"Subject\" element.");
            }

            var subjectConfirmationPresent = false;
            foreach (var item in subject.Items)
            {
                if (item is SubjectConfirmation)
                {
                    var subjectConfirmation = (SubjectConfirmation)item;
                    if (subjectConfirmation.Method == SubjectConfirmation.BearerMethod)
                    {
                        subjectConfirmationPresent = true;
                    }

                    SubjectConfirmationValidator.ValidateSubjectConfirmation(subjectConfirmation);
                }
            }

            if (!subjectConfirmationPresent)
            {
                throw new DKSaml20FormatException("The DK-SAML 2.0 Profile requires that a bearer \"SubjectConfirmation\" element is present.");
            }
        }
    }
}