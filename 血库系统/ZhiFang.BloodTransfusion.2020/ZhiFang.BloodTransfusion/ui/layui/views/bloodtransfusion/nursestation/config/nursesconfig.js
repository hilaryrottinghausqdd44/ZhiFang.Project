layui.extend({
	uxutil: 'ux/util',
	cachedata: '/views/modules/bloodtransfusion/cachedata'
}).define(['jquery', 'uxutil', 'cachedata'], function(exports) {
	"use strict";
	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var cachedata = layui.cachedata;
	var getScanCodeField = function(callback){
		var url = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?'; 
		var params =[];
		params.push("paraNo=BL-BBSC-IDFT-0022");
		params.push("t=" + (new Date().getTime()));
	    var data = params.join('&');
		url = url + data;
		var config={
    		type: 'get',
        	url: url
		};
    	//查询数据
		uxutil.server.ajax(config, function(data) {
			if (data.success){
				if (callback) callback(data.value.ParaValue);
			}else{
			    layer.msg("查询血袋扫码识别字段数据错误！" + data.msg, {time: 3000});
			};
		});  
	};
	//护士站配置
	var nursesconfig = {
		isUseLIIP: false,   //默认不使用集成平台
		isBagCode: false,   //是否扫码血袋码
		isB3Code: false,    //是否扫码产品码
		//获取血袋扫码识别字段
		getScanCodeIDField: function(){
			var scanCodeIDField = "";
			if (JcallShell && JcallShell.BLTF) {
				scanCodeIDField = "" + JcallShell.BLTF.RunParams.Lists.BloodBagScanCodeIDField.Value;
				if (!scanCodeIDField) scanCodeIDField = JShell.BLTF.cachedata.getCache("BloodBagScanCodeIDField");
			} else {
				scanCodeIDField = cachedata.getCache("BloodBagScanCodeIDField");
			};
            return scanCodeIDField;
	    }
	};
	
	//获取扫码字段参数
	getScanCodeField(function(fieldname){
		nursesconfig.isB3Code = fieldname =="B3Code"? true:false; 
		nursesconfig.isBagCode = !nursesconfig.isB3Code;
	});
	exports('nursesconfig', nursesconfig);
})