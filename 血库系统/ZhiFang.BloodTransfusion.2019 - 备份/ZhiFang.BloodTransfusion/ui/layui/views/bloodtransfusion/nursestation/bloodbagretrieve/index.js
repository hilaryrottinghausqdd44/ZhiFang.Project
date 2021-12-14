layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: 'config/bloodsconfig',
	bagretrievetable: 'views/bloodtransfusion/nursestation/bloodbagretrieve/bagretrievetable',
	bagretrieveform: 'views/bloodtransfusion/nursestation/bloodbagretrieve/bagretrieveform'
}).use(['form', 'layer', 'util', 'table',
    'uxutil', 'dataadapter','cachedata', 'bloodsconfig',
    'bagretrievetable', 'bagretrieveform'], function(){
	"use strict";
	var $ = layui.$;
	var form = layui.form;
	var layer = layui.layer;
	var util = layui.util;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	var bagretrievetable = layui.bagretrievetable;
	var bagretrieveform = layui.bagretrieveform;

	//触发控按钮事件
	var onDocActive = {
		search: function(){
			onSearch();
		},
		save: function(){
			onSave();
		}
	};
	
	//获取默认时间
	function getDefaultDate(days){
		var bdate;
		var edate;
		var where = [];
		edate = new Date().getTime();
		//默认前7天
		days = days || 7;
		bdate = edate - days * 3600 * 24 * 1000;
		bdate = util.toDateString(bdate, 'yyyy-MM-dd');
	    edate = edate + 3600 * 24 * 1000; //加上一天不用使用时分秒
	    edate = util.toDateString(edate, 'yyyy-MM-dd');
		where.push("bloodboutitem.BloodBOutForm.OperTime >= '" + bdate + "'");
		where.push("bloodboutitem.BloodBOutForm.OperTime < '" + edate + "'");
		return where.join(' and ');
	};
	
	//获取查询条件
	function getSearchWhere(){
		var where = [];
		var bdate;
		var edate;
		var rangedate = $('#opertime_range_date').val();
		if (rangedate){
			rangedate = rangedate .split(" - ");
			if (rangedate && rangedate.length == 2) {
				bdate = rangedate[0];
				edate = rangedate[1];
		        edate = new Date(edate).getTime() + 3600 * 24 * 1000; //加上一天不用使用时分秒
		        edate = util.toDateString(edate, 'yyyy-MM-dd');				
			};
		};
		var patno = $('#search_patno').val();
		var name  = $('#search_name').val();
		//根据存在就诊号，按就诊号查询，否则按科室
		if (bagretrievetable.AdmId){
			where.push("bloodboutitem.BloodBOutForm.BloodBReqForm.AdmID ='" + bagretrievetable.AdmId + "'");	
		} else {
			where.push("bloodboutitem.BloodBOutForm.BloodBReqForm.DeptNo =" + bagretrievetable.DeptId);	
		};
		if (bdate) where.push("bloodboutitem.BloodBOutForm.OperTime >= '" + bdate + "'");
		if (edate) where.push("bloodboutitem.BloodBOutForm.OperTime < '" + edate + "'");
		if (patno) where.push("bloodboutitem.BloodBOutForm.BloodBReqForm.PatNo = '" + patno + "'");
		if (name) where.push("bloodboutitem.BloodBOutForm.BloodBReqForm.CName = '" + name + "'");
		return where.join(' and ');
	};
	
	//保存
	function onSave(){
		var checkStatus = table.checkStatus('boutitem_retrieve_table');
        var data = checkStatus.data;
        var carrier_ele = $('#carrier_select');
        bagretrieveform.CarrierID = carrier_ele.val();
        var CarrierName = carrier_ele.find('option:selected').text();
        bagretrieveform.CarrierName = CarrierName;
        CarrierName = CarrierName.split('-');
        if (CarrierName.length > 1){
        	bagretrieveform.CarrierName = CarrierName[0];
        };
        if (!bagretrieveform.CarrierID || bagretrieveform.CarrierID == ""){
        	layer.msg("请选择护工！", {time:3000});
        	return;
        };
        //保存数据
        bagretrieveform.onSave(data, function(result){
        	onSearch();
        });
	};
	
	//检索
	function onSearch(){
		//这里设置config的data为空，因为扫码需要叠加
		var data = [];
		var url = bagretrievetable.config.selUrlByDefault;
		var where = getSearchWhere();
		if (!where) where = getDefaultDate();
		bagretrievetable.setAutoCheckbox(false);
		bagretrievetable.setWhere(where);
		bagretrievetable.loadData(data, url);
	};
	
	//扫码
	function onScanCode(bagcode){
		bagretrievetable.setWhere('');
		bagretrievetable.loadDataBybagCode(bagcode, function(res){
			bagretrievetable.addScanData(res);
		});
	};
	
	//初始表单事件
	function InitDocEvent(){
		//按钮事件
		$('.layui-btn').on('click', function(){
			var type = $(this).data('type');
			onDocActive[type] ? onDocActive[type].call(this) : '';
		});
		//扫码回车事件
		$('#bagcode_text').on('keydown', function(e){
			if (e.keyCode == 13){
				onScanCode(this.value);
			}
		});
	};
	//初始表单
	function Initform(){
		bagretrieveform.InitDate();
		bagretrieveform.initCarrier();
		InitDocEvent();
	};
	
	Initform(); //初始表单
	//查询服务已经默认查询条件为发血明细的回收登记完成度不等于3(全部回收:3)
	bagretrievetable.loadDefaultdata();
});
