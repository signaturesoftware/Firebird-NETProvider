using ICE.FirebirdEncryption;
using System.Text;

namespace FirebirdSql.Data.Client
{
	internal sealed class ImpersonateAuthClient

	{
		public const string PluginName = "Impersonate_Auth";

		public string GetEncryptedPassowrd(string plainText)
		{
			var encPassword = FbEncryption.EncryptString(plainText);
			return encPassword;
		}

	}
}
