/**
   @Name：未确认条码
   @Author：GHX
   @version 2021-11-05
 */
layui.extend({
	uxutil: 'ux/util',
	uxtoolbar: 'ux/toolbar',
	PreSampleBarcodeMenzhenIndexNotConfirm: 'modules/pre/sample/barcode/model2/notConfirmBarCode',
	CommonHostType: 'modules/common/hosttype'
}).use(['uxutil', 'uxtoolbar', 'PreSampleBarcodeMenzhenIndexNotConfirm', 'CommonHostType'], function () {
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleBarcodeMenzhenIndex = layui.PreSampleBarcodeMenzhenIndexNotConfirm,
		CommonHostType = layui.CommonHostType;

	//站点类型实例
	var CommonHostTypeInstance = null;
	//样本条码实例
	var PreSampleBarcodeMenzhenIndexInstance = null;

	//选中的行
	var checkedTr = null;
	//选择的列表数据
	var checkedData = null;

	//初始化功能按钮栏
	function initToolbar() {
		//实例化功能按钮栏
		uxtoolbar.render({
			domId: 'toolbar',
			buttons: [
				{ type: 'barcodePrint', text: '条码打印(F5)', iconCls: 'layui-icon-print', buttonCls: '' },
				/*{ type: 'scanPrint', text: '扫码补打', iconCls: 'layui-icon-print', buttonCls: '' },*/
				{ type: 'listPreint', text: '打印清单', iconCls: 'layui-icon-print', buttonCls: '' },
				{ type: 'barcodeCancel', text: '条码作废', iconCls: 'layui-icon-refresh', buttonCls: '' },
				{ type: 'cancel', text: '<i class="iconfont">&#xe657;</i>&nbsp;撤销确认', iconCls: '', buttonCls: '' },
				{ type: 'voucher', text: '取单凭证', iconCls: 'layui-icon-file', buttonCls: '' },
				{ type: 'help', text: '帮助', iconCls: 'layui-icon-help', buttonCls: '' },
				{ type: 'hisOrderInfo', text: 'HIS医嘱信息', iconCls: 'layui-icon-form', buttonCls: '' },
				{ type: 'clearData', text: '清空数据', iconCls: 'layui-icon-delete', buttonCls: '' }
			],
			event: {
				barcodePrint: function () { PreSampleBarcodeMenzhenIndexInstance.onBarcodePrint(); },
				scanPrint: function () { PreSampleBarcodeMenzhenIndexInstance.onScanPrint(); },
				listPreint: function () { PreSampleBarcodeMenzhenIndexInstance.onListPrint(); },
				barcodeCancel: function () { PreSampleBarcodeMenzhenIndexInstance.onBarcodeCancel(); },
				cancel: function () { onRevoke(); },
				voucher: function () { PreSampleBarcodeMenzhenIndexInstance.onVoucher(); },
				help: function () { PreSampleBarcodeMenzhenIndexInstance.onHelp(); },
				hisOrderInfo: function () { PreSampleBarcodeMenzhenIndexInstance.onHisOrderInfo(); },
				clearData: function () { PreSampleBarcodeMenzhenIndexInstance.clear(); }
			}
		});
	};
	//撤销确认
	function onRevoke() {
		layer.open({
			title: '撤销确认',
			type: 2,
			content: uxutil.path.ROOT + '/ui/layui/views/pre/sample/barcode/revoke/form.html?nodetype=' + CommonHostTypeInstance.config.HostTypeID + '&t=' + new Date().getTime(),
			maxmin: false,
			toolbar: true,
			resize: true,
			area: ['400px', '270px'],
			success: function (layero, index) {
			}
		});
	};
	//初始化列表
	function initList() {
		//实例化条码列表
		PreSampleBarcodeMenzhenIndexInstance = PreSampleBarcodeMenzhenIndex.render({
			domId: 'barcode-index',
			nodetype: CommonHostTypeInstance.config.HostTypeID
		});
	};
	function init() {
		//初始化功能按钮栏
		initToolbar();
		//实例化站点类型
		CommonHostTypeInstance = CommonHostType.render({
			//站点类型上级功能栏ID，可以不设置站点类型下拉框ID属性，自动生成下拉框
			selectParentDomId: 'toolbar',
			//站点类型下拉框ID，如果已经设置站点类型上级功能栏ID，本参数可以不设置，自动生成下拉框
			selectDomId: '',
			//样本条码参数枚举名称
			paraTypeCode: 'Pre_OrderBarCode_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun: function () {
				//初始化列表
				initList();
			},
			//站点类型下拉框选择触发事件
			selectClickFun: function () {
				//清空列表数据
				$("#barcode-index").html("");
				$("#patient-index").html("");
				$("#patient-barcode-index").html("");
				initList();
			}
		});
	};

	//初始化
	init();
});