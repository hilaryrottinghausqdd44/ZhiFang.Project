layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreSampleGetherIndex:'modules/pre/sample/gether/model1/index',
	CommonHostType:'modules/common/hosttype'
}).use(['uxutil','uxtoolbar','PreSampleGetherIndex','CommonHostType'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar,
		PreSampleGetherIndex = layui.PreSampleGetherIndex,
		CommonHostType = layui.CommonHostType;
		
	//站点类型实例
	var CommonHostTypeInstance = null;
	//样本条码实例
	var PreSampleGetherIndexInstance = null;
	
	//初始化功能按钮栏
	function initToolbar(){
	  	//实例化功能按钮栏
		uxtoolbar.render({
			domId:'toolbar',
			buttons:[
				{type:'clear',text:'清空',iconCls:'layui-icon-delete',buttonCls:''},
				{type:'packPreint',text:'打包清单',iconCls:'layui-icon-print',buttonCls:''},
				{type:'listPreint',text:'打印清单',iconCls:'layui-icon-print',buttonCls:''},
				{type:'revoke',text:'<i class="iconfont">&#xe657;</i>&nbsp;撤销采集',iconCls:'',buttonCls:''}
			],
			event:{
				clear:function(){PreSampleGetherIndexInstance.clearData();},
				packPreint:function(){PreSampleGetherIndexInstance.packNoByBarCode();},
				listPreint:function(){PreSampleGetherIndexInstance.onListPrint();},
				revoke:function(){onRevoke()}
			}
		});
	};
     //初始化实例化
	function initPreSampleGetherIndex(){
		$('#gether1-index').html('');
		//实例化
		PreSampleGetherIndexInstance = PreSampleGetherIndex.render({
			domId:'gether1-index',
			nodetype:CommonHostTypeInstance.config.HostTypeID//站点类型
		});
	};
	//初始化列表
	function initHtml(){
		$('#gether1-index').html('');
		$("#hosttype").hide();
		$("#content").show();
	    //实例化
	    initPreSampleGetherIndex();
	};
	//撤销采集
	function onRevoke(){
		layer.open({
			title:'撤销采集',
			type:2,
			content:uxutil.path.ROOT + '/ui/layui/views/pre/sample/gether/revoke/form.html?nodetype='+CommonHostTypeInstance.config.HostTypeID+'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:true,
			area:['400px','270px'],
			success: function (layero, index) {
//				var iframe = $(layero).find("iframe")[0].contentWindow;
//				iframe.fireEventSaveSuccessFun(function () {
//					layer.close(index);
//					onSearch();
//				})
			}
		});
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
			paraTypeCode:'Pre_OrderGether_DefaultPara',
			//站点类型列表点击触发事件
			listClickFun:function(){
				//初始化
				initHtml();
			},
 			//站点类型下拉框选择触发事件
			selectClickFun:function(){
				//清空列表数据
				initPreSampleGetherIndex();
			}
		});
	};
	//初始化
	init();
});