layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	nursesconfig: 'views/bloodtransfusion/nursestation/config/nursesconfig',
	bloodsconfig: 'config/bloodsconfig'		
}).define(['table', 'form', 'uxutil', 'dataadapter', 'bloodsconfig', 'nursesconfig'], function(exports){
	"use strict";
	var $ = layui.$;
	var form = layui.form;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	var nursesconfig = layui.nursesconfig;
	//当前部门
    var deptId = uxutil.cookie.get(uxutil.cookie.map.DEPTID) || "";
    var deptName = uxutil.cookie.get(uxutil.cookie.map.DEPTNAME) || "";
    
    var urlParams = uxutil.params.get() || {};
    var admId = urlParams["admId"] ? urlParams["admId"] : ""; //就诊号或者登记号
	//血袋回收代理对象
	var bagretrievetableProxy = {
		DeptId: deptId, //部门ID
		AdmId: admId,   //就诊号、登记号或者患者id
		searchInfo: {
	        isLike: true,
	        fields:[]
	    },
		config:{
        	elem: '#boutitem_retrieve_table',
		    id: 'boutitem_retrieve_table',
            page: false,
			limit: 1000,
			height:'full-300',
			url: '',
			internalWhere: "",
			defaultToolbar: ['filter'],	
			selUrlByDefault: uxutil.path.ROOT+ "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfRecycleByHQL?isPlanish=true",
			selUrlByBagCode: uxutil.path.ROOT+ "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL",
			cols:[
					[{
					    type:'checkbox',
					    LAY_CHECKED: false
					},{
						field: 'BloodBOutItem_Bloodstyle_CName',
		           		sort: true, 
		           		width: 230, 
		           		title: '血制品名称'							
					},{
						field: 'BloodBOutItem_BBagCode',
		           		sort: true, 
		           		width: 150, 
		           		title: '血袋号'							
					},{
						field: 'BloodBOutItem_B3Code',
		           		sort: true, 
		           		width: 150, 
		           		title: '产品号'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_AdmID',
		           		sort: true, 
		           		width: 120, 
		           		title: '登记号'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_PatNo',
		           		sort: true, 
		           		width: 120, 
		           		title: '住院号'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_CName',
		           		sort: true, 
		           		width: 100, 
		           		title: '姓名'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_Sex',
		           		sort: true, 
		           		width: 70, 
		           		title: '性别'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_AgeALL',
		           		sort: true, 
		           		width: 70, 
		           		title: '年龄'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_DeptNo',
		           		sort: true, 
		           		width: 100, 
		           		title: '科室'							
					},{
						field: 'BloodBOutItem_BloodBReqFormVO_Bed',
		           		sort: true, 
		           		width: 100, 
		           		title: '床号'							
					},{
						field: 'BloodBOutItem_Id',
		           		sort: true, 
		           		width: 120, 
		           		title: '发血明细Id',
		           		hide: true
					},{
		           		field:'BloodBOutItem_BloodBReqItem_BReqFormID', 
		           		sort: true, 
		           		width: 120, 
		           		title: '申请单ID',
		           		hide: true
		            },{
		           		field:'BloodBOutItem_BloodBOutForm_Id', 
		           		sort: true, 
		           		width: 120, 
		           		title: '出库单号',
		           		hide: true
		            },{
		           		field:'BloodBOutItem_Bloodstyle_Id', 
		           		sort: true, 
		           		width: 100, 
		           		title: '血制品Id',
		           		hide: true
		            }]
			],
		    response: dataadapter.toResponse(), 
		    parseData: function(res) {
				if (res.success){
					var result = dataadapter.toList(res);
					return result;
				} else{
					layer.msg(res.ErrorInfo);
				}
			},
		    done:function(res, curr, response){

		    }			
		}

	};
	
    //构造器,通过代理对象生成table对象
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, bagretrievetableProxy.config, me.config, options);
		var inst = $.extend(true, {}, bagretrievetableProxy, me); // table,
		inst.config.url = inst.config.selUrlByDefault;
		inst.config.where = inst.getWhere();
		return inst;
	}; 
	
	//获取查询Fields
	bagretrievetableProxy.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for(var i = 0; i < length; i++) {
			if(columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	
	//设置外部传入的查询where
	bagretrievetableProxy.setWhere = function(where) {
		var me = this;
		me.config.internalWhere = where;
		me.config.where = me.getWhere();
	};
	
	//获取查询where
	bagretrievetableProxy.getWhere = function() {
		var me = this,
			arr = [];
		//内部条件
		if(me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.internalWhere);
		}
        var where = "";
		if(arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		where = {
			"where": where,
			'fields': me.getFields(true).join(',')
		};

		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	
	bagretrievetableProxy.render = function(options){
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.tableIns = table.render(me.config);
	};
	
	//装载默认数据
	bagretrievetableProxy.loadDefaultdata = function(){
		var me = this;
		var where = [];
		where.push("bloodboutitem.BloodBOutForm.RecoverCompletion != 3");
		//修改如果存在就诊id也叫登记号、患者id
		if (admId){
			where.push("bloodboutitem.BloodBOutForm.BloodBReqForm.AdmID ='" + admId + "'");	
		} else {
			where.push("bloodboutitem.BloodBOutForm.BloodBReqForm.DeptNo =" + deptId);	
		};
		me.setWhere(where.join(' and '));
		me.loadData();
	};
	
	//获取数据
	bagretrievetableProxy.loadData = function(data, url){
		var  me = this;
		var params = {
			url: url,
			data: data,
			where: me.getWhere()
		};
		me.render(params);
	};
    
    //获取缓冲数据,扫码使用
    bagretrievetableProxy.getBufferData = function(){
    	var me = this;
    	var currTable = me.tableIns ? me.tableIns.config.table : {};
		var data = (currTable && currTable.config.data) ? currTable.config.data : [];	
		return data;
    };
    
    //检查血袋是否已经扫码在缓存数据中,已存在缓存返回true,否则false,扫码使用
    bagretrievetableProxy.checkBagcodeByBuffer = function(bagcode){
    	var me = this;
    	var OldbagCode = '';
		var data = me.getBufferData();
		//检查已扫码的血袋不新增
		for(var i = 0; i < data.length; i++){
			//判断是以什么字段扫码
			if (nursesconfig.isBagCode)
			{
				OldbagCode = data[i]["BloodBOutItem_BBagCode"];
			}else{
				OldbagCode = data[i]["BloodBOutItem_B3Code"];
			};
			if (OldbagCode == bagcode) return true;
		}; 
		return false;
    };
    
    //增加数据到缓存使用，因为条码数据可以重复，添加时只能是使用OutitemId进行判断
    bagretrievetableProxy.checkOutItemIdByBuffer = function(outitemid){
    	var me = this;
    	var OldOutitemId = '';
		var data = me.getBufferData();
		//检查已扫码的血袋不新增
		for(var i = 0; i < data.length; i++){
			OldOutitemId = data[i]["BloodBOutItem_Id"];
			if (OldOutitemId == outitemid) return true;
		}; 
		return false;
    };
	//根据血袋号获取数据，把之前的扫描的数据进行叠加
	bagretrievetableProxy.loadDataBybagCode = function(bagcode, callback){
		var  me = this;
		var  url = me.config.selUrlByBagCode;
		var params = {bagCode: "", page: 0, limit: 1000, fields: "", where: "", isPlanish: true};
		//检查条码是否已经扫码
		if (me.checkBagcodeByBuffer(bagcode)){
		   layer.msg("该条码已经扫过！", {time: 3000});
		   return;
		};
		
		//修改如果存在就诊id也叫登记号、患者id
		if (admId){
		    params["where"] = "bloodboutitem.BloodBOutForm.BloodBReqForm.AdmID = " + admId;
		} else {
			params["where"] = "bloodboutitem.BloodBOutForm.BloodBReqForm.DeptNo = " + deptId;	
		};
		
		params["bagCode"] = bagcode;
		params["fields"] = me.getFields(true).join(',');
		var config = {
			type: 'get',
        	url: url,
        	data: params
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
	
	//设置表格滚到到行
	bagretrievetableProxy.scrollRow = function(row){
	 	var me = this;
    	var currTable = me.tableIns ? me.tableIns.config.table : {}; 
    	if (!currTable) return;
    	var layMain = currTable.layMain;
    	if (!layMain) return;
    	var tbody = layMain.find('tbody');
    	var index = row || 0; //行号	
    	var tr = tbody.find('tr[data-index=' + index + ']');
    	if (!tr) return;
    	var top = tr.offset().top;
    	layMain.scrollTop(top); //滚动到该行
    	currTable.setThisRowChecked(index); //选中到该行
	};
	
	//设置checkbox为自动选择
	bagretrievetableProxy.setAutoCheckbox = function(checked){
		var me = this;
		var data = {};
		var col = me.config.cols ? me.config.cols[0] : [];
		if (!col) return;
		for(var i = 0; i < col.length; i++){
			data= col[i]; 
			if (data["type"] && data["type"] =="checkbox"){
				data["LAY_CHECKED"] = checked;
				break;
			};
		};
	};
	
	
	//设置选中某一行Checkbox为true
	bagretrievetableProxy.setRowCheckbox = function(chkdata, checked){
	 	var me = this;
	 	var data = me.getBufferData(); //获取缓冲数据
	 	chkdata = chkdata || []; //选择数据
    	var currTable = me.tableIns ? me.tableIns.config.table : {}; 
    	if (!currTable) return;
    	var checkName = currTable.config.checkName;
    	var layMain = currTable.layMain;
    	if (!layMain) return;
    	var tbody = layMain.find('tbody');
    	var index = 0; //行号
    	var tr, chkbox;
    	for (var i = 0; i < chkdata.length; i++ )
        {
        	index = chkdata[i]["rowno"]; //行号
	    	tr = tbody.find('tr[data-index=' + index + ']');
	    	if (!tr) continue
	    	chkbox = tr.find('input[type="checkbox"]');
	    	if (!chkbox) continue;
	    	chkbox.prop('checked', true); 
	    	data[index][checkName] = true; //必须设置这个属性，不然table.checkStatus取不到数据 
        }
    	form.render('checkbox');
	};
	
	//增加扫码数据
	bagretrievetableProxy.addScanData = function(res){
		var me = this;
		//获取扫码的数据血袋号
		if (res && res.length <= 0) return; 
		var i = 0;
		//获取已经选择的数据
		var checkStatus = table.checkStatus('boutitem_retrieve_table');
        var chkdata = checkStatus.data || []; //获取已经选择的数据
		var data = me.getBufferData(); //获取缓冲数据
		for(i = 0; i < res.length; i++)
		{  
			//因为前面被设置为checked后，后面重新装载数据会刷掉，所以要记录行号
			res[i]['rowno'] = data.length; 
			data[data.length] = res[i]; //添加数据			
		};
		me.loadData(data, ''); //装载数据
		//判断是否返回唯一数据，是自动选择该条数据
		if (i === 1){
			chkdata[chkdata.length] = data[data.length - 1]; //加上刚扫描的数据
		    me.setRowCheckbox(chkdata, true); //设置行checkbox为true
		};
		me.scrollRow(data.length - 1); //滚动到最后一行
	};
	//暴露接口
	var bagretrievetable = new Class();
	//这里的bagretrievetable跟extend定义必须一致
	exports("bagretrievetable", bagretrievetable);
});