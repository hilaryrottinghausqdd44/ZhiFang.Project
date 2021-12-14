layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取采样管列表数据
	var GET_TCUVETE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTcuveteByHQL?isPlanish=true';

	var tcuveteTable = {
		//参数配置
		config:{
           page: true,
			limit: 50,
			loading : true,
			defaultOrderBy:"[{property: 'LBTcuvete_DispOrder',direction: 'ASC'}]",
			where:"",
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBTcuvete_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBTcuvete_DispOrder',title:'显示次序',width:100,sort:true},
				{field:'LBTcuvete_CName',title:'名称',width:150,sort:true},
				{field:'LBTcuvete_SName',title:'简称',width:100,sort:true},
				{field:'LBTcuvete_Code',title:'简称代码',width:100,sort:true},
				{field:'LBTcuvete_Color',title:'颜色描述',width:100,sort:true},
				{field:'LBTcuvete_SCode',title:'简码',width:80,sort:true},
				{field:'LBTcuvete_Synopsis',title:'采样管说明',width:150,sort:true},	
				{field:'LBTcuvete_Capacity',title:'容量',width:80,sort:true},
				{field:'LBTcuvete_MinCapability',title:'最小采样量',width:120,sort:true},
				{field:'LBTcuvete_Unit',title:'单位',width:80,sort:true},
				{field:'LBTcuvete_ColorValue',title:'颜色值',width:90}
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
		},me.config,tcuveteTable.config,setings);
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
		var url = GET_TCUVETE_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	//主入口
	tcuveteTable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		result.loadData(me.config.where);

		return result;
	};
	//对外公开
	tcuveteTable.onSearch = function(where){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_TCUVETE_LIST_URL;
		var hql = "(" + where + ")";
	    table.reload('tcuvete-table',{
	    	url:url,
	    	where:{
	    		where:where,
	    		sort : me.config.defaultOrderBy,
	    		fields:fields.join(','),
	    	}
	    });
	};
	//暴露接口
	exports('tcuveteTable',tcuveteTable);
});