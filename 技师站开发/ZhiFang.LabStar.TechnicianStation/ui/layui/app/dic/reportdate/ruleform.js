	/**
	@name：取单时间段表单信息
	@author：liangyl
	@version 2019-10-15
 */
layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form','laydate'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		laydate = layui.laydate,
		formtype='edit';
	//获取取单时间段规则数据服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateRuleById?isPlanish=true';
	//删除取单时间段规则
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateRule';
   	//新增取单时间段规则
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateRule';
	//修改取单时间段规则
	var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateRuleByField';
   //按钮是否可点击
	var BUTTON_CAN_CLICK = true;
   //外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	var REPORTDATEDESCID = PARAMS.REPORTDATEDESCID;
	
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
        if(data.LBReportDateRule_BeginTime){
        	var str1 = data.LBReportDateRule_BeginTime.split(' ')[1];
            var strTime = str1.split(':')[0]+":"+str1.split(':')[1];
       	    data.LBReportDateRule_BeginTime = strTime;
        }
        if(data.LBReportDateRule_EndTime){
        	var str2 = data.LBReportDateRule_EndTime.split(' ')[1];
            var endTime = str2.split(':')[0]+":"+str2.split(':')[1];
       	    data.LBReportDateRule_EndTime = endTime;
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
        	ReportDateDescID:REPORTDATEDESCID
		};
		if(rec.LBReportDateRule_BeginWeekDay)entity.BeginWeekDay=rec.LBReportDateRule_BeginWeekDay;
		if(rec.LBReportDateRule_EndWeekDay)entity.EndWeekDay=rec.LBReportDateRule_EndWeekDay;
		
//		if(rec.LBReportDateRule_Id)entity.Id=rec.LBReportDateRule_Id;
		var isBeginTimeValid  = uxutil.date.isValid(rec.LBReportDateRule_BeginTime);
		var isEndTimeValid  = uxutil.date.isValid(rec.LBReportDateRule_EndTime);
	
		if(rec.LBReportDateRule_BeginTime){
			var strBeginTime = rec.LBReportDateRule_BeginTime;
			if(!isBeginTimeValid)strBeginTime =  date1+" "+rec.LBReportDateRule_BeginTime;
			entity.BeginTime=uxutil.date.toServerDate(strBeginTime);
		}
		if(rec.LBReportDateRule_EndTime){
			var strEndTime = rec.LBReportDateRule_EndTime;
			if(!isEndTimeValid)strEndTime =  date1+" "+rec.LBReportDateRule_EndTime;
			entity.EndTime=uxutil.date.toServerDate(strEndTime);
		}
		return {entity:entity};
	}
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		var fields ='Id,ReportDateDescID,BeginWeekDay,EndWeekDay,BeginTime,EndTime';

		entity.fields = fields;//
		if (data["LBReportDateRule_Id"])
			entity.entity.Id = data["LBReportDateRule_Id"];
		return entity;
	};
	//保存
	function onSaveClick(obj){
		if(!BUTTON_CAN_CLICK)return;
		//校验
        var msg = isVerificationfunction(obj.field);
        if(msg){
        	layer.msg(msg);
        	return;
        }
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
					parent.afterUpdateRule(data);
				});
			} else {
				var msg = formtype=='add' ? '新增失败！' :'修改失败！';
				if(!data.msg)data.msg=msg;
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//数据校验,必填项，并且开始要小于结束
	function isVerificationfunction(obj){
		var msg = '';
		var BeginWeekDay = obj.LBReportDateRule_BeginWeekDay;
		var EndWeekDay = obj.LBReportDateRule_EndWeekDay;
		var BeginTime = obj.LBReportDateRule_BeginTime;
		var EndTime = obj.LBReportDateRule_EndTime;
		if(!BeginWeekDay || !EndWeekDay || !BeginTime || !EndTime){
			msg="开始星期,结束星期,开始时间,结束时间都不能为空!";
		}
		if(BeginWeekDay>EndWeekDay){
			msg="开始星期不能大于结束星期!";
		}
//		if(BeginTime>EndTime){
//			msg="开始时间不能大于结束星期!";
//		}
		return msg;
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
	function formatminutes(date) {
       $(".laydate-time-list li").eq(86).remove()
       $(".laydate-time-list li").css("width","50%")
       $(".laydate-time-list li ol li").css("width","100%")
    }
  //时间选择器
	laydate.render({
	    elem: '#LBReportDateRule_BeginTime',
	    type: 'time',
	    format: "HH:mm",
	    trigger: 'click',
		ready:formatminutes
	});
	 //时间选择器
	laydate.render({
	    elem: '#LBReportDateRule_EndTime',
	    type: 'time',
	    format: "HH:mm",
	     trigger: 'click',
		ready:formatminutes
	});
  
	//初始化
	function init(){
		if(!ID)formtype ='add';
		if(ID){
			//获取取单时间分类信息
			getInfoById(ID,function(data){
				form.val('LBReportDateRule',changeResult(data.ResultDataValue));
			});
		}
      
	}
	//初始化
	init();
});