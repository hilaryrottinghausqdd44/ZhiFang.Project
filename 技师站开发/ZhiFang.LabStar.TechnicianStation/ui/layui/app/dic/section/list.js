/**
	@name：小组列表
	@author：liangyl	
	@version 2019-05-31
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
	//获取检验小组列表数据
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';

	var sectionTable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			defaultOrderBy:"[{property: 'LBSection_DispOrder',direction: 'ASC'}]",
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field: 'LBSection_Id',width: 60,title: '主键ID',sort: true,hide: true},
                {field:'LBSection_CName', minWidth:150,flex:1, title: '名称', sort: true},
				{field:'LBSection_SName', width:150, title: '简称', sort: true},
				{field:'LBSection_EName', width:150, title: '英文名称', sort: true},
				{field:'LBSection_Shortcode', width:150, title: '快捷码', sort: true},
				{field:'LBSection_DispOrder', width:100, title: '显示次序', sort: true,hide:true},
				{field:'LBSection_Comment', minWidth:100,flex:1, title: '小组描述', sort: true,hide:true},
				{field:'LBSection_UseCode', width:100, title: '用户代码', sort: true,hide:true},
				{field:'LBSection_ProDLL', width:100, title: '专业编辑', sort: true,hide:true}
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
		},me.config,sectionTable.config,setings);
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
		var url = GET_SECTION_LIST_URL+'&fields='+fields.join(',')+'&sort='+me.config.defaultOrderBy;

        var obj ={};
		var searchValue = $('#searchText').val();
		if(searchValue){
			var hql="(lbsection.CName like '%" + searchValue + 
	    		"%' or lbsection.EName like '%" + searchValue + "%' or lbsection.SName like '%" + 
	    		searchValue + "%' or Shortcode like '%" +
	    		searchValue + "%')";
			obj.where=hql;
		}
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		//监听查询，小组列表
	    form.on('submit(search)', function (data) {
	    	result.loadData({});
	    });
	};
	//主入口
	sectionTable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		result.loadData(me.config.where);
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('sectionTable',sectionTable);
});