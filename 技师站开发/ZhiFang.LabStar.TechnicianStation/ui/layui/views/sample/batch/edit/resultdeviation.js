/**
	@name：结果偏移
	@author：liangyl	
	@version 2021-05-24
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
    //批量样本单项目结果偏移
    var SAVE_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemResultOffset';
	
	var table_Ind = null;
	
	var saveErrorCount = 0,
            saveCount = 0,saveLength = 0;
            
	var resultdeviationtable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			cols:[[
				{field:'LisTestItem_Id', title: '检验项目编号', sort: false,hide:true},
                {field:'LBItem_Id', title: '检验项目编号', sort: false,hide:true},
				{field:'LBItem_CName', width:150, title: '检验项目名称', sort: false},
				{field:'LBItem_SName', width:150, title: '检验项目简称', sort: false},
				{field:'Coefficient', width:150, title: '偏离系数', sort: false,edit:'text'},
				{field:'AddValue', width:150, title: '附加值', sort: false,edit:'text'},
				{field:'LBItem_DefaultRange', title: '默认参考范围', width:120,sort: false}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
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
		},me.config,resultdeviationtable.config,setings);
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
			table_Ind.instance.reload({data:list});
		});
	};
	//联动
	Class.pt.initListeners= function(){
		var me =  this;
		//执行
		$('#save_resultdeviation').on('click',function(){
			if(table_Ind.config.FORMLIST.length ==0){
				uxbase.MSG.onWarn("请先选择检验单!");
				return;
			}
			me.onSave();
		});
       
	};

	//根据样本单ID调用样本单项目服务，组合拼成数据行
	Class.pt.getTestItemByID = function(TestFormID,ItemID,CoefficientArr,callback){
		var me = this;
		var url = GET_TESTITEM_LIST_URL;
		url += '&fields=LisTestItem_Id,LisTestItem_LBItem_Id';
		url+='&where=listestitem.LisTestForm.Id='+TestFormID +' and listestitem.LBItem.Id in ('+ItemID+')';
		uxutil.server.ajax({
			url:url,
			async: false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				if(list.length==0)return;
				var testItemInfolist = [];
				for(var i = 0;i<list.length;i++){
					var LBItemId = list[i].LisTestItem_LBItem_Id;
					var LisTestItemID = list[i].LisTestItem_Id;
				    for(var j=0;j<CoefficientArr.length;j++){
				    	//根据项目id 匹配
				    	 if(LBItemId == CoefficientArr[j].LBItem_Id){
				    	 	var Coefficient = CoefficientArr[j].Coefficient;
				    	 	var AddValue = CoefficientArr[j].AddValue;
                            //不设置偏移系数，偏移系数默认=1
                            if(!Coefficient && AddValue)Coefficient=1;
                            //不设置附加值，附加值默认=0
                            if(Coefficient && !AddValue)AddValue=0;
                            
				    	 	testItemInfolist.push({
				    	 		LisTestItemID:LisTestItemID,
				    	 		LBItemID:LBItemId,
				    	 		Coefficient:Coefficient,
				    	 		AddValue:AddValue
				    	 	});
				    	 	break;
				    	 }
				    }
				}
				callback(testItemInfolist);
			} else {
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	},
	/**
     * 保存(批量新增样本单项目结果)
     * list检验单
     * */
	Class.pt.onSave  = function(){
	   	var me = this;
	    var filter = $(me.config.elem).attr("lay-filter");
		var items = table_Ind.table.cache.resultdeviation_table,
		    len = items.length; 

		//满足偏移条件的数组，用于存储临时数据，减少循环匹配次数
		var CoefficientArr = [];
        //校验，获取列表项目id
		var itemids = "",str="",itemarr = [];
		for(var i=0;i<len;i++){
			var Coefficient = items[i].Coefficient;//系数
			var AddValue = items[i].AddValue;//附加值
			//不设置偏移系数和附加值时  不保存
            if((!Coefficient && AddValue) || (Coefficient && !AddValue) || (Coefficient && AddValue)){
            	itemarr.push(items[i].LBItem_Id);
            	CoefficientArr.push(items[i]);
            }
		}
		if(itemarr.length==0){
			uxbase.MSG.onWarn("请设置偏移系数和附加值!");
			return;
		}
	    str = itemarr.splice(',');
		if(str.length>0)itemids = str.join(',');
		if(!itemids)return;

		var index =layer.load()//显示遮罩层
		saveErrorCount = 0;
        saveCount = 0;
        saveLength = table_Ind.config.FORMLIST.length;
		
		for(var i =0;i<table_Ind.config.FORMLIST.length;i++){
	   		var testFormID = table_Ind.config.FORMLIST[i].LisTestForm_Id;
			if(!testFormID)return;
			//找出检验单项目ID
			me.getTestItemByID(testFormID,itemids,CoefficientArr,function(testItemInfolist){
				if(testItemInfolist.length>0)me.addOne({testItemInfo:JSON.stringify(testItemInfolist)});
			});
		}
	};
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
	resultdeviationtable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_Ind = result;
		result.loadData = me.loadData;
        me.initListeners();
		return result;
	};
	//暴露接口
	exports('resultdeviationtable',resultdeviationtable);
});