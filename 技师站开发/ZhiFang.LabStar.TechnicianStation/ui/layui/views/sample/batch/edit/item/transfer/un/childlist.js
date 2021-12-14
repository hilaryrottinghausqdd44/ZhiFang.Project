/**
	@name：未选项目
	@author：liangyl	
	@version 2021-05-19
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
	//获取项目列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true';

	var childtable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			/**项目类型,0-全部项目,1-医嘱项目，2-组合项目*/
	        type:'0' ,
	        defaultOrderBy: [{ property: "LBItemGroup_LBItem_DispOrder", direction: "ASC" }],
            cols:[[
				{field:'LBItemGroup_LBItem_Id',width: 60,title: '项目id',sort: true,hide: true},
                {field:'LBItemGroup_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
				{field:'LBItemGroup_LBItem_SName', width:100, title: '项目简称', sort: true},
				{field:'LBItemGroup_LBItem_UseCode', width:80, title: '用户代码', sort: true},
				{field:'LBItemGroup_LBItem_PinYinZiTou', width:80, title: '拼音字头', sort: true}
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
		},me.config,childtable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(GroupItemID){
		var  me = this,
    		cols = me.config.cols[0],
    		where ='',
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
	    //组合项目id
		if(GroupItemID)where = "(GroupItemID="+GroupItemID+")";
        var obj ={"where":where};
		me.instance.reload({
			url:url,
			where:$.extend({},obj,{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
	};
	//主入口
	childtable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.instance.reload({data:[]});
		result.loadData = me.loadData;
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('childtable',childtable);
});