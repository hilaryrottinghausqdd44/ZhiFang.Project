layui.extend({
	uxutil: 'ux/util'		
}).use(['form', 'uxutil', 'layer'], function(){
	"use strict";
	var $ = layui.jquery;
	var form = layui.form; 
	var layer = layui.layer;
    var uxutil = layui.uxutil;	
	
	//获取传入血袋登记ID
    var urlParams = uxutil.params.get() || {};
    var BagOperationID = urlParams["bagoperationid"] ? urlParams["bagoperationid"] : "";
    //定义血袋登记配置信息
	var bagoperation = {
		bagdata:{
			"BloodBagHandover_BagOperTime": "", //登记时间
			"BloodBagHandover_Id": "", //交接登记ID
			"BloodAppearance_Id": "",    //外观登记明细id
			"BloodAppearance_BDict_Id": "", //外观登记字典id
			"BloodIntegrity_Id": "",    //完整性登记明细Id
			"BloodIntegrity_BDict_Id": ""  //完整性登记字典id
		},
		bagopfields:[
		    'BloodBagHandoverVO_BloodBagHandover_BagOperTime', //登记时间
			'BloodBagHandoverVO_BloodBagHandover_Id',
		    'BloodBagHandoverVO_BloodAppearance_Id',
			'BloodBagHandoverVO_BloodAppearance_BDict_Id',
			'BloodBagHandoverVO_BloodIntegrity_Id',
		    'BloodBagHandoverVO_BloodIntegrity_BDict_Id'],
    	params:{id:"", fields: "",  isPlanish: true}, //table 690
    	bagopUrl: uxutil.path.ROOT + '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagHandoverVOById'
	};
	
	//禁用启用按钮
    function disabledLayerbutton(index, layero){
    	var buttonClass = '.layui-layer-btn' + index;
		var button = layero.find(buttonClass);
		var	className = "layui-btn-disabled";
		//禁用状态直接不处理
		if(button.hasClass(className)){
			return false;
		}
		button.addClass(className);
		setTimeout(function(){ 
			button.removeClass(className);
		}, 3000);   	
    };
    
    //设置确认框数据
    function setConfigData(layero){
        var data = {};
        var framedoc = layero.find('iframe').contents();
        var ele = framedoc.find('#appearance_select');  
        var appearanceId = ele.val(); //外观id
        ele = framedoc.find('#integrity_select');
        var integrityId = ele.val(); //完整性id
        ele = framedoc.find('#bagopertime_date');
        var bagopertime = ele.val(); //登记时间
        data["BloodAppearance_BDict_Id"] = appearanceId;
        data["BloodIntegrity_BDict_Id"] = integrityId;
        data["BloodBagHandover_BagOperTime"] = bagopertime;
        return data;
    };
    
	//通过血袋登记Id查询数据
    bagoperation.getBagOperationById = function(Id, callback){
     	var me = this;
       	var fields =  me.bagopfields.join(',');
		var data = me.params || {};
		data["id"] = Id; 
		data["fields"] = fields;
		data["where"] = "";
    	var config= {
    		type: 'get',
        	url: me.bagopUrl,
        	data: data
    	};
    	//查询数据
		uxutil.server.ajax(config, function(data) {
		    if (callback) callback(data);
		});   	
    };
    
    //保存查询回来的数据
    bagoperation.setBagOprationData = function(data){
    	var me = this;
    	me.bagdata["BloodBagHandover_Id"] = data["BloodBagHandoverVO_BloodBagHandover_Id"];
    	me.bagdata["BloodAppearance_Id"] = data["BloodBagHandoverVO_BloodAppearance_Id"];
    	me.bagdata["BloodAppearance_BDict_Id"] = data["BloodBagHandoverVO_BloodAppearance_BDict_Id"];
    	me.bagdata["BloodIntegrity_Id"] = data["BloodBagHandoverVO_BloodIntegrity_Id"];
    	me.bagdata["BloodIntegrity_BDict_Id"] = data["BloodBagHandoverVO_BloodIntegrity_BDict_Id"];    	
    	me.bagdata["BloodBagHandover_BagOperTime"] = data["BloodBagHandoverVO_BloodBagHandover_BagOperTime"];
    };
    
    //提交保存
    bagoperation.onSave =function(data, callback){
    	//提交url
    	var url = uxutil.path.ROOT + '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagHandoverVO';
    	var entity = {};
    	//提交参数格式
    	var params={"BloodBagHandover":{}, 
    		"BloodAppearance":{}, 
    		"BloodIntegrity":{}};
    	//血袋主单登记信息
    	params.BloodBagHandover["Id"] = data["BloodBagHandover_Id"];
    	params.BloodBagHandover["BagOperTime"] = uxutil.date.toServerDate(data["BloodBagHandover_BagOperTime"]);
    	//血袋外观信息
    	params.BloodAppearance["Id"] = data['BloodAppearance_Id'];
    	params.BloodAppearance["BDict"] = {"Id":data['BloodAppearance_BDict_Id']};
    	//血袋完整信息
    	params.BloodIntegrity["Id"] = data['BloodIntegrity_Id'];
    	params.BloodIntegrity["BDict"] = {"Id":data['BloodIntegrity_BDict_Id']};
    	entity["entity"] = params;
        params = JSON.stringify(entity); //序列化参数
		var config = {
				type: "POST",
				url: url,
				data: params
		};
		uxutil.server.ajax(config, function(data) {
		    //隐藏遮罩层
		    layer.closeAll();
		    if (data.success) {
				layer.msg("保存成功！");
				typeof callback === 'function' && callback(data);
			} else {
				layer.msg(data.msg);
			}
		});	
    	
    };
    
    //打开确认对话框
    bagoperation.openConfigDialog = function(callback){
    	var me = this;
    	var msg = '';
		var content = 'editconfigdlg.html?';
		//查询血袋交接数据
		me.getBagOperationById(BagOperationID, function(data){
			if (data.success){
				me.setBagOprationData(data.value);
				if (!me.bagdata["BloodBagHandover_Id"] || me.bagdata["BloodBagHandover_Id"] == ''){
					layer.msg('查询血袋登记信息错误!无法找到该Id记录!' + BagOperationID, {time:5000});
					return;
				};
				var params = [];
				params.push('bagopertime=' + me.bagdata["BloodBagHandover_BagOperTime"]);
				params.push('appearance_bdict_id=' + me.bagdata["BloodAppearance_BDict_Id"]);
				params.push('integrity_bdict_id=' + me.bagdata["BloodIntegrity_BDict_Id"]);
				content = content + params.join('&');
				layer.open({			
					type: 2,
					title: "血袋交接修改",
					area:['350px', '300px'],
					content: content,
					id: "lay-app-form-open-bloodbagconfig",
					btn: ['确定', '取消'],
					btnAlign:"c",
					yes: function (index, layero) {
		                disabledLayerbutton(0, layero); //防止多次点击按钮
		                var data = setConfigData(layero);
		                if(!uxutil.date.isValid(data["BloodBagHandover_BagOperTime"])){
							layer.msg("血袋登记时间无效！", {time:3000});
							return;
						};
		                layer.close(index);
		                if (callback && typeof callback == 'function') callback.call(me, data);
						return false; 
					},
					btn2: function (index, layero) {
		                //取消关闭
		                layer.close(index);
						return false; 
					}
				});				
			} else {
				layer.msg('查询血袋登记信息错误!' + data.msg, {time:5000});;
			};
		});
	};
	
	//
	bagoperation.openConfigDialog(function(data){
		var me = this;
		//设置数据
		me.bagdata["BloodBagHandover_BagOperTime"] = data["BloodBagHandover_BagOperTime"];
		me.bagdata["BloodAppearance_BDict_Id"] = data["BloodAppearance_BDict_Id"];
		me.bagdata["BloodIntegrity_BDict_Id"] = data["BloodIntegrity_BDict_Id"];
		layer.load();
		//提交保存数据
		me.onSave(me.bagdata, function(){
			
		})
	});
});
