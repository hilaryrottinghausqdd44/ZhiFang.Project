/**
 * @name：modules/pre/sample/tranrule/index 样本单列表与项目分发信息
 * @author：liangyl
 * @version 2020-10-13
 */
layui.extend({
	InfoList:'modules/pre/sample/tranrule/info',//医嘱信息	
	TranRuleHostSectionList:'modules/pre/sample/tranrule/section' //分发规则对应小组
}).define(['uxutil','element','InfoList','TranRuleHostSectionList'],function(exports){
	"use strict";
	
	var $ = layui.$,
		element = layui.element,
		uxutil = layui.uxutil,
		table = layui.table,
		InfoList = layui.InfoList,
		TranRuleHostSectionList = layui.TranRuleHostSectionList,
		MOD_NAME = 'PreSampleContentTabIndex';
	//模块DOM
	var MOD_DOM = [
	    '<div class="layui-row myStyle" style="margin:0px;padding:0px;">',
	     '<div class="layui-tab layui-tab-brief my-tab-brief" lay-filter="{domId}-tab" id="{domId}-tab"  style="margin:0px;padding:0px;">',
		  '<ul class="layui-tab-title">',
		    '<li class="layui-this"><i class="iconfont">&#xe673;</i>&nbsp;医嘱信息</li>',
		    '<li><i class="iconfont">&#xe654;</i>&nbsp;分发小组</li>',
		  '</ul>',
		 '<div class="layui-tab-content">',
		   '<div class="layui-tab-item layui-show">',
		     '<div id="{domId}-Info"></div>',
		   '</div>',
           '<div class="layui-tab-item">',
           	  '<div id="{domId}-TranRuleHostSection"></div>',
           '</div>',
		 '</div>',
		'</div>',
		'<style>',
		  '.myStyle .layui-card-body{ padding:2px!important; }',
          '.myStyle .layui-tab-content{ padding:0!important; }',
	      '.myStyle .layui-tab-title { height:24px; }',
	      '.myStyle .layui-tab-title>li{ line-height:24px;font-size:12px;padding:0 8px;min-width:50px; }',
	      '.myStyle .layui-tab-title>li i{ font-size:12px; }',
	      '.myStyle .layui-tab-title .layui-this:after{ height:26px!important; }',
	      '.myStyle .layui-tab-title>li .layui-icon-close{ margin-left:5px;border-radius:10px;font-size:16px; }',
	      '.myStyle .layui-tab-title > li .layui-icon-close:hover {background-color: red;color: #fff; }',
		'</style>'
		
	].join('');
	
	//样本单实例
	var InfoListInstance = null;
	//样本单项目列表实例
	var TranRuleHostSectionListInstance = null;
	//已加载小组
	var IsLoad = false;
	var Height_Tab =0;
	var PreSampleContentTabIndex = {
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:null,
			ordercols:[],//医嘱信息字段显示
			PrinterName:null
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleContentTabIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		Height_Tab = me.config.height - 26;//页签高度 
		//样本信息列表实例 
		InfoListInstance = InfoList.render({
			domId: me.config.domId+'-Info',
			height:Height_Tab+'px',
			ordercols:me.config.ordercols,
			nodetype:me.config.nodetype //站点类型
		});
		//样本单项目信息
		TranRuleHostSectionListInstance = TranRuleHostSectionList.render({
			domId: me.config.domId+'-TranRuleHostSection',
			height:Height_Tab+'px',
			nodetype:me.config.nodetype
		});
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		  //一些事件触发
	    element.on('tab('+me.config.domId+'-tab)', function(data){
	    	me.changeSize(Height_Tab);
		});
	};
	//查询样本单列表字段
	Class.prototype.getFields = function(){
		return InfoListInstance.getFields();
	};
    Class.prototype.changeSize= function(height){
    	var me = this;
    	var height1 = height-5;
		$('#'+me.config.domId+'-TranRuleHostSection').css('height',height1+'px');
		TranRuleHostSectionListInstance.changeSize(height1);
    	$('#'+me.config.domId+'-Info').css('height',height1+'px');
    	InfoListInstance.changeSize(height1);
    };
    //列表数据清空
    Class.prototype.clearData= function(){
    	var me = this;
    	InfoListInstance.clearData();
    };
    //样本单数据重载
    Class.prototype.loadData = function(data,height){
    	var me = this;
    	var list = [];
    	for(var i in me.config.ordercols){
    		var arr = me.config.ordercols[i].split('&');
            list.push({
            	field:arr[0],
            	name:arr[1],
            	value:data[arr[0]]
            })
        }
    	height = height-55;
    	InfoListInstance.loadData(list,height);
    };
	//核心入口
	PreSampleContentTabIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		me.initHtml();
		me.initListeners();
		return me;
	};
	//暴露接口
	exports(MOD_NAME,PreSampleContentTabIndex);
});