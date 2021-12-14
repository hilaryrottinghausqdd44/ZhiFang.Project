layui.extend({
	uxutil:'ux/util',
	uxform:'ux/form',
	dataadapter: 'ux/dataadapter',
	bloodsconfig: 'config/bloodsconfig'	,
	nursesconfig: 'views/bloodtransfusion/nursestation/config/nursesconfig'	
}).define(['form', 'layer','uxutil', 'dataadapter', 'bloodsconfig', 'nursesconfig'], function(exports){
	"use strict";
	var $ = layui.jquery;	
	var form = layui.form;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	var nursesconfig = layui.nursesconfig;
	//当前用户
    var userInfo = bloodsconfig.getCurOper();
    var empID = userInfo.empID;
	var empName = userInfo.empName;	
	//当前操部门
    var deptId = uxutil.cookie.get(uxutil.cookie.map.DEPTID) || "";
    var deptName = uxutil.cookie.get(uxutil.cookie.map.DEPTNAME) || "";
    var selctCarrierElem = null;
    var selInputValue = "";
	//如果有按就诊号就按就诊号 走
	
	//登记表单对象
	var bagoperationfrom = {
		urlConfig: {
        	addbagopUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagOperationAndDtlOfHandover',
        	selEmpUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/RS_UDTO_SearchHREmployeeOFLIMPByHQL',
        	selPuserUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL'
		},
		fdConfig:{
			empFields:['HREmployee_Id','HREmployee_CName', 'HREmployee_Shortcode'],
			userFields:['PUser_Id','PUser_CName', 'PUser_ShortCode']
		},
		
		paramsConfig:{
			empParams:{page: 0, limit: 10000, fields: "", where:"", isPlanish:true}
		}
	};
	
	//护工选择输入按键事件，输入工号按回车直接选择护工功能兼容layui控件的功能
	function oNselectInputEvent(e){
	  var input = e.target;
	  var keyCode = e.keyCode;
	  var value = input.value; //如果是按方向键再按回车键这个值会马上改变，如果不是这个值保持原值
	  //回车键
	  if (keyCode === 13){
	  	if (selctCarrierElem == null || !selctCarrierElem) return;
	  	var dlElem = selctCarrierElem.find('dl');
	  	if (dlElem.length <= 0) return;
	  	var dds = dlElem.find('dd[class=""]'); //查找没有被隐藏的元素
	  	if (dds.length <= 0) return;
	  	//如果两个值相等说明没有按方向键和回车键选择值，触发选择第一个元素
	  	if (selInputValue === value) dds[0].click();
	    //缓存选择后的值，用来判断使用方向按钮回车选择就不执行dds[0].click()；否则执行；  	
	  	selInputValue = input.value; 
	  };
	};
	//初始护工
	bagoperationfrom.initCarrier = function(){
		var me = this;	
		var carrier_elem_Id = "#carrier_select";
		var html = '';
    	var name = '';
    	var Id = '';
    	var ShortCode = '';
    	var idField = nursesconfig.isUseLIIP ? me.fdConfig.empFields[0] : me.fdConfig.userFields[0];
    	var nameField = nursesconfig.isUseLIIP ? me.fdConfig.empFields[1] : me.fdConfig.userFields[1];
    	var ShortField = nursesconfig.isUseLIIP ? me.fdConfig.empFields[2] : me.fdConfig.userFields[2];
    	me.getEmployee(function(res){
    		html = '<option value=""></option>';
	     	for(var i=0; i < res.length; i++){
	    		Id = res[i][idField];
	    		ShortCode = res[i][ShortField];
	    		name = res[i][nameField] + '-' + ShortCode;
	    		html = html + '<option value="' + Id + '">' + name  +'</option>';
	    	};
	    	if (html){
	    		var selectele = $(carrier_elem_Id).empty().append(html);
	    		form.render('select');
	    		//这里注册回车事件,查找替代元素的的兄弟元素标题元素找到input元素
	    		selctCarrierElem = $(carrier_elem_Id).siblings("div.layui-form-select");
	    		var titleEle = selctCarrierElem.find('div.layui-select-title');
	    		var inputEle = titleEle.find('input');
	    		inputEle.on('keydown', oNselectInputEvent);
	    	};   		
    	});
	};
	
	//获取护工
	bagoperationfrom.getEmployee = function(callback){
		var me = this;
		var url = nursesconfig.isUseLIIP ? me.urlConfig.selEmpUrl : me.urlConfig.selPuserUrl;
		var fielsds = nursesconfig.isUseLIIP ? me.fdConfig.empFields.join(',') : me.fdConfig.userFields.join(',');
		var where = ""; //待定
		var data = me.paramsConfig.empParams || {};
		data["fields"] = fielsds;
		data["where"] = where;
		var config = {
			type: 'get',
        	url: url,
        	data: data
		};
		//查询数据
		uxutil.server.ajax(config, function(data) {
			if (data.success){
				callback && typeof callback === 'function' && callback(data.value.list);
			} else{
				layer.msg(data.msg);
			};
		});
	};
	
	//设置提交的交接登记主单
	bagoperationfrom.setOperationEntity = function(data){
		var entity = {};
		var dtStamp = [0,0,0,0,0,0,0,8];
		entity["DeptID"] = deptId; //部门Id
    	entity["DeptCName"] = deptName;	//部门名称
		entity["BagOperTypeID"] = 2; //交接登记
    	entity["BagOperID"] = empID; //操作者ID
    	entity["BagOper"] = empName; //操作人姓名
    	entity["CarrierID"] = data["CarrierID"]; //护工id
    	entity["Carrier"] = data["Carrier"]; //护工姓名
    	entity["BBagCode"] = data["BBagCode"]; //血袋号 
    	entity["BagOperResultID"] = data["BagOperResultID"]; //交接记录结果ID 
    	entity["BloodBReqForm"] = {"Id": data["BReqFormID"], DataTimeStamp:dtStamp}; //申请单号
    	entity["BloodBOutForm"] = {"Id": data["BOutFormID"], DataTimeStamp:dtStamp}; //出库单号    	    
    	entity["BloodBOutItem"] = {"Id": data["BOutItemID"], DataTimeStamp:dtStamp}; //出库明细ID
    	entity["Bloodstyle"] = {"Id": data["BloodNo"], DataTimeStamp:dtStamp}; //血制品编号
    	return entity;
	};
	
	//设置提交的交接登记明细
	bagoperationfrom.setOperationDtlList = function(data){	
		var list = [];
		var bagOpDtl = data["bagOpDtl"] || [];
		for (var i = 0; i < bagOpDtl.length; i++ ){
			var dtl = {};
			layui.each(bagOpDtl[i], function(key, value){
				if (key === "Id"){
					dtl["BDict"] = {"Id": value, DataTimeStamp:[0,0,0,0,0,0,0,8]};
			    }; //BagOperResult这个不是 保存字典的名称
			});	
			list.push(dtl);
		};
		return list;
	};
	
	//保存数据
	bagoperationfrom.onSave = function(data, callback){
    	var me = this,
    	    //提交参数格式
    	    params = {"entity":{}, "bloodBagOperationDtlList":[], "empID": "", "empName":""};
    	    params["empID"] = empID;
    	    params["empName"] = empName;
    	    //交接主单
            params["entity"] = me.setOperationEntity(data);
            //交接明细
    	    params["bloodBagOperationDtlList"] = me.setOperationDtlList(data);
    	    
	    	params = JSON.stringify(params);
			var config = {
				type: "POST",
				url: me.urlConfig.addbagopUrl,
				data: params
			};
			uxutil.server.ajax(config, function(data) {
				if (data.success) {
				    layer.msg("保存成功！");
				    typeof callback === 'function' && callback(data);
				} else {
					layer.msg(data.msg);
				}
			});	 	
	};
	
	exports("bagoperationfrom", bagoperationfrom);
	
});
