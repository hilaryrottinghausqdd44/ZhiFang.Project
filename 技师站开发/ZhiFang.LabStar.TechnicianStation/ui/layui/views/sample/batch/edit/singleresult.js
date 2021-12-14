/**
	@name：单项目结果录入
	@author：liangyl	
	@version 2021-05-22
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','form','uxbasic','tableSelect'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		uxbasic = layui.uxbasic,
		uxbase = layui.uxbase,
		tableSelect = layui.tableSelect,
		uxtable = layui.uxtable;
		
		/**获取该小组下所有项目数据服务路径*/
	var GET_SECTIONTEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';

	/**获取样本单项目数据服务路径*/
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
	/**样本单项目结果保存*/
    var ADD_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItemResult';
	//默认条件
	var DEFAULTWHERE = 'lbsectionitem.LBItem.GroupType=0';
	
	var table_Ind = null;
	
	var singleresulttable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			DIRECTION:'asc',
			autoSort:false,
			defaultOrderBy:[{ property: 'LBSectionItem_DispOrder', direction: 'ASC' }, { property: 'LBSectionItem_LBItem_DispOrder', direction: 'ASC' }],
			cols:[[
			    {field:'LisTestForm_GTestDate', width:100, title: '检验日期', sort: true, templet:function(record){
					var value = record["LisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{field:'LisTestForm_GSampleNoForOrder', width:90, title: '样本号', sort: true, templet:function(record){
					var v = record["LisTestForm_GSampleNo"];
	                return v;
				}},
				{field:'LisTestForm_GSampleNo', width:80, title: '样本号排序',hide:true, templet:function(record){
					var v = record["LisTestForm_GSampleNoForOrder"];
	                return v;
				}},
				{field:'LisTestForm_CName', width:80, title: '姓名'},
				{field:'LisTestForm_LisPatient_GenderName',width:70,title:'性别',sort:false},
				{field:'LisTestItem_ReportValue', width: 100, title: '原报告值', sort: false },
				{field:'LisTestItem_NewReportValue', width: 100, title: '录入报告值', sort: false,edit:'text' },
				{field:'LisTestItem_OriglValue', width: 80, title: '仪器结果', sort: false },
                {field:'LisTestItem_RefRange', width:80, title: '参考范围', sort: false},
				{field:'LisTestItem_LBItem_Id', width:150, title: '项目ID', sort: false,hide:true},
				{field:'LisTestItem_LBItem_CName', minWidth:120,flex:1, title: '项目名称', sort: false,hide:true},
				{field:'LisTestItem_LBItem_Prec', width:100, title: '项目精度', sort: false,hide:true},
				{field:'LisTestItem_Id', width:150,title: '样本单项目ID', sort: false,hide:true},
				{field:'LisTestForm_Id', width:150,title: '样本单ID', sort: false,hide:true},
				{field:'Exist', width:70,title: '存在', sort: false, templet:function(record){
					var v = record["Exist"];
					if(v=='1')v = '<span style="color:green;">是</span>';
					else
					   v = '<span style="color:red;">否</span>';
	                return v;
				}}
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
							//监听样本单列表排序
			        that.table.on('sort(' + filter + ')', function (obj) {
			            var field = obj.field, //当前排序的字段名
			                type = obj.type, //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
			                sort = [];
	                    table_Ind.config.DIRECTION = type;
			            me.storeSort(type);

			        });
				}
			}
		},me.config,singleresulttable.config,setings);
	};
	
	Class.pt = Class.prototype;

	//初始化系统下拉框
	Class.pt.initSystemSelect = function(){
		var me = this;
		me.GroupItemList('single_SectionItemName','single_SectionItemID');
	};
		  //初始化小组项目
    Class.pt.GroupItemList =  function (CNameElemID, IdElemID) {
        var me = this;
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['LBItem_Id','LBItem_CName','LBItem_SName','LBItem_GroupType','LBItem_Prec'],
			url = GET_SECTIONTEM_LIST_URL + "&where=lbsectionitem.LBSection.Id="+me.config.SECTIONID+" and lbsectionitem.LBItem.GroupType=0";
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
                    { field: 'LBSectionItem_LBItem_GroupType', width: 120, title: '组合项目', sort: false,hide:true }
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
                    
                    var filter = $(me.config.elem).attr("lay-filter");
		        	table_Ind.instance.reload({data:[]});
		
		        	if(table_Ind.config.FORMLIST.length==0){
						uxbase.MSG.onWarn("请先选择检验单!");
						return;
					}
		        	me.onAddItem(record);
                }else{
                	table_Ind.instance.reload({data:[]});
                }
            }
        });
    };
	
	//联动
	Class.pt.initListeners= function(){
		var me =  this;
		
		$('#save_singleresult').on('click',function(){
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
		//添加单项目
	Class.pt.onAddItem = function(record){
		var me = this;
		
	    if(!$("#single_SectionItemID").val())return;
        var arr = [];
	    for(var i=0;i<table_Ind.config.FORMLIST.length;i++){
			var id = table_Ind.config.FORMLIST[i].LisTestForm_Id;
			var obj =table_Ind.config.FORMLIST[i];
			obj.LisTestItem_LBItem_Id = record["LBSectionItem_LBItem_Id"];
			obj.LisTestItem_LBItem_CName = record["LisTestItem_LBItem_CName"];
			obj.LisTestItem_LBItem_Prec = record["LisTestItem_LBItem_Prec"];
			obj.LisTestItem_NewReportValue = "";
			obj.LisTestItem_ReportValue = "";
			obj.LisTestItem_OriglValue = "";
			obj.LisTestItem_RefRange = "";
			obj.LisTestItem_Id = "";
			obj.Exist = 0;

			me.getTestItemByID(id,$("#single_SectionItemID").val(),function(list){
				for(var j=0;j<list.length;j++){
					obj.LisTestItem_NewReportValue = '';
					obj.LisTestItem_ReportValue = list[j].LisTestItem_ReportValue;
					obj.LisTestItem_LBItem_Id = list[j].LisTestItem_LBItem_Id;
					obj.LisTestItem_LBItem_CName = list[j].LisTestItem_LBItem_CName;
					obj.LisTestItem_LBItem_Prec = list[j].LisTestItem_LBItem_Prec;
					obj.LisTestItem_OriglValue = list[j].LisTestItem_OriglValue;
					obj.LisTestItem_RefRange = list[j].LisTestItem_RefRange;
					obj.LisTestItem_Id = list[j].LisTestItem_Id;
					obj.Exist = 1;
				}
			});
			arr.push(obj);
		}
	    
	    table_Ind.instance.reload({data:uxbasic.getStoreList(arr,table_Ind.config.DIRECTION)});
	};
	//根据样本单ID调用样本单项目服务，组合拼成数据行
	Class.pt.getTestItemByID=function(TestFormID,ItemID,callback){
		var me = this;
		var url = GET_LIST_URL + '&fields=LisTestItem_LBItem_Id,LisTestItem_LBItem_CName,LisTestItem_LBItem_Prec,' +
			'LisTestItem_ReportValue,LisTestItem_OriglValue,LisTestItem_RefRange,LisTestItem_Id';
		url+='&where=(listestitem.LisTestForm.Id='+TestFormID +' and listestitem.LBItem.Id='+ItemID+')';
		
		uxutil.server.ajax({
			url:url,
			async: false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	
	};
	//获取listAddTestItem
	Class.pt.getAddTestItem = function(){
		var me = this;
		var items = table_Ind.table.cache.single_item_reportvalue_table,
		    len = items.length,
		    list=[];
		for(var i=0;i<len;i++){
			if(items[i].LisTestItem_NewReportValue){
				var obj ={
					LBItem: {
						Id: items[i].LisTestItem_LBItem_Id,
						CName: items[i].LisTestItem_LBItem_CName,
						Prec: items[i].LisTestItem_LBItem_Prec,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					},
					LisTestForm: {
						Id: items[i].LisTestForm_Id,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					}
				};
				if(items[i].LisTestItem_PItemID){
					obj.PItemID = items[i].LisTestItem_PItemID;
				}
                if(items[i].LisTestItem_Id)obj.Id = items[i].LisTestItem_Id;
				obj.ReportValue = items[i].LisTestItem_NewReportValue;
				list.push(obj);
			}
		}
		return list; 
	};
	
	  /**
     * 保存(批量新增样本单项目结果)
     * list检验单
     * */
	Class.pt.onSave = function(){
	   	var me = this;
	   	if(table_Ind.config.FORMLIST.length ==0){
			uxbase.MSG.onWarn("请先选择检验单!");
			return;
		}
	   	//校验
	   	var list = table_Ind.table.cache.single_item_reportvalue_table;
	   	if(list.length==0){
			uxbase.MSG.onWarn("请选择项目后再保存!");
	   		return;
	   	}
	
        var listAddTestItem = me.getAddTestItem();
		if(listAddTestItem.length==0){
			uxbase.MSG.onWarn("录入报告值不能为空!");
			return;
		}
		var index = layer.load();
	
		var arr=[];
	   	for(var i =0;i<table_Ind.config.FORMLIST.length;i++){
	   		arr.push(table_Ind.config.FORMLIST[i].LisTestForm_Id);
		}
	   	var strArr=arr.splice(',');
	   	var params ={
			testFormID:strArr.join(','),
			listAddTestItem:listAddTestItem,
		    isAddItem: $("#single_isAddItem").prop("checked"),
		    isSingleItem: true
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
	};
	//store 重新按样本号+时间排序
	Class.pt.storeSort = function(direction){
		var me = this;
		if(!direction)return false;
		table_Ind.instance.reload({data:uxbasic.getStoreList(table_Ind.table.cache.single_item_reportvalue_table,direction)});
	};
	//主入口
	singleresulttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_Ind = result;
        me.initListeners();
        me.initSystemSelect();
		return result;
	};
	//暴露接口
	exports('singleresulttable',singleresulttable);
});