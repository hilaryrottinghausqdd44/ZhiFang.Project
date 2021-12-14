/**
	@name：人员表
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
	
		
	//获取人员表列表数据
	var GET_EMP_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true';

	var emptable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'HREmpIdentity_HREmployee_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'HREmpIdentity_HREmployee_CName',title:'员工姓名',width:150,sort:true},
				{field:'HREmpIdentity_HREmployee_UseCode',title:'员工代码',width:100,sort:true},
				{field:'HREmpIdentity_HREmployee_HRDept_CName',title:'隶属部门',width:150,sort:true},
				{field:'HREmpIdentity_HREmployee_IsUse',title:'使用',width:100,sort:true,templet: '#isuseTpl'}
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
		},me.config,emptable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_EMP_LIST_URL;
		
		var where = me.config.where;
		var searchText = document.getElementById('searchText').value;
        if(searchText){
        	where+=" and (hrempidentity.HREmployee.CName like '%" + searchText +
	    		"%' or hrempidentity.HREmployee.UseCode like '%" + searchText +"%')";
	    }
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy,
				where:where
			})
		});
	};
	//主入口
	emptable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		result.loadData();

		return result;
	};
	Class.pt.initListeners = function(){
//		
//		$('#search').on('click',function(){
////			var 
//		});
	};
	
	//暴露接口
	exports('emptable',emptable);
});