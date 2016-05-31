using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public  class FixtureTypeBLL
    {
      /// <summary>
      /// To Add Fixture Type
      /// </summary>
      /// <param name="ObjFixture"></param>
      /// <returns></returns>
      public string AddFixtureType(FixtureTypeBO ObjFixture)
      {
          return (new FixtureTypeDAL()).AddFixtureType(ObjFixture);
      }

      /// <summary>
      /// To Update Fixture Type
      /// </summary>
      /// <param name="ObjFixture"></param>
      /// <returns></returns>
      public string UpdateFixtureType(FixtureTypeBO ObjFixture)
      {
          return (new FixtureTypeDAL()).UpdateFixtureType(ObjFixture);
      }

      /// <summary>
      /// To Delete Fixture Type
      /// </summary>
      /// <param name="fixtureID"></param>
      /// <returns></returns>
      public string DeleteFixtureType(int fixtureID)
      {
          return (new FixtureTypeDAL()).DeleteFixtureType(fixtureID);
      }

      /// <summary>
      /// To Obsolete Fixture Type
      /// </summary>
      /// <param name="fixtureID"></param>
      /// <param name="IsDeleted"></param>
      /// <returns></returns>
      public string ObsoleteFixtureType(int fixtureID, string IsDeleted)
      {
          return (new FixtureTypeDAL()).ObsoleteFixtureType(fixtureID, IsDeleted);
      }

      /// <summary>
      /// To Get All Fixture Type
      /// </summary>
      /// <param name="fixturetype"></param>
      /// <returns></returns>
      public FixtureTypeList GetAllFixtureType(string fixturetype)
      {
          return (new FixtureTypeDAL()).GetAllFixtureType(fixturetype);
      }
    }
}
