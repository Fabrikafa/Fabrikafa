# About
Fabrikafa Library contains helper classes and methods for Fabrikafa projects. 


# Version History
1.0.0.0 
- Initial (Legacy) Version which is already used in some projects.

2.0.0.0 
- Upgraded to .NET 6.0

2.0.1.0
- LangVersion upgraded to 10.0
- Added FabrikafaSettings class to the Fabrikafa.Common.Settings class to reflect latest settings files used.

2.0.2.0
- Added Date Service. With this service "Now" returns UtcNow by default.
- Added "ViewDataGlobal" class for to store global keys to use along with ViewData
- Added "TempDataGlobal" class for to store global keys to use along with TempData
- Namespace and folder changed from "Fabrikafa.Web" to "Fabrikafa.Sistem" for classes: "CookieKeyGlobal", "CustomClaimTypesGlobal", "PageNameGlobal", "PolicyNameGlobal", "RoleNameGlobal", "SessionKeyGlobal", "TempDataGlobal", "ViewDataGlobal".
- All namespaces changed to File Scoped Namespaces