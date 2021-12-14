/**
	@name：多项目结果录入
	@author：liangyl	
	@version 2021-05-22
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','form','tableSelect'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		tableSelect = layui.tableSelect,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
		
	/**获取该小组下所有项目数据服务路径*/
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';
	 /**获取组合项目子项服务路径*/
	var GET_ITEMGROUP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true';
	/**样本单项目结果保存*/
    var ADD_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItemResult';
	//默认条件
	var DEFAULTWHERE = 'lbsectionitem.LBItem.GroupType=0';
	
	var table_Ind = null;
	
	var manyresulttable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			defaultOrderBy:[{ property: 'LBSectionItem_DispOrder', direction: 'ASC' }, { property: 'LBSectionItem_LBItem_DispOrder', direction: 'ASC' }],
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
                {field:'PItemName', width:150, title: '组合项目', sort: false},
				{field:'PItemID', width:150, title: '组合项目ID', sort: false,hide:true},
				{field:'LBSectionItem_LBItem_Id', width:150, title: '项目id', sort: false,hide:true},
				{field:'LBSectionItem_LBItem_CName', width:150, title: '项目名称', sort: false},
				{field:'LBSectionItem_LBItem_SName', width:120, title: '项目简称', sort: false},
				{field:'LBSectionItem_LBItem_Prec', width:100,title: '项目精度', sort: false,hide:true},
				{field:'ReportValue', width:100, title: '报告值', sort: false,edit:'text'}
			]],
			text: {none: '暂无相关数据' },
			done: function (res, curr, count) {
				var filter = $(table_Ind.config.elem).attr("lay-filter");
			}
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
		},me.config,manyresulttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		
		if(table_Ind.config.FORMLIST.length ==0){
			table_Ind.instance.reload({data:[]});
			return;
		}
		$('#SectionItemName').val('');
		$('#SectionItemID').val('');
		var where = DEFAULTWHERE +' and lbsectionitem.LBSection.Id='+me.config.SECTIONID;
		var url = GET_LIST_URL+'&fields='+fields.join(',')+'&where='+where+'&sort='+JSON.stringify(me.config.defaultOrderBy);
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				table_Ind.instance.reload({data:list});
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};

	//初始化系统下拉框
	Class.pt.initSystemSelect = function(){
		var me = this;
		me.GroupItemList('SectionItemName','SectionItemID');
	};
	  //初始化小组项目
    Class.pt.GroupItemList =  function (CNameElemID, IdElemID) {
        var me = this;
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['LBItem_Id','LBItem_CName','LBItem_SName','LBItem_GroupType','LBItem_Prec'],
			url = GET_LIST_URL + "&where=lbsectionitem.LBSection.Id="+me.config.SECTIONID;
		url += '&fields=LBSectionItem_' + fields.join(',LBSectionItem_');
		
        if (!CNameElemID) return;
        var height = $('.layui-tab-content').height()-150;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBSectionItem_LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbsectionitem.LBItem.CName,lbsectionitem.LBItem.SName,lbsectionitem.LBItem.Shortcode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '项目名称/简称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: height,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBSectionItem_LBItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBSectionItem_LBItem_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBSectionItem_LBItem_SName', width: 150, title: '项目名称', sort: false },
                    { field: 'LBSectionItem_LBItem_Shortcode', width: 120, title: '快捷码', sort: false },
                    { field: 'LBSectionItem_LBItem_GroupType', width: 120, title: '组合项目', sort: false,hide:true },
                    { field: 'LBSectionItem_LBItem_Prec', width: 120, title: '精度', sort: false,hide:true }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["LBSectionItem_LBItem_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBSectionItem_LBItem_Id"]);
                    var GroupType = record["LBSectionItem_LBItem_GroupType"]+"";
                    //如果选择的是组合项目需要找出子项,添加到列表
		        	if(GroupType=='1'){
		        		me.getItemGroup(record["LBSectionItem_LBItem_Id"],function(list){
		        			table_Ind.instance.reload({data:list});
		        		});
		        	}else{
		        		table_Ind.instance.reload({data:[record]});
		        	}
                }else{
                	table_Ind.loadData();
                }
            }
        });
    };
	//联动
	Class.pt.initListeners= function(){
		var me =  this;
		
		$('#save_manyresult').on('click',function(){
			me.onSave();
		});
		  //icon 前存在icon 则点击该icon 等同于点击input
	    $("input.layui-input+.layui-icon").on('click', function () {
	        if (!$(this).hasClass("myDate")) {
	            $(this).prev('input.layui-input')[0].click();
	            return false;//不加的话 不能弹出
	        }
	    });      
	};

	/**根据组合项目id获取组合项目子项*/
	Class.pt.getItemGroup = function(GroupItemID,callback){
		var me  = this,
			url = GET_ITEMGROUP_LIST_URL;
		url += '&fields=LBItemGroup_LBItem_Id,LBItemGroup_LBItem_CName,LBItemGroup_LBItem_SName,LBItemGroup_LBItem_Prec';
		url += '&where=GroupItemID='+GroupItemID;
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				var arr =[]; 
				for(var i=0;i<list.length;i++){
        			arr.push({
				 		LBSectionItem_LBItem_Id: list[i].LBItemGroup_LBItem_Id,
						LBSectionItem_LBItem_CName:list[i].LBItemGroup_LBItem_CName,
						LBSectionItem_LBItem_SName: list[i].LBItemGroup_LBItem_SName,
						LBSectionItem_LBItem_Prec: list[i].LBItemGroup_LBItem_Prec,
						PItemID:$("#SectionItemID").val(),
		                PItemName:$("#SectionItemName").val()
				 	});
			 	}
				callback(arr);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//获取listAddTestItem
	Class.pt.getAddTestItem = function(){
		var me = this;
		var items = table_Ind.table.cache.item_reportvalue_table,
		    len = items.length,
		    list=[];
		for(var i=0;i<len;i++){
			if(items[i].ReportValue){
				var obj ={
					LBItem: {
						Id: items[i].LBSectionItem_LBItem_Id,
						CName: items[i].LBSectionItem_LBItem_CName,
						Prec: items[i].LBSectionItem_LBItem_Prec,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					}
				};
				if(items[i].PItemID){
					obj.PLBItem = { Id: items[i].PItemID, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
				}
				obj.ReportValue = items[i].ReportValue;
				list.push(obj);
			}
		}
		return list; 
	};
	/**
     * 保存(批量新增样本单项目结果)
     * list检验单
     * */
	Class.pt.onSave  = function(list){
	   	var me = this;
	   	if(table_Ind.config.FORMLIST.length ==0){
			uxbase.MSG.onWarn("请先选择检验单!");
			return;
		}
	   	//校验
	   	var list = table_Ind.table.cache.item_reportvalue_table;
	   	if(list.length==0){
			uxbase.MSG.onWarn("请选择项目后再保存!");
	   		return;
	   	}
        var listAddTestItem = me.getAddTestItem();
		if(listAddTestItem.length==0){
			uxbase.MSG.onWarn("录入报告值不能为空!");
			return;
		}
		var index =layer.load()//显示遮罩层
		
		var arr=[];
	   	for(var i =0;i<table_Ind.config.FORMLIST.length;i++){
	   		arr.push(table_Ind.config.FORMLIST[i].LisTestForm_Id);
		}
	   	var strArr=arr.splice(',');
	   	var params ={
			testFormID:strArr.join(','),
			listAddTestItem:listAddTestItem,//是否新增项目
			isAddItem: $("#isAddItem").prop("checked"),
		    isSingleItem: false
		};
		var config = {
			type:'post',
			url:ADD_URL,
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
	},
	//主入口
	manyresulttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_Ind = result;
       	result.loadData = me.loadData;
       	me.initListeners();
        me.initSystemSelect();
		return result;
	};
	//暴露接口
	exports('manyresulttable',manyresulttable);
});