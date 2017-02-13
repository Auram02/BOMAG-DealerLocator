#region FILE HEADER
/// <project>ZipCodeUtil</project>
/// <assembly>SagaraSoftware.ZipCodeUtil.dll</assembly>
/// <filename>Globals.cs</filename>
/// <creator>Jon Sagara</creator>
/// <description>
/// Contains the Globals class.
/// </description>
/// <copyright>
/// Copyright (c) 2004 Sagara Software.  All rights reserved.
/// </copyright>
/// <disclaimer>
/// This file is provided "as is" with no expressed or implied warranty.  The author accepts no 
///  liability for any damage/loss of business that this product may cause.
/// </disclaimer>
/// <history>
///	<change date="12/29/2004" changedby="Jon Sagara">File created.</changed>
/// </history>
#endregion

using System;

using System.Configuration;


namespace SagaraSoftware.ZipCodeUtil
{
	/// <summary>
	/// Summary description for Globals.
	/// </summary>
	public class Globals
	{
		#region CONSTANTS
		/// <summary>
		/// The radius of the Earth in miles, assuming it is a sphere.
		/// </summary>
		public const Double kEarthRadiusMiles = 3956.0;
        //public const string sConnectionString = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=C:\\Caspar\\Development\\Bomag Americas\\Dealer Locator (a.k.a. Dark Llama)\\Documents\\;Extensions=csv,txt";
        //public const string sConnectionString = "data source=Arad\\AradSQL;Initial Catalog=DealerLocator;Persist Security Info=True;User ID=dl;Password=dl";
        
        
        //public const string sConnectionString = "Data Source=sql2k503.discountasp.net;Initial Catalog=SQL2005_325715_dealerlocate;Persist Security Info=True;User ID=SQL2005_325715_dealerlocate_user;Password=ttiemann";
        public static string sConnectionString = ConfigurationManager.ConnectionStrings["DealerLocatorConnectionString"].ToString();
        
        

        //Dbq=C:\\Caspar\\Development\\Bomag Americas\\Dealer Locator (a.k.a. Dark Llama)\\Documents\\ZIPCODEWORLD-US-PREMIUM-SAMPLE.CSV;
        public const string CSVFileName = "ZIPCODEWORLD.CSV";



        public const string sProvider = "ACCESS";

		#endregion
	}
}
