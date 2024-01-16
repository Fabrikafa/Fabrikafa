using System;

namespace Fabrikafa.Globals;

public class CustomClaimTypesGlobal
{
    public const string UserId = "UserId";
    
    [Obsolete("CorporateId global claim type is obsolote. Please change it to: CorporationId")]
    public const string CorporateId = "CorporateId";
    public const string CorporateName = "CorporateName";
    public const string CorporateTitle = "CorporateTitle";
    public const string CorporateHostName = "CorporateHostName";
    public const string CorporationId = "CorporationId";
}
