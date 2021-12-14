layui.extend({
	
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取采样组列表数据
	var GET_SAMPLINGGROUP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingGroupByHQL?isPlanish=true';
   
	var SamplingGroupList = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			defaultOrderBy:"[{property: 'LBSamplingGroup_DispOrder',direction: 'ASC'}]",
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBSamplingGroup_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBSamplingGroup_CName',title:'采样组名称',width:150,sort:false},
				{field:'LBSamplingGroup_SName',title:'简称',width:80,sort:false},
				{field:'LBSamplingGroup_SCode',title:'简码',width:80,sort:false},
				{field:'LBSamplingGroup_Synopsis',title:'说明',width:100,sort:false},
				{field:'LBSamplingGroup_PrintCount',title:'条码打印份数',width:150,sort:false},	
				{field:'LBSamplingGroup_AffixTubeFlag',title:'打包机通道',width:100},
			    {field:'LBSamplingGroup_VirtualNo',title:'虚拟最大项目数',width:150,sort:false},
			    {field:'LBSamplingGroup_PrepInfo',title:'预制条码匹配信息',width:160,sort:false},
				{field:'LBSamplingGroup_DispOrder',title:'显示次序',width:90}
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
		},me.config,SamplingGroupList.config,setings);
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
		if(!whereObj)whereObj={};
        if($('#search_val').val()){
    		var hql="(lbsamplinggroup.CName like '%" + $('#search_val').val() +
    		"%' or lbsamplinggroup.SCode like '%" + $('#search_val').val() + "%' or lbsamplinggroup.SName like '%" +$('#search_val').val()+"%')";
    	    whereObj = {'where':hql};
        }
        
		var url = GET_SAMPLINGGROUP_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	//主入口
	SamplingGroupList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.loadData = me.loadData;
		return result;
	};
	//对外公开
//	SamplingGroupList.onSearch = function(where){
//		var  me = this,
//  		cols = me.config.cols[0],
//			fields = [];
//		for(var i in cols){
//			if(cols[i].field)fields.push(cols[i].field);
//		}
//		var url = GET_SAMPLINGGROUP_LIST_URL;
//	    table.reload('samplinggroup-table',{
//	    	url:url,
//	    	where:{
//	    		where:where,
//	    		sort:me.config.defaultOrderBy,
//	    		fields:fields.join(',')
//	    	}
//	    });
//	};
	//暴露接口
	exports('SamplingGroupList',SamplingGroupList);
});