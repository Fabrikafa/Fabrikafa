# Version History
## 1.0.0
- Initial (Legacy) Version which is already used in some projects.

## 2.0.0
- Upgraded to .NET 6.0

## 2.0.1
- LangVersion upgraded to 10.0
- Added FabrikafaSettings class to the Fabrikafa.Common.Settings class to reflect latest settings files used.

## 2.0.2
- Added Date Service. With this service "Now" returns UtcNow by default.
- Added "ViewDataGlobal" class for to store global keys to use along with ViewData
- Added "TempDataGlobal" class for to store global keys to use along with TempData
- Namespace and folder changed from "Fabrikafa.Web" to "Fabrikafa.Sistem" for classes: "CookieKeyGlobal", "CustomClaimTypesGlobal", "PageNameGlobal", "PolicyNameGlobal", "RoleNameGlobal", "SessionKeyGlobal", "TempDataGlobal", "ViewDataGlobal".
- All namespaces changed to File Scoped Namespaces

## 2.0.3
- "Fabrikafa.Result" class XML documentation updated.
- "Fabrikafa.Result" class refactorings.
- RequestId added for "Fabrikafa.Result" class.
- CorporateId global claim type is obsolote. New type added: CorporationId
- OperationTypeEnum moved to "Fabrikafa.Sistem.Enums"
- CorporationTypeEnum added to "Fabrikafa.Sistem.Enums"

## 2.0.4
- Added Hash and Salt classes and related create functions.
- Added "GetHostNameFromEmail" function
- Corrected class names for email sender services

## 2.0.5
- Added "API" class under Settings > Application for "FabrikafaSettings" class to reflect latest settings files used.
- Added new method to "EmailSender" classes with ability to add email to CC and BCC. 
- Added nuget package "Microsoft.Extensions.Configuration.Binder" to bind "FabrikafaSettings" class instead of accessing configuration setting with their keys.
- Added "Domain" and "UseDefaultCredentials" SMTP settings for "FabrikafaSettings" class
- Refactored "EmailSender"
- Added missing global page names
- Added "Pager" class for displaying paged grid tables
- Added string extension method to return intials of a string
- Simple extension method to sanitize input strings. Filters unwanted chars from untrusted inputs
- Modified post built script tp point output folder to C drive again. Also added folder for keeping versions
- Modified "solution items" and "documents" folder for solution
- Explicit Index page names for "PageNameGlobal"
- FilterInput extension method fix for not creating redundant white spaces.
- "Sistem" folder renamed to "Globals" and namespaces changed accordingly. Fabrikafa.Sistem -> Fabrikafa.Globals.
- "ViewDataGlobal" class renamed to "ViewDataKeyGlobal"
- Name other global classes accordingly ot match others. "CustomClaimTypesGlobal" -> "CustomClaimTypeGlobal"
- Add "ConstantGlobal" class
- Add new IsValidEmail method to validate without RegEx and make obsolete the current IsValidEmail method.
- Update to .NET 9.0
- Make obsolete app specific constants and enums which will be removed later.