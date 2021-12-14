using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.IBLL
{ 
    /// <summary>
    ///
    /// </summary>
    public interface IBOSDoctorOrderForm : IBGenericManager<OSDoctorOrderForm>
    {
        BaseResultDataValue AddOSDoctorOrderFormVO(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageTestAction,string v, Entity.ViewObject.Request.OSDoctorOrderFormVO entity);
        BaseResultDataValue AddOSDoctorOrderFormVOByUser(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageTestAction,  Entity.ViewObject.Request.OSDoctorOrderFormVO entity);

        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.OSDoctorOrderFormVO> VO_OSDoctorOrderFormList(IList<OSDoctorOrderForm> listOSDoctorOrderForm);

        ZhiFang.WeiXin.Entity.ViewObject.Response.OSDoctorOrderFormVO VO_OSDoctorOrderForm(OSDoctorOrderForm osDoctorOrderForm);

    }
}