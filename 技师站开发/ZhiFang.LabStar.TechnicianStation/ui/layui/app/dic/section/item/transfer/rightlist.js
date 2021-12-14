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
	
	//获取项目列表数据
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true';
    //检验小组查询服务
	var GET_SECTION_LIST_URL =  uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
	
	var righttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:true,//是否默认加载
			oldListData:[],//初始化数据
			SectionID:null,//小组
			groupItemID:null,//小组项目
			searchInfo: {
				isLike: true,
				fields: ['lbitem.CName','lbitem.EName','lbitem.SName','lbitem.UseCode']
			},
			cols:[[
			    {type: 'checkbox',fixed: 'left'},
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBItem_Id',width: 150,title: '项目编号',sort: true},
                {field:'LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
			    {field:'LBItem_EName', width:150, title: '英文名称', sort: true},
                {field:'LBItem_UseCode', width:150, title: '用户编码', sort: true},
                {field:'LBSectionItemVO_LBSectionItem_Id', width:150, title: '小组项目ID', hide: true}
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
	Class.pt.loadData = function(whereObj,searchval,groupid,strids){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ITEM_LIST_URL+'&fields='+fields;
		var where ="";
		if(searchval) where = "(lbitem.CName like '%"+searchval+"%' or lbitem.EName like '%"+searchval+"%' or lbitem.UseCode like '%"+searchval+"%')"; 
        if(groupid)url+='&sectionID='+groupid;
        if(where)where = encodeURI(where);
        if(me.config.groupItemID){
        	if(where)where+=' and ';
        	where+= 'lbitem.Id not in('+me.config.groupItemID+")";
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
		//下拉框数据加载
		me.initSelectSection();
		var result = uxtable.render(me.config);
		result.loadData = me.loadData;
		if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
		return result;
	};
	//初始化检验小组下拉选择项
	Class.pt.initSelectSection = function(){
		var me = this;
		var url = GET_SECTION_LIST_URL+ '&lbsection.IsUse=1'+
		'&fields=LBSection_CName,LBSection_Id';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
				var tempAjax = "<option value=''>请选择</option>";
                if(!value)return;
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSection_Id + "'>" + value.list[i].LBSection_CName + "</option>";
                    $("#select_section").empty();
                    $("#select_section").append(tempAjax);
                    
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
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