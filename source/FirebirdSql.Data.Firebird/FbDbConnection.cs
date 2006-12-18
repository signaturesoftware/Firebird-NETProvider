/*
 *  Firebird ADO.NET Data provider for .NET and Mono 
 * 
 *     The contents of this file are subject to the Initial 
 *     Developer's Public License Version 1.0 (the "License"); 
 *     you may not use this file except in compliance with the 
 *     License. You may obtain a copy of the License at 
 *     http://www.ibphoenix.com/main.nfs?a=ibphoenix&l=;PAGES;NAME='ibp_idpl'
 *
 *     Software distributed under the License is distributed on 
 *     an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either 
 *     express or implied.  See the License for the specific 
 *     language governing rights and limitations under the License.
 * 
 *  Copyright (c) 2002, 2004 Carlos Guzman Alvarez
 *  All Rights Reserved.
 */

using System;
using System.Text;
using System.Collections;

using FirebirdSql.Data.Common;

namespace FirebirdSql.Data.Firebird
{	
	internal class FbDbConnection : MarshalByRefObject
	{
		#region Fields

		private IDbAttachment		db;
		private AttachmentParams	parameters;
		private long				created;
		private long				lifetime;
		private bool				pooled;
		
		#endregion

		#region Properties

		public IDbAttachment DB
		{
			get { return this.db; }
		}

		public long Lifetime
		{
			get { return this.lifetime; }
		}

		public long Created
		{
			get { return this.created; }
			set { this.created = value; }
		}
		
		public bool Pooled
		{
			get { return this.pooled; }
			set { this.pooled = value; }
		}

		public AttachmentParams Parameters
		{
			get { return this.parameters; }
		}

		#endregion

		#region Constructors

		public FbDbConnection(AttachmentParams parameters)
		{
			this.parameters	= parameters;
			this.lifetime	= this.parameters.LifeTime;
		}

		#endregion

		#region Methods

		public void Connect()
		{							
			try
			{
				FactoryBase factory = ClientFactory.GetInstance(
					this.parameters.ServerType);
				this.db = factory.CreateDbConnection(this.parameters);
				this.db.Attach();
			}
			catch (IscException ex)
			{
				throw new FbException(ex.Message, ex);
			}
		}
		
		public void Disconnect()
		{	
			try
			{
				this.db.Detach();
			}
			catch (IscException ex)
			{
				throw new FbException(ex.Message, ex);
			}
		}

		public bool Verify()
		{
			int INFO_SIZE = 16;
			
			byte[] buffer = new byte[INFO_SIZE];
			
			// Do not actually ask for any information
			byte[] items  = new byte[]
			{
				IscCodes.isc_info_end
			};

			try 
			{
				this.db.GetDatabaseInfo(items, buffer);

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}
}
