/**
 * @name：modules/pre/sample/inspect/searchbar 样本采集查询工具栏
 * @author：liangyl
 * @version 2021-09-24
 */
layui.extend({
    uxutil: 'ux/util',
   	CommonSelectDept:'modules/common/select/dept'
}).define(['uxutil','laydate', 'form','CommonSelectDept'],function(exports){
	"use strict";
	
	var $ = layui.$,
		laydate = layui.laydate,
		form = layui.form,
        uxutil = layui.uxutil,
        CommonSelectDept = layui.CommonSelectDept,
		MOD_NAME = 'SearchBar';

    //查询工具栏dom
	var SEARCH_DOM = [
		'<div class="layui-form" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
			'<div class="layui-form-item" id="{domId}-show-toolbar" style="margin-bottom:0;">',
	           '<div class="layui-inline">', 
		          '<label class="layui-form-label">病区:</label>',
		          '<div class="layui-input-inline"> ',
		             '<select name="{domId}-diseaseArea" id="{domId}-diseaseArea" lay-search="" lay-filter="{domId}-diseaseArea"> <option value="">请选择</option> </select>', 
		          '</div>', 
		       '</div>',
		        '<div class="layui-inline">', 
		          '<label class="layui-form-label">科室:</label>',
		          '<div class="layui-input-inline"> ',
		             '<select name="{domId}-dept" id="{domId}-dept" lay-search="" lay-filter="{domId}-dept"> <option value="">请选择</option> </select>', 
		          '</div>', 
		       '</div>',
		       '<div class="layui-inline">', 
		          '<label class="layui-form-label">病历</label>',
		          '<div class="layui-input-inline"> ',
		          	 '<input type="text" name="{domId}-patNo" id="{domId}-patNo" placeholder="病历号"  autocomplete="off" class="layui-input" />',
		          '</div>',
		       '</div>',
		         '<div class="layui-inline">',
			   '<label class="layui-form-label">时间范围:</label>',
		          '<div class="layui-input-inline" style="width:185px;"> ',
					  '<input type="text" id="{domId}-gDate" name="{domId}-gDate" class="layui-input myDate" placeholder="开始时间-结束时间" />',
				      '<i class="layui-icon layui-icon-date"></i> ',
				      '<input type="text" name="{domId}-dateType" id="{domId}-dateType" class="layui-input layui-hide" placeholder="查询时间字段" />', 
	              '</div>',
	           '</div>',
		       '<div class="layui-inline" style="padding-left: 10px;">', 
		          '<button type="button" id="{domId}-search" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询未采样确认</button>', 
		       '</div>',
	        '</div>',
		'</div>',
		'<style>',
            '.layui-form-item .layui-inline{margin-bottom: 1px; margin-right: 0px;}',
	        '.layui-form-item{margin-bottom: 0px;}',
	        '.layui-input + .layui-icon { cursor:pointer;position: absolute;top: 0px;right: 6px;color: #009688; }',
		'</style>'
	];
	//查询工具栏
	var SearchBar = {
		searchId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			//查询按钮触发事件
			searchClickFun:function(){},
			nodetype:null,
			DateType:''//查询时间字段
		},
		//内部列表参数
		searchConfig:{
			elem:null
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,SearchBar.config,setings);
		me.searchConfig = $.extend({},me.searchConfig,SearchBar.searchConfig);
		if(me.config.height)me.searchConfig.height = me.config.height;
		me.searchId = me.config.domId;
		me.searchConfig.elem = "#" + me.searchId;
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		$('#' + me.config.domId).html('');
		var html = SEARCH_DOM.join("").replace(/{domId}/g,me.searchId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
		//根据参数设置初始化查询时间字段
		me.initParamsHtml();
	    //时间范围初始化
	    me.initDateHtml();
	   	//开单科室(送检科室)
		CommonSelectDept.render({
			domId:me.config.domId+'-dept',
			code:'1001101',
			done:function(){
				
			}
		});
		//病区
		CommonSelectDept.render({
			domId:me.config.domId+'-diseaseArea',
			code:'1001102',
			done:function(){
				
			}
		});
	};
	//根据参数设置初始化查询时间字段
    Class.prototype.initParamsHtml = function(){
    	var me = this;
		var defaultValues = {};
		//查询时间字段
		defaultValues[me.config.domId + '-dateType'] = me.config.DateType;
		form.val(me.config.domId + '-form',defaultValues);
    };
	 //时间范围初始化
    Class.prototype.initDateHtml = function(){
    	var me = this;
      //监听日期图标
		 $('#' + me.config.domId+"-gDate+i.layui-icon").on("click", function () {
			 var elemID = $(this).prev().attr("id");
			 if ($("#" + elemID).hasClass("layui-disabled")) return false;
			 var key = $("#" + elemID).attr("lay-key");
			 if ($('#layui-laydate' + key).length > 0) {
				 $("#" + elemID).attr("data-type", "date");
			 } else {
				 $("#" + elemID).attr("data-type", "text");
			 }
			 var datatype = $("#" + elemID).attr("data-type");
			 if (datatype == "text") {
				//时间范围
				laydate.render({
					 elem: '#' + me.config.domId+"-gDate",
					 eventElem:me.config.domId+'-gDate+i.layui-icon',
					 type: 'date',
					 range: true,
					 show:true
				 });
				 $("#" + elemID).attr("data-type", "date");
			 } else {
				 $("#" + elemID).attr("data-type", "text");
				 var key = $("#" + elemID).attr("lay-key");
				 $('#layui-laydate' + key).remove();
			 }
		 });
		 //监听日期input -- 不弹出日期框
		 $('#' + me.config.domId+"-form").on('focus', '#' + me.config.domId+'gDate', function () {
			 me.preventDefault();
			 layui.stope(window.event);
			 return false;
		 });
		var today = new Date();
		var defaultvalue = uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true);
		 //赋值默认日期框
        $("#"+me.config.domId+'-gDate').val(defaultvalue);
	};
	//获取按钮查询条件
	Class.prototype.getWhere = function(){
		var me = this,
			values = form.val(me.config.domId + '-form'),
			startDate='',endDate='',
	        diseaseArea = values[me.config.domId + '-diseaseArea'], //病区
	        deptID = values[me.config.domId + '-dept'], //科室
	        patNo = values[me.config.domId + '-patNo'], //病历号
	        dateType = values[me.config.domId + '-dateType'], //时间类型
	        gDate = values[me.config.domId + '-gDate']; //时间范围
        if(gDate){
        	startDate = gDate.substring(0,10); //开始日期
            endDate = gDate.substring(13,gDate.length); //结束时间
        }      
		var where = "",arr=[];
		//病区
		if(diseaseArea)arr.push("LisPatient.DistrictID='"+diseaseArea+"'");
		//病历号
		if(patNo)arr.push("LisPatient.PatNo='"+patNo+"'");
		//开单科室
		if(deptID)arr.push("ExecDeptID="+deptID);
	    //时间类型
		if(dateType && startDate && endDate){
			arr.push(dateType + " between '" + startDate + ' 00:00:00'+"' and '" + endDate + " 23:59:59'");
		}
		//未采集
		arr.push("BarCodeStatusID <4 and CollectTime is null");
		if(arr.length>0)where= arr.join(' and ');
		return where;
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		//查询未采集
		$('#'+me.config.domId+'-search').on('click',function(){
			me.config.searchClickFun && me.config.searchClickFun(me.getWhere());
		});
	};
	//核心入口
	SearchBar.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.searchtool = form.render(me.searchConfig);
		//监听事件
		me.initListeners();
		return me;
	};
	//暴露接口
	exports(MOD_NAME,SearchBar);
});