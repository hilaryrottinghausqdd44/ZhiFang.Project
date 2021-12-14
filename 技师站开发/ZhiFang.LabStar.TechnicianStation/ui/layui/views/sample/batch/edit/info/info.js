/**
 * @name：检验单修改
 * @author liangyl
 * @version 2021-05-21
 */
layui.extend({
	uxtable:'ux/table',
}).define(['uxutil','uxbase','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		form = layui.form;
	
	//修改数据服务路径
	var EDIT_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateBatchLisTestForm';
    //获取样本类型服务
  	var GET_SAMPLETYPE_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true';
    //获取检验人服务
  	var GET_EMPIDENTITY_URL = uxutil.path.LIIP_ROOT +'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true';
    
    var info_ind =null;
	var info = {
		//参数配置
		config:{
           SECTIONID:null
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
		},me.config,info.config,setings);
	};
	
	Class.pt = Class.prototype;

	//获取仪器表
	Class.pt.SampleTypeList = function(callback){
		var fields = ['Id','CName'],
			url = GET_SAMPLETYPE_URL + '&where=lbsampletype.IsUse=1';
		url += '&fields=LBSampleType_' + fields.join(',LBSampleType_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//获取检验人
	Class.pt.EmpIdentityList =  function(callback){
		var fields = ['HREmployee_Id','HREmployee_CName'],
			url = GET_EMPIDENTITY_URL + "&where=(hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001001')";
		url += '&fields=HREmpIdentity_' + fields.join(',HREmpIdentity_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//初始化系统下拉框
	Class.pt.initSystemSelect = function(){
		var me = this;
		me.SampleTypeList(function(list){
			var len = list.length,
				html = ['<option value="">请选择样本类型</option>'];
				
			for(var i=0;i<len;i++){
				html.push("<option value='" + list[i].LBSampleType_Id + "'>" + list[i].LBSampleType_CName + "</option>");
			}
			$("#LisTestForm_GSampleTypeID").html(html.join(""));
			form.render('select');
		});
		me.EmpIdentityList(function(list){
			var len = list.length,
				html = ['<option value="">请选择检验人</option>'];
				
			for(var i=0;i<len;i++){
				html.push("<option value='" + list[i].HREmpIdentity_HREmployee_Id + "'>" + list[i].HREmpIdentity_HREmployee_CName + "</option>");
			}
			$("#LisTestForm_MainTester").html(html.join(""));
			form.render('select');
		});
	};
	//打开窗口
	Class.pt.openWin =  function(obj,com){
		layer.open({
			title:obj.TYPENAME,
			type:2,
			content:'info/remarks/index.html?t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:true,
			area:['650px','420px'],
			success:function(layero,index){
				setTimeout(function(){
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.initData(obj,function(data){
						layer.close(index);
						com.val(data);
					});
				},100);
			}
		});
	};	
	//@overwrite 获取新增的数据
	Class.pt.getAddParams = function(list,values){
		var me = this;
			
		//要更新的样本单实体列表
		var entityList =[];
		for(var i=0;i<list.length;i++){
			var obj = {
				Id:list[i].LisTestForm_Id
			};
			if (values.LisTestForm_GSampleInfo) obj.GSampleInfo = values.LisTestForm_GSampleInfo;
			if (values.LisTestForm_FormMemo) obj.FormMemo = values.LisTestForm_FormMemo;
			if (values.LisTestForm_SampleSpecialDesc) obj.SampleSpecialDesc = values.LisTestForm_SampleSpecialDesc;
			if (values.LisTestForm_TestComment) obj.TestComment = values.LisTestForm_TestComment;
			if (values.LisTestForm_TestInfo) obj.TestInfo = values.LisTestForm_TestInfo;

			if(values.LisTestForm_MainTester){
				obj.MainTester = $("#LisTestForm_MainTester option:checked").text();
				obj.MainTesterId = values.LisTestForm_MainTester;
			}	
			if (values.LisTestForm_GSampleTypeID) {
				obj.GSampleTypeID = values.LisTestForm_GSampleTypeID;
				obj.GSampleType = $("#LisTestForm_GSampleTypeID option:checked").text();
			}
			entityList.push(obj);
		}
		//修改字段
		var fields = [];
		for (var o in obj) {
			fields.push(o);
		}
		var entity={
			entityList: entityList,
			fields: fields.join(",")
		};
		return entity;
	};
	/**批量修改保存
	 *list 已选的检验单
	 * */
	Class.pt.onSave = function(list,obj){
		var me = this;
		var params = me.getAddParams(list,obj);
		if(!params) return;	
		var index = layer.load();
			var config = {
			type:'post',
			url:EDIT_URL,
			data:JSON.stringify(params)
		};
       
		uxutil.server.ajax(config,function(data){
			layer.close(index);
			if(data.success){
				uxbase.MSG.onSuccess("保存成功!");
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
         //监听+ 弹出短语
        $("#info_form input.myedit+i.layui-icon").on("click", function () {
            var elemID = $(this).prev().attr("id"),
				value = $(this).prev().val(),
				TypeCode = $(this).prev().attr("data-typecode"),
                TypeName = $(this).prev().attr("data-typename");
			me.openPhrase(elemID, value, TypeName, TypeCode);
        });
//		//小组样本描述
//		$('#add_GSampleInfo').on('click',function(){
//			var obj={
//				SECTIONID :me.config.SECTIONID ,
//				TYPENAME  :'小组样本描述',
//				TESTINFO :$("input[name='LisTestForm_GSampleInfo']").val()
//			}
//			me.openWin(obj,$("input[name='LisTestForm_GSampleInfo']"));
//	    });
//	    //检验样本备注
//		$('#add_FormMemo').on('click',function(){
//			var obj={
//				SECTIONID :me.config.SECTIONID ,
//				TYPENAME  :'检验样本备注',
//				TESTINFO :$( "input[name='LisTestForm_FormMemo']").val()
//			}
//			me.openWin(obj,$("input[name='LisTestForm_FormMemo']"));
//	    });
//	     //样本特殊性状
//		$('#add_SampleSpecialDesc').on('click',function(){
//			var obj={
//				SECTIONID :me.config.SECTIONID ,
//				TYPENAME  :'样本特殊性状',
//				TESTINFO :$( "input[name='LisTestForm_SampleSpecialDesc']").val()
//			}
//			me.openWin(obj,$("input[name='LisTestForm_SampleSpecialDesc']"));
//	    });
//	     //检验备注
//		$('#add_TestComment').on('click',function(){
//			var obj={
//				SECTIONID :me.config.SECTIONID ,
//				TYPENAME  :'检验备注',
//				TESTINFO :$( "input[name='LisTestForm_TestComment']").val()
//			}
//			me.openWin(obj,$("input[name='LisTestForm_TestComment']"));
//	    });
//	     //检验评语
//		$('#add_TestInfo').on('click',function(){
//				var obj={
//				SECTIONID :me.config.SECTIONID ,
//				TYPENAME  :'检验评语',
//				TESTINFO :$( "input[name='LisTestForm_TestInfo']").val()
//			}
//			me.openWin(obj,$("input[name='LisTestForm_TestInfo']"));
//	    });  
	    //重置
		$('#info_reset').on('click',function(){
			$("#info_form").find('input[type=text],select,input[type=hidden]').each(function() {
	           $(this).val('');
	        });
	    });

	    //保存
		form.on("submit(submit_info)",function(data){
			if(info_ind.config.FORMLIST.length==0){
				uxbase.MSG.onWarn("请先选择检验单!");
				return;
			}
		    me.onSave(info_ind.config.FORMLIST,data.field);
			return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
		});
	
	};
		 //弹出短语选择
    Class.pt.openPhrase = function (elemID, value, TypeName,TypeCode) {
        var me = this,
            sectionID = me.config.SECTIONID || null,
            elemID = elemID || null,
            value = value || "",
            //短语表配置
			TypeName = TypeName || null,
			TypeCode = TypeCode || null,
            ObjectType = 1,
            ObjectID = sectionID,
            PhraseType = "SamplePhrase";
        if (!sectionID) {
			uxbase.MSG.onWarn("小组ID不能为空，请选择小组!");
            return;
        }
        if (!TypeName) {
			uxbase.MSG.onWarn("缺少TypeName参数!");
            return;
		}
		parent.layer.open({
			type: 2,
			area: ['600px', '420px'],
			fixed: false,
			maxmin: true,
			title: TypeName,
			content: uxutil.path.ROOT + '/ui/layui/views/sample/basic/phrase/new/index.html?CName=' + TypeName + '&ObjectType=' + ObjectType + '&ObjectID=' + ObjectID + '&PhraseType=' + PhraseType + '&TypeName=' + TypeName + '&TypeCode=' + TypeCode + '&isAppendValue=1&&ISNEXTLINEADD=1',
			success: function (layero, index) {
				var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
				//body.find('#CName').html("当前" + TypeName);
				body.find('#Comment').val(value.replace(/\|/g, "\n"));
				var iframeWin = parent.window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
				iframeWin.externalCallFun(function (v) { $("#" + elemID).val(v.replace(/\n/g, "\|")); });
			},
			//cancel: function (index, layero) {
			//    var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
			//    var val = body.find('#Comment').val();
			//    $("#" + elemID).val(val);
			//    parent.layer.close(index);
			//    return false;
			//}  
		});
    };
	//主入口
	info.render = function(options){
		info_ind = new Class(options);
		//初始化下拉框
	    info_ind.initSystemSelect();
	    //
	    info_ind.initListeners();
		return info_ind;
	};
	//暴露接口
	exports('info',info);
});