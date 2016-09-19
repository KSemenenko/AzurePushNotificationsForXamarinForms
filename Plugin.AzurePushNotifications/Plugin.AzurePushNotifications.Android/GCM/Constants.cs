using Android.App;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")] //, ProtectionLevel = Android.Content.PM.Protection.Signature)]

[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below

[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace Gcm.Client
{
    public class Constants
    {
        public const string IntentToGcmRegistration = "com.google.android.c2dm.intent.REGISTER";

        /// <summary>
        ///     Intent sent to GCM to unregister the application.
        /// </summary>
        public const string IntentToGcmUnregistration = "com.google.android.c2dm.intent.UNREGISTER";

        /// <summary>
        ///     Intent sent by GCM indicating with the result of a registration request.
        /// </summary>
        public const string IntentFromGcmRegistrationCallback = "com.google.android.c2dm.intent.REGISTRATION";

        /// <summary>
        ///     Intent used by the GCM library to indicate that the registration call
        ///     should be retried.
        /// </summary>
        public const string IntentFromGcmLibraryRetry = "com.google.android.gcm.intent.RETRY";

        /// <summary>
        ///     Intent sent by GCM containing a message.
        /// </summary>
        public const string IntentFromGcmMessage = "com.google.android.c2dm.intent.RECEIVE";

        /// <summary>
        ///     Extra used on {@link #INTENT_TO_GCM_REGISTRATION} to indicate the sender
        ///     account (a Google email) that owns the application.
        /// </summary>
        public const string ExtraSender = "sender";

        /// <summary>
        ///     Extra used on {@link #INTENT_TO_GCM_REGISTRATION} to get the application id.
        /// </summary>
        public const string ExtraApplicationPendingIntent = "app";

        /// <summary>
        ///     Extra used on {@link #INTENT_FROM_GCM_REGISTRATION_CALLBACK} to indicate
        ///     that the application has been unregistered.
        /// </summary>
        public const string ExtraUnregistered = "unregistered";

        /// <summary>
        ///     Extra used on {@link #INTENT_FROM_GCM_REGISTRATION_CALLBACK} to indicate
        ///     an error when the registration fails. See constants starting with ERROR_
        ///     for possible values.
        /// </summary>
        public const string ExtraError = "error";

        /// <summary>
        ///     Extra used on {@link #INTENT_FROM_GCM_REGISTRATION_CALLBACK} to indicate
        ///     the registration id when the registration succeeds.
        /// </summary>
        public const string ExtraRegistrationId = "registration_id";

        /// <summary>
        ///     Type of message present in the {@link #INTENT_FROM_GCM_MESSAGE} intent.
        ///     This extra is only set for special messages sent from GCM, not for
        ///     messages originated from the application.
        /// </summary>
        public const string ExtraSpecialMessage = "message_type";

        /// <summary>
        ///     Special message indicating the server deleted the pending messages.
        /// </summary>
        public const string ValueDeletedMessages = "deleted_messages";

        /// <summary>
        ///     Number of messages deleted by the server because the device was idle.
        ///     Present only on messages of special type
        ///     {@link #VALUE_DELETED_MESSAGES}
        /// </summary>
        public const string ExtraTotalDeleted = "total_deleted";

        /// <summary>
        ///     Permission necessary to receive GCM intents.
        /// </summary>
        public const string PermissionGcmIntents = "com.google.android.c2dm.permission.SEND";

        /// <summary>
        ///     @see GCMBroadcastReceiver
        /// </summary>
        public const string DefaultIntentServiceClassName = ".GCMIntentService";

        /// <summary>
        ///     The device can't read the response, or there was a 500/503 from the
        ///     server that can be retried later. The application should use exponential
        ///     back off and retry.
        /// </summary>
        public const string ErrorServiceNotAvailable = "SERVICE_NOT_AVAILABLE";

        /// <summary>
        ///     There is no Google account on the phone. The application should ask the
        ///     user to open the account manager and add a Google account.
        /// </summary>
        public const string ErrorAccountMissing = "ACCOUNT_MISSING";

        /// <summary>
        ///     Bad password. The application should ask the user to enter his/her
        ///     password, and let user retry manually later. Fix on the device side.
        /// </summary>
        public const string ErrorAuthenticationFailed = "AUTHENTICATION_FAILED";

        /// <summary>
        ///     The request sent by the phone does not contain the expected parameters.
        ///     This phone doesn't currently support GCM.
        /// </summary>
        public const string ErrorInvalidParameters = "INVALID_PARAMETERS";

        /// <summary>
        ///     The sender account is not recognized. Fix on the device side.
        /// </summary>
        public const string ErrorInvalidSender = "INVALID_SENDER";

        /// <summary>
        ///     Incorrect phone registration with Google. This phone doesn't currently support GCM.
        /// </summary>
        public const string ErrorPhoneRegistrationError = "PHONE_REGISTRATION_ERROR";
    }
}