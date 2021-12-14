/**
 * @name：样本送达
 * @author：zhangda
 * @version 2020-08-27
 */
layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreSampleDeliveryIndex: 'modules/pre/sample/delivery/index',
	PreSampleDeliveryParams: 'modules/pre/sample/delivery/params',//参数
	CommonHostType: 'modules/common/hosttype'
}).use(['uxutil', 'dropdown', 'uxtoolbar', 'PreSampleDeliveryIndex', 'PreSampleDeliveryParams','CommonHostType'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleDeliveryIndex = layui.PreSampleDeliveryIndex,
		PreSampleDeliveryParams = layui.PreSampleDeliveryParams,
		CommonHostType = layui.CommonHostType,
		dropdown = layui.dropdown;

	//外部参数
	var PARAMS = uxutil.params.get(true);
	//模式类型
	var MODELTYPE = PARAMS.MODELTYPE;

	//站点类型实例
	var CommonHostTypeInstance = null;
	//功能参数实例
	var PreSampleDeliveryParamsInstance = null;
	//样本签收实例
	var PreSampleDeliveryIndexInstance = null;

	//初始化页面
	function initHtml(){
		$("#hosttype").hide();
		$("#content").show();
		//站点类型
		var nodetype = CommonHostTypeInstance.config.HostTypeID || null;
		if (!nodetype) return;

		//实例化功能按钮栏
		uxtoolbar.render({
			domId: 'toolbar',
			buttons: ['clear'],//, 'close'
			//按钮MAP
			buttonsMap: {
				clear: { type: 'clear', text: '清空', iconCls: 'layui-icon-delete', buttonCls: '' }
			},
			event: {
				clear: function () { onClearClick(); }
			}
		});

		PreSampleDeliveryParamsInstance = PreSampleDeliveryParams.render({ nodetype: nodetype});
		//初始化功能参数
		PreSampleDeliveryParamsInstance.init(function () {
			//实例化送达实例
			initPreSampleDeliveryIndex();
		});
		//初始化监听
		initListeners();
	};
	//实例化送达实例
	function initPreSampleDeliveryIndex() {
		$('#delivery-index').html('');
		//实例化送达
		PreSampleDeliveryIndexInstance = PreSampleDeliveryIndex.render({
			domId: 'delivery-index',
			modelType: MODELTYPE || 1,//模式
			nodetype: CommonHostTypeInstance.config.HostTypeID //站点类型
		});
	};
	//初始化站点下拉选框
	function initHostTypeDropdown() {
		var HistoryInfo = CommonHostTypeInstance.getHistoryInfo();

		//当前所在站点
		$("#toolbar-right") && $("#toolbar-right").remove();
		$("#toolbar").append(
			'<button class="layui-btn layui-btn-sm layui-btn-primary" style="float:right;" id="toolbar-right">' +
			(HistoryInfo ? HistoryInfo.HostTypeName : '请选择站点') +
			'<i class="layui-icon layui-icon-down layui-font-12"></i>' +
			'</button>'
		);

		var data = [];
		for (var i in HostTypeList) {
			if (HistoryInfo && HistoryInfo.HostTypeID == HostTypeList[i].HostTypeID) continue;
			data.push({
				title: HostTypeList[i].HostTypeName,
				id: HostTypeList[i].HostTypeID,
				href: '#'
			});
		}

		dropdown.render({
			elem: '#toolbar-right',
			data: data,
			click: function (obj) {
				CommonHostTypeInstance.insertHistoryInfo({
					HostTypeID: obj.id,
					HostTypeName: obj.title
				});
				initHostTypeDropdown();
				//实例化送达实例
				initPreSampleDeliveryIndex(obj.id);
			}
		});
	};
	//初始化监听
	function initListeners(){
		
	};
	//清空
	function onClearClick(){
		PreSampleDeliveryIndexInstance.onClearClick();
	};
	//初始化
	function init() {
		//实例化站点类型
		CommonHostTypeInstance = CommonHostType.render({
			//站点类型上级功能栏ID，可以不设置站点类型下拉框ID属性，自动生成下拉框
			selectParentDomId: 'toolbar',
			//站点类型下拉框ID，如果已经设置站点类型上级功能栏ID，本参数可以不设置，自动生成下拉框
			selectDomId: '',
			paraTypeCode: 'Pre_OrderExchangeDelivery_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun: function () {
				//初始化
				initHtml();
			},
			//站点类型下拉框选择触发事件
			selectClickFun: function () {
				//清空列表数据
				initPreSampleDeliveryIndex();
			}
		});
	};
	
	//初始化
	init();
});