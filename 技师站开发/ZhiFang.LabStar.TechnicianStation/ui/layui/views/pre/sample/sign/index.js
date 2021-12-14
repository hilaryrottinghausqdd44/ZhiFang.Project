/**
 * @name：样本签收
 * @author：zhangda
 * @version 2020-08-27
 */
layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreSampleSignIndex: 'modules/pre/sample/sign/index',
	PreSampleSignParams: 'modules/pre/sample/sign/params',//参数
	CommonHostType: 'modules/common/hosttype'
}).use(['uxutil', 'dropdown', 'uxtoolbar', 'PreSampleSignIndex', 'PreSampleSignParams','CommonHostType'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleSignIndex = layui.PreSampleSignIndex,
		PreSampleSignParams = layui.PreSampleSignParams,
		CommonHostType = layui.CommonHostType,
		dropdown = layui.dropdown;

	//外部参数
	var PARAMS = uxutil.params.get(true);
	//模式类型
	var MODELTYPE = PARAMS.MODELTYPE;

	//站点类型实例
	var CommonHostTypeInstance = null;
	//功能参数实例
	var PreSampleSignParamsInstance = null;
	//样本签收实例
	var PreSampleSignIndexInstance = null;
	
	
	//初始化页面
	function initHtml() {
		$("#hosttype").hide();
		$("#content").show();
		var nodetype = CommonHostTypeInstance.config.HostTypeID || null;
		if (!nodetype) return;
		
		//功能参数
		PreSampleSignParamsInstance = PreSampleSignParams.render({ nodetype: nodetype });

		var buttons = ['clear', 'sign', 'signList', 'cancelSign'];
		if (MODELTYPE == 4 && PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0042') == 1) buttons = ['clear', 'refresh', 'sign', 'signList', 'cancelSign'];

		//实例化功能按钮栏
		uxtoolbar.render({
			domId: 'toolbar',
			buttons: buttons,//, 'close'
			//按钮MAP
			buttonsMap: {
				clear: { type: 'clear', text: '清空', iconCls: 'layui-icon-delete', buttonCls: '' },
				refresh: { type: 'refresh', text: '刷新', iconCls: 'layui-icon-refresh', buttonCls: '' },
				sign: { type: 'sign', text: '样本签收', iconCls: 'layui-icon-ok', buttonCls: '' },
				signList: { type: 'signList', text: '签收清单', iconCls: 'layui-icon-form', buttonCls: '' },
				cancelSign: { type: 'signList', text: '取消签收', iconCls: 'layui-icon-refresh-1', buttonCls: '' }
			},
			event: {
				clear: function () { onClearClick(); },
				refresh: function () { onSearch(); },
				sign: function () { onSignClick(); },
				signList: function () { onSignListClick(); },
				cancelSign: function () { onCancelSign(); }
			}
		});
		//初始化功能参数
		PreSampleSignParamsInstance.init(function () {
			//实例化签收实例
			initPreSampleSignIndex();
		});
		//初始化监听
		initListeners();
	};
	//实例化签收实例
	function initPreSampleSignIndex() {
		$('#sign-index').html('');
		//实例化签收
		PreSampleSignIndexInstance = PreSampleSignIndex.render({
			domId: 'sign-index',
			modelType: MODELTYPE || 1,//模式
			nodetype: CommonHostTypeInstance.config.HostTypeID//站点类型
		});
	};
	//初始化监听
	function initListeners(){
		
	};
	//清空
	function onClearClick(){
		PreSampleSignIndexInstance.onClearClick();
	};
	//刷新
	function onSearch(){
		PreSampleSignIndexInstance.onRefreshClick();
	};
	//样本签收
	function onSignClick() {
		PreSampleSignIndexInstance.onSignClick();
	};
	//签收清单
	function onSignListClick() {
		PreSampleSignIndexInstance.onSignListClick();
	};
	//取消签收
	function onCancelSign() {
		PreSampleSignIndexInstance.onCancelSignClick();
	};
	
	//初始化
	function init() {
		//实例化站点类型
		CommonHostTypeInstance = CommonHostType.render({
			//站点类型上级功能栏ID，可以不设置站点类型下拉框ID属性，自动生成下拉框
			selectParentDomId: 'toolbar',
			//站点类型下拉框ID，如果已经设置站点类型上级功能栏ID，本参数可以不设置，自动生成下拉框
			selectDomId: '',
			paraTypeCode: 'Pre_OrderSignFor_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun: function () {
				//初始化
				initHtml();
			},
			//站点类型下拉框选择触发事件
			selectClickFun: function () {
				//清空列表数据
				initPreSampleSignIndex();
			}
		});
	};
	
	//初始化
	init();
});