	/**
	@name：取单时间分类明细
	@author：liangyl
	@version 2019-10-15
 */
layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		formtype='edit';
	//获取取单时间段规则数据服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateDescById?isPlanish=true';
	//删除取单时间段规则
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateDesc';
   	//新增取单时间段规则
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateDesc';
	//修改取单时间段规则
	var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateDescByField';
   //按钮是否可点击
	var BUTTON_CAN_CLICK = true;
   //外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	var REPORTDATEID = PARAMS.REPORTDATEID;
	
	form.render();
	 /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		var me = this;
		var type = typeof data,
			obj = data;
		if(type == 'string'){
			obj = obj.replace(/\\"/g, '&quot;');
			obj = obj.replace(/\\/g, '\\\\');
			obj = obj.replace(/&quot;/g, '\\"');
			
		}
        var data = obj ? $.parseJSON(obj) : {};
        if(data.LBReportDateDesc_BeginTime){
        	var str1 = data.LBReportDateDesc_BeginTime.split(' ')[1];
            var strTime = str1.split(':')[0]+":"+str1.split(':')[1];
       	    data.LBReportDateDesc_BeginTime = strTime;
        }
        if(data.LBReportDateDesc_EndTime){
        	var str2 = data.LBReportDateDesc_EndTime.split(' ')[1];
            var endTime = str2.split(':')[0]+":"+str2.split(':')[1];
       	    data.LBReportDateDesc_EndTime = endTime;
        }
	    return data; 
	};
	/**创建数据字段*/
	function getStoreFields () {
		var fields = [];
		$(":input").each(function(){ 
			if(this.name)fields.push(this.name)
	    });
		return fields;
	}
	//获取取单时间分类信息
	function getInfoById(id,callback){
		var url = GET_LIST_URL;
		url +='&id=' + id+
		'&fields='+getStoreFields().join(',');
		uxutil.server.ajax({
			url:url,
		},function(data){
			if(data.success){
				callback(data);
			}
		});
	}
	 /**@overwrite 获取新增的数据*/
	function getAddParams(rec){
		var me = this;
		var sysdate = new Date();//应该取集成平台的系统服务时间
		var date1 = uxutil.date.toString(sysdate,true);
	    var entity = {
			ReportDateDesc: rec.LBReportDateDesc_ReportDateDesc,
            IsUse:1
		};
		if(REPORTDATEID)entity.LBReportDate = {Id:REPORTDATEID,DataTimeStamp:[0,0,0,0,0,0,0,0]};
		if(rec.LBReportDateDesc_DispOrder)entity.DispOrder=rec.LBReportDateDesc_DispOrder;
		
		return {entity:entity};
	}
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		var fields ='Id,ReportDateDesc,LBReportDate_Id,IsUse,DispOrder';

		entity.fields = fields;//
		if (data["LBReportDateDesc_Id"])
			entity.entity.Id = data["LBReportDateDesc_Id"];
		return entity;
	};
	//保存
	function onSaveClick(obj){
		if(!BUTTON_CAN_CLICK)return;
		var url = formtype == 'add' ? ADD_URL : EDIT_URL;
		var params = formtype == 'add' ? getAddParams(obj.field) : getEditParams(obj.field);
		if (!params) return;
		var id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: url,
			data: params
		};
		var index = layer.load();
		BUTTON_CAN_CLICK = false;
		uxutil.server.ajax(config1, function(data) {
			BUTTON_CAN_CLICK = false;
			//隐藏遮罩层
			if (data.success) {
				layer.msg('保存成功', {
					icon: 6, anim: 0 ,time:2000 
				}, function(){
					parent.layer.closeAll('iframe');
					parent.afterUpdateDesc(data);
				});
			} else {
				var msg = formtype=='add' ? '新增失败！' :'修改失败！';
				if(!data.msg)data.msg=msg;
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//取消
	$("#cancel").on("click",function(){
		if(!BUTTON_CAN_CLICK)return;
		parent.layer.closeAll('iframe');
	});
	//保存
	form.on("submit(save)",function(obj){
		onSaveClick(obj);	
	});

	//初始化
	function init(){
		if(!ID)formtype ='add';
		if(ID){
			//获取取单时间分类信息
			getInfoById(ID,function(data){
				form.val('LBReportDateDesc',changeResult(data.ResultDataValue));
			});
		}
      
	}
	//初始化
	init();
});