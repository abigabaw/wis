using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CardTypeBLL
    {
        /// <summary>
        /// To GET ALL CardType
        /// </summary>
        /// <returns></returns>
        public CardTypeList GETALLCardType()
        {
            CardTypeDAL CardTypeDALObj = new CardTypeDAL();
            return CardTypeDALObj.GETALLCardType();
        }

        // serach the data from the Database Mst_CardType
        public CardTypeList GetCardType()
        {
            CardTypeDAL CardTypeDALObj = new CardTypeDAL();
            return CardTypeDALObj.GetCardType();
        }
        //Insert the data into Database
        public string Insert(CardTypeBO objCardType)
        {
            CardTypeDAL CardTypeDAl = new CardTypeDAL(); //Data pass -to Database Layer

            try
            {
                return CardTypeDAl.Insert(objCardType);
            }
            catch
            {
                throw;
            }
            finally
            {
                CardTypeDAl = null;
            }
        }
        //Search the Singal Data by passing ID
        public CardTypeBO GetCardTypeById(int CardTypeID)
        {
            CardTypeDAL CardTypeDALObj = new CardTypeDAL();
            return CardTypeDALObj.GetCardTypeById(CardTypeID);
        }
        //Edit the data
        public string EDITCardType(CardTypeBO objCardType)
        {
            CardTypeDAL CardTypeDAl = new CardTypeDAL(); //Data pass -to Database Layer

            try
            {
                return CardTypeDAl.EDITCardType(objCardType);
            }
            catch
            {
                throw;
            }
            finally
            {
                CardTypeDAl = null;
            }
        }
        //Delete the data
        public string DeleteCardType(int CardTypeID)
        {
            CardTypeDAL CardTypeDALObj = new CardTypeDAL();
            return CardTypeDALObj.DeleteCardType(CardTypeID);
        }

        public string ObsoleteCardType(int CardTypeID, string Isdeleted)
        {
            CardTypeDAL CardTypeDALObj = new CardTypeDAL();
            return CardTypeDALObj.ObsoleteCardType(CardTypeID, Isdeleted);
        }
    }
}