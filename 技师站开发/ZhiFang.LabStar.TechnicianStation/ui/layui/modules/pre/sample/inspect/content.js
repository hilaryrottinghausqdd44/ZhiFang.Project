/**
 * @name：modules/pre/sample/inspect/index 样本单列表
 * @author：liangyl
 * @version 2020-09-23
 */
layui.extend({
	uxutil:'ux/util',
	BarCodeFormList:'modules/pre/sample/inspect/list',	
	MateList:'modules/pre/sample/inspect/matelist'
}).define(['uxutil','form','BarCodeFormList','MateList','PreSampleInspectBasicParams'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		table = layui.table,
		BarCodeFormList = layui.BarCodeFormList,
		MateList = layui.MateList,
		PreSampleInspectBasicParams = layui.PreSampleInspectBasicParams,
		MOD_NAME = 'PreSampleContentIndex';
	//模块DOM
	var MOD_DOM = [
	    '<div class="layui-row style="margin:0px;padding:0px;">',
			'<div class="layui-col-xs8" id="{domId}-col-BarCodeForm">',
                '<div id="{domId}-BarCodeForm"></div>',
			'</div>',
			'<div class="layui-col-xs4" id="{domId}-col-BarCodeForm-Mate">',
                '<div id="{domId}-BarCodeForm-Mate"></div>',
			'</div>',
		'</div>'
	].join('');
	
	//样本单实例
	var BarCodeFormListInstance = null;
	//样本单匹配列表实例
	var MateListInstance = null;
	//功能参数
	var PreSampleInspectBasicParamsInstance = null;
	var PreSampleContentIndex = {
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:null,
			cols:[],
			MODELTYPE:null,
			PrinterName:null,
			IsOrderListPrinter:null,//打印附加清单
			//刷新
			refresh:function(){}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleContentIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		//样本信息列表实例 
		BarCodeFormListInstance = BarCodeFormList.render({
			domId: me.config.domId+'-BarCodeForm',
			height:me.config.height+'px',
			cols:[me.getCols()],
			PrinterName:me.config.PrinterName,
			nodetype:me.config.nodetype //站点类型
		});
		//模式4 -显示匹配列表
		if(me.config.MODELTYPE !='3'){
			//隐藏右列表
			$('#' + me.config.domId+'-col-BarCodeForm-Mate').removeClass('layui-col-xs4');
			$('#' + me.config.domId+'-col-BarCodeForm-Mate').addClass('layui-col-xs12');
			$('#' + me.config.domId+'-col-BarCodeForm-Mate').addClass('layui-hide');
		    //左列表宽度修改
		    $('#' + me.config.domId+'-col-BarCodeForm').removeClass('layui-col-xs8');
			$('#' + me.config.domId+'-col-BarCodeForm').addClass('layui-col-xs12');
		}else{
			var height = me.config.height-25;
			MateListInstance = MateList.render({
				domId: me.config.domId+'-BarCodeForm-Mate',
				height:height+'px',
				nodetype:me.config.nodetype, //站点类型
				//刷新
				refresh:function(){
					me.config.refresh && me.config.refresh();
				}
			});
		}
		
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		
	};
	//列表列
	Class.prototype.getCols = function(){
		var me = this;
		var cols = [
			{type: 'checkbox', fixed: 'left'},
			{field:'LisBarCodeFormVo_IsConfirm', width:80, title: '是否已确认', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID', width:80, title: '状态', hide: true},
			{field:'LisBarCodeFormVo_failureInfo', width:80, title: 'LisBarCodeFormVo_failureInfo', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_ColorValue', width:80, title: 'LisBarCodeFormVo_LisBarCodeForm_ColorValue', hide: true}];
		for(var i in me.config.cols){
			//BarCode&条码号&100&show
			var arr = me.config.cols[i].split('&');
			cols.push({
				field:arr[0],title:arr[1],width:arr[2],
				hide:(arr[3] =='show' ? false : true)
			});
		}
		return cols;
	};
	//查询样本单列表字段
	Class.prototype.getFields = function(){
		return BarCodeFormListInstance.getFields();
	};
    Class.prototype.changeSize= function(height,bo){
    	var me = this;
    	//模式4 -需改变两个列表高度（匹配列表高度也需调整）
		if(me.config.MODELTYPE =='3'){
			$('#'+me.config.domId+'-BarCodeForm-Mate').css('height',height+'px');
			MateListInstance.changeSize(height,bo);
		}
		$('#'+me.config.domId+'-BarCodeForm').css('height',height+'px');
    	BarCodeFormListInstance.changeSize(height,bo);
    };
    //列表数据清空
    Class.prototype.clearData= function(){
    	var me = this;
    	if(me.config.MODELTYPE =='3'){
			MateListInstance.clearData();
		}
    	BarCodeFormListInstance.clearData();
    };
    //扫码判断条码号是否已存在
    Class.prototype.isExistBarcode = function(value){
    	var me = this,
			tableCache = BarCodeFormListInstance.getListData(),
			isExist = false,isExec=false;

		$.each(tableCache, function (i, item) {
			if (value == item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
				isExist = true;
				return false;
			}
		});
		if(isExist){
			BarCodeFormListInstance.checkRow(value);
//			layer.msg("该条码已存在!", { icon: 5});
			isExec = true;
		}
		return isExec;
    };
    //样本单数据重载
    Class.prototype.loadData = function(data){
    	var me = this;
    	BarCodeFormListInstance.loadData(data);
    };
     //数量计算
    Class.prototype.getNum = function(){
    	var me = this;
    	return BarCodeFormListInstance.getNum();
    };
    //查询按钮查询
    Class.prototype.onSearch = function(where,relationForm,callback){
    	var me = this;
    	if(me.config.MODELTYPE == '3'){ //匹配查询列表查询
    		MateListInstance.onSearch(where,function(list){
    			MateListInstance.loadData(list);
    		});
    	}else{ //样本单列表查询
    		BarCodeFormListInstance.onSearch(where,relationForm,function(list){
    			BarCodeFormListInstance.loadData(list);
    			callback();
    		});
    	}
    };
    //打印清单
    Class.prototype.onListPrint = function(){
    	var me = this;
    	BarCodeFormListInstance.onListPrint(me.config.IsOrderListPrinter);
    };

    //获取匹配列表数据
    Class.prototype.getMateList = function(){
    	var me = this;
    	return MateListInstance.getListData();
    };
    //匹配列表更新匹配上的标记 改变颜色-
    Class.prototype.findBarCode = function(barcode){
    	var me = this;
    	 MateListInstance.findBarCode(barcode);
    };
    //获取样本单列表数据
    Class.prototype.getList = function(){
    	var me = this;
    	return BarCodeFormListInstance.getListData();
    };
    //获取样本单勾选的数组
    Class.prototype.getCheckedList = function(){
    	var me = this;
    	return BarCodeFormListInstance.getCheckedList();
    };
    //获取样本单勾选的条码号
    Class.prototype.getCheckedBarcodes = function(){
    	var me = this;
    	return BarCodeFormListInstance.getCheckedBarcodes();
    };
    //根据送检信息特定字段匹配数据是否加入列表(参数)，Field --匹配字段，FieldValue -匹配值    ，true - 能加入
	Class.prototype.isSpecificField = function(Field,FieldValue,list){
    	var me = this;
    	var isExec = false,value = "";
    	for(var i=0;i<Field.length;i++){
    		value += list[0][Field[i]];//如果存在多个特定字段匹配则是多字字段拼接值
    	}
		 //样本送检-匹配字段，相同内容才可以刷入条码，如ZDY1
		if(value == FieldValue)isExec = true;
        return isExec;
	};
	//撤销采集,取消采集的数据应清掉
	Class.prototype.onRevocation = function(barcode){
		var me = this,
			arr=[];
		var list = me.getList();
		for(var i in list){
			var IsExec = true;
		 	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode ==barcode ){
                IsExec = false;
		 	}
		 	if(IsExec)arr.push(list[i]);
		}
		BarCodeFormListInstance.clearData();
		BarCodeFormListInstance.loadData(arr);
    	if(me.config.MODELTYPE =='3'){ //匹配查询列表查询
	    	//数据匹配到病人信息列表（刷新列表）
			MateListInstance.findBarCodeRevocation(barcode);
		}
	};
	//核心入口
	PreSampleContentIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		me.initHtml();
		return me;
	};
		//暴露接口
	exports(MOD_NAME,PreSampleContentIndex);
});