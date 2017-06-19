 
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.BLL
{
		
    public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService //crud
    {
	} 
	
    public partial class FileInfoService:BaseService<FileInfo>,IFileInfoService //crud
    {
	} 
	
    public partial class MenuInfoService:BaseService<MenuInfo>,IMenuInfoService //crud
    {
	} 
	
    public partial class OrderInfoService:BaseService<OrderInfo>,IOrderInfoService //crud
    {
	} 
	
    public partial class R_UserInfo_ActionInfoService:BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService //crud
    {
	} 
	
    public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService //crud
    {
	} 
	
    public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService //crud
    {
	} 
	
    public partial class UserInfoExtService:BaseService<UserInfoExt>,IUserInfoExtService //crud
    {
	} 
	
    public partial class WF_InstanceService:BaseService<WF_Instance>,IWF_InstanceService //crud
    {
	} 
	
    public partial class WF_StepService:BaseService<WF_Step>,IWF_StepService //crud
    {
	} 
	
    public partial class WF_TempService:BaseService<WF_Temp>,IWF_TempService //crud
    {
	} 
}