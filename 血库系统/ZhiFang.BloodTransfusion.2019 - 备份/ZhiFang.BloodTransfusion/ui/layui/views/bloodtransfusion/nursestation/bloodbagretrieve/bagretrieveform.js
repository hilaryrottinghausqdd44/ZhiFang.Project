layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	bloodsconfig: 'config/bloodsconfig',
	nursesconfig: 'views/bloodtransfusion/nursestation/config/nursesconfig'
}).define(['table', 'form', 'layer', 'util', 'laydate',
   'uxutil', 'dataadapter', 'bloodsconfig', 'nursesconfig'], function(exports){
	"use strict";
	var $ = layui.$;
	var table = layui.table;
	var form = layui.form;
	var layer = layui.layer;
	var util = layui.util;
	var laydate = layui.laydate;
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
	var urlParams = uxutil.params.get() || {};
    var admId = urlParams["admId"] ? urlParams["admId"] : ""; //就诊号或者登记号
    var selctCarrierElem = null;
    var selInputValue = "";
    
	var bagretrieveform = {
		CarrierID: "",   //护工ID
		CarrierName: "",  //护工姓名
		config:{
			addUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagOperationListOfRecycle',
			selEmpUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/RS_UDTO_SearchHREmployeeOFLIMPByHQL',
			selPuserUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL',
			empFields:['HREmployee_Id', 'HREmployee_CName', 'HREmployee_Shortcode'],
			userFields:['PUser_Id', 'PUser_CName', 'PUser_ShortCode']
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
	bagretrieveform.initCarrier = function(){
		var me = this;	
		var carrier_elem_Id = "#carrier_select";
		var html = '';
    	var name = '';
    	var Id = '';
    	var ShortCode = '';
    	var idField = nursesconfig.isUseLIIP ? me.config.empFields[0] : me.config.userFields[0];
    	var nameField = nursesconfig.isUseLIIP ? me.config.empFields[1] : me.config.userFields[1]; 
    	var ShortField = nursesconfig.isUseLIIP ? me.config.empFields[2] : me.config.userFields[2];
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
	
	//初始日期
	bagretrieveform.InitDate = function(){
		var me = this;
		var dateId = "#opertime_range_date";
		//当前日期
		var currtime = new Date().getTime();
		//默认前7天
		var begindate = currtime - 7 * 3600 * 24 * 1000;
		var begindate =  util.toDateString(begindate, 'yyyy-MM-dd');
		var enddate = util.toDateString(currtime, 'yyyy-MM-dd');
		var initdate = begindate + ' - ' + enddate;
		laydate.render({
		 	elem: dateId,
		    range:true,
		    value: initdate
		});
	};
	
	//获取护工
	bagretrieveform.getEmployee = function(callback){
		var me = this;
		var url = nursesconfig.isUseLIIP ? me.config.selEmpUrl : me.config.selPuserUrl;
		var fields = nursesconfig.isUseLIIP ? me.config.empFields.join(',') : me.config.userFields.join(',');
		var where = ""; //待定
		var data = {page: 0, limit: 1000, isPlanish:true}; 
		data["fields"] = fields;
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
	
	//设置提交的回收交接登记
	bagretrieveform.setOperationListEntity = function(data){
		var me = this;
		var List = [];
		var ReqFormID, OutFormID, OutItemID, styleID, bagCode; 
		var dtStamp = [0,0,0,0,0,0,0,8];
		data = data || [];
		for(var i = 0; i < data.length; i++)
		{
			OutItemID = data[i]["BloodBOutItem_Id"];
			styleID = data[i]["BloodBOutItem_Bloodstyle_Id"];
			bagCode = data[i]["BloodBOutItem_BBagCode"];
			ReqFormID = data[i]["BloodBOutItem_BloodBReqItem_BReqFormID"];
			OutFormID = data[i]["BloodBOutItem_BloodBOutForm_Id"];			
		    List.push({
		    	DeptID: deptId,
		    	DeptCName: deptName,
		    	BagOperTypeID: 3,
		    	BagOperID: empID,
		    	BagOper: empName,
		    	CarrierID: me.CarrierID,
		    	Carrier: me.CarrierName,
		    	BagOperResultID: 1,
		    	BBagCode: bagCode,
		    	BloodBReqForm: {"Id": ReqFormID, DataTimeStamp: dtStamp}, //申请单号
		    	BloodBOutForm: {"Id": OutFormID, DataTimeStamp: dtStamp}, //出库单号 
		    	BloodBOutItem: {"Id": OutItemID, DataTimeStamp: dtStamp}, //出库明细ID
		    	Bloodstyle:{"Id": styleID, DataTimeStamp: dtStamp} //血制品编号
		    });
		}
    	return List;
	};
	
	bagretrieveform.onSave = function(data, callback){
		var me = this;
		var url = me.config.addUrl; //提交URL
		//新增参数格式
		var AddParams = {
			bloodBagOperationList:[],
			empID: empID,
			empName: empName
		};
		data = data || [];
		AddParams.bloodBagOperationList = me.setOperationListEntity(data);
		var params = JSON.stringify(AddParams);
		//提交配置
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
			    layer.msg("保存成功！");
			} else {
				layer.msg(data.msg);
			};
		    typeof callback === 'function' && callback(data);
		});			
	};
	
	exports("bagretrieveform", bagretrieveform)
   
});
	