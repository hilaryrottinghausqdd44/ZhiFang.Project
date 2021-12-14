layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreSampleTranRuleIndex:'modules/pre/sample/tranrule/index',
	CommonHostType:'modules/common/hosttype'
}).use(['uxutil','uxtoolbar','PreSampleTranRuleIndex','CommonHostType'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleTranRuleIndex = layui.PreSampleTranRuleIndex,
		CommonHostType = layui.CommonHostType;
		
	//站点类型实例
	var CommonHostTypeInstance = null;
	//样本条码实例
	var PreSampleTranRuleIndexInstance = null;
	//初始化功能按钮栏
	function initToolbar(){
	  	//实例化功能按钮栏
		uxtoolbar.render({
			domId:'toolbar',
			buttons:[
				{type:'clear',text:'清空',iconCls:'layui-icon-delete',buttonCls:''},
				{type:'accept',text:'样本分发',iconCls:'layui-icon-ok-circle',buttonCls:''},
				{type:'revoke',text:'<i class="iconfont">&#xe657;</i>&nbsp;分发取消',iconCls:'',buttonCls:''},
				{type:'acceptSign',text:'样本签收',iconCls:'layui-icon-ok-circle',buttonCls:''},
				{type:'listPreint',text:'清单打印',iconCls:'layui-icon-print',buttonCls:''},
				{type:'listPrintRoam',text:'流转单',iconCls:'layui-icon-print',buttonCls:''}
			],
			event:{
				acceptSign:function(){PreSampleTranRuleIndexInstance.onSignClick()},
				revoke:function(){PreSampleTranRuleIndexInstance.revoke();},
				accept:function(){PreSampleTranRuleIndexInstance.onAccept();},
				clear:function(){PreSampleTranRuleIndexInstance.clearData();},
				listPreint:function(){PreSampleTranRuleIndexInstance.onListPrint();},
				listPrintRoam:function(){PreSampleTranRuleIndexInstance.onPrintRoam();}
			}
		});
	};
     //初始化实例化
	function initPreSampleTranRuleIndex(){
		$('#tranrule-index').html('');
		//实例化
		PreSampleTranRuleIndexInstance = PreSampleTranRuleIndex.render({
			domId:'tranrule-index',
			nodetype:CommonHostTypeInstance.config.HostTypeID//站点类型
		});
	};
	//初始化列表
	function initHtml(){
		$('#tranrule-index').html('');
		$("#hosttype").hide();
		$("#content").show();
	    //实例化
	    initPreSampleTranRuleIndex();
	};
	function init(){
		//初始化功能按钮栏
		initToolbar();
		//实例化站点类型
		CommonHostTypeInstance = CommonHostType.render({
			//站点类型上级功能栏ID，可以不设置站点类型下拉框ID属性，自动生成下拉框
			selectParentDomId:'toolbar',
			//站点类型下拉框ID，如果已经设置站点类型上级功能栏ID，本参数可以不设置，自动生成下拉框
			selectDomId:'',
			paraTypeCode:'Pre_OrderDispense_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun:function(){
				//初始化
				initHtml();
			},
 			//站点类型下拉框选择触发事件
			selectClickFun:function(){
				//清空列表数据
				initPreSampleTranRuleIndex();
			}
		});
	};
	//初始化
	init();
});