/**
 * @name：未确认条码
 * @author：GHX
 * @version 2020-11-05
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase:'ux/base',
	PreSampleBarcodeBasicParams:'modules/pre/sample/barcode/basic/params',
	CommonSelectDept:'modules/common/select/dept',
	CommonPrint:'modules/common/print'
}).define(['uxutil','uxbase','table','form','dropdown','PreSampleBarcodeBasicParams','CommonSelectDept','CommonPrint'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		dropdown = layui.dropdown,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		table = layui.table,
		PreSampleBarcodeBasicParams = layui.PreSampleBarcodeBasicParams,
		CommonSelectDept = layui.CommonSelectDept,
		CommonPrint = layui.CommonPrint,
		MOD_NAME = 'PreSampleBarcodeMenzhenIndexConfirm';
	
	//HIS获取条码列表服务地址(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex)
	var HIS_GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreHISGetSamplingGrouping";
	//LIS获取条码列表服务地址(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex)
	var LIS_GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreLISGETSamplingGrouping";
	//数据查询服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetHaveToPrintBarCodeForm";
	//样本确认服务地址(long nodetype, string barcodes)
	var CHECK_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreBarCodeAffirm";
	//条码作废服务地址(long nodetype, string barcodes)
	var CANCEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreBarCodeInvalid";
	//取消确认服务地址
	//var UNCHECK_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/CancelOrder";
	//重新分组(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex)
	var REGROUP_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreANewSamplingGrouping";
	//HIS医嘱信息服务地址(long nodetype, string barcode)
	var HIS_ORDER_INFO_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_HISOrderInfo";
	//条码打印次数更新(long nodetype, string barcodes)
	var PrintBarCodeNumUpdate_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateBarCodeFromPrintStatus";
	//条码打印份数
	var GetPrintBarCodeCount_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_GetPrintBarCodeCount";
	//采集排队信息
	var CreatEqueuingMachineInfo_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_CreatEqueuingMachineInfo";
	//取单凭证
	var PreBarCodeGatherVoucher_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreBarCodeGatherVoucher";
	//预制条码绑定
	var UpdateLisBarCodeFormBarCodeByBarCodeFormID_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateLisBarCodeFormBarCodeByBarCodeFormID";

	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" lay-filter="{domId}-form">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<label class="layui-form-label" style="width:60px;">核收条件</label>',
				'<div class="layui-input-inline" style="width:80px;">',
					'<select name="{domId}-barcodeType" id="{domId}-barcodeType" lay-filter="{domId}-barcodeType"></select>',
				'</div>',
				'<div class="layui-input-inline" id="{domId}-barcode-div">',
					'<input type="text" id="{domId}-barcode" name="{domId}-barcode" placeholder="请输入内容" autocomplete="off" class="layui-input">',
				'</div>',			
				'<label class="layui-form-label" style="width:60px;">打印份数</label>',
				'<div class="layui-input-inline" style="width:30px;">',
					'<input type="text" name="{domId}-printCount" lay-filter="{domId}-printCount" class="layui-input">',
				'</div>',
				'<label class="layui-form-label" style="width:85px;" id="sampleSeletDayLabel">样本过滤天数</label>',
				'<div class="layui-input-inline" style="width:40px;" id="sampleSeletDayInline">',
					'<input type="text" name="{domId}-filterDays" lay-filter="{domId}-filterDays" class="layui-input">',
				'</div>',
				'<div class="layui-input-inline" id="autoPrintDiv" style="width: 81px;">',
					'<input type="checkbox" id="autoPrint" name="autoPrint" lay-filter="autoPrint" lay-skin="switch" lay-text="自动打印|自动打印">',
				'</div>',
				'<div class="layui-input-inline" id="toPrintDiv" style="width: 81px;">',
					'<input type="checkbox" id="toPrint" name="toPrint" lay-filter="toPrint" lay-skin="switch" lay-text="直接打印|直接打印">',
				'</div>',
				
				'<div class="layui-input-inline" id="check_all_div" style="width:105px;float:right;">',
					'<input type="checkbox" name="" id="{domId}_check_all" title="全部勾选" lay-filter="{domId}_check_all" lay-skin="primary">',
				'</div>',
				'<div class="layui-input-inline" id="colsTypeDiv" style="width:80px;float:right;">',
					'<select name="colsType" id="colsType" lay-filter="colsType">',
						'<option value="6" selected>二列</option>',
						'<option value="4">三列</option>',
						'<option value="3">四列</option>',
					'</select>',
				'</div>',
				'<label class="layui-form-label" id="colsTypeLabel" style="width:60px;float:right;">显示列数</label>',
				'<div class="layui-input-inline" style="width:65px;float:right;">',
					'<input type="checkbox" name="showType" lay-filter="showType" lay-skin="switch" lay-text="卡片|列表" checked>',
				'</div>',
				'<div class="layui-input-inline" id="patientPriority" style="width:65px;float:right;">',
					'<input type="checkbox" id="patientType" name="patientType" lay-filter="patientType" lay-skin="switch" lay-text="优先|一般">',
				'</div>',
			'</div>',	
			'<div class="layui-form-item" style="margin-bottom:0;" id="{domId}-prebarcode-div">',
				'<label class="layui-form-label" style="width:88px;">预制条码匹配</label>',
				'<div class="layui-input-inline">',
					'<input type="text" id="{domId}-prebarcode" name="{domId}-prebarcode" placeholder="请输入内容" autocomplete="off" class="layui-input">',
				'</div>',
			'</div>',	
		'</div>',		
		'<style>',
			'.{domId}-grid-div{padding:2px;margin-bottom:2px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
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
					'<div class="layui-form">条码号：{LisBarCodeFormVo_LisBarCodeForm_BarCode}<div class="layui-input-inline" id="{domId}-{LisBarCodeFormVo_LisBarCodeForm_Id}-entering-prebarcode-div"><input type="text" id="{domId}-{LisBarCodeFormVo_LisBarCodeForm_Id}-entering-prebarcode" name="{domId}-{LisBarCodeFormVo_LisBarCodeForm_Id}-entering-prebarcode" placeholder="请输入条码号" autocomplete="off" class="layui-input"></div></div>',
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
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_Age',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_AgeUnitName',
		'LisBarCodeFormVo_LisBarCodeForm_LisPatient_GenderName',
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
	var ConfirmOrPrintAfterBarCode=[];
	//站点类型实例
	//var PreSampleBarcodeBasicHostTypeInstance = PreSampleBarcodeBasicHostType.render();
	//功能参数实例
	var PreSampleBarcodeBasicParamsInstance = null;
	//直接打印按钮状态
	var directPrintButtonStatus = false;
	//自动打印按钮状态
	var autoPrintButtonStatus = false;
	//人员ID
	var EmpId = "";
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
		var html = MOD_DOM.replace(/{domId}/g, me.config.domId);
		
		$('#' + me.config.domId).append(html);

		var barcodelisthtml = BarCodeList_MOD_DOM.replace(/{domId}/g, me.config.domId);
		$('#patient-barcode-index').html(barcodelisthtml);

		//根据参数设置初始化核收条件下拉框
		me.initCollectionSelectHtml();
		
		//根据参数设置初始化打印份数、样本过滤天数
		var defaultValues = {};
		defaultValues[me.config.domId + '-printCount'] = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0049');
		defaultValues[me.config.domId + '-filterDays'] = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0021');
		form.val(me.config.domId + '-form', defaultValues);
		me.htmlControl();

		me.initTable();
	};
	//初始化病人信息列表
	Class.prototype.initTable = function () {
		var me = this;
		table.render({
			elem: '#patient-index-table',
			height: 'full-110',
			defaultToolbar:false,
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
					},100);
				}
			}
		});
	};
	//根据参数控制界面控件显示
	Class.prototype.htmlControl = function () {
		var me = this;
		//自动打印按钮显示  生成条码后自动打印条码
		var ZDDY = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0054');
		//直接打印按钮显示  是否发送打包机
		var ZJDY = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0055');
		//直接打印按钮显示  是否发送打包机
		var ISSendBaler = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0034');
		//是否显示自定义天数的框
		var ZDYTS = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0024');
		//叫号系统病人类型选择
		var JHBRLX = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0041');
		//是否显示预制条码自动匹配输入框
		var para0046 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0046');
		//是否调用HIS
		var para0060 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0060');
		//是否使用叫号系统
		var para0036 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0036');
		//叫号系统病人类型选择
		var para0041 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0041');
		var contenttype = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0080');
		if (contenttype == 0) {
			form.val(me.config.domId + "-form", {
				"showType": false
			});
		} else {
			form.val(me.config.domId + "-form", {
				"showType": true
			});
		}
		if (para0036 == "1" && para0041 == "1") {
			if ($("#patientPriority").hasClass("layui-hide")) {
				$("#patientPriority").removeClass("layui-hide");
			}
		} else {
			if (!$("#patientPriority").hasClass("layui-hide")) {
				$("#patientPriority").addClass("layui-hide");
			}
		}
		if (para0060 != null && para0060 != "") {
			if ($("#toolbar-button0").hasClass("layui-hide")) {
				$("#toolbar-button0").removeClass("layui-hide");
			}
		} else {
			if (!$("#toolbar-button0").hasClass("layui-hide")) {
				$("#toolbar-button0").addClass("layui-hide");
			}
		}
		if (para0046 == "1") {
			if ($("#" + me.config.domId + "-prebarcode-div").hasClass("layui-hide")) {
				$("#" + me.config.domId + "-prebarcode-div").removeClass("layui-hide");
			}
		}
		else {
			if (!$("#" + me.config.domId + "-prebarcode-div").hasClass("layui-hide")) {
				$("#" + me.config.domId + "-prebarcode-div").addClass("layui-hide");
			}
		}
		if (ZDDY == "1") {
			if ($("#autoPrintDiv").hasClass("layui-hide")) {
				$("#autoPrintDiv").removeClass("layui-hide");
			}
		}
		else {
			if (!$("#autoPrintDiv").hasClass("layui-hide")) {
				$("#autoPrintDiv").addClass("layui-hide");
			}
		}
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
		if (ZDYTS == "1")
		{
			if ($("#sampleSeletDayLabel").hasClass("layui-hide")) {
				$("#sampleSeletDayLabel").removeClass("layui-hide");
			}
			if ($("#sampleSeletDayInline").hasClass("layui-hide")) {
				$("#sampleSeletDayInline").removeClass("layui-hide");
			}
		}
		else
		{
			if (!$("#sampleSeletDayLabel").hasClass("layui-hide")) {
				$("#sampleSeletDayLabel").addClass("layui-hide");
			}
			if (!$("#sampleSeletDayInline").hasClass("layui-hide")) {
				$("#sampleSeletDayInline").addClass("layui-hide");
			}
		}
		if (JHBRLX == "1") {
			if ($("#patientType").next().hasClass("layui-hide")) {
				$("#patientType").next().removeClass("layui-hide");
			}
		}
		else {
			if (!$("#patientType").next().hasClass("layui-hide")) {
				$("#patientType").next().addClass("layui-hide");
			}
		}
	};
	//初始化核收条件下拉框HTML
	Class.prototype.initCollectionSelectHtml = function(){
		var me = this;
		EmpId = uxutil.cookie.get(uxutil.cookie.map['USERID']);
		//根据参数设置初始化核收条件下拉框
		//"ParaValue": "lispatient.PatNo&病历号&Text,lispatient.PatCardNo&就诊卡号&Text",
		//lispatient.DeptID&科室ID&Select,lispatient.DistrictID&病区ID&Select,lisorderform.HospitalID&院区ID&Select,
		//lisorderform.ExecDeptID&检验执行科室ID&Select,lisorderform.DestinationID&送检目的地ID&Select
		var param = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0015'),
			CollectionList = param ? param.split(',') : [],
			selectHtml = [];
			
		for(var i in CollectionList){
			var arr = CollectionList[i].split('&');
			selectHtml.push('<option value="' + arr[0] + '">' + arr[1] + '</option>');
		}
		$('#' + me.config.domId + '-barcodeType').html(selectHtml.join(''));
		form.on('select(' + me.config.domId + '-barcodeType)', function (data) {
			uxutil.localStorage.set(EmpId + "SampleBarCodeCheckWhere", data.value);
			me.initCollectionValueSelectHtml(data.value);
		});
		form.render();
		var localstoragevalue = uxutil.localStorage.get(EmpId + "SampleBarCodeCheckWhere");
		if (localstoragevalue) {
			$("#" + me.config.domId + '-barcodeType').val(localstoragevalue);
			form.render();
			me.initCollectionValueSelectHtml(localstoragevalue);
		} else {
			var formvalue = form.val(me.config.domId + '-form');
			if (formvalue[me.config.domId + '-barcodeType']) {
				uxutil.localStorage.set(EmpId + "SampleBarCodeCheckWhere", formvalue[me.config.domId + '-barcodeType']);
				me.initCollectionValueSelectHtml(formvalue[me.config.domId + '-barcodeType']);
			}
		}
	};
	//初始化核收条件值下拉框
	Class.prototype.initCollectionValueSelectHtml = function(type){
		var me = this;
		
		//根据参数设置初始化核收条件下拉框
		//"ParaValue": "lispatient.PatNo&病历号&Text,lispatient.PatCardNo&就诊卡号&Text",
		//lispatient.DeptID&科室ID&Select,lispatient.DistrictID&病区ID&Select,
		//lisorderform.HospitalID&院区ID&Select,lisorderform.ExecDeptID&检验执行科室ID&Select,lisorderform.DestinationID&送检目的地ID&Select
		
		function initSelectHtml(){
			
			$("#" + me.config.domId + '-barcode-div').append(
				'<select id="' + me.config.domId + '-barcode" name="' + me.config.domId + 
					'-barcode" lay-filter="' + me.config.domId + '-barcode"></select>'
			);
		};
		if($("#" + me.config.domId + '-barcode').next().length > 0){
			$("#" + me.config.domId + '-barcode').next().remove();
		}
		$("#" + me.config.domId + '-barcode').remove();
		
		if(type == "lispatient.DeptID"){//科室ID
			initSelectHtml();
			CommonSelectDept.render({domId:me.config.domId + '-barcode',code:'1001101'});
		}else if(type == "lispatient.DistrictID"){//病区ID
			initSelectHtml();
			CommonSelectDept.render({domId:me.config.domId + '-barcode',code:'1001102'});
		}else if(type == "lisorderform.HospitalID"){//院区ID
			initSelectHtml();
			CommonSelectDept.render({domId:me.config.domId + '-barcode',code:'1001108'});
		}else if(type == "lisorderform.ExecDeptID"){//检验执行科室ID
			initSelectHtml();
			CommonSelectDept.render({domId:me.config.domId + '-barcode',code:'1001104'});
		}else if(type == "lisorderform.DestinationID"){//送检目的地ID
			initSelectHtml();
			CommonSelectDept.render({domId:me.config.domId + '-barcode',code:'1001106'});
		}else{
			$("#" + me.config.domId + '-barcode-div').append(
				'<input type="text" id="' + me.config.domId + '-barcode" name="' + me.config.domId + 
					'-barcode" placeholder="请输入内容" autocomplete="off" class="layui-input">'
			);
		}
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		//卡片显示类型
		form.on('select(colsType)', function(data){
			me.config.colsType = data.value;
			me.onShowCards();
		});
		//显示类型 卡片 列表
		form.on('switch(showType)', function(data){
			if (data.elem.checked) {
				if ($("#colsTypeDiv").hasClass("layui-hide")) {
					$("#colsTypeDiv").removeClass("layui-hide");
				}
				if ($("#colsTypeLabel").hasClass("layui-hide")) {
					$("#colsTypeLabel").removeClass("layui-hide");
				}
				if ($("#check_all_div").hasClass("layui-hide")) {
					$("#check_all_div").removeClass("layui-hide");
				}
				me.onShowCards();
			} else {
				if (!$("#colsTypeDiv").hasClass("layui-hide")) {
					$("#colsTypeDiv").addClass("layui-hide");
				}
				if (!$("#colsTypeLabel").hasClass("layui-hide")) {
					$("#colsTypeLabel").addClass("layui-hide");
				}
				if (!$("#check_all_div").hasClass("layui-hide")) {
					$("#check_all_div").addClass("layui-hide");
				}
				me.onShowTable();
			}
			me.onShowData();
		});		
		//全部勾选和取消勾选
		form.on('checkbox(' + me.config.domId + '_check_all)',function(data){
			if(data.elem.checked){
				//$("#" + me.config.domId + '_check_all').attr("title","取消勾选");
				$("#" + me.config.domId + '_check_all').next().find('span').html("取消勾选");
				me.onCheckAll();
			}else{
				$("#" + me.config.domId + '_check_all').next().find('span').html("全部勾选");
				me.onUnCheckAll();
			}
		});
		//自动打印
		form.on('switch(autoPrint)', function (data) {
			autoPrintButtonStatus = data.elem.checked;			
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
					if (document.activeElement == document.getElementById(me.config.domId + "-prebarcode")) {
						var value = $("#"+me.config.domId + "-prebarcode").val();
						if (value) {
							for (var i = 0; i < me.config.LIST_DATA.length; i++) {
								if ((me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == "" || me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == null) && me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_IsPrep == "1") {
									if (me.config.LIST_DATA[i].LisBarCodeFormVo_PreInfo && value.indexOf(me.config.LIST_DATA[i].LisBarCodeFormVo_PreInfo) == 0) {
										me.UpdateBarCode(value, me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id);
										return;
									}
								}
							}
						}
					}
					if ($(document.activeElement).attr("id").indexOf("-entering-prebarcode") != -1) {
						var newbarcode = $(document.activeElement).val();
						var idarr = $(document.activeElement).attr("id").split('-');
						for (var i = 0; i < me.config.Original_LIST_DATA.length; i++) {
							if (idarr[2] == me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id) {
								if ((me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == "" || me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == null) && me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_IsPrep == "1") {
									me.UpdateBarCode(newbarcode, me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id);
								}
							}
						}
					}
					break;				
			}
		});
		//右键菜单
		var inst = dropdown.render({
			elem:'#' +me.config.domId,
			trigger:'contextmenu',
			isAllowSpread:false,//禁止菜单组展开收缩
			style:'width:220px',//定义宽度，默认自适应
			data: [{
				title:'重新分组-选中条码(X)',
				id:'regroupX',
				templet: '<i class="layui-icon layui-icon-reduce-circle"></i> {{d.title}}',
			}],
			click: function (obj, othis) {
				if (obj.id === 'regroupX') {
					me.reGroup();
				}
			}
		});
		//预制条码监听
		table.on('edit(' + me.config.domId + '-table)', function (obj) {
			for (var i = 0; i < me.config.Original_LIST_DATA.length; i++) {
				if (obj.data.LisBarCodeFormVo_LisBarCodeForm_Id == me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id) {
					if ((me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == "" || me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == null) && me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_IsPrep == "1") {
						me.UpdateBarCode(obj.value, me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id);
					}
					else
					{
						for (var i = 0; i < me.config.LIST_DATA.length; i++) {
							var barcode = me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode+"";
							if (me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id == obj.data.LisBarCodeFormVo_LisBarCodeForm_Id) {
								me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode = barcode;
								me.table.reload({
									data: me.config.LIST_DATA || []
								});
							}
						}
					}
				}
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
	//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		$("#" + me.tableId).find('.layui-table-main tr[data-index="0"]');
	};
	//显示数据
	Class.prototype.onShowData= function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			showType = values.showType;
		
		if(showType == 'on'){//卡片方式显示
			me.onShowCards();
		}else{//列表方式显示
			me.onShowTable();
		}
		//如果存在
		if (ConfirmOrPrintAfterBarCode.length > 0) {
			me.ConfirmPrintAfterDispose([], ConfirmOrPrintAfterBarCode, data.elem.checked);
		}
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
		for (var i in list) {
			if (list[i].LisBarCodeFormVo_LisBarCodeForm_IsPrep == "1") {
				$("#" + me.config.domId + "_prefabricate_" + list[i].LisBarCodeFormVo_LisBarCodeForm_Id).html("预制");
				$("#" + me.config.domId + "_prefabricate_" + list[i].LisBarCodeFormVo_LisBarCodeForm_Id).removeClass("layui-hide");
			}
			if (list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == "" || (list[i].LisBarCodeFormVo_LisBarCodeForm_IsPrep == "1" && list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == "")) {
				if ($("#" + me.config.domId + "-" + list[i].LisBarCodeFormVo_LisBarCodeForm_Id + "-entering-prebarcode-div").hasClass("layui-hide")) {
					$("#" + me.config.domId + "-" + list[i].LisBarCodeFormVo_LisBarCodeForm_Id + "-entering-prebarcode-div").removeClass("layui-hide");
				}
			}
			else {
				if (!$("#" + me.config.domId + "-" + list[i].LisBarCodeFormVo_LisBarCodeForm_Id + "-entering-prebarcode-div").hasClass("layui-hide")) {
					$("#" + me.config.domId + "-" + list[i].LisBarCodeFormVo_LisBarCodeForm_Id + "-entering-prebarcode-div").addClass("layui-hide");
				}
			}
		}		
		if (html.length == 0) {
			$("#" + me.config.domId + '-cards').html('<div class="layui-none" style="text-align: center;line-height: 26px;padding: 30px 15px;color: #999;">无数据</div>');
		}
		form.render('checkbox');
	};
	//创建卡片
	Class.prototype.createCard = function(data){
		var me = this,
			html = CARD_TEMPLET.replace(/{colsType}/g, me.config.colsType).replace(/{domId}/g, me.config.domId);
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
				if (arr[0] == "LisBarCodeFormVo_LisBarCodeForm_BarCode") {
					cols.push({
						field: arr[0], title: arr[1], width: arr[2],
						hide: (arr[3] == 'show' ? false : true),
						edit:'text'
					});
				} else {
					cols.push({
						field: arr[0], title: arr[1], width: arr[2],
						hide: (arr[3] == 'show' ? false : true)
					});
				}
			}
			me.table = table.render({
				elem:'#' + me.config.domId + '-table',
				page: false,
				height: 'full-150',
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
	//HIS获取条码
	Class.prototype.getHisBarcode = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			barcodeType = values[me.config.domId + '-barcodeType'],
			barcode = values[me.config.domId + '-barcode'],
			filterDays = values[me.config.domId + '-filterDays'];		
		if(!barcodeType || !barcode){
			layer.msg("核收条件不能为空！",{icon:5});
		}else{
			var formValues = {};
			formValues[me.config.domId + '-printStatus'] = false;
			form.val(me.config.domId + '-form',formValues);
			
			var loadIndex = layer.load();//开启加载层
			uxutil.server.ajax({
				url:HIS_GET_LIST_URL,
				type:'post',
				data: JSON.stringify({
					nodetype: me.config.nodetype,
					receiveType:barcodeType,
					value:barcode,
					days:filterDays,
					fields:LIST_FIELDS.join(','),
					isPlanish:true
				})
			},function(data){
				layer.close(loadIndex);//关闭加载层
				if(data.success){
					me.afterDataLoad(data);
					me.ReloadTable();
					//me.onShowData();
				}else{
					me.config.LIST_DATA = [];
					me.config.Original_LIST_DATA = [];
					me.config.Table_Data = [];
					me.ReloadTable();
					//me.onShowData();
					layer.msg(data.msg,{icon:5});
				}
			},true);
		}
	}; 
	//重新加载表格
	Class.prototype.ReloadTable = function () {
		var me = this;
		table.reload("patient-index-table", {
			data: me.config.Table_Data
		});
	};
	//HIS获取、LIS获取、重新分组后数据处理
	Class.prototype.afterDataLoad = function(data){
		var me = this;
		var list = (data.value ||{}).list || [],
			error = [],
			result = [];
		for (var i in list) {
			//将无采样组和没有默认采样组的数据提示出来
			if (list[i].LisBarCodeFormVo_SampleGroupingType == 2 || list[i].LisBarCodeFormVo_SampleGroupingType == 3)
			{
				var errorinfo = "姓名：" + list[i].LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName + "。项目ID：" + list[i].LisBarCodeFormVo_ItemId + "。项目名称：" + list[i].LisBarCodeFormVo_LisBarCodeForm_ParItemCName;
				error.push(errorinfo);
			} else
			{
				result.push(list[i]);
			}
		}
		if (error.length > 0) {
			/*layer.open({
				title: '以下项目无采样组或无默认采样组'
				, content: error.join('<BR>')
			}); */
			uxbase.MSG.onConfirm(error.join('<BR>'), { title: '以下项目无采样组或无默认采样组', enter: true },
				function (index) {
					layer.close(index);//执行完后关闭
				},
				function (index) {
					layer.close(index);//执行完后关闭
				}
			);
		}
		
		me.config.TheInitial_LIST_DATA = result;
		me.config.TheInitial_Original_LIST_DATA = JSON.parse(JSON.stringify(result));
		var arr = [];
		for (var i = me.config.TheInitial_LIST_DATA.length - 1; i >= 0; i--) {
			var entity = me.config.TheInitial_LIST_DATA[i];
			if (arr.length == 0) {
				var obj = {};
				obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName;
				obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed;
				obj["LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo"] = entity.LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo;
				arr.push(obj);
			} else
			{
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
		//显示条码信息后打印条码
		var isautoprint = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0053');
		//界面是否控制自动打印
		var ZDDY = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0054');
		if (me.config.TheInitial_LIST_DATA.length > 0 && (ZDDY == "1" && autoPrintButtonStatus) || (ZDDY != "1" && isautoprint == "1")) {
			var barcodes = [];
			for (var i = 0; i < me.config.TheInitial_LIST_DATA.length; i++) {
				barcodes.push(me.config.TheInitial_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
			}
			me.config.LIST_DATA = me.config.TheInitial_LIST_DATA;
			me.config.Original_LIST_DATA = JSON.parse(JSON.stringify(me.config.TheInitial_LIST_DATA));
			me.onBarcodePrint(barcodes, 1);
		}
		
	};
	//LIS获取条码
	Class.prototype.getLisBarcode = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			barcodeType = values[me.config.domId + '-barcodeType'],
			barcode = values[me.config.domId + '-barcode'],
			filterDays = values[me.config.domId + '-filterDays'];
			
		if(!barcodeType || !barcode){
			layer.msg("核收条件不能为空！",{icon:5});
		}else{
			var formValues = {};
			formValues[me.config.domId + '-printStatus'] = false;
			form.val(me.config.domId + '-form',formValues);
			
			//var HistoryInfo = PreSampleBarcodeBasicHostTypeInstance.getHistoryInfo();
			var loadIndex = layer.load();//开启加载层
			uxutil.server.ajax({
				url:LIS_GET_LIST_URL,
				type:'post',
				data:JSON.stringify({
					nodetype: me.config.nodetype,
					receiveType:barcodeType,
					value:barcode,
					days:filterDays,
					fields:LIST_FIELDS.join(','),
					isPlanish:true
				})
			},function(data){
				layer.close(loadIndex);//关闭加载层
				if (data.success) {
					me.afterDataLoad(data);
					me.ReloadTable();
					//me.onShowData();
				} else {
					me.config.LIST_DATA = [];
					me.config.Original_LIST_DATA = [];
					me.config.Table_Data = [];
					me.ReloadTable();
					//me.onShowData();
					layer.msg(data.msg, { icon: 5 });
				}
			},true);
		}
	};
	//样本确认
	Class.prototype.onCheckSimple = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();
		if(barcodes.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			//var HistoryInfo = PreSampleBarcodeBasicHostTypeInstance.getHistoryInfo();
			var isrefresh = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0025');
			var loadIndex = layer.load();//开启加载层
			uxutil.server.ajax({
				url:CHECK_URL,
				type:'post',
				data:JSON.stringify({
					nodetype: me.config.nodetype,
					barcodes:barcodes.join(',')
				})
			},function(data){
				layer.close(loadIndex);//关闭加载层
				if(data.success){
					var BarCodeConfirmError = "";
					var BarCodeConfirmSuccess = [];
					var BarCodeConfirmResult = data.value.split(',');
					for (var i = 0;i < BarCodeConfirmResult.length;i++) {
						var errorbarcode = BarCodeConfirmResult[i].split(':');
						if (errorbarcode[1] == "false") {
							if (BarCodeConfirmError == "") {
								BarCodeConfirmError = errorbarcode[0];
							} else {
								BarCodeConfirmError += "," + errorbarcode[0];
							}
						} else {
							BarCodeConfirmSuccess.push(errorbarcode[0]);
						}
					}
					if(BarCodeConfirmError != ""){
						//layer.alert("以下条码确认失败,请检查样本状态：" + BarCodeConfirmError);
						uxbase.MSG.onConfirm(BarCodeConfirmError, { title: '以下条码确认失败,请检查样本状态', enter: true },
							function (index) {
								layer.close(index);//执行完后关闭
							},
							function (index) {
								layer.close(index);//执行完后关闭
							}
						);
					}
					//判断确认后是否刷新
					if(isrefresh == '1'){
						for (var i = me.config.LIST_DATA.length - 1; i >= 0; i--) {
							for (var a = 0; a < BarCodeConfirmSuccess.length; a++) {
								if (me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == BarCodeConfirmSuccess[a]) {
									me.config.LIST_DATA.splice(i,1);
									me.config.Original_LIST_DATA.splice(i, 1);
								}
							}							
						}
						me.onShowData();
					}else{
						me.ConfirmPrintAfterDispose(barcodes,BarCodeConfirmResult);
					}			
				}else{
					layer.msg(data.msg,{icon:5});
				}
			},true);
		}
	};
	//条码打印
	Class.prototype.onBarcodePrint = function(printbarcodes,isautoprint){
		var me = this,
			barcodes = me.getCheckedBarcodes();
		if (printbarcodes && printbarcodes.length > 0) {
			barcodes = printbarcodes;
		}
		if (barcodes.length == 0) {
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			var list = [];
			//指定打印机
			var printer = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0052');
			//是否直接打印
			var ZJDY = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0055');
			//是否发送到打包机
			var DBJ = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0034');
			//是否校验医嘱执行时间
			var verifyOrderExecTime = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0056');
			//确认或打印后是否刷新列表
			var isrefresh = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0025');
			//是否使用叫号系统
			var isUseQueuingMachine = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0036');
			//叫号系统病人类型选择
			var JHXTBRLXXZ = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0041');
			//打印条码时是否打印取单凭证
			var para0057 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0057');
			//不打印预制标签
			var para0044 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0044');
			//根据条码查找条码所属的全部数据信息
			for (var i in barcodes) {
				for(var j in me.config.LIST_DATA){
					if (me.config.LIST_DATA[j].LisBarCodeFormVo_LisBarCodeForm_BarCode == barcodes[i]) {
						if (para0044 == "1") {
							if (me.config.LIST_DATA[j].LisBarCodeFormVo_LisBarCodeForm_IsPrep != "1") {
								list.push(me.config.LIST_DATA[j]);
							}
						}
						else
						{
							list.push(me.config.LIST_DATA[j]);
						}
					}
				}
			}
			if (list.length < 1) {
				layer.msg("没有可打印的条码标签！", { icon: 5 });
				return;
			}
			//叫号系统排队号生成
			if (isUseQueuingMachine == "1") {
				var patientType = "1";
				if (JHXTBRLXXZ) {
					if ($("#patientType").prop("checked")) {
						patientType = "2";
					} else {
						patientType = "1";
					}
				}
				uxutil.server.ajax({
					url: CreatEqueuingMachineInfo_URL,
					type: 'post',
					data: JSON.stringify({
						nodetype: me.config.nodetype,
						barcode: barcodes.join(","),
						patientType: patientType
					})
				}, function (data) {
					if (data.success) {
						
					} else {
						layer.msg(data.msg, { icon: 5 });
						return;
					}
				}, true);
			}
			//判断是直接打印条码还是发送到打包机
			if ((ZJDY == "1" && DBJ == "1" && directPrintButtonStatus) || DBJ != "1") {
				var zdyPrintCount = form.val(me.config.domId + '-form')[me.config.domId + '-printCount'];
				var notArriveTimeBarCode = [];
				for (var i = 0; i < list.length; i++) {
					var isPrintBarCode  = true;
					//判断条码是否到医嘱执行时间
					if (verifyOrderExecTime == "1") {
						var presentTime = new Date();
						var execTime = new Date(Date.parse(list[i].LisBarCodeFormVo_LisBarCodeForm_OrderExecTime.replace("-", "/")));
						if (presentTime > execTime) {
							isPrintBarCode = false;
							notArriveTimeBarCode.push(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
						}
					}
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
							listarr.push(list[i]);
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
											IsAffirmBarCode :"0"
										})
									}, function (data) {
										//layer.close(loadIndex);//关闭加载层
										if (data.success) {
											//判断确认后是否刷新
											if (isrefresh == '1') {
												for (var i = me.config.LIST_DATA.length - 1; i >= 0; i--) {
													if (me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == item.LisBarCodeFormVo_LisBarCodeForm_BarCode) {
														me.config.LIST_DATA.splice(i, 1);
														me.config.Original_LIST_DATA.splice(i, 1);
													}
												}
												me.onShowData();
											} else {
												var arr = [];
												for (var i = 0; i < barcodes.length; i++) {
													arr.push(barcodes[i] + ":true");
												}
												me.ConfirmPrintAfterDispose(barcodes, arr, null, true);
											}
										} else {
											layer.msg(data.msg, { icon: 5 });
										}
									}, true);	
									if (para0057 == "1") {
										me.onVoucher(item.LisBarCodeFormVo_LisBarCodeForm_BarCode);
									}
								}
							});
						}
					}
				}
				if (notArriveTimeBarCode.length > 0) {
					//layer.alert("以下条码号未到医嘱指定执行时间不允许打印:" + notArriveTimeBarCode.join(","));
					uxbase.MSG.onConfirm(notArriveTimeBarCode.join(","), { title: '以下条码号未到医嘱指定执行时间不允许打印', enter: true },
						function (index) {
							layer.close(index);//执行完后关闭
						},
						function (index) {
							layer.close(index);//执行完后关闭
						}
					);
				}
			} else if (((ZJDY == "1" && !directPrintButtonStatus) || ZJDY != "1") && DBJ == "1" ){
				//发送到打包机
				//layer.alert("发送到打包机！");
				uxbase.MSG.onConfirm("发送到打包机!", { title: '提示', enter: true },
					function (index) {
						layer.close(index);//执行完后关闭
					},
					function (index) {
						layer.close(index);//执行完后关闭
					}
				);
			}
		}
	};
	//打印清单
	Class.prototype.onListPrint = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();
			
		if(barcodes.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{			
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
			
		if(barcodes.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			layer.confirm('确定要作废吗?',{icon: 3,title:'提示'}, function(index){
				layer.close(index);
				var loadIndex = layer.load();//开启加载层
				uxutil.server.ajax({
					url:CANCEL_URL,
					type:'post',
					data:JSON.stringify({
						nodetype: me.config.nodetype,
						barcodes:barcodes.join(',')
					})
				},function(data){
					layer.close(loadIndex);//关闭加载层
					if (data.success) {
						for (var i = me.config.LIST_DATA.length - 1; i >= 0; i--) {
							for (var a = 0; a < barcodes.length; a++) {
								if (me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == barcodes[a]) {
									me.config.LIST_DATA.splice(i, 1);
									me.config.Original_LIST_DATA.splice(i, 1);
								}
							}
						}
						me.onShowData();
					}else{
						layer.msg(data.msg,{icon:5});
					}
				},true);
			});
		}
	};
	//取单凭证
	Class.prototype.onVoucher = function(inbarcode){
		var me = this,
			BusinessTypeCode = 3,//前处理
			ModelTypeCode = 7,//样本签收_样本清单
			ModelTypeName = "样本条码_取单凭证";
		var barcodes = [];
		if (inbarcode){
			barcodes = [inbarcode];
		} else{
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
			title:'帮助文档',
			type:2,
			content:uxutil.path.LAYUI + '/views/help/pre/sample/barcode/menzhen/index.html?t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['95%','95%']
		});
	};
	//HIS医嘱信息
	Class.prototype.onHisOrderInfo = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();			
		if(barcodes.length == 0){
			layer.msg("请勾选样本条码进行操作！",{icon:5});
		}else{			
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
	//重新分组
	Class.prototype.reGroup = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			barcodeType = values[me.config.domId + '-barcodeType'],
			barcode = values[me.config.domId + '-barcode'],
			barcodes = me.getCheckedBarcodes(),
			filterDays = values[me.config.domId + '-filterDays'];
			
		if (!barcodeType || !barcode) {
			layer.msg("核收条件不能为空！", { icon: 5 });
		} else if (barcodes.length < 1) {
			layer.msg("请选择条码！", { icon: 5 });
		} else {
			var loadIndex = layer.load();//开启加载层
			uxutil.server.ajax({
				url:REGROUP_URL,
				type:'post',
				data: JSON.stringify({
					barcode: barcodes.join(','),
					nodetype: me.config.nodetype,
					receiveType:barcodeType,
					value:barcode,
					days:filterDays,
					fields:LIST_FIELDS.join(','),
					isPlanish:true
				})
			},function(data){
				layer.close(loadIndex);//关闭加载层
				if(data.success){
					me.afterDataLoad(data);
					me.onShowData();
				}else{
					me.config.LIST_DATA = [];
					me.config.Original_LIST_DATA = [];
					me.onShowData();
					layer.msg(data.msg,{icon:5});
				}
			},true);
		}
	};
	//获取勾选的条码数组
	Class.prototype.getCheckedBarcodes = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			showType = values.showType,
			barcodes = [];
		
		if(showType == 'on'){//卡片方式显示
			var checkValues = form.val(me.config.domId + '-form-cards');
			for(var i in checkValues){
				if (checkValues[i] === "on") {
					if (i.split('_').slice(-1) != null && i.split('_').slice(-1) != "") {
						barcodes.push(i.split('_').slice(-1));
					} else
					{
						layer.msg("请不要选择无条码号样本单！");
						return;
					}
				}
			}
//			var checkbox = $("input[checkbox='true']"),
//				len = checkbox.length;
//			
//			for(var i=0;i<len;i++){
//				var card = $(checkbox[i]);
//				if(card.attr("checked")){
//					barcodes.push(card.attr("name").split('_').slice(-1));
//				}
//			}
		}else{//列表方式显示
			var checkedList = table.checkStatus(me.config.domId + '-table').data;
			for (var i in checkedList) {
				if (checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode) {
					barcodes.push(checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
				} else
				{
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
		table.reload("patient-index-table", {data:[]});
		me.onShowData();
	};
	//确认或打印后不刷新处理
	Class.prototype.ConfirmPrintAfterDispose = function(barcodes,BarCodeConfirmResult,check,isprint){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			showType = values.showType;
		if(check != null && check != ""){
			if(check){
				showType = "on";
			}else{
				showType = "off";
			}
		}
		for (var i = 0;i < BarCodeConfirmResult.length;i++) {
			var errorbarcode = BarCodeConfirmResult[i].split(':');
			if(errorbarcode[1] == "true"){
				var isadd = true;
				$.each(ConfirmOrPrintAfterBarCode,function(i,item){
					if(item.split(':')[0] == errorbarcode[0]){
						isadd = false;
					}
				});
				if(isadd){
					ConfirmOrPrintAfterBarCode.push(BarCodeConfirmResult[i]);
				}
				if(showType == 'on'){//卡片方式显示
					if(isprint){
						$("#"+me.config.domId+"_isprint_"+errorbarcode[0]).html("已打印");
					}else{
						$("#"+me.config.domId+"_isprint_"+errorbarcode[0]).html("已确认");
					}
					$("#"+me.config.domId+"_isprint_"+errorbarcode[0]).removeClass("layui-hide");
				}else{//列表方式显示
					var trs = $("div[lay-id="+ me.config.domId +"-table] .layui-table-main table tr");
					$.each(trs,function(i,item){
						var td = $(item).find("td[data-field='LisBarCodeFormVo_LisBarCodeForm_BarCode']");
						var  barcode = $(td).find("div.layui-table-cell").html();
						if(barcode == errorbarcode[0]){
							$(item).css("background","#87CEFA");
						}
					});
				}
			}
		}	
	}
	//预制条码号绑定
	Class.prototype.UpdateBarCode = function(barcode,barcodeformid){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url: UpdateLisBarCodeFormBarCodeByBarCodeFormID_URL,
			type: 'post',
			data: JSON.stringify({
				nodetype: me.config.nodetype,
				barcode: barcode,
				barcodeformid: barcodeformid
			})
		}, function (data) {
			layer.close(loadIndex);//关闭加载层
			if (data.success) {
				for (var i = 0; i < me.config.LIST_DATA.length; i++) {
					if (me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id == barcodeformid) {
						me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode = barcode;
					}
					if (me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id == barcodeformid) {
						me.config.Original_LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode = barcode;
					}
				}
				layer.msg("绑定成功！");
				//绑定完后立即打印条码
				var para45 = PreSampleBarcodeBasicParamsInstance.get('Pre_OrderBarCode_DefaultPara_0045');
				if (para45 == "1") {
					me.onBarcodePrint([barcode]);
				}
				me.onShowData();
			} else {
				layer.msg(data.msg, { icon: 5 });
				for (var i = 0; i < me.config.LIST_DATA.length; i++) {
					if (me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_Id == barcodeformid) {
						me.config.LIST_DATA[i].LisBarCodeFormVo_LisBarCodeForm_BarCode = "";
					}
				}
			}
		}, true);
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
	//暴露接口
	exports(MOD_NAME,PreSampleBarcodeMenzhenIndex);
}); 