/**
 * @name：modules/pre/sample/tranrule/list 样本单列表
 * @author：liangyl
 * @version 2021-09-07
 */
layui.extend({
}).define(['uxutil','uxtable','laydate','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		laydate = layui.laydate,
		form = layui.form,
		MOD_NAME = 'BarCodeFormList';
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
		  '<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		  '<div class="layui-form tranrule-form" id="{tableId}" lay-filter="{tableId}">',
		   '<div class="layui-form-item">', 
	        '<div class="layui-inline layui-hide" id="{tableId}-isSpecifyDate-row">', 
	         '<input type="checkbox" name="{tableId}-isSpecifyDate" title="指定分发日期"  lay-skin="primary">',
	        '</div>', 
	         '<div class="layui-inline">', 
	          '<input type="text" class="layui-input layui-hide" id="{tableId}-SpecifyDate" placeholder="yyyy-MM-dd" style="width:120px">',
	        '</div>', 
	        '<div class="layui-inline">', 
	         '<label class="layui-form-label">分发类型</label>', 
	          '<div class="layui-input-inline" >', 
	          	'<select name="{tableId}-TranRuleTypeID" id="{tableId}-TranRuleTypeID" lay-filter="{tableId}-TranRuleTypeID"> <option value="">请选择</option></select>',
	          '</div>', 
	        '</div>', 
	        '<div class="layui-inline">',
	          '<button type="button" id="{tableId}-print" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-print"></i>补打条码</button>', 
	        '</div>',
	       '</div>', 
		  '</div>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
			'.tranrule-form{padding:2px;margin-bottom:0px;border:1px solid #e6e6e6;}',
//			'.{tableId}-table .layui-table-body .layui-table-cell{height:80px !important;}',
//			'.{tableId}-table .layui-table-body .layui-table-cell .layui-form-checkbox{margin-top:30px !important;}',
		'</style>'
	];
	//查询
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleDispenseGetBarCodeFromListByWhere";
	//获取分发类型
	var GET_RULE_TYPE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeByHQL?isPlanish=true";

	//排序
	var SortFields = [];
	//样本单列表
	var BarCodeFormList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			cols:[],
			nodetype:null,//站点类型
			
			//是否显示分发日期下拉框
			IsShowDateCom:null,
			//界面排序字段
			sortFields:null,
			//行选择触发事件
			rowClickFun:function(){},
			//补打条码按钮事件
			printClickFun:function(){}
		},
		//内部列表参数
		tableConfig:{
			elem:null,
//			toolbar:null,
//			skin:'line',//行边框风格
			size:'sm',//小尺寸的表格
			height:'full-110',
			where:{},
			page: false,
			limit: 1000,
			limits: [100, 200, 500, 1000, 1500],
			cols:[[
				{type:'checkbox'}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,BarCodeFormList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,BarCodeFormList.tableConfig);
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
//		me.tableConfig.cols = me.config.cols;
		me.tableConfig.cols = [me.getCols()];
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			res.data.forEach(function (item, index) {
				var ColorValue = item.LisBarCodeFormVo_LisBarCodeForm_ColorValue;
				if (ColorValue){//采样管颜色
					//背景色-红色
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_IsUrgent"]').
					css('background-color', ColorValue);
				}
			});
		    if(count>0)me.onClickFirstRow();
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
		//指定分发日期是否显示
		if(me.config.IsShowDateCom=='1'){
			$('#' + me.config.domId+'-table-SpecifyDate').removeClass('layui-hide');
			$('#' + me.config.domId+'-table-isSpecifyDate-row').removeClass('layui-hide');
			var today = new Date();var today = new Date();
		    var defaultvalue = uxutil.date.toString(today, true);
			$('#'+me.config.domId+"-table-SpecifyDate").val(defaultvalue);
			laydate.render({
			    elem:'#' + me.config.domId+"-table-SpecifyDate"
		    });
		}
		me.getRuleType(function(list){
			var htmls = ['<option value="">请选择分发规则类型</option>'];
			for(var i=0;i<list.length;i++){
				var selectHtml ='';
				//默认规则类型
				if(list[i].LBTranRuleType_CName == '默认规则'){
				     selectHtml =" selected";
				}
				htmls.push("<option value='" + list[i].LBTranRuleType_Id + "'"+selectHtml+">" + list[i].LBTranRuleType_CName + "</option>");
			}
			$('#' + me.config.domId +'-table-TranRuleTypeID').html(htmls.join(''));
			
			form.render('select');
		});
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		//触发行单击事件
		me.uxtable.table.on('row(' + me.tableId + ')', function(obj){
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        	me.config.rowClickFun && me.config.rowClickFun(obj);
		});
		//监听样本单列表排序
        me.uxtable.table.on('sort(' + me.tableId + ')', function (obj) {
            var field = obj.field, //当前排序的字段名
                type = obj.type, //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
                sort = [];
            field = field.replace(RegExp("LisBarCodeFormVo_", "g"), "");
//	        SortFields='[{ property: '+field+', direction: '+type+'}]';
	        SortFields=[{"property":field,"direction": type}];
           
        });
		//补打条码
		$('#'+me.tableId+'-print').on('click',function(){
			me.config.printClickFun && me.config.printClickFun();
		});
	};
		//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		setTimeout(function () {
			$("#" + me.tableId + "+div").find('.layui-table-main tr[data-index="0"]').click();
		}, 0);
	};
	//获取指定分发日期和分发规则选择值
	Class.prototype.getRule = function(){
		var me = this;
		var obj ={};
		var TestDate = null,TranRuleTypeID=null;
        //指定分发日期是否显示
		if(me.config.IsShowDateCom=='1')TestDate = $('#'+me.tableId+'-SpecifyDate').val();
		TranRuleTypeID = $('#' + me.tableId +'-TranRuleTypeID').val();
        return {
        	TestDate:TestDate,
        	ruleType:TranRuleTypeID
        }
	};
	//获取可排序字段  LisBarCodeFormVo_LisBarCodeForm_InceptTime-签收时间,LisBarCodeFormVo_LisBarCodeForm_CollectTime-采样时间
	Class.prototype.getSortFields = function(){
		var me = this;
		var sortFields = [];
		if(me.config.sortFields){
			var arr = me.config.sortFields.split(',');
			for(var i =0;i<arr.length;i++){
				var fields_arr = arr[i].split('-');
				sortFields.push(fields_arr[0]);
			}
		}
		return sortFields;
	};
	//列表列
	Class.prototype.getCols = function(){
		var me = this;
		var cols = [
			{type: 'checkbox', fixed: 'left'},
			{field:'LisBarCodeFormVo_IsConfirm', width:80, title: '是否已分发', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_Id', width:80, title: 'LisBarCodeForm_Id', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID', width:80, title: '状态', hide: true},
			{field:'LisBarCodeFormVo_failureInfo', width:80, title: 'LisBarCodeFormVo_failureInfo', hide: true},
			{field:'LisBarCodeFormVo_PrintDispenseTagAndFlowSheetInfo', width:80, title: '自动打印分发标签内容', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_IsUrgent', width:80, title: '急查', hide: true},
		{field:'LisBarCodeFormVo_SignForMan', width:80, title: '签收人'}];
		var cols2 = JSON.stringify(me.config.cols);
		//判断病人姓名列是否存在   --条码打印需要用
		if(cols2.indexOf('LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName') == -1){  //如果不存在，则加入
		    cols.push({field:'LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName', width:80, title: '姓名', hide: true});
		}
       //判断条码号列是否存在--条码打印需要用
		if(cols2.indexOf('LisBarCodeFormVo_LisBarCodeForm_BarCode') == -1){  //如果不存在，则加入
		    cols.push({field:'LisBarCodeFormVo_LisBarCodeForm_BarCode', width:80, title: '条码号', hide: true});
		}
		//判断病历号列是否存在--条码打印需要用
		if(cols2.indexOf('LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo') == -1){  //如果不存在，则加入
		    cols.push({field:'LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo', width:80, title: '病历号', hide: true});
		}
		//判断年龄列是否存在--条码打印需要用
		if(cols2.indexOf('LisBarCodeFormVo_LisBarCodeForm_LisPatient_Age') == -1){  //如果不存在，则加入
		    cols.push({field:'LisBarCodeFormVo_LisBarCodeForm_LisPatient_Age', width:80, title: '病历号', hide: true});
		}
		for(var i in me.config.cols){
			var sort = false;
			var sortFields = me.getSortFields();
			//BarCode&条码号&100&show
			var arr = me.config.cols[i].split('&');
			for(var j in sortFields){
				if(sortFields[j] == arr[0]){
					sort = true;
					continue;
				}
			}
			cols.push({
				field:arr[0],title:arr[1],width:arr[2],
				hide:(arr[3] =='show' ? false : true),
				sort:sort
			});
		}
		return cols;
	};
	//从服务器获取拒收短语信息列表
	Class.prototype.getRuleType = function(callback){
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_RULE_TYPE_LIST_URL,
			type:'get',
			data:{
				page:1,
				limit:1000,
				fields:'LBTranRuleType_Id,LBTranRuleType_CName'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value ||{}).list || []);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
	//获取勾选的条码数组
	Class.prototype.getCheckedBarcodes = function(){
		var me = this,
			barcodes = [];
		
		var checkedList = me.uxtable.table.checkStatus(me.config.domId + '-table').data;
		for(var i in checkedList){
			barcodes.push(checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
		}
		return barcodes;
	};
	 //获取勾选的数组
	Class.prototype.getCheckedList = function(){
		var me = this,
			barcodes = [];
		var checkedList = me.uxtable.table.checkStatus(me.config.domId + '-table').data;
		return checkedList;
	};
    //获取查询字段
	Class.prototype.getFields = function(){
		var me = this,
		    cols = me.tableConfig.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
	    return fields.join(',');
	};

	//新增行  
	Class.prototype.addRow = function(data){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
		list.push.apply(list, data);
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	 //数据加载(新增数据)
    Class.prototype.loadData = function(data){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    for(var i=0;i<data.length;i++){  //新增数据
	    	var isExec=true;
	    	for(var j=0;j<list.length;j++){
	    		if(data[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == list[j].LisBarCodeFormVo_LisBarCodeForm_BarCode){
	    			isExec = false;	
	    			data[i].LisBarCodeFormVo_IsConfirm  =  data[i].LisBarCodeFormVo_IsConfirm;
	    			list[j] = data[i];
	    		}
	    	}
	    	if(isExec)list.push(data[i]);
	    }
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	 //分发标记添加与删除，tag==1，增加 --没有值 删除分发标记
    Class.prototype.loadByBarcode= function(barcode,tag){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	  
    	for(var i=0;i<list.length;i++){
			if(barcode== list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode){
				list[i].LisBarCodeFormVo_IsConfirm = tag;
				break;
			}   				
    	}
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	//列表总数
	Class.prototype.getNum = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list.length;
	};
	//获取列表数据
	Class.prototype.getListData = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list;
	};
	
	 //改变高度
	Class.prototype.changeSize = function(height){
		var me = this;
		height = height-60; 
        $('#'+me.config.domId).find('div.layui-table-body.layui-table-main').css('height',height+'px');
	};
	 //查询数据
	Class.prototype.onSearch = function(obj,callback){
		var me = this;
	    var params={
	    	nodetype:me.config.nodetype,//站点类型
			where:obj.where,
			relationForm:obj.relationForm,
			fields:me.getFields(),
		    isPlanish:true
	    };
	    if(SortFields.length ==0)params.sortFields = null;
	    else
	       params.sortFields = JSON.stringify(SortFields);
		var config = {
			type:'post',
			url:GET_LIST_URL,
			data:JSON.stringify(params)
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				for(var i=0;i<list.length;i++){
					var BarCodeStatusID = list[i].LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID;
		    		if(Number(BarCodeStatusID) >11){ //已分发的 打上已分发标记
		    			list[i].LisBarCodeFormVo_IsConfirm = '1';
		    		}
		    	}
				me.loadData(list)
				callback(list);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	//核心入口
	BarCodeFormList.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	//暴露接口
	exports(MOD_NAME,BarCodeFormList);
});