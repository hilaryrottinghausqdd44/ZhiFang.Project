/**
	@name：添加检验项目
	@author：liangyl	
	@version 2021-05-20
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
	
		
	//获取项目列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
	//批量新增样本单项目
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItem';
   //已选检验单
    var FORMLIST=[];
    
    var CHECK_ROW = [];
    var SEARCHOBJ ={};
    var table_ind = null;
	var itemtable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			SECTIONID:null,
			cols:[[            
				{type: 'numbers',title: '行号',fixed: 'left'},
			    {field:'LisTestItem_PLBItem_CName', width:100,title: '组合项目', sort: false},
			    {field:'LisTestItem_LBItem_Id', width:100,title: '项目id', sort: false,hide: true},
                {field:'LisTestItem_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: false},
				{field:'LisTestItem_LBItem_SName', width:150, title: '项目简称', sort: false},
				{field:'LisTestItem_PItemID', width:100, title: '组合项目ID', sort: false,hide:true},
				{field:'LisTestItem_Id', minWidth:100,flex:1, title: '检验项目ID', sort: false,hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function (res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
//					//默认选择第一行
//					var rowIndex = 0;
//		            //默认选择行
//				    Class.pt.doAutoSelect(this,rowIndex);
				    
			    }else{
			    	CHECK_ROW=[];
			    }
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
					    CHECK_ROW=obj;
					});
				}
			}
		},me.config,itemtable.config,setings);
	};
	
	Class.pt = Class.prototype;
    //数据加载
	Class.pt.loadData = function(obj){
		SEARCHOBJ = obj;
		var cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		me.instance.reload({
			url:GET_LIST_URL,
			where:$.extend({},{"where":obj},{
				fields:fields.join(',')
			})
		});
	};
	
    //加载批量检验单项目的数据
	Class.pt.loadDataByItem = function(){
		var me = this;
		//只加载存在此次批量新增检验单项目的的数据
		var id = table_ind.config.FORMLIST[0].LisTestForm_Id;
		if(!id)return;
		
	    var StrID = me.getStrID(table_ind.table.cache.item_table);
	    if(StrID.length==0)return;
	    var where = "listestitem.LisTestForm.Id="+id+' and listestitem.LBItem.Id in('+StrID+') and listestitem.MainStatusID=0';
	    table_ind.loadData(SEARCHOBJ);
	};
	//获取批量新增的检验项目
	Class.pt.getStrID = function(items){
		var me = this;
	    var arr=[],strIds="",str="";
	    for(var i=0;i<items.length;i++){
	    	arr.push(items[i].LisTestItem_LBItem_Id);
	    }
	    if(arr.length>0)str = arr.splice(',');
	    if(str.length>0)strIds = str.join(',');
		return strIds;
	},
	//获取listAddTestItem
	Class.pt.getAddTestItem = function(items,testFormID){
		var me = this,
		    list=[];
		for(var i=0;i<items.length;i++){
			var obj ={
				LBItem:{Id:items[i].LisTestItem_LBItem_Id,DataTimeStamp:[0,0,0,0,0,0,0,0]}
			};
			
			if(items[i].LisTestItem_PItemID){
				obj.PLBItem = {Id:items[i].LisTestItem_PItemID,DataTimeStamp:[0,0,0,0,0,0,0,0]};
			}
			list.push(obj);
		}
		return list; 
	};
	//执行添加项目
	Class.pt.onSaveClick = function(formlist,list){
		var me = this;
		if(formlist.length==0){
			uxbase.MSG.onWarn("请先选择检验单!");
   	    	return;
   	    }
	   	//校验
	   	if(list.length==0){
			uxbase.MSG.onWarn("请选择添加项目后再保存!");
	   		return;
	   	}
	   	var testFormID="",arr=[];
	   	for(var i =0;i<formlist.length;i++){
	   		arr.push(formlist[i].LisTestForm_Id);
		}
	   	testFormID = arr.splice(',');
	   	var params ={ 
			testFormID:testFormID.join(','),
			listAddTestItem:Class.pt.getAddTestItem(list,testFormID),
			isRepPItem:$("#isRepPItem").prop("checked") ? 0 : 1  
		};
		var config = {
			type:'post',
			url:ADD_URL,
			data:JSON.stringify(params)
		};
       
		uxutil.server.ajax(config,function(data){
			if(data.success){
				uxbase.MSG.onSuccess("保存成功!");
				me.loadDataByItem();
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	},
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		//选项目
	   $('#additem').on('click',function(){
		   	var win = $(window),
				maxWidth = win.width()-80,
				maxHeight = win.height()-10;
	    	layer.open({
				title:'选择项目',
				type:2,
				content:'item/transfer/index.html?sectionId='+me.config.SECTIONID+'&module=additem&t=' + new Date().getTime(),
				maxmin:true,
				toolbar:true,
				resize:true,
				area:[maxWidth+'px',maxHeight+'px']
			});
	    });
	    
	    //执行添加项目
	    $('#save_item').on('click',function(){
	   	    me.onSaveClick(table_ind.config.FORMLIST,table_ind.table.cache.item_table);
	    });
	    //删除
	   $('#del_item').on('click',function(){
	   	    if(CHECK_ROW.length==0){
				uxbase.MSG.onWarn("请选择行!");
	   	    	return;
	   	    }
	     	layer.confirm('确定删除选中项', { icon: 3, title: '提示' }, function(index) {
		        CHECK_ROW.del();
		        layer.close(index);
		    });
	    });
	     
	};
	//主入口
	itemtable.render = function(options){
		var me = new Class(options);
		me.initListeners();
		table_ind= uxtable.render(me.config);
		return table_ind;
	};
	function afterUpdate (items){
		layer.closeAll('iframe');
        var list = table_ind.table.cache.item_table;
        for(var i=0;i<items.length;i++){
         	var isAdd = true;
         	for(var j=0;j<list.length;j++){
         		if(list[j].LisTestItem_LBItem_Id == items[i].LBSectionItem_LBItem_Id) {
         		   //如果列表存在非组合项目，需要替换为组合项目
					if(list[j] && !list[j].LisTestItem_PItemID){
						list[j].LisTestItem_PItemID=items[i].LBSectionItem_LBItem_GroupItemID;
					}
					isAdd = false;
					break;
         		}
         	}
         	if(isAdd == true){
			    var result = JSON.stringify(items[i]);
			    var obj = result.replace(/LBSectionItem/g,"LisTestItem");
	            obj = JSON.parse(obj);
	            obj.LisTestItem_PLBItem_CName = items[i].LBSectionItem_LBItem_GroupItemCName;
	            obj.LisTestItem_PItemID = items[i].LBSectionItem_LBItem_GroupItemID;
	            list.push(obj);
			}
        }
		table_ind.instance.reload({data:list});
       
    }
	window.afterAddUpdate = afterUpdate;

	//暴露接口
	exports('itemtable',itemtable);
});