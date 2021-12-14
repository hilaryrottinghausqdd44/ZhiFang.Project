/**
 * @name：modules/pre/sample/tranrule/index 样本单列表与项目分发信息
 * @author：liangyl
 * @version 2020-10-13
 */
layui.extend({
	BarCodeFormList:'modules/pre/sample/tranrule/list',	 //样本单列表（扫码对应的列表）
	ItemList:'modules/pre/sample/tranrule/item', //项目分发信息列表
	PreSampleContentTabIndex:'modules/pre/sample/tranrule/tab', //页签
	//Model:'views/system/comm/BarCodePrint/print',//'modules/common/print',
	CommonPrint:'modules/common/print',
	print:'ux/print'
}).define(['uxutil','form','BarCodeFormList','ItemList','PreSampleContentTabIndex','CommonPrint','print'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		table = layui.table,
		BarCodeFormList = layui.BarCodeFormList,
		ItemList = layui.ItemList,
		CommonPrint = layui.CommonPrint,
		PreSampleContentTabIndex = layui.PreSampleContentTabIndex,
		print = layui.print,
		MOD_NAME = 'PreSampleContentIndex';
	//模块DOM
	var MOD_DOM = [
	    '<div class="layui-row" style="margin:0px;padding:0px;">',
			'<div class="layui-col-xs6">',
                '<div id="{domId}-BarCodeForm"></div>',
			'</div>',
			'<div class="layui-col-xs3">',
                '<div id="{domId}-BarCodeForm-ItemList"></div>',
			'</div>',
			'<div class="layui-col-xs3">',
                '<div id="{domId}-BarCodeForm-Tab"></div>',
			'</div>',
		'</div>'
	].join('');
	
	 //条码打印次数更新(long nodetype, string barcodes)
	var GET_PRINT_BARCODE_COUNT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetPrintBarCodeCount";
   //条码打印次数更新(long nodetype, string barcodes)
	var PRINT_BARCODE_NUM_UPDATE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateBarCodeFromPrintStatus";
 
    //查询打印模板
    var SELECT_PRINT_MODEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelAndAutoUploadModel?isPlanish=true";
	//生成pdf文件
	var CREATE_PDF_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_PrintDataByPrintModel";

	//样本单实例
	var BarCodeFormListInstance = null;
	//样本单项目列表实例
	var ItemListInstance = null;
	//tab实例
	var PreSampleContentTabIndexInstance = null;
	//打印实例
	var ModelInstance = CommonPrint.render({
		modelUrl:uxutil.path.LAYUI + '/model/tranrule/model.txt'
	});
	var PreSampleContentIndex = {
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:null,
			cols:[],
			ordercols:[],//医嘱信息配置字段
			//标签打印机名  ---补打条码的打印机名
			TagPrintName:null,
			//样本清单打印机名
			PrintName:null,
			//是否显示分发日期下拉框
			IsShowDateCom:null,
			//界面排序
			sortFields:null
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
		var b_height = me.config.height -32;
		//样本信息列表实例 
		BarCodeFormListInstance = BarCodeFormList.render({
			domId: me.config.domId+'-BarCodeForm',
			height:b_height+'px',
			cols:me.config.cols,
			//界面排序
			sortFields:me.config.sortFields,
			//标签打印机名  ---补打条码的打印机名
			TagPrintName:me.config.TagPrintName,
			//样本清单打印机名
			PrintName:me.config.PrintName,
			//是否显示分发日期下拉框
			IsShowDateCom:me.config.IsShowDateCom,
			nodetype:me.config.nodetype, //站点类型
			rowClickFun:function(obj){//行选择
				//高度调整
				var height_item = $('#'+me.config.domId+'-BarCodeForm').height();
				//查样本单项目信息
				ItemListInstance.loadData(obj.data.LisBarCodeFormVo_LisBarCodeForm_Id,height_item);
				//查医嘱信息信息
				PreSampleContentTabIndexInstance.loadData(obj.data,height_item);
    	    },
			printClickFun:function(){ //补打条码
				me.printBarcode();
			}
		});
		//样本单项目信息
		ItemListInstance = ItemList.render({
			domId: me.config.domId+'-BarCodeForm-ItemList',
			height:me.config.height+'px',
			nodetype:me.config.nodetype
		});
		//页签
		PreSampleContentTabIndexInstance =  PreSampleContentTabIndex.render({
			domId: me.config.domId+'-BarCodeForm-Tab',
			height:me.config.height,
			ordercols:me.config.ordercols,
			nodetype:me.config.nodetype
		});
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		
	};
	//查询样本单列表字段
	Class.prototype.getFields = function(){
		return BarCodeFormListInstance.getFields();
	};
	//获取分发规则类型和指定分发日期
	Class.prototype.getRule = function(){
		return BarCodeFormListInstance.getRule();
	};
    Class.prototype.changeSize= function(height){
    	var me = this;
    	height = height;
    	$('#'+me.config.domId+'-BarCodeForm').css('height',height+'px');
		BarCodeFormListInstance.changeSize(height);
		
		$('#'+me.config.domId+'-BarCodeForm-ItemList').css('height',height+'px');
    	ItemListInstance.changeSize(height);
  	
    	$('#'+me.config.domId+'-BarCodeForm-Tab').css('height',height+'px');
    	PreSampleContentTabIndexInstance.changeSize(height);
    };
    //列表数据清空
    Class.prototype.clearData= function(){
    	var me = this;
    	BarCodeFormListInstance.clearData();
    	ItemListInstance.clearData();
    	PreSampleContentTabIndexInstance.clearData();
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
			layer.msg("该条码已存在!", { icon: 5});
			isExec = true;
		}
		return isExec;
    };
    //条码打印(执行 )   isAutoPrint  是否自动打印 autoPrint=true  自动
	Class.prototype.onBarcodePrint = function(list,autoPrint){
		var me = this;
		for(var i in list){
			var CName = list[i].LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName;
			//条码
			var BarCode = list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode;
			//年龄
			var Age = list[i].LisBarCodeFormVo_LisBarCodeForm_LisPatient_Age;
			//单位
			var AgeUnitName = list[i].LisBarCodeFormVo_LisBarCodeForm_LisPatient_AgeUnitName;
	        //病历号
	        var PatNo = list[i].LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo;
		    //PrintDispenseTagAndFlowSheetInfo
		    var TagAndFlowSheetInfo = list[i].LisBarCodeFormVo_PrintDispenseTagAndFlowSheetInfo;
    	    if(TagAndFlowSheetInfo)TagAndFlowSheetInfo = JSON.parse(TagAndFlowSheetInfo);
    	    TagAndFlowSheetInfo  = TagAndFlowSheetInfo || [];
    	    //指定打印机
		    var printer = me.config.TagPrintName;
    	    //一条数据对应一个规则，一条规则会对应一个检验单,一个样本单可能分发成多个检验单
			for(var j in TagAndFlowSheetInfo){
				//打印份数 （规则设置的打印份数)
				 var printcount = 1;
				if(TagAndFlowSheetInfo[j].LBTranRule && TagAndFlowSheetInfo[j].LBTranRule.PrintCount)printcount =  TagAndFlowSheetInfo[j].LBTranRule.PrintCount;
				TagAndFlowSheetInfo[j].CName = CName;
				TagAndFlowSheetInfo[j].BarCode = BarCode;
                TagAndFlowSheetInfo[j].Age = Age;
                TagAndFlowSheetInfo[j].PatNo = PatNo;
                TagAndFlowSheetInfo[j].AgeUnitName = AgeUnitName;
				if(autoPrint){  //自动打印  （需要跟规则的自动打印标签一起判断）
					//规则设置是否自动打印  IsAutoPrint
					var IsAutoPrint = TagAndFlowSheetInfo[j].LBTranRule.IsAutoPrint;
					if(IsAutoPrint){
					    for(var a = 0; a < printcount; a++) {
							ModelInstance.print([TagAndFlowSheetInfo[j]], printer, function (item, isLastOne) {
								if (isLastOne) {
								}
							});
						}
					}
				}else{
					for(var a = 0; a < printcount; a++) {
						ModelInstance.print([TagAndFlowSheetInfo[j]], printer, function (item, isLastOne) {
							if (isLastOne) {
							}
						});
					}
				}
			}
		}
	};
	//条码打印
	Class.prototype.printBarcode = function(){
		var me = this;
		var checklist  = me.getCheckedList();
		if(checklist.length==0){
			layer.msg("请先选择行数据!", { icon: 0, anim: 0 });
			return false;
		}
		var printData =[];
		for(var i in checklist){
			if(checklist[i].LisBarCodeFormVo_PrintDispenseTagAndFlowSheetInfo){
				printData.push(checklist[i]);
			}
		}
		if(printData.length==0)layer.msg("没有分发标签可以打印", { icon: 5, anim: 0 });
		me.onBarcodePrint(printData,false);
	};
    //样本单数据重载
    Class.prototype.loadData = function(data){
    	var me = this;
    	BarCodeFormListInstance.loadData(data);
    };
    //样本分发标记记录
    Class.prototype.loadByBarcode = function(barcode,tag){
    	var me = this;
    	BarCodeFormListInstance.loadByBarcode(barcode,tag);
    };
     //数量计算
    Class.prototype.getNum = function(){
    	var me = this;
    	return BarCodeFormListInstance.getNum();
    };
    //查询按钮查询
    Class.prototype.onSearch = function(where,callback){
    	var me = this;
    	me.clearData();
    	BarCodeFormListInstance.onSearch(where,function(list){
			callback(list);
		});
    };
 	//打印
	Class.prototype.onPrintList = function(data,ModelType,ModelTypeName,title){
		var me = this;
		data = data || [];
		if(data.length==0)return false;
		 //去除前缀
		data = JSON.stringify([data]).replace(RegExp("LisBarCodeFormVo_", "g"), "").replace(RegExp("LisBarCodeForm_", "g"), "");
		var url = uxutil.path.LAYUI + '/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=3&ModelType='+ModelType+'&ModelTypeName='+ModelTypeName+'&isDownLoadPDF=true&IsPreview=0'+ (me.config.PrintName ? ("&PrintName=" + me.config.PrintName) : "");
		layer.open({
			title:title,
			type:2,
			content:url,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['500px','380px'],
			success:function(layero,index){
				var iframe = $(layero).find('iframe')[0].contentWindow;
				iframe.PrintDataStr = data;
			}
		});
	};

    //获取匹配列表数据
    Class.prototype.getItemList = function(){
    	var me = this;
    	return ItemListInstance.getListData();
    };
    //匹配列表更新匹配上的标记 改变颜色-
    Class.prototype.findBarCode = function(barcode){
    	var me = this;
    	 ItemListInstance.findBarCode(barcode);
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
	//打印流转单
	Class.prototype.PrintModel = function(BusinessTypeCode,ModelTypeCode,ModelTypeName,PrintData){
		var me = this;
	    if(PrintData.length==0)return false;
		PrintData = JSON.stringify([PrintData]).replace(RegExp("LisBarCodeFormVo_", "g"), "").replace(RegExp("LisBarCodeForm_", "g"), "");
		//获取指定模板（默认第一个模板）
		me.getPrintModel(BusinessTypeCode,ModelTypeCode,function(list){
			if(list.length==0){
				layer.msg("请上传打印模板!", { icon: 0, anim: 0 });
                return false;
			}
			//默认第一个模板
			var PrintModelID = list[0].BPrintModelVO_Id;
			//根据模板生成pdf
			me.createPdf(PrintData,PrintModelID,function(data){
				var url = uxutil.path.ROOT +'/'+ data;
				print.instance.pdf.print(url, ModelTypeName, function () { }, true, function () {
                    if(me.config.TagPrintName) LODOP.SET_PRINTER_INDEX(me.config.TagPrintName);//设置打印机
                });
			});
		});
	};

    //获取指定模板 
	Class.prototype.getPrintModel = function(BusinessTypeCode,ModelTypeCode,callback){
        var  url = SELECT_PRINT_MODEL_URL + "&BusinessTypeCode=" + BusinessTypeCode + "&ModelTypeCode=" + ModelTypeCode + '&fields=BPrintModelVO_Id,BPrintModelVO_FileName&sort=[{ "property": "BPrintModel_DispOrder","direction": "ASC" }]';
        var index = layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
		    layer.close(index);
			if(data.success){
				var list = (data.value || {}).list || [];
				//默认选择第一行
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	 //根据模板生成PDF 
	Class.prototype.createPdf = function(PrintDataStr,PrintModelID,callback){
		if (typeof (PrintDataStr) != 'string') {
            layer.msg("请传递正确的JSON字符串!", { icon: 0, anim: 0 });
            return;
        }
		//获得PDF
        var index = layer.load();
		uxutil.server.ajax({
			type: "POST",
            url : CREATE_PDF_URL,
            data: JSON.stringify({ Data: PrintDataStr, PrintModelID: PrintModelID })
		},function(data){
			layer.close(index);
			if(data.success){
				callback(data.value);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//核心入口
	PreSampleContentIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		me.initHtml();
		me.initListeners();
		return me;
	};
	function afterUpateInspectRevocation(barcode){
	    BarCodeFormListInstance.revokeLoadData(barcode);
        layer.msg("撤销送检成功",{icon:6,time:2000});
	}
	window.afterUpateInspectRevocation = afterUpateInspectRevocation;
	//暴露接口
	exports(MOD_NAME,PreSampleContentIndex);
});