namespace SAML2.Profiles.DKSAML20
{
    /// <summary>
    /// Thrown when a token does not comply with the DK-Saml 2.0 specification. This does not necessarily imply that the
    /// token is not a valid DK SAML 2.0 Assertion.
    /// </summary>
    public class DKSAML20FormatException : Saml20FormatException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DKSAML20FormatException"/> class.
        /// </summary>
        public DKSAML20FormatException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DKSAML20FormatException"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public DKSAML20FormatException(string msg) : base(msg) {}

    }
}