layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreSampleRefuseIndex:'modules/pre/sample/refuse/index',
	CommonHostType:'modules/common/hosttype'
}).use(['uxutil','uxtoolbar','PreSampleRefuseIndex','CommonHostType'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleRefuseIndex = layui.PreSampleRefuseIndex,
		CommonHostType = layui.CommonHostType;
		
	//站点类型实例
	var CommonHostTypeInstance = null;
	//样本条码实例
	var PreSampleRefuseIndexInstance = null;
	
	//初始化功能按钮栏
	function initToolbar(){
		//实例化功能按钮栏
		uxtoolbar.render({
			domId:'toolbar',
			buttons:[
				{type:'clear',text:'清空',iconCls:'layui-icon-delete',buttonCls:''},
				{type:'accept',text:'确认拒收',iconCls:'layui-icon-ok-circle',buttonCls:''},
				{type:'listPreint',text:'拒收清单',iconCls:'layui-icon-print',buttonCls:''},
				{type:'phrases',text:'短语维护',iconCls:'layui-icon-set-fill',buttonCls:''}
			],
			event:{
				phrases:function(){onPhrases()},
				accept:function(){PreSampleRefuseIndexInstance.onAccept();},
				clear:function(){PreSampleRefuseIndexInstance.clearData();},
				listPreint:function(){PreSampleRefuseIndexInstance.onListPrint();}
			}
		});
	};

	 //短语维护
	function onPhrases(){
		layer.open({
			title:'短语维护',
			type:2,
			content:uxutil.path.LAYUI+'/app/dic/phraseswatch/index.html?t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['95%','95%'],
			end: function () {
				PreSampleRefuseIndexInstance.initPhrasesWatchHtml();
			}
		});
	};
     //初始化实例化
	function initPreSampleRefuseIndex(){
		$('#refuse-index').html('');
		//实例化样本采集模式1
		PreSampleRefuseIndexInstance = PreSampleRefuseIndex.render({
			domId:'refuse-index',
			nodetype:CommonHostTypeInstance.config.HostTypeID//站点类型
		});
	};
	//初始化列表
	function initHtml(){
		$('#refuse-index').html('');
		$("#hosttype").hide();
		$("#content").show();
		//实例化样本采集
		initPreSampleRefuseIndex();
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
			paraTypeCode:'Pre_OrderRejection_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun:function(){
				//初始化
				initHtml();
			},
			//站点类型下拉框选择触发事件
			selectClickFun:function(){
				//清空列表数据
				initPreSampleRefuseIndex();
			}
		});
	};
	//初始化
	init();
});