/**
	@name：检验中权限列表
	@author：liangyl
	@version 2019-11-14
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取检验中权限列表数据
	var GET_LBRIGHT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true';

	var righttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:false,
			data:[],	
			defaultOrderBy:"[{property: 'LBRight_DataAddTime',direction: 'ASC'}]",
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBRight_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBRight_EmpID',title:'LBRight_EmpID',width:150,sort:true,hide:true},
				{field:'LBRight_LBSection_Id',title:'LBRight_LBSection_Id',width:150,sort:true,hide:true},
				{field:'LBRight_LBSection_CName',title:'小组名称',width:150,sort:true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
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
		},me.config,righttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,EmpID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LBRIGHT_LIST_URL+'&fields='+fields.join(',')+'&sort='+me.config.defaultOrderBy;
		var where = 'lbright.EmpID='+EmpID +' and lbright.LBSection.Id is not null and lbright.RoleID is null';
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				where:where
			})
		});
	};
	//主入口
	righttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		if(me.config.defaultLoad){
			//加载数据
			result.loadData();
		}
        result.OpenWin = me.OpenWin;
		return result;
	};
	//弹出小组选择框
	Class.pt.OpenWin = function(EmpID,EmpName){
		var me = this;
		var win = $(window),
			maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 800 ? maxWidth : 800,
			height = maxHeight > 600 ? maxHeight : 600;
		layer.open({
			title:'选择小组',
			type:2,
			content: '../role/transfer/index.html',
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px'],
			success: function(layero, index){
       	        var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
       	        body.find('#EmpID').val(EmpID);
       	        body.find('#EmpCName').html(EmpName);
       	        var filter = $(me.config.elem).attr("lay-filter");
                var tableData = table.cache[filter];
                var strId=[];
                for(var i=0;i<tableData.length;i++){
                	strId.push(tableData[i].LBRight_LBSection_Id);
                }
       	        body.find('#groupID').val(strId.join(","));
	        },
	        cancel: function (index, layero) {
	        	parent.layer.closeAll('iframe');
            }
		});
	};
	Class.pt.initListeners = function(){
	
	};
	
	//暴露接口
	exports('righttable',righttable);
});