using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using NUnit.Framework;

using System.Collections.Generic;

namespace Dealer_Locator.Tests
{
    [TestFixture]
    public class MapTests
    {

        [SetUp]
        public void Init()
        {

        }


        [Test, Category("Tests")]
        public void GetMapByIDTest()
        {
            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(1);

            Assert.IsNotNull(tempMap);
            Assert.AreEqual(1, tempMap.MapID);

        }

        [Test, Category("Tests")]
        public void GetAllMapsTest()
        {
            BR.Mapping temp = new Dealer_Locator.BR.Mapping();

            List<BR.Mapping> tempMaps = temp.GetAllMaps();

            Assert.Greater(tempMaps.Count, 0, "No maps retrieved");
        }

        [Test, Category("Tests")]
        public void AddRemoveMapFieldTest()
        {
            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(999);

            int TotalFieldMappings = tempMap.MapFields.Count;

            int NewFieldMapID = tempMap.AddMapping("TestField", 999);


            // add
            tempMap.RefreshFieldMappings();

            Assert.Greater(NewFieldMapID, -1, "Field Mapping Add Failed");
            Assert.AreEqual(TotalFieldMappings + 1, tempMap.MapFields.Count, "Field Mapping Counts are not the same.  The insert may have failed");

            // Remove
            bool IsRemoveMappingSuccessful = tempMap.RemoveMapping(NewFieldMapID);

            tempMap.RefreshFieldMappings();
            Assert.IsTrue(IsRemoveMappingSuccessful, "Field Mapping Removal Failed");

            Assert.AreEqual(tempMap.MapFields.Count, TotalFieldMappings, "Field Mapping Counts are not the same.  The removal may have failed");
        }

        [Test, Category("Tests")]
        public void WorldOfAsphalt_v1_ReadTest()
        {
            string TextToProcess = "%WOA071^2206^WILSON^STEVE^^BOMAG AMERICAS^2000 KENTVILLE RD^^KEWANEE^IL^61443?@^^3098526211^3098520350^KAREN.DECONINCK*BOMAG.COM^RXF1^B04^C08^D50^E86^^0000?&010                                                                         ?" + Environment.NewLine;

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(999);
            List<string> fieldList = tempMap.ProcessMap("World of Asphalt", TextToProcess);

            Assert.Greater(fieldList.Count, 0, "No fields returned");
         }




        [Test, Category( "Tests" )]
        public void SmartReg_v1_ReadTest()
        {
            string TextToProcess = @"%10078\STEVE^BASARICH\\GREAT WEST EQUIPMENT INC.\ATTN: DOUG ZOERB\2000 KENTVILL?@E RD.\KEWANEE^IL^61443\\\\\END]?& ?" + Environment.NewLine;

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "SmartReg", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }


        [Test, Category( "Tests" )]
        public void Epic_v1_ReadTest()
        {
            string TextToProcess = @"974-900	974-900	Tim		Tiemann	Tim	Bomag Americas, Inc.	2000 Kentville Road	Kewanee	IL	61443	United States of America	3098533571	Graphics Supervisor		Mr.		3098520350	info.bomag@bomag.com					EXC									" + Environment.NewLine;

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "Epic", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }

        [Test, Category("Tests")]
        public void WasteExpo_v1_ReadTest()
        {
            string TextToProcess = "201689$          $CHRIS$CONNOLLY$$BOMAG$2000 KENTVILLE ROAD$$KEWANEE$IL$61443$UNITED STATES$$$$KAREN.DECONINCK@BOMAG.COM$EX$FF$B$F$B,C$B$A$ZZ$";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(999);
            List<string> fieldList = tempMap.ProcessMap("Waste Expo", TextToProcess);

            Assert.Greater(fieldList.Count, 0, "No fields returned");
        }


        [Test, Category("Tests")]
        public void ConExpo2008_v1_ReadTest()
        {
            string TextToProcess = "%CON081^19915^ZOERB^DOUG^^BOMAG AMERICAS, INC.^2000 KENTVILLE RD^^KEWANEE^IL^?@61443^^RX99^3098526115^3098525791^DOUG.ZOERB*BOMAG.COM^B04^C05^D06^^X^^19021?&64^000000000000000                                                          ?";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(999);
            List<string> fieldList = tempMap.ProcessMap("Experient", TextToProcess);

            Assert.Greater(fieldList.Count, 0, "No fields returned");
        }

