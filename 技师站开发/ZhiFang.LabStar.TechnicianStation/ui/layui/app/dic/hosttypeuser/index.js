/**
   @Name：站点类型关系
   @Author：liangyl
   @version 2021-08-04
 */
layui.extend({
	uxutil: 'ux/util',
	HostTypeEmp:'app/dic/hosttypeuser/hosttypeemp/hosttypeemp',//站点类型与人员关系
	EmpHostType:'app/dic/hosttypeuser/emphosttype/emphosttype'//人员与站点类型关系
}).use(['uxutil','element','HostTypeEmp','EmpHostType'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		element = layui.element,
		HostTypeEmp = layui.HostTypeEmp,
		EmpHostType = layui.EmpHostType;
    //站点类型与人员页签实例
    var tab_ind0 = null;
     //人员与站点类型页签实例
    var tab_ind1 = null;
    //人员与站点类型页签是否已切换
    var isload =false;
    //初始化站点类型与人员页签
    tab_ind0 = HostTypeEmp.render({});
    //页签切换
    element.on('tab(tab)', function(obj){
        if(obj.index==1 && !isload){//初始化 人员与站点类型页签实例
       	    tab_ind1 = EmpHostType.render({});
            isload = true;
        }
    });
});