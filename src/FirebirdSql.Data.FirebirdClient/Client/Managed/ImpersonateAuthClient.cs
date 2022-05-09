using System.IO;
using System.Text;
using ICE.FirebirdEncryption;

namespace FirebirdSql.Data.Client
{
	internal sealed class ImpersonateAuthClient

	{
		public string Name => "Impersonate_Auth";
		public string ImpersonateUser { get; }
		public string Database { get; }

		internal ImpersonateAuthClient(string database, string impersonateUser)
		{
			ImpersonateUser = impersonateUser;
			Database = database;
		}

		internal string GetEncryptedPassowrd(string plainText)
		{
			var encPassword = FbEncryption.EncryptString(plainText);
			return encPassword;
		}

		internal byte[] GetClientData()
		{
			using (var s = new MemoryStream())
			{
				var sw = new BinaryWriter(s);

				sw.Write(ImpersonateUser.Length);
				sw.Write(Encoding.UTF8.GetBytes(ImpersonateUser));
				sw.Write(Database.Length);
				sw.Write(Encoding.UTF8.GetBytes(Database));
				s.Position = 0;

				return s.GetBuffer();
			}
		}
	}
}