        [Test, Category( "Tests" )]
        public void ConExpo2008_v1_ReadTest2()
        {
            string TextToProcess = "%CON081^98337^POCOCK^STUART^^S.A LIFT AND LOADER^253 HANSON ROAD^WINGFIELD^WI?@NGFIELD SA^^5013^AUSTRALIA^RG1A^61882430788^61883471466^SPOCOCK*SALIFTANDLOA?&DER.COM.AU^B04^C02^D50^F80^^^^000000000008000                               ?";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "Experient", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }

        [Test, Category( "Tests" )]
        public void CompuLead_v1_ReadTest1()
        {
            string TextToProcess = "JohnSmithOwnerCompusystemsssssssssssssss1620 N Ronsted BLVDCarrolltonTX75006526276242Badge NOT for show";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "CompuLead", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }

        [Test, Category( "Tests" )]
        public void CompuLead_v1_ReadTest2()
        {
            string TextToProcess = "TAKATOSHIKWANVICE PRESIDENTGOYO OPTICAL INC3617 HAMAZAKI ASAKABLDGH123abc@987xyzSAITAMA351JAPAN81 0484742235708 3444444485154576Badge NOT for show";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "CompuLead", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }


        [Test, Category( "Tests" )]
        public void CompuLead_v1_ReadTest3()
        {
            string TextToProcess = "KimGulczynskiManagerNational Retail Federation325 7th St. NW, Ste.1100kimg@compusystms.comWashingtonDC20004-0000202 783 7971202 737 2849420190120Badge NOT for show";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "CompuLead", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }

        

        [Test, Category( "Tests" )]
        public void CompuLead__ARA_v1_ReadTest()
        {
            string TextToProcess = "|JEAN|P||BLACK|||COMPUSYSTEMS||507 PLACE D'ARMES #1407||TOWER HEIGHTS BLDG|||PARIS||||FRANCE|5148443450||670164022||Institution ID:|Individual ID:|000005039026027032036+";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping( 999 );
            List<string> fieldList = tempMap.ProcessMap( "CompuLead_ARA", TextToProcess );

            Assert.Greater( fieldList.Count, 0, "No fields returned" );
        }

        // Note: Epic maps may work
        [Test, Category("Tests")]
        public void ConExpo2011_v1_ReadTest()
        {
            string TextToProcess = "CON111	74582	Lead	Record1	TitleTitleTitleTitleTitleTitleTitleTitle	CompanyCompanyCompanyCompanyCompanyCompa	AddressAddressAddressAddressAddressAddre	Add2Add2Add2Add2Add2Add2Add2Add2Add2Add2	Frederick	MD	21701		1111111111	2222222222	3333333333	stephanie.gatti.stephanie.gatti.stephanie@test.com	B01	C0";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(999);
            List<string> fieldList = tempMap.ProcessMap("ConExpo 2011", TextToProcess);

            Assert.Greater(fieldList.Count, 1, "Only One field returned");
        }

        [Test, Category("Tests")]
        public void ConExpo2011_v2_ReadTest()
        {
            string TextToProcess = "CON111	74583	Lead	Record2	Title	Company	Address1	Address2	Frederick	MD	21701		3016629401		3016629411	stephanie.gatti@experient-inc.com	B02	C01	D01	E01	20000	80000					000	0000	";

            BR.Mapping tempMap = new Dealer_Locator.BR.Mapping(999);
            List<string> fieldList = tempMap.ProcessMap("ConExpo 2011", TextToProcess);

            Assert.Greater(fieldList.Count, 1, "Only one field returned");
        }



        [Test, Category("Tests")]
        public void AddRemoveTest()
        {
            int NewMapID = -1;

            bool ResultRemove = false;


            NewMapID = BR.Mapping.AddNewMap("TestInsertMap");

            ResultRemove = BR.Mapping.DeleteMap(NewMapID);

            Assert.Greater(NewMapID, -1, "New Map was not created successfully");
            Assert.IsTrue(ResultRemove, "Removal of new map TestInsertMap with pk_mapID (" + NewMapID + ") failed.");


        }


    }
}
