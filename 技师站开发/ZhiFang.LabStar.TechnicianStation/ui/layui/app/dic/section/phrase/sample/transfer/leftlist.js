layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取短语列表数据
	var GET_BPHRASE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true';

	var lefttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:true,
			//原始数据
			oldListData:[],
			cols:[[
			    {type: 'checkbox',fixed: 'left'},
				{field:'LBPhrase_Id',width: 150,title: 'ID',sort: true,hide:true},
			    {field: 'LBPhrase_TypeName',title: '短语类型',minWidth: 80,hide:true},
				{field: 'LBPhrase_CName', title: '短语名称',minWidth: 100},
	            {field: 'LBPhrase_Shortcode',title: '快捷码',minWidth: 80},
	            {field: 'LBPhrase_PinYinZiTou',title: '拼音字头',minWidth: 80},
	            {field: 'LBPhrase_DispOrder',title: '显示次序',minWidth: 80,hide:true},	
	            {field: 'ID',title: '新增标记',minWidth: 80,hide:true}

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
		var url = GET_BPHRASE_LIST_URL+'&fields='+fields;
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