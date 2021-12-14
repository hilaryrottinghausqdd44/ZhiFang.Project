/**
 * @name：modules/common/select/sicktype 就诊类型下拉框
 * @author：Jcall
 * @version 2020-06-30
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		MOD_NAME = 'CommonSelectSickType';
	
	//获取机构身份列表服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL";
	
	//就诊类型下拉框
	var CommonSelectSickType = {
		//对外参数
		config:{
			domId:null,
			defaultName:'请选择',//默认文字显示
			isFromRender:true,//加载完数据后是否立即表单渲染
			done:function(instance){}//加载完数据并渲染后回调
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,CommonSelectSickType.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(list){
		var me = this,
			len = list.length,
			htmls = [];
			
		if(me.config.defaultName){
			htmls.push('<option value="">' + me.config.defaultName + '</option>');
		}
			
		for(var i=0;i<len;i++){
			htmls.push('<option value="' + list[i].Id + '">' + list[i].CName + '</option>');
		}
		$("#" + me.config.domId).html(htmls.join(""));
		
		if(me.config.isFromRender){
			form.render('select');
		}
		me.config.done(me);
	};
	//加载数据
	Class.prototype.loadData = function(callback){
		var me = this;
		
		uxutil.server.ajax({
			url:GET_LIST_URL,
			data:{
				fields:'LBSickType_Id,LBSickType_CName',
				where:'lbsicktype.IsUse=1',
				sort:JSON.stringify([{property:'LBSickType_DispOrder',direction:'ASC'}])
			}
		},function(data){
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				callback([]);
				layer.msg(data.msg);
			}
		});
	};
	
	//核心入口
	CommonSelectSickType.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//加载数据
		me.loadData(function(list){
			//初始化HTML
			me.initHtml(list);
		});
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,CommonSelectSickType);
});