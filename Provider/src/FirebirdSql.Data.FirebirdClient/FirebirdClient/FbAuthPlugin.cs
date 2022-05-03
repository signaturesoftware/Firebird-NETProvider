using System;

namespace FirebirdSql.Data.FirebirdClient
{
	[Serializable]
	public enum FbAuthPlugin
	{
		Srp,
		Win_Sspi,
		Impersonate_Auth
	}
}
