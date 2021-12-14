layui.extend({
//  dictselect: 'app/pre/common/dictselect'
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
//		dictselect = layui.dictselect,
		uxtable = layui.uxtable;
	
	//检验小组查询服务
	var GET_SECTION_LIST_URL =  uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
	
	var righttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:false,//是否默认加载
			oldListData:[],//初始化数据
			searchInfo: {
				isLike: true,
				fields: ['lbitem.CName','lbitem.EName','lbitem.SName','lbitem.UseCode']
			},
			cols:[[
			    {type: 'checkbox',fixed: 'left'},
			    {type: 'numbers',title: '行号',fixed: 'left'},
	            {field: 'LBSection_Id',width: 60,title: '主键ID',sort: true,hide: true},
                {field:'LBSection_CName', minWidth:150,flex:1, title: '名称', sort: true},
				{field:'LBSection_SName', width:150, title: '简称', sort: true},
                {field:'LBRight_LBSection_Id', width:150, title: '小组ID', hide: true}
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
		},me.config,righttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	 //数据加载
	Class.pt.loadData = function(whereObj,strids){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SECTION_LIST_URL+'&fields='+fields;
		var where ="";
		var searchText = document.getElementById('searchText').value;
		if(searchText) where = "(lbsection.CName like '%"+searchText+"%' or lbsection.SName like '%"+searchText+"%' or lbsection.UseCode like '%"+searchText+"%')"; 
        if(where)where = encodeURI(where);
        if(strids){
        	if(where)where+=' and ';
        	where+= '(lbsection.Id not in('+strids+"))";
        }
        if(where)url+= '&where='+where;
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
	//主入口
	righttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.loadData = me.loadData;
		if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
		return result;
	};
	
	//暴露接口
	exports('righttable',righttable);
});