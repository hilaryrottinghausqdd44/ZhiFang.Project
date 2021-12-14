/**
	@name：小组历史对比列表
	@author：liangyl	
	@version 2019-11-05
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取小组历史对比列表数据
	var GET_HIS_COMP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionHisCompByHQL?isPlanish=true';
  
	var lefttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:true,
			//原始数据
			oldListData:[],
			size: 'sm', //小尺寸的表格
			cols:[[
			    {type: 'checkbox',fixed: 'left'},
				{field:'LBSectionHisComp_Id',width: 150,title: '主键',sort: true,hide:true},
				{field:'LBSectionHisComp_HisComp_Id',width: 150,title: '小组编号',sort: true,hide:true},
                {field:'LBSectionHisComp_HisComp_CName', widthidth:150, title: '小组名称', sort: false},
                {field:'LBSectionHisComp_HisComp_SName', width:150, title: '简称', sort: true},
				{field:'LBSectionHisComp_HisComp_UseCode', width:100, title: '用户代码', sort: true,hide:false}
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
		},me.config,lefttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//主入口
	lefttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
        result.loadData = me.loadData;
        if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
		return result;
	};
	 //数据加载
	Class.pt.loadData = function(where){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var hql =  where ;
		var url = GET_HIS_COMP_LIST_URL+'&fields='+fields;
		if(where)url+= '&where='+hql;
		if(me.config.defaultOrderBy)url+='&sort='+me.config.defaultOrderBy;
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = [];
				if(data.value)list=data.value.list;
				me.config.oldListData=list;
                me.instance.reload({data:list});
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//暴露接口
	exports('lefttable',lefttable);
});