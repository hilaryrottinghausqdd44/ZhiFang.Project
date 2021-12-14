/**
	@name：没有设置采样组的项目信息列表
	@author：liangyl	
	@version 2019-09-27
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取没有设置采样组的项目信息列表数据
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryItemNoSamplingGroup?isPlanish=true';
	
	var itemtable = {
		//参数配置
		config:{
             page: false,
			limit: 5000,
			loading : true,
			defaultOrderBy:"[{property: 'LBSamplingGroup_DispOrder',direction: 'ASC'}]",
			GroupID:null,
			CheckRowData:null,
			cols:[[
//			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBItem_Id',width: 150,title: '项目编号',sort: true,hide:true},
				{field:'LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
			    {field:'LBItem_EName', width:150, title: '英文名称', sort: true},
                {field:'LBItem_UseCode', width:150, title: '用户编码', sort: true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
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
						me.config.CheckRowData = obj;
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,itemtable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,GroupID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ITEM_LIST_URL;
		var obj = {
			fields:fields.join(','),
		    sort:me.config.defaultOrderBy
		};
		me.config.GroupID = GroupID;
		if(GroupID)obj.strSectionID = me.config.GroupID;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
		/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	itemtable.doAutoSelect = function(that, rowIndex) {
		var me = this;
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 300);
	};
	//主入口
	itemtable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		result.loadData(me.config.where);

		return result;
	};
	//对外公开
	itemtable.onSearch = function(groupid){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ITEM_LIST_URL;
	    me.config.GroupID = groupid;

	    table.reload('onerow-item-table',{
	    	url:url,
	    	where:{
	    		where:me.config.where,
	    		sort:me.config.defaultOrderBy,
	    		strSectionID : groupid,
	    		fields:fields.join(',')
	    	}
	    });
	};
	//暴露接口
	exports('itemtable',itemtable);
});