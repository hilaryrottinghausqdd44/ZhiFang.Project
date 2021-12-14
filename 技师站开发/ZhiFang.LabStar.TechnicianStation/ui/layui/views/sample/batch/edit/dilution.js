/**
	@name：稀释处理
	@author：liangyl	
	@version 2021-05-22
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
		
		//获取多样本单共有项目列表
	var GET_LIST_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_QueryCommonItemByTestFormID';
	//查询样本单项目
	var GET_TESTITEM_LIST_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
	/**稀释处理*/
    var SAVE_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemResultDilution';

	var table_Ind = null;
	
	var saveErrorCount = 0,
            saveCount = 0,saveLength = 0;
            
	var dilutiontable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			cols:[[
				{type: 'checkbox', fixed: 'left'},
                {field:'LBItem_Id', title: '检验项目编号', sort: true,hide:true},
				{field:'LBItem_CName', width:150, title: '检验项目名称'},
				{field:'LBItem_SName', width:150, title: '检验项目简称'},
				{field:'LBItem_DefaultRange', title: '默认参考范围',  width:120}
			]],
			text: {none: '暂无相关数据' },
			done: function (res, curr, count) {
			}
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				//默认选中
				var arr = [];
				for(var i=0;i<data.list.length;i++){
					data.list[i].LAY_CHECKED = true;
					arr.push(data.list[i]);
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": arr || []
				};
			},
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,dilutiontable.config,setings);
	};
	
	Class.pt = Class.prototype;
	
	//数据加载
	Class.pt.loadDataByID = function(FORMLIST,callback){
		var  me = this;
		var arr=[];
	   	for(var i =0;i<FORMLIST.length;i++){
	   		arr.push(FORMLIST[i].LisTestForm_Id);
		}
	   	var strArr=arr.splice(',');
	   	var params ={
			testFormIDList:strArr.join(','),
			fields:'LBItem_Id,LBItem_CName,LBItem_SName,LBItem_DefaultRange'
		};
		var config = {
			type:'post',
			url:GET_LIST_URL,
			data:JSON.stringify(params)
		};
		uxutil.server.ajax(config,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//数据加载
	Class.pt.loadData = function(FORMLIST){
			var  me = this,
    		cols = me.config.cols[0],
			fields = [];

		table_Ind.config.FORMLIST = FORMLIST;
		if(table_Ind.config.FORMLIST.length ==0){
			uxbase.MSG.onWarn("请先选择检验单!");
			return;
		}
		Class.pt.loadDataByID(FORMLIST,function(list){
			var filter = $(me.config.elem).attr("lay-filter");
			for(var i=0;i<list.length;i++){
				list[i].LAY_CHECKED = true;
			}
			table_Ind.instance.reload({data:list});
		});
	};
	//联动
	Class.pt.initListeners= function(){
		var me =  this;
		
		$('#save_dilution').on('click',function(){
			if(table_Ind.config.FORMLIST.length ==0){
				uxbase.MSG.onWarn("请先选择检验单!");
				return;
			}
		   	//校验有勾选项
		   	var filter = $(me.config.elem).attr("lay-filter");
		   	var list = table_Ind.table.checkStatus(filter).data;
		   	if(list.length==0){
				uxbase.MSG.onWarn("请勾选需要稀释的项目行!");
		   		return;
		   	}
			//校验 校验稀释倍数。稀释倍数不能小于等于0；不能等于1，等于1没有意义。当稀释系数＜1，提示稀释倍数＜1，确定要执行稀释样本结果调整吗？
			if($('#InputMultiple').val()==0 || $('#InputMultiple').val()==1 ){
				uxbase.MSG.onWarn("稀释倍数不能小于等于0；不能等于1");
				return;
			}
			var itemids = me.getItemIds(list);
			if(!itemids)return;
			me.onSave(itemids);
		});
        //结果单位下拉框监听 同步输入框
	    form.on('select(Multiple)', function (data) {
	        $("#InputMultiple").val(data.value);
	    });
	};

	//根据样本单ID调用样本单项目服务，组合拼成数据行
	Class.pt.getTestItemByID = function(TestFormID,ItemID,callback){
		var me = this;
		var url = GET_TESTITEM_LIST_URL+'&fields=LisTestItem_Id';
		url+='&where=listestitem.LisTestForm.Id='+TestFormID +' and listestitem.LBItem.Id in ('+ItemID+')';
		
		uxutil.server.ajax({
			url:url,
			async: false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				if(list.length==0)return;
				var testItemIDList="",testitemarr=[],strids="";
				for(var i = 0;i<list.length;i++){
					testitemarr.push(list[i].LisTestItem_Id);
				}
			    if(testitemarr.length>0)strids = testitemarr.splice(',');
			    if(strids.length>0)testItemIDList = strids.join(',');
				callback(testItemIDList);
			} else {
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	},
     //获取项目ID
    Class.pt.getItemIds  = function(list){
     	var me = this,
     	    itemids = "",str="",itemarr = [];
		for(var i=0;i<list.length;i++){
			itemarr.push(list[i].LBItem_Id);
		}
	    if(itemarr.length>0)str = itemarr.splice(',');
		if(str.length>0)itemids = str.join(',');
		return itemids;
     };
	/**
     * 保存(批量新增样本单项目结果)
     * list检验单
     * */
	Class.pt.onSave  = function(itemids){
	   	var me = this;
	    
		var index =layer.load()//显示遮罩层
		saveErrorCount = 0;
        saveCount = 0;
        saveLength = table_Ind.config.FORMLIST.length;
		for(var i =0;i<table_Ind.config.FORMLIST.length;i++){
	   		var testFormID = table_Ind.config.FORMLIST[i].LisTestForm_Id;
			if(!testFormID)return;
			//找出检验单勾选的检验单项目ID
			me.getTestItemByID(testFormID,itemids,function(testItemIDList){				
			    var entity = {testItemIDList:testItemIDList,dilutionTimes:$('#InputMultiple').val()};
				me.addOne(entity);
			});
		}
	},
	Class.pt.onSaveEnd = function(){
		if (saveCount + saveErrorCount == saveLength) {
			layer.closeAll('loading');//隐藏遮罩层
			if (saveErrorCount == 0){
				uxbase.MSG.onSuccess("保存成功!");
				table_Ind.loadData(table_Ind.config.FORMLIST);
			}else{
				uxbase.MSG.onError("保存失败!");
			}
		}
	};
	Class.pt.addOne=function(params){
		var me = this;
		var config = {
			type:'post',
			async:false,
			url:SAVE_URL,
			data:JSON.stringify(params)
		};
		uxutil.server.ajax(config, function(data) {
			if (data.success) {
				saveCount++;
			} else {
				saveErrorCount++;
			}				
			me.onSaveEnd();
		});
	};
	//主入口
	dilutiontable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_Ind = result;
		result.loadData = me.loadData;
        me.initListeners();
		return result;
	};
	//暴露接口
	exports('dilutiontable',dilutiontable);
});