	/**
	@name：取单时间分组表单信息
	@author：liangyl
	@version 2019-10-15
 */
layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		//获取取单时间分类信息列表服务地址
		GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateById?isPlanish=true',
		ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBReportDate',
		UPADTE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateByField',
		formtype='edit';
	 //获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBReportDate&entityField=DispOrder';
	
	form.render();
   //按钮是否可点击
	var BUTTON_CAN_CLICK = true;
   //外部参数
	var PARAMS = uxutil.params.get(true);
	//消息ID
	var ID = PARAMS.ID;
	 /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		data = data.replace(/\\u000d\\u000a/g,'').replace(/\\u000a/g,'<br />').replace(/[\r\n]/g,'<br />');
		var list =  JSON.parse(data);
		if(list.LBReportDate_IsUse=="false")list.LBReportDate_IsUse="";
		var reg = new RegExp("<br />", "g");
		list.LBReportDate_Datememo = list.LBReportDate_Datememo.replace(reg, "\r\n");
	    return list;
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
	function getAddParams(data) {
		var entity = JSON.stringify(data).replace(/LBReportDate_/g, "");
		if (entity) entity = JSON.parse(entity);
		if (entity.IsUse) entity.IsUse = entity.IsUse ? 1 :0;
		if (entity.DispOrder)entity.DispOrder = Number(entity.DispOrder);
		if (!entity.DispOrder)delete entity.DispOrder;
		if (!entity.Id)delete entity.Id;
		
		if(data.LBReportDate_Datememo) {
           entity.Datememo = data.LBReportDate_Datememo.replace(/\\/g, '&#92');
		}
		return {
			entity: entity
		};
	};
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		
		entity.fields = 'Id,CName,DispOrder,Datememo,IsUse';//
		if (data["LBReportDate_Id"])
			entity.entity.Id = data["LBReportDate_Id"];
		return entity;
	};
	//保存
	function onSaveClick(obj){
		if(!BUTTON_CAN_CLICK)return;
		var url = formtype == 'add' ? ADD_URL : UPADTE_URL;
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
					parent.afterUpdate(data);
				});
			} else {
				var msg = formtype=='add' ? '新增失败！' :'修改失败！';
				if(!data.msg)data.msg=msg;
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	 //获取指定实体字段的最大号
    function getMaxNo(callback) {
        var me = this;
        var result = "";
        uxutil.server.ajax({
            url: GET_MAXNO_URL
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
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
		//add
		if(!ID){
			formtype ='add';
		    getMaxNo(function (val) {
	            document.getElementById('LBReportDate_DispOrder').value = val;
	        })
		}
		if(ID){
			//获取取单时间分类信息
			getInfoById(ID,function(data){
				form.val('LBReportDate',changeResult(data.ResultDataValue));
			});
		}
	}
	
	//初始化
	init();
});