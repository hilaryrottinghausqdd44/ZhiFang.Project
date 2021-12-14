/**
 * @name：已确认条码
 * @author：GHX
 * @version 2021-11-05
 */
layui.extend({
	uxutil:'ux/util',
	//PreSampleBarcodeBasicHostType:'modules/pre/sample/barcode/basic/hosttype',
	PreSampleBarcodeBasicParams:'modules/pre/sample/barcode/basic/params',
	CommonSelectDept:'modules/common/select/dept',
	CommonPrint:'modules/common/print'
}).define(['uxutil','table','form','laydate','PreSampleBarcodeBasicParams','CommonSelectDept','CommonPrint'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		laydate = layui.laydate,
		uxutil = layui.uxutil,
		table = layui.table,
		//PreSampleBarcodeBasicHostType = layui.PreSampleBarcodeBasicHostType,
		PreSampleBarcodeBasicParams = layui.PreSampleBarcodeBasicParams,
		CommonSelectDept = layui.CommonSelectDept,
		CommonPrint = layui.CommonPrint,
		MOD_NAME = 'PreSampleBarcodeMenzhenIndexNotConfirm';
	//数据查询服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetHaveToPrintBarCodeForm";
	//条码作废服务地址(long nodetype, string barcodes)
	var CANCEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreBarCodeInvalid";
	//HIS医嘱信息服务地址(long nodetype, string barcode)
	var HIS_ORDER_INFO_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_HISOrderInfo";
	//条码打印次数更新(long nodetype, string barcodes)
	var PrintBarCodeNumUpdate_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateBarCodeFromPrintStatus";
	//条码打印份数
	var GetPrintBarCodeCount_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetPrintBarCodeCount";
	//取单凭证
	var PreBarCodeGatherVoucher_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreBarCodeGatherVoucher";
	
	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" lay-filter="{domId}-form">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<label class="layui-form-label" style="width:36px;">日期:</label>',
				'<div class="layui-input-inline" style="width:180px;">',
					'<input type="text" id="{domId}-TestDate" name="{domId}-TestDate"  autocomplete="off" class="layui-input" placeholder="日期范围" />',
				'</div>',
				'<label class="layui-form-label" style="width:30px;">姓名</label>',
				'<div class="layui-input-inline" style="width:100px;">',
					'<input type="text" name="{domId}-search-name" placeholder="姓名" autocomplete="off" class="layui-input">',
				'</div>',
				'<label class="layui-form-label" style="width:30px;">病历</label>',
				'<div class="layui-input-inline" style="width:120px;">',
					'<input type="text" name="{domId}-search-patno" placeholder="病历" autocomplete="off" class="layui-input">',
				'</div>',
				'<label class="layui-form-label " style="width:30px;" >条码</label>',
				'<div class="layui-input-inline " style="width:125px;" >',
					'<input type="text" id="{domId}-search-barcode" name="{domId}-search-barcode" placeholder="条码号" autocomplete="off" class="layui-input">',
				'</div>',
				'<label class="layui-form-label" style="width:30px;">科室</label>',
				'<div class="layui-input-inline" style="width:120px;">',
					'<select name="{domId}-search-dept" id="{domId}-search-dept" lay-filter="{domId}-search-dept"></select>',
				'</div>',
				'<label class="layui-form-label" style="width:30px;">病区</label>',
				'<div class="layui-input-inline" style="width:120px;">',
					'<select name="{domId}-search-district" id="{domId}-search-district" lay-filter="{domId}-search-district"></select>',
				'</div>',
				'<label class="layui-form-label" style="width:30px;">床号</label>',
				'<div class="layui-input-inline" style="width:75px;">',
					'<input type="text" name="{domId}-search-bed" placeholder="床号" autocomplete="off" class="layui-input">',
				'</div>',
				'<div class="layui-input-inline" style="width:30px;">',
					'<button type="button" class="layui-btn layui-btn-xs" id="{domId}-search—button"><i class="layui-icon layui-icon-search"></i>查询</button>',
				'</div>',
			'</div>',
			//查询栏
			'<div class="layui-form-item" id="QueryItemConditions" style="margin-top:5px;">',
				'<label class="layui-form-label" style="width:60px;">打印份数</label>',
				'<div class="layui-input-inline" style="width:30px;">',
					'<input type="text" name="{domId}-printCount" lay-filter="{domId}-printCount" class="layui-input">',
				'</div>',
				'<label class="layui-form-label layui-hide" style="width:85px;">样本过滤天数</label>',
				'<div class="layui-input-inline layui-hide" style="width:40px;">',
					'<input type="text" name="{domId}-filterDays" lay-filter="{domId}-filterDays" class="layui-input">',
				'</div>',
				'<div class="layui-input-inline" id="toPrintDiv">',
				'<input type="checkbox" id="toPrint" name="toPrint" lay-filter="toPrint" lay-skin="switch" lay-text="直接打印|直接打印">',
				'</div>',
				'<div class="layui-input-inline layui-hide" style="width:90px;float:right;">',
					'<input type="checkbox" name="" id="{domId}_check_all" title="全部勾选" lay-filter="{domId}_check_all" lay-skin="primary">',
				'</div>',
				'<div class="layui-input-inline layui-hide" style="width:65px;float:right;">',
					'<input type="checkbox" name="showType" lay-filter="showType" lay-skin="switch" lay-text="卡片|列表">',
				'</div>',
			'</div>',
		'</div>',
		'<style>',
			'.{domId}-grid-div{padding:2px;margin-bottom:2px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
			'.layui-input + .layui-icon { cursor:pointer;position: absolute;top: 4px;right: 5px;color: #009688; }',
//			'.layui-form-onswitch-red{border-color:#FF5722;background-color:#FF5722;}',
		'</style>'
	].join('');
	//数据表格模板
	var BarCodeList_MOD_DOM = [
		'<div class="layui-row layui-col-space10 layui-form" lay-filter="{domId}-form-cards" style="background-color:#f2f2f2;padding:10px;margin:0;">',
		'<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12">',
		'<div id="{domId}-cards" class="layui-row layui-col-space15"></div>',
		'<table id="{domId}-table" lay-filter = "{domId}-table" class="layui-hide"></table>',
		'</div>',
		'</div>'
	].join('');
	//卡片模板
	var CARD_TEMPLET = [
		'<div class="layui-col-xs{colsType} layui-col-sm{colsType} layui-col-md{colsType} layui-col-lg{colsType}">',
		'<div class="layui-card">',
		'<div class="layui-card-header">',
		'{LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName}({LisBarCodeFormVo_LisBarCodeForm_LisPatient_Age}{LisBarCodeFormVo_LisBarCodeForm_LisPatient_AgeUnitName}) {LisBarCodeFormVo_LisBarCodeForm_LisPatient_GenderName} ',
		'<span class="layui-badge-rim" >病历号：{LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo}</span>',
		'<span class="layui-badge-rim layui-hide" style="color:red;" id="{domId}_isprint_{LisBarCodeFormVo_LisBarCodeForm_BarCode}"></span>',
		'<span class="layui-badge-rim layui-hide" style="color:blue;" id="{domId}_prefabricate_{LisBarCodeFormVo_LisBarCodeForm_Id}"></span>',
		'<span style="float:right;">',
		'<input type="checkbox" checkbox="true" name="{domId}_check_{LisBarCodeFormVo_LisBarCodeForm_BarCode}" id="{domId}_check_{LisBarCodeFormVo_LisBarCodeForm_BarCode}" ',
		'lay-filter="{domId}_check_{LisBarCodeFormVo_LisBarCodeForm_BarCode}" lay-skin="primary">',
		'</span>',
		'</div>',
		'<div class="layui-card-body">',
		'<div class="layui-form">条码号：{LisBarCodeFormVo_LisBarCodeForm_BarCode}</div>',
		'<div>科室：{LisBarCodeFormVo_LisBarCodeForm_LisPatient_DeptName}</div>',
		//					'<div>检验组：{section}</div>',
		'<div>{LisBarCodeFormVo_SampleTypeName} （{LisBarCodeFormVo_LisBarCodeForm_Color}）<span class="layui-badge" style="background-color:{LisBarCodeFormVo_LisBarCodeForm_ColorValue}"></span></div>',
		//					'<div>审核时间：{checkTime}</div>',
		'<div>医嘱项目：{LisBarCodeFormVo_LisBarCodeForm_ParItemCName}</div>',
		'</div>',
		'</div>',
		'</div>'
	].join('');
	//默认的字段:
	//姓名、年龄、年龄单位、性别、病历号、条码号、科室、样本类型、颜色、颜色值、医嘱项目
	var DEFALT_LIST_FIELDS = [
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_Age',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_AgeUnitName',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_GenderName',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo',
		'LisBarCodeFormVo_LisBarCodeForm_BarCode',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_DeptName',
		'LisBarCodeFormVo_SampleTypeName',
		'LisBarCodeFormVo_LisBarCodeForm_Color',
		'LisBarCodeFormVo_LisBarCodeForm_ColorValue',
		'LisBarCodeFormVo_LisBarCodeForm_OrderExecTime',
		'LisBarCodeFormVo_LisBarCodeForm_ParItemCName',
		'LisBarCodeFormVo_SampleGroupingType',
		'LisBarCodeFormVo_ItemId',
		'LisBarCodeFormVo_LisBarCodeForm_IsPrep',
		'LisBarCodeFormVo_LisBarCodeForm_Id',
		'LisBarCodeFormVo_PreInfo'
	];
	//列表字段：格式=BarCode&条码号&100&show,OrderExecTime&医嘱指定执行时间&100&show,
	var LIST_COLS_INFO = null;
	//后台获取字段数组
	var LIST_FIELDS = null;
	//确认或打印后不刷新数据，需要进行标识的条码
	var ConfirmOrPrintAfterBarCode = [];
	//直接打印按钮状态
	var directPrintButtonStatus = false;
	//功能参数实例
	var PreSampleBarcodeBasicParamsInstance = null;
	//打印实例
	var CommonPrintInstance = CommonPrint.render({
		modelUrl:uxutil.path.LAYUI + '/model/barcode/model1.txt'
	});
	//门诊样本条码
	var PreSampleBarcodeMenzhenIndex = {
		//对外参数
		config:{
			domId:null,
			height: null,
			nodetype:null,
			colsType:6,//卡片显示宽度
			LIST_DATA: [],//根据姓名病历号等过滤过的数据(根据修改变化)
			Original_LIST_DATA: [],//根据姓名病历号等过滤过的数据(原始数据)
			Table_Data: [],//病人列表数据
			TheInitial_LIST_DATA: [],//未过滤全部数据(根据修改变化)
			TheInitial_Original_LIST_DATA: []//未过滤全部数据(原始数据)
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleBarcodeMenzhenIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		
		$('#' + me.config.domId).append(html);
		form.render();

		var barcodelisthtml = BarCodeList_MOD_DOM.replace(/{domId}/g, me.config.domId);
		$('#patient-barcode-index').html(barcodelisthtml);

		//根据参数设置初始化打印份数、样本过滤天数
		var defaultValues = {};
		defaultValues[me.config.domId + '-printCount'] = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0049');
		defaultValues[me.config.domId + '-filterDays'] = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0021');
		form.val(me.config.domId + '-form',defaultValues);
		
		//科室下拉框
		CommonSelectDept.render({domId:me.config.domId + '-search-dept',code:'1001101'});
		//病区下拉框
		CommonSelectDept.render({ domId: me.config.domId + '-search-district', code: '1001102' });


		//直接打印按钮显示  是否发送打包机
		var ZJDY = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0055');
		//直接打印按钮显示  是否发送打包机
		var ISSendBaler = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0034');
		if (ZJDY == "1" && ISSendBaler == "1") {
			if ($("#toPrintDiv").hasClass("layui-hide")) {
				$("#toPrintDiv").removeClass("layui-hide");
			}
		}
		else {
			if (!$("#toPrintDiv").hasClass("layui-hide")) {
				$("#toPrintDiv").addClass("layui-hide");
			}
		}

		var newdate = uxutil.date.toString(new Date(uxutil.server.date.getDate()), true)
		//初始化日期
		laydate.render({
			elem: '#' + me.config.domId + "-TestDate"
			, range: true
			, value: newdate + " - " + newdate
		}); 
		me.initTable();
	};	
	//初始化病人信息列表
	Class.prototype.initTable = function () {
		var me = this;
		table.render({
			elem: '#patient-index-table',
			height: 'full-130',
			defaultToolbar: false,
			size: 'sm',
			page: false,
			data: [],
			url: "",
			cols: [[{ type: 'numbers' },
			{
				field: 'LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName',
				title: '姓名',
				minWidth: 80,
				hide: false,
				sort: false
			}, {
				field: 'LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo',
				title: '病历号',
				minWidth: 100,
				hide: false,
				sort: false
			}, {
				field: 'LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed',
				title: '床号',
				minWidth: 100,
				hide: false,
				sort: false
			}
			]],
			limit: 99999,
			autoSort: true, //禁用前端自动排序
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				if (count > 0) {
					setTimeout(function () {
						if ($("#patient-index-table+div .layui-table-body table.layui-table tbody tr:first-child")[0])
							$("#patient-index-table+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
					}, 100);
				}
			}
		});
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		//卡片显示类型
		form.on('select(colsType)', function (data) {
			me.config.colsType = data.value;
			me.onShowCards();
		});
		//显示类型 卡片 列表
		form.on('switch(showType)', function (data) {
			if (data.elem.checked) {
				me.onShowCards();
			} else {
				me.onShowTable();
			}
			me.onShowData();
			//如果存在
			if (ConfirmOrPrintAfterBarCode.length > 0) {
				me.ConfirmPrintAfterDispose([], ConfirmOrPrintAfterBarCode, data.elem.checked);
			}
		});
		//全部勾选和取消勾选
		form.on('checkbox(' + me.config.domId + '_check_all)', function (data) {
			if (data.elem.checked) {
				//$("#" + me.config.domId + '_check_all').attr("title","取消勾选");
				$("#" + me.config.domId + '_check_all').next().find('span').html("取消勾选");
				me.onCheckAll();
			} else {
				$("#" + me.config.domId + '_check_all').next().find('span').html("全部勾选");
				me.onUnCheckAll();
			}
		});	
		//查询
		$("#" + me.config.domId + "-search—button").on("click", function () {
			me.onSearch();
		});
		//直接打印
		form.on('switch(toPrint)', function (data) {
			directPrintButtonStatus = data.elem.checked;
		});		
		//回车监听
		$(document).keydown(function (event) {
			switch (event.keyCode) {
				case 13://回车 -- 样本号
					//预制条码文本框回车监听
					if (document.activeElement == document.getElementById(me.config.domId + "-search-barcode")) {
						me.onSearch();
					}					
					break;
			}
		});
		//监听行单击事件（双击事件为：rowDouble）
		table.on('row(patient-index-table)', function (obj) {
			var data = obj.data;
			//标注选中样式
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			me.config.LIST_DATA = [];
			me.config.Original_LIST_DATA = [];
			for (var i = 0; i < me.config.TheInitial_LIST_DATA.length; i++) {
				var entity = me.config.TheInitial_LIST_DATA[i];
				if (data.LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName == entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName && data.LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo == entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo && data.LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed == entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed) {
					me.config.LIST_DATA.push(entity);
					me.config.Original_LIST_DATA.push(entity);
				}
			}
			me.onShowData();
		});
	};
	//查询处理
	Class.prototype.onSearch = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			barcodeType = values[me.config.domId + '-barcodeType'],
			barcode = values[me.config.domId + '-barcode'],
			filterDays = values[me.config.domId + '-filterDays'],
			printStatus = values[me.config.domId + '-printStatus'],
			name = values[me.config.domId + '-search-name'],
			patno = values[me.config.domId + '-search-patno'],
			barcodeno = values[me.config.domId + '-search-barcode'],
			dept = values[me.config.domId + '-search-dept'],
			district = values[me.config.domId + '-search-district'],
			bed = values[me.config.domId + '-search-bed'],
			testdate = values[me.config.domId + '-TestDate'];

		var where = [];
		if(barcodeType && barcode){
			where.push(barcodeType + "='" + barcode + "'");
		}
		if (testdate) {
			var testdatearr = testdate.split(' - ');
			where.push("lisorderform.OrderTime>='" + testdatearr[0] + "'");
			where.push("lisorderform.OrderTime<='" + testdatearr[1] + "'");
		}
		else if (filterDays && filterDays > 0)
		{
			var start = uxutil.date.toString(uxutil.date.getNextDate(new Date(),0-filterDays),true),
			end = uxutil.date.toString(uxutil.date.getNextDate(new Date()),true);
			
			where.push("lisorderform.OrderTime>='" + start + "'");
			where.push("lisorderform.OrderTime<'" + end + "'");
		}
		if(name){where.push("lispatient.CName='" + name + "'");}
		if(patno){where.push("lispatient.PatNo='" + patno + "'");}
		//if(barcode){where.push("='" + barcode + "'");}
		if(dept){where.push("lispatient.DeptID='" + dept + "'");}
		if(district){where.push("lispatient.DistrictID='" + district + "'");}
		if (bed) { where.push("lispatient.Bed='" + bed + "'"); }
		if (where.length < 1 && !barcodeno) {
			layer.msg("请输入查询条件！", { icon: 5 });
			return;
		}
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_LIST_URL,
			type:'post',
			data: JSON.stringify({
				barcode: barcodeno ? barcodeno:"",
				where:where.join(' and '),
				printStatus: true,
				fields:LIST_FIELDS.join(','),
				isPlanish:true
			})
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				me.config.TheInitial_LIST_DATA = (data.value || {}).list || [];
				me.config.TheInitial_Original_LIST_DATA = JSON.parse(JSON.stringify(me.config.TheInitial_LIST_DATA));
				var arr = [];
				for (var i = me.config.TheInitial_LIST_DATA.length - 1; i >= 0; i--) {
					var entity = me.config.TheInitial_LIST_DATA[i];
					if (arr.length == 0) {
						var obj = {};
						obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName;
						obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed;
						obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo;
						arr.push(obj);
					} else {
						for (var a = 0; a < arr.length; a++) {
							if (arr[a].LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName != entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName || arr[a].LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed != entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed || arr[a].LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo != entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo) {
								var obj = {};
								obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName;
								obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed;
								obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo;
								arr.push(obj);
							}
						}
					}
				}
				me.config.Table_Data = arr;
				me.ReloadTable();
			}else{
				layer.msg(data.msg,{icon:5});
			}
		},true);
	};
	//重新加载表格
	Class.prototype.ReloadTable = function () {
		var me = this;
		table.reload("patient-index-table", {
			data: me.config.Table_Data
		});
	};
	//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		$("#" + me.tableId).find('.layui-table-main tr[data-index="0"]');
	};
	//显示数据
	Class.prototype.onShowData= function(){
		var me = this,
			values = form.val(me.config.domId + '-form');
			//showType = values.showType;
		me.onShowTable();
		//if(showType == 'on'){//卡片方式显示
		//	me.onShowCards();
		//}else{//列表方式显示
			
		//}
	};
	//显示卡片
	Class.prototype.onShowCards = function(){
		var me = this,
			list = me.config.LIST_DATA || [],
			html= [];
			
		for(var i in list){
			html.push(me.createCard(list[i]));
		}
		$("#" + me.config.domId + '-cards').html(html.join(''));
		
		$("#" + me.config.domId + '-table').next().hide();
		$("#" + me.config.domId + '-cards').show();
		
//		for(var i in data){
//			data[i].SexName = data[i].SexName == '男' ? '<span class="layui-badge">男</span>' : '<span class="layui-badge layui-bg-green">女</span>';
//		}
		
		form.render('checkbox');
	};
	//创建卡片
	Class.prototype.createCard = function(data){
		var me = this,
			html = CARD_TEMPLET.replace(/{colsType}/g,me.config.colsType).replace(/{domId}/g,me.config.domId);
			
		for(var i in data){
			var reg = new RegExp('{' + i + '}', 'g');
			html = html.replace(reg,data[i]);
		}
		
		return html;
	};
	//显示列表
	Class.prototype.onShowTable = function(){
		var me = this;
		$("#" + me.config.domId + '-cards').hide();
		//初始化卡片列表
		if(me.table){
			me.table.reload({
				data:me.config.LIST_DATA || []
			});
		}else{
			var cols = [{ type: 'checkbox', fixed: 'left' }];
			cols.push({
				field: "LisBarCodeFormVo_LisBarCodeForm_ColorValue", title: "颜色", width: 40,
				hide: false,
				templet: function (d) {
					var result = '<div style="height:100%;background-color:' + d.LisBarCodeFormVo_LisBarCodeForm_ColorValue + '"></div>';
					return result;
				}
			});
			for(var i in LIST_COLS_INFO){
				//BarCode&条码号&100&show
				var arr = LIST_COLS_INFO[i].split('&');
				cols.push({
					field:arr[0],title:arr[1],width:arr[2],
					hide:(arr[3] == 'show' ? false : true)
				});
			}
			me.table = table.render({
				elem: '#' + me.config.domId + '-table',
				height: 'full-150',
				page:false,
				cols:[cols],
//				cols:[[
//					{type: 'checkbox', fixed: 'left'},
//					{field:'bed',title:'床号',width:50},
//					{field:'CName',title:'姓名',width:80},
//					{field:'SexName',title:'性别',width:50,templet:function(d){
//						var result = d.SexName == '男' ? '<span class="layui-badge">男</span>' : '<span class="layui-badge layui-bg-green">女</span>';
//						return result;
//					}},
//					{field:'age',title:'年龄',width:60},
//					{field:'patNo',title:'病历号',width:100},
//					{field:'checkG',title:'采样管',width:70},
//					{field:'type',title:'样本类型',width:80},
//					{field:'bgColor',title:'颜色',width:50,templet:function(d){
//						var result = '<div style="background-color:' + d.bgColor + '"></div>';
//						return result;
//					}},
//					{field:'Barcode',title:'条码号',width:120},
//					{field:'items',title:'医嘱项目'},
//					{field:'section',title:'检验组',width:100},
//					{field:'deptName',title:'科室',width:70}
//				]],
				data:me.config.LIST_DATA || []
			});
		}
		
		$("#" + me.config.domId + '-table').next().show();
	};
	//获取所有勾选框元素
	Class.prototype.getAllCheckbox = function(){
		var me = this,
			checkbox = $("input[checkbox='true']");
			
		return checkbox;
	};
	//勾选所有的卡片
	Class.prototype.onCheckAll = function(){
		var me = this,
			checkbox = me.getAllCheckbox(),
			len = checkbox.length,
			values = {};
			
		for(var i=0;i<len;i++){
			values[$(checkbox[i]).attr("name")] = true;
		}
		form.val(me.config.domId + '-form-cards',values);
	};
	//去掉所有的卡片的勾选
	Class.prototype.onUnCheckAll = function(){
		var me = this,
			checkbox = me.getAllCheckbox(),
			len = checkbox.length,
			values = {};
			
		for(var i=0;i<len;i++){
			values[$(checkbox[i]).attr("name")] = false;
		}
		form.val(me.config.domId + '-form-cards',values);
	};
	//条码打印
	Class.prototype.onBarcodePrint = function (printbarcodes) {
		var me = this,
			barcodes = me.getCheckedBarcodes();
		if (printbarcodes && printbarcodes.length > 0) {
			barcodes = printbarcodes;
		}
		if (barcodes.length == 0) {
			layer.msg("请勾选样本条码！", { icon: 5 });
		} else {
			var list = [];
			//指定打印机
			var printer = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0052');
			//是否直接打印
			var ZJDY = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0055');
			//是否发送到打包机
			var DBJ = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0034');
			//根据条码查找条码所属的全部数据信息
			for (var i in barcodes) {
				for (var j in me.config.LIST_DATA) {
					if (me.config.LIST_DATA[j].LisBarCodeFormVo_LisBarCodeForm_BarCode == barcodes[i]) {
						list.push(me.config.LIST_DATA[j]);
					}
				}
			}
			if (list.length < 1) {
				layer.msg("没有可打印的条码标签！", { icon: 5 });
				return;
			}
			//判断是直接打印条码还是发送到打包机
			if ((ZJDY == "1" && directPrintButtonStatus) || (ZJDY != "1" && DBJ != "1")) {
				var zdyPrintCount = form.val(me.config.domId + '-form')[me.config.domId + '-printCount'];
				var notArriveTimeBarCode = [];
				for (var i = 0; i < list.length; i++) {
					var isPrintBarCode = true;					
					//是否可以打印条码
					if (isPrintBarCode) {
						var printcount = 1;
						if (zdyPrintCount > 0) {
							printcount = zdyPrintCount;
						} else {
							uxutil.server.ajax({
								url: GetPrintBarCodeCount_URL,
								type: 'post',
								data: JSON.stringify({
									nodetype: me.config.nodetype,
									barcodes: list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode
								})
							}, function (data) {
								if (data.success) {
									printcount = data.value;
								} else {
									layer.msg(data.msg, { icon: 5 });
								}
							}, true);
						}
						for (var a = 0; a < printcount; a++) {
							var listarr = [];
							listarr = list[i];
							CommonPrintInstance.print(listarr, printer, function (item, isLastOne) {
								if (isLastOne) {
									//增加打印次数
									//var loadIndex = layer.load();//开启加载层
									uxutil.server.ajax({
										url: PrintBarCodeNumUpdate_URL,
										type: 'post',
										data: JSON.stringify({
											nodetype: me.config.nodetype,
											barcodes: item.LisBarCodeFormVo_LisBarCodeForm_BarCode,
											IsAffirmBarCode: "1"
										})
									}, function (data) {
										//layer.close(loadIndex);//关闭加载层
										if (data.success) {
										} else {
											layer.msg(data.msg, { icon: 5 });
										}
									}, true);									
								}
							});
						}
					}
				}
				if (notArriveTimeBarCode.length > 0) {
					layer.alert("以下条码号未到医嘱指定执行时间不允许打印:" + notArriveTimeBarCode.join(","));
				}
			} else if ((ZJDY == "1" && !directPrintButtonStatus) || (ZJDY != "1" && DBJ == "1")) {
				//发送到打包机
				layer.alert("发送到打包机！");
			}
		}
	};
	//扫码补打
	Class.prototype.onScanPrint = function(){
		var index = layer.prompt({
			title:'请输入补打条码',
			success:function(layero){
				var input = $(layero).find('input');
				input.on("keypress",function(event){
					if(event.keyCode == "13"){//回车
						var value = $(input).val();
						alert(value); //得到value
						layer.close(index);
					}
				});
			}
		},function(value, index, elem){
			alert(value); //得到value
			layer.close(index);
		});
	};
	//打印清单
	Class.prototype.onListPrint = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();

		if (barcodes.length == 0) {
			layer.msg("请勾选样本条码！", { icon: 5 });
		} else {
			var list = [], data = [[]];
			for (var i in barcodes) {
				for (var j in me.config.LIST_DATA) {
					if (me.config.LIST_DATA[j].LisBarCodeFormVo_LisBarCodeForm_BarCode == barcodes[i]) {
						list.push(me.config.LIST_DATA[j]);
						break;
					}
				}
			}
			if (list.length == 0) return data;
			data = JSON.stringify([list]);
			parent.layer.open({
				type: 2,
				area: ['45%', '55%'],
				fixed: false,
				maxmin: false,
				title: '打印',
				content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=3&ModelType=6&ModelTypeName=样本条码_样本清单',
				success: function (layero, index) {
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.PrintDataStr = data;
				},
				end: function () {
				}
			});
		}
	};
	//条码作废
	Class.prototype.onBarcodeCancel = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();

		if (barcodes.length == 0) {
			layer.msg("请勾选样本条码！", { icon: 5 });
		} else {
			layer.confirm('确定要作废吗?', { icon: 3, title: '提示' }, function (index) {
				layer.close(index);
				var loadIndex = layer.load();//开启加载层
				uxutil.server.ajax({
					url: CANCEL_URL,
					type: 'post',
					data: JSON.stringify({
						nodetype: me.config.nodetype,
						barcodes: barcodes.join(',')
					})
				}, function (data) {
					layer.close(loadIndex);//关闭加载层
					if (data.success) {
						me.onSearch();
					} else {
						layer.msg(data.msg, { icon: 5 });
					}
				}, true);
			});
		}
	};
	//取单凭证
	Class.prototype.onVoucher = function(){
		var me = this,
			BusinessTypeCode = 3,//前处理
			ModelTypeCode = 7,//样本签收_样本清单
			ModelTypeName = "样本条码_取单凭证";
		var barcodes = [];
		if (inbarcode) {
			barcodes = [inbarcode];
		} else {
			barcodes = me.getCheckedBarcodes();

		}
		if (barcodes.length == 0) {
			layer.msg("请勾选样本条码！", { icon: 5 });
		} else {
			//调用通用打印界面
			var config = {
				nodetypeID: me.config.nodetype,
				barCodes: barcodes.join(","),
				isloadtable: true,
				isupdatebcitems: true
			};
			layer.open({
				type: 2,
				area: ['50%', '60%'],
				fixed: false,
				maxmin: false,
				title: '打印',
				content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=' + BusinessTypeCode + '&ModelType=' + ModelTypeCode + '&ModelTypeName=' + ModelTypeName + '&isDownLoadPDF=false',
				success: function (layero, index) {
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.PrintDataStr = JSON.stringify(config);
					iframe.GetPDFUrl = PreBarCodeGatherVoucher_URL;
				},
				end: function () { }
			});
		}
	};
	//帮助
	Class.prototype.onHelp = function(){
		layer.open({
			title: '帮助文档',
			type: 2,
			content: uxutil.path.LAYUI + '/views/help/pre/sample/barcode/menzhen/index.html?t=' + new Date().getTime(),
			maxmin: true,
			toolbar: true,
			resize: true,
			area: ['95%', '95%']
		});
	};
	//HIS医嘱信息
	Class.prototype.onHisOrderInfo = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();
		if (barcodes.length == 0) {
			layer.msg("请勾选样本条码进行操作！", { icon: 5 });
		} else {
			layer.open({
				type: 2,
				area: ['90%', '90%'],
				fixed: false,
				maxmin: false,
				title: '原始医嘱信息',
				content: uxutil.path.ROOT + '/ui/layui/modules/pre/sample/barcode/basic/hisOrderInfo.html?NODETYPE=' + me.config.nodetype + '&BARCODE=' + barcodes.join(","),
				success: function (layero, index) {
				},
				end: function () { }
			});
		}
	};
	//获取勾选的条码数组
	Class.prototype.getCheckedBarcodes = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			showType = values.showType,
			barcodes = [];

		if (showType == 'on') {//卡片方式显示
			var checkValues = form.val(me.config.domId + '-form-cards');
			for (var i in checkValues) {
				if (checkValues[i] === "on") {
					if (i.split('_').slice(-1) != null && i.split('_').slice(-1) != "") {
						barcodes.push(i.split('_').slice(-1));
					} else {
						layer.msg("请不要选择无条码号样本单！");
						return;
					}
				}
			}
		} else {//列表方式显示
			var checkedList = table.checkStatus(me.config.domId + '-table').data;
			for (var i in checkedList) {
				if (checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode) {
					barcodes.push(checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
				} else {
					layer.msg("请不要选择无条码号样本单！");
					return;
				}
			}
		}
		return barcodes;
	};
	//清空数据
	Class.prototype.clear = function(){
		var me = this;
		me.config.LIST_DATA = [];
		me.config.Original_LIST_DATA = [];
		me.config.Table_Data = [];
		me.TheInitial_LIST_DATA = [];
		me.TheInitial_Original_LIST_DATA = [];
		$("#patient-barcode-index").html("");
		table.reload("patient-index-table", { data: [] });
		me.onShowData();
	};
	//核心入口
	PreSampleBarcodeMenzhenIndex.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		PreSampleBarcodeBasicParamsInstance = PreSampleBarcodeBasicParams.render({
			nodetype: me.config.nodetype
		});
		//初始化功能参数
		PreSampleBarcodeBasicParamsInstance.init(function(){
			//列表字段
			LIST_COLS_INFO = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0064').split(',') || [];
			LIST_FIELDS = DEFALT_LIST_FIELDS;
			for(var i in LIST_COLS_INFO){
				var arr = LIST_COLS_INFO[i].split('&');
				var hasValue = false;
				for(var j in DEFALT_LIST_FIELDS){
					if(DEFALT_LIST_FIELDS[j] == arr[0]){
						hasValue = true;
						break;
					}
				}
				if(!hasValue){
					LIST_FIELDS.push(arr[0]);
				}
			}
			
			//初始化HTML
			me.initHtml();
			//监听事件
			me.initListeners();
		});
		
		return me;
	};
	//返回撤销成功的条码号
	function afterUpateRevocation(data, barCode) {
		//被撤销确认的条码号
		if (data && data[0]) {
			var obj = JSON.parse(data[0]);
			if (obj[barCode] == 'true') {
				layer.msg("撤销确认成功", { icon: 6, time: 2000 });
			} else if (obj[barCode] == 'false') {
				layer.alert("以下条码撤销失败，请检查样本状态：" + BarCodeConfirmError);
			}
		}
		Class.prototype.onSearch();
	}
	window.afterUpateRevocation = afterUpateRevocation;
	//暴露接口
	exports(MOD_NAME,PreSampleBarcodeMenzhenIndex);
});