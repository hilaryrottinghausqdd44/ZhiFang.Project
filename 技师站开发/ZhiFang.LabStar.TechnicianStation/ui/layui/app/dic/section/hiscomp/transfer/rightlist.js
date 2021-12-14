/**
	@name：小组列表
	@author：liangyl	
	@version 2019-11-05
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
	//获取小组列表数据
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
	
	var righttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:false,//是否默认加载
			oldListData:[],//初始化数据
			SectionID:null,//小组
			groupItemID:null,//小组项目
			size: 'sm', //小尺寸的表格
			searchInfo: {
				isLike: true,
				fields: ['lbsection.CName','lbsection.SName','lbsection.UseCode']
			},
			cols:[[
			    {type:'checkbox',fixed: 'left'},
			    {type:'numbers',title: '行号',fixed: 'left'},
				{field:'LBSection_Id',width: 60,title:'主键ID',sort: true,hide: true},
                {field:'LBSection_CName', minWidth:150,flex:1, title: '名称', sort: true},
				{field:'LBSection_SName', width:150, title: '简称', sort: true},
				{field:'LBSection_UseCode', width:100, title: '用户代码', sort: true,hide:false},
				{field:'LBSectionHisComp_Id',width: 150,title: 'LBSectionHisComp_Id',sort: true,hide:true},
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
	Class.pt.loadData = function(whereObj,searchval,strids){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SECTION_LIST_URL+'&fields='+fields;
		var where ="";
		if(searchval) where = "(lbsection.CName like '%"+searchval+"%' or lbsection.SName like '%"+searchval+"%' or lbsection.UseCode like '%"+searchval+"%')"; 
        if(where)where = encodeURI(where);
        
        me.config.groupItemID = strids;
        
        if(me.config.groupItemID){
        	if(where)where+=' and ';
        	where+= 'lbsection.Id not in('+me.config.groupItemID+")";
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
	
		/**获取查询框内容*/
	Class.pt.getSearchWhere = function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.config.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];
		for (var i = 0; i < len; i++) {
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	};
	//暴露接口
	exports('righttable',righttable);
});