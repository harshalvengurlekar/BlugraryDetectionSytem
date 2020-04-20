using BlugraryDetectionSystemEntities.RequestEntities;
using BlugraryDetectionSystemEntities.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemBAL.Contracts
{
    public interface IResidentBAL
    {
        string AddResident(ReqAddResidents reqAddResident);

        List<ResGetUserResidents> GetUserResidents(ReqGetUserResidents reqGetResidents);
    }
}
