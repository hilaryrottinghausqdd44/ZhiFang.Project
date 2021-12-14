/**
	@name：删除检验项目
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
	
		
	    //批量删除样本单项目
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestItemByTestFormIDList';
   //当前选择行
    var CHECK_ROW = [];
    //实例
    var table_ind = null;
	var delitemtable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			SECTIONID:null,
			cols:[[   
				{type: 'numbers',title: '行号',fixed: 'left'},
			    {field:'LisTestItem_PLBItem_CName', width:100,title: '组合项目', sort: true},
			    {field:'LisTestItem_LBItem_Id', width:100,title: '项目id', sort: true,hide: true},
                {field:'LisTestItem_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
				{field:'LisTestItem_LBItem_SName', width:150, title: '项目简称', sort: true},
				{field:'LisTestItem_PItemID', width:100, title: '组合项目ID', sort: true,hide:true},
				{field:'LisTestItem_Id', minWidth:100,flex:1, title: '检验项目ID', sort: true,hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function (res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
					//默认选择第一行
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
		},me.config,delitemtable.config,setings);
	};
	
	Class.pt = Class.prototype;
    //数据加载
	Class.pt.loadData = function(obj){
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
	Class.pt.loadDataByItem = function(formlist,list){
		var me = this;
		//只加载存在此次批量新增检验单项目的的数据
		var id = formlist[0].data.LisTestForm_Id;
		if(!id)return;
	    var StrID = me.getStrID(list);
	    if(StrID.length==0)return;
	    var where = "listestitem.LisTestForm.Id="+id+' and listestitem.LBItem.Id in('+StrID+') and listestitem.MainStatusID=0';
	    parent.afterSaveItemUpdate(where);//执行刷新
	};
	//获取批量新增的检验项目
	Class.pt.getStrID = function(items){
		var me = this;
	    var arr=[],strIds="",str="";
	    for(var i=0;i<items.length;i++){
	    	arr.push(items[i].data.LisTestItem_LBItem_Id);
	    }
	    if(arr.length>0)str = arr.splice(',');
	    if(str.length>0)strIds = str.join(',');
		return strIds;
	},
	//执行删除项目
	Class.pt.onDelClick = function(formlist,list,callback){
		var me = this;
	   	var testFormID="",arr=[];
	   	for(var i =0;i<formlist.length;i++){
	   		arr.push(formlist[i].LisTestForm_Id);
		}
	   	testFormID = arr.splice(',');
	   	var  itemIDList =[];
	   	for (var j = 0; j < list.length; j++) {
			itemIDList.push(list[j].LisTestItem_LBItem_Id);
		}
	   	var params ={ 
	   		itemIDList:itemIDList.join(','),
	   		testFormIDList:testFormID.join(','),
			isDelNoResultItem:$("#isDelNoResultItem").prop("checked"), //仅删除没有结果的项目
			isDelNoOrderItem:$("#isDelNoOrderItem").prop("checked")  //仅删除非医嘱项目
		};
		var config = {
			type:'post',
			url:DEL_URL,
			data:JSON.stringify(params)
		};
       
		uxutil.server.ajax(config,function(data){
			if(data.success){
				uxbase.MSG.onSuccess("保存成功!");
				callback();
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	   	
	},
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		//选项目
	   $('#del_additem').on('click',function(){
		   	var win = $(window),
				maxWidth = win.width()-80,
				maxHeight = win.height()-10;
	    	layer.open({
				title:'选择项目',
				type:2,
				content:'item/transfer/index.html?sectionId='+me.config.SECTIONID+'&module=delitem&t=' + new Date().getTime(),
				maxmin:true,
				toolbar:true,
				resize:true,
				area:[maxWidth+'px',maxHeight+'px']
			});
	    });
	    //删除
	   $('#del_delitem').on('click',function(){
	     	layer.confirm('确定删除吗?', { icon: 3, title: '提示' }, function(index) {
		        if(table_ind.config.FORMLIST.length==0){
					uxbase.MSG.onWarn("请先选择检验单!");
		   	    	return;
		   	    }
			    var list = table_ind.table.cache.del_item_table;
			   	//校验
			   	if(list.length==0){
					uxbase.MSG.onWarn("请选择项目后再删除!");
			   		return;
			   	}
		        me.onDelClick(table_ind.config.FORMLIST,list,function(){
		        	layer.close(index);
		        })
		    });
	    });
	     
	};
	//主入口
	delitemtable.render = function(options){
		var me = new Class(options);
		me.initListeners();
		table_ind = uxtable.render(me.config);
//	    table_ind.loadData = me.loadData;
//		table_ind.onDelClick = me.onDelClick;
		return table_ind;
	};
	function afterUpdate (items){
		layer.closeAll('iframe');
        var list = table_ind.table.cache.del_item_table;
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
	window.afterDelUpdate = afterUpdate;

	//暴露接口
	exports('delitemtable',delitemtable);
});