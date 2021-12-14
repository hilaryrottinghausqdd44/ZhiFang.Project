/**
 * @name：modules/pre/sample/inspect/searchbar 样本送达查询工具栏
 * @author：liangyl
 * @version 2021-09-24
 */
layui.extend({
    uxutil: 'ux/util',
    CommonSelectUser: 'modules/common/select/preuser',
    CommonSelectDept: 'modules/common/select/dept'
}).define(['uxutil','laydate', 'form', 'CommonSelectUser', 'CommonSelectDept'],function(exports){
	"use strict";
	
	var $ = layui.$,
		laydate = layui.laydate,
		form = layui.form,
        uxutil = layui.uxutil,
        CommonSelectUser = layui.CommonSelectUser,
        CommonSelectDept = layui.CommonSelectDept,
		MOD_NAME = 'SearchBar';

    //查询工具栏dom
	var SEARCH_DOM = [
		'<div class="layui-form" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
		  '<div class="layui-form-item" id="{domId}-not-inspection">',//查询未送检工具栏
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">查询时间:</label>', 
		        '<div class="layui-input-inline" style="width:185px;">', 
		           '<input type="text" id="{domId}-gDate" name="{domId}-gDate" class="layui-input myDate" placeholder="开始时间-结束时间" />', 
		           '<i class="layui-icon layui-icon-date"></i>',
		           	'<input type="text" name="{domId}-dateType" id="{domId}-dateType" class="layui-input layui-hide" placeholder="查询时间字段" />', 
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">姓名:</label>', 
		        '<div class="layui-input-inline" >', 
		          	'<input type="text" name="{domId}-CName" id="{domId}-CName" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">科室:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-DeptId" id="{domId}-DeptId" lay-search="" lay-filter="{domId}-DeptId"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">病区:</label>', 
		        '<div class="layui-input-inline">', 
	                '<select name="{domId}-AreaId" id="{domId}-AreaId" lay-search="" lay-filter="{domId}-AreaId"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline" style="padding-left: 5px;">', 
		        '<button type="button" id="{domId}-not-inspection-btn" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询未送检</button>', 
		       '</div>', 
		   '</div>',
		  '<div class="layui-form-item" id="{domId}-inspection">',//查询已送检工具栏
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">病历号:</label>', 
		        '<div class="layui-input-inline" style="width:185px;">', 
		          	'<input type="text" name="{domId}-PatNo" id="{domId}-PatNo" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">打包号:</label>', 
		        '<div class="layui-input-inline">', 
		          	'<input type="text" name="{domId}-CollectPackNo" id="{domId}-CollectPackNo" autocomplete="off" class="layui-input" />',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline">', 
		        '<label class="layui-form-label">医生:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-DoctorId" id="{domId}-DoctorId" lay-search="" lay-filter="{domId}-DoctorId"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		    '<div class="layui-inline">', 
		        '<label class="layui-form-label">送检人:</label>', 
		        '<div class="layui-input-inline" >', 
	                '<select name="{domId}-TransportUserId" id="{domId}-TransportUserId" lay-search="" lay-filter="{domId}-TransportUserId"> <option value="">请选择</option></select>',
		        '</div>', 
		     '</div>',
		     '<div class="layui-inline" style="padding-left: 5px;">', 
		        '<button type="button" id="{domId}-inspection-btn" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询已送检</button>', 
		       '</div>', 
		  '</div>',
		'</div>',
		'<style>',
            '.layui-form-item .layui-inline{margin-bottom: 2px; margin-right: 0px;}',
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
			//查询未送检按钮触发事件
			notInspectionClickFun:function(){},
			//查询已送检按钮触发事件
			inspectionClickFun:function(){},
			MODELTYPE:null,
			nodetype:null,
			isDefaultDept:null,//默认科室
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
		//模式1，2 不显示查询未送检工具栏  (模式12不需要查询未送检)
		if(me.config.MODELTYPE =='1' || me.config.MODELTYPE =='2')$('#' + me.config.domId+'-not-inspection').addClass('layui-hide');
		//模式3 只能查询未送检数据，已送检的查询条件隐藏
		if(me.config.MODELTYPE =='3')$('#' + me.config.domId+'-inspection').addClass('layui-hide');
	    //时间范围初始化
	    me.initDateHtml();
	    //送检科室
		CommonSelectDept.render({
			domId:me.config.domId+'-DeptId',
			code:'1001101',
			done:function(){
				if(me.config.IsDefaultDept=='1'){ //还原记录默认科室
					if(me.getHistoryInfo() && me.getHistoryInfo().DeptID){
						$('#'+me.config.domId+'-DeptId').val(me.getHistoryInfo().DeptID);
					}
				}
			}
		});
		 //病区
		CommonSelectDept.render({
			domId:me.config.domId+'-AreaId',
			code:'1001102'
		});
		//送检人
		CommonSelectUser.render({
			domId:me.config.domId+'-TransportUserId',
			code:[1001002, 1001004],
			done:function(){
			}
		});
		 //医生
		CommonSelectUser.render({
			domId:me.config.domId+'-DoctorId',
			code:[1001003]
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
	};
	//获取默认科室信息
	Class.prototype.getHistoryInfo = function(){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
			
		return uxutil.localStorage.get('PreSampleBarcodeInspectDeptID_' + me.config.nodetype + empId,true);
	};
	//记录默认科室信息{DeptID:''}
	Class.prototype.insertHistoryInfo = function(info){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
			
		uxutil.localStorage.set('PreSampleBarcodeInspectDeptID_' + me.config.nodetype + empId,JSON.stringify(info));
	};
	
    //获取按钮查询条件
	Class.prototype.getWhere = function(){
		var me = this,
		    where ="",//已送检查询条件
		    notwhere ="",//未送检查询条件
			values = form.val(me.config.domId + '-form'),
			startDate='',endDate='',
	        CName = values[me.config.domId + '-CName'], //病人姓名
	        DeptId = values[me.config.domId + '-DeptId'], //科室
	        AreaId = values[me.config.domId + '-AreaId'], //病区
	        dateType = values[me.config.domId + '-dateType'], //时间类型
	        gDate = values[me.config.domId + '-gDate'], //时间范围
	        
	        PatNo = values[me.config.domId + '-PatNo'], //病历号
	        CollectPackNo = values[me.config.domId + '-CollectPackNo'], //打包号
	        DoctorId = values[me.config.domId + '-DoctorId'], //医生
	        TransportUserId = values[me.config.domId + '-TransportUserId'];//送检人
	        
        if(gDate){
        	startDate = gDate.substring(0,10); //开始日期
            endDate = gDate.substring(13,gDate.length); //结束时间
        }      
		var notarr=[],arr=[];
		//病人姓名
		if(CName)notarr.push("lisbarcodeform.LisPatient.CName='"+CName+"'");
		//病区
		if(AreaId)notarr.push("lisbarcodeform.LisPatient.DistrictID='"+AreaId+"'");
		//开单科室
		if(DeptId)notarr.push("lisbarcodeform.ExecDeptID="+DeptId);
	    //时间类型
		if(dateType && startDate && endDate){
			notarr.push('lisbarcodeform.'+dateType + " between '" + startDate + ' 00:00:00'+"' and '" + endDate + " 23:59:59'");
		}
		//未送检
		notarr.push("lisbarcodeform.BarCodeStatusID <5 and lisbarcodeform.SendTime is null");
		//未送检查询条件
		if(notarr.length>0)notwhere = notarr.join(' and ');
		//病历号
		if(PatNo)arr.push("lisbarcodeform.LisPatient.PatNo='"+PatNo+"'");
		//打包号
		if(CollectPackNo)arr.push("lisbarcodeform.CollectPackNo='"+CollectPackNo+"'");
		//医生
		if(DoctorId)arr.push("lisbarcodeform.LisPatient.DoctorID="+DoctorId);
	    //已送检
		arr.push("lisbarcodeform.BarCodeStatusID >4 and lisbarcodeform.SendTime is not null");
	
	    //已送检查询条件
		if(arr.length>0)where = arr.join(' and ');
	    //入参说明 relationForm 运送人不在barcodeform表里 而在操作记录表
		return {
			notwhere : notwhere,  //未送检查询条件
			where : where,         //已送检查询条件
			userID:TransportUserId,//送检人ID,用于联合已查询条件判断已查询条件是否为空
			relationForm:'lisbarcodeform.Id=lisoperate.OperateObjectID  and lisoperate.OperateUserID='+TransportUserId+' and lisoperate.OperateTypeID=100013'
		};
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		form.on('select('+me.config.domId+'-DeptId)', function(data){
			if(me.config.IsDefaultDept=='1'){ //还原记录默认科室
		        me.insertHistoryInfo({DeptID:data.value});
		    }
		});      
		//查询未送检
		$('#'+me.config.domId+'-not-inspection-btn').on('click',function(){
			me.config.notInspectionClickFun && me.config.notInspectionClickFun(me.getWhere());
		});
		//查询已送检
		$('#'+me.config.domId+'-inspection-btn').on('click',function(){
			me.config.inspectionClickFun && me.config.inspectionClickFun(me.getWhere());
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