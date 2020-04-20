using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlugraryDetectionSystemDAL.Contracts
{
    public interface IResidentsDAL
    {
        DataSet AddResidents(ReqAddResidents reqAddResidents);

        DataSet GetUserResidents(ReqGetUserResidents reqGetResidents);
    }
}
