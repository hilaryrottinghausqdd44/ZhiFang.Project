layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreSampleInspectBasicParams:'modules/pre/sample/inspect/params',
	PreSampleInspectIndex:'modules/pre/sample/inspect/index',
	CommonHostType:'modules/common/hosttype'
}).use(['uxutil','uxtoolbar','PreSampleInspectIndex','CommonHostType','PreSampleInspectBasicParams'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleInspectIndex = layui.PreSampleInspectIndex,
		PreSampleInspectBasicParams = layui.PreSampleInspectBasicParams,
		CommonHostType = layui.CommonHostType;
		
	//站点类型实例
	var CommonHostTypeInstance = null;
	//样本条码实例
	var PreSampleInspectIndexInstance = null;
	//参数实例
	var PreSampleInspectBasicParamsInstance = null;
	var uxtoolbarInstance = null;
	//外部参数
	var PARAMS = uxutil.params.get(true);
    //模式 1(MODELTYPE=1)-单个确认,模式 2(MODELTYPE=2)-批量确认,模式 3(MODELTYPE=3)-匹配并批量确认,模式 4(MODELTYPE=4)-批量查询确认
	var MODELTYPE = PARAMS.MODELTYPE;
	//初始化功能按钮栏
	function initToolbar(){
	  	//是否显示打印清单按钮
		var showlistPreint = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0005');
        //是否撤销送检按钮
		var showrevokebtn = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0019');
		var buttons = [{type:'clear',text:'清空',id:'clear', iconCls:'layui-icon-delete',buttonCls:'',hide:true}];
		//非模式1，都有确认送检按钮
		if(MODELTYPE!='1')buttons.splice(1, 0,{type:'accept',text:'确认送检',iconCls:'layui-icon-ok-circle',buttonCls:''});
		//显示打印清单按钮
		if(showlistPreint=='1')buttons.splice(buttons.length, 0,{type:'listPreint',text:'打印清单',iconCls:'layui-icon-print',buttonCls:''});
		//显示打印清单按钮
		if(showrevokebtn=='1')buttons.splice(buttons.length, 0,{type:'revoke',text:'<i class="iconfont">&#xe657;</i>&nbsp;撤销送检',iconCls:'',buttonCls:''});
		//实例化功能按钮栏
		uxtoolbar.render({
			domId:'toolbar',
			buttons:buttons,
			event:{
				revoke:function(){onRevoke()},
				accept:function(){PreSampleInspectIndexInstance.onAccept();},
				clear:function(){PreSampleInspectIndexInstance.clearData();},
				listPreint:function(){PreSampleInspectIndexInstance.onListPrint();}
			}
		});
	};
	//撤销采集
	function onRevoke(){
		layer.open({
			title:'撤销送检',
			type:2,
			content:uxutil.path.ROOT + '/ui/layui/views/pre/sample/inspect/revoke/form.html?nodetype='+CommonHostTypeInstance.config.HostTypeID+'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:true,
			area:['400px','270px']
		});
	};
     //初始化实例化
	function initPreSampleInspectIndex(){
        //清空功能栏按钮和inspect-index
        $('#toolbar .layui-btn-group').html('');
        $('#toolbar .layui-btn-group').remove();
		$('#inspect-index').html('');
		
		//初始化功能按钮栏
		initToolbar();
		//实例化
		PreSampleInspectIndexInstance = PreSampleInspectIndex.render({
			domId:'inspect-index',
			MODELTYPE:MODELTYPE,//模式
			nodetype:CommonHostTypeInstance.config.HostTypeID//站点类型
		});
	};
	//初始化列表
	function initHtml(){
		$('#inspect-index').html('');
		$("#hosttype").hide();
		$("#content").show();
	  //参数功能
	    PreSampleInspectBasicParamsInstance = PreSampleInspectBasicParams.render({nodetype:CommonHostTypeInstance.config.HostTypeID});
        PreSampleInspectBasicParamsInstance.init(function(){
			//实例化样本采集
			initPreSampleInspectIndex();
		});
	};
	function init(){
		//实例化站点类型
		CommonHostTypeInstance = CommonHostType.render({
			//站点类型上级功能栏ID，可以不设置站点类型下拉框ID属性，自动生成下拉框
			selectParentDomId:'toolbar',
			//站点类型下拉框ID，如果已经设置站点类型上级功能栏ID，本参数可以不设置，自动生成下拉框
			selectDomId:'',
			paraTypeCode:'Pre_OrderExchangeInspect_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun:function(){
				//初始化
				initHtml();
			},
 			//站点类型下拉框选择触发事件
			selectClickFun:function(){
				//清空列表数据
				initPreSampleInspectIndex();
			}
		});
	};
	//初始化
	init();
});