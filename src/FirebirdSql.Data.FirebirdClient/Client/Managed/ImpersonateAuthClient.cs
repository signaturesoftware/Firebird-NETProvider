using ICE.FirebirdEncryption;

namespace FirebirdSql.Data.Client
{
	internal sealed class ImpersonateAuthClient

	{
		public string Name => "Impersonate_Auth";
		public string ImpersonateUser { get; private set; }

		public ImpersonateAuthClient(string impersonateUser)
		{
			ImpersonateUser = impersonateUser;
		}

		public string GetEncryptedPassowrd(string plainText)
		{
			var encPassword = FbEncryption.EncryptString(plainText);
			return encPassword;
		}

	}
}
