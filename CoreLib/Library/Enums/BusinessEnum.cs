using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.Library
{
    public enum ConstContact
    {

        X = -1,
        Y = 2
    }
    public enum ConstFeatureView
    {
        Uncertain = -1,
        Sector,
        ProductType,
        ContactEmployeeCount,
        Department,
        ProductCategory,
        CurrencyType,
        MeasureOfUnit
    }
    public enum ConstItem
    {
        [DescriptionML("sample")] sample = 22149
    }
    public enum ConstUser
    {
        Administrator = 1,
        AdminZy = 3
    }

    public enum SerializeFrom
    {
        Uncertain=-1,

        View = 1,
        Entity = 3
    }
    public enum ConstDefaultValue
    {

    }
    public enum TaskStatus
    {
        Success = 200,
        Created = 201,
        NotFound = 300,
        Fail = 400,
        NotAuthorize = 401,
        ServerInternalError = 500,
        BusinessError = 501,
        Failed = 800,
        AuthorityNotFound = 997,
        AccessDenied = 998,
        NotSupported = 999
    }
    public enum RecordType
    {
        Nothing = 0,
        Added = 1,
        Modified = 2,
        Removed = 3
    }
    public enum ProductStatus
    {
        Normal = 0,
        CriticalStock = 1,
        EmptyStock = 2,
        ClosedSelling = 3
    }
    public enum TraverseResult
    {
        ComboTreeview,
        Treeview
    }

    public enum ProjectEnviroment
    {
        Live,
        Test,
        Local
    }
}
