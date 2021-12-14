/**
	@name：样本短语列表
	@author：liangyl	
	@version 2019-10-31
 */
layui.extend({
}).define(['uxutil','uxtable','table','commonzf'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		commonzf = layui.commonzf,
		uxtable = layui.uxtable;
	
		
	//获取检验小组列表数据
	var GET_SECTION_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?isPlanish=true';

	var sampletable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBSection_DispOrder',direction: 'ASC'}]",
			defaultLoad:true,
			cols:[[
			    {field:'Id',width: 150,title: '主键',sort: true,hide:true},
				{field:'Code',width: 150,title: 'Code',sort: true,hide:true},	
                {field:'Name', minWidth:150,flex:1, title: '短语分类', sort: false}
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
		},me.config,sampletable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
			
	    commonzf.classdict.init('SamplePhrase',function(){
			var list=commonzf.classdict.SamplePhrase;
			me.instance.reload({data:list});
		}); 
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		
	};
	//主入口
	sampletable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		if(me.config.defaultLoad)result.loadData();
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('sampletable',sampletable);
});