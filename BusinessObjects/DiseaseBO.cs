using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class DiseaseBO
    {
        private int diseaseID = -1;
        private string diseaseName = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string isdeleted;

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }

        public int DiseaseID
        {
            get
            {
                return diseaseID;
            }
            set
            {
                diseaseID = value;
            }
        }

        public string DiseaseName
        {
            get
            {
                return diseaseName;
            }
            set
            {
                diseaseName = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }
    }
}

/**
 * 
 * @version          :Disease Master
 * @package          :Disease
 * @copyright        :Copyright © 2013 - All rights reserved.
 * @author           :Hanamant SIngannavar
 * @Created Date     :17-Apr-2013 
 * @Updated By
 * @Updated Date
 * 
 */