using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemBAL.Contracts
{
    public interface IResidentBAL
    {
        string AddResident(ReqAddResidents reqAddResident);
    }
}
