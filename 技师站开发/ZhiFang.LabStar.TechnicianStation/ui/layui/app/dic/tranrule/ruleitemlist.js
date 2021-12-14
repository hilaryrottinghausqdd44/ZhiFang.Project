/**
	@name：分发规则项目明细
	@author：liangyl
	@version 2020-08-16
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
		
	//获取分发规则项目明细列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleItemByHQL?isPlanish=true';
	var table_ind = null;
	
	var RuleItemList = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			cols:[[
			    {type: 'checkbox', fixed: 'left'},
				{field:'LBTranRuleItem_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBTranRuleItem_TranRuleID',title:'规则ID',width:100,hide:true},
				{field:'LBTranRuleItem_LBItem_Id',title:'项目id',width:100,hide:true},
				{field:'LBTranRuleItem_LBItem_CName',title:'项目名称',minWidth:100,flex:1}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
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
		},me.config,RuleItemList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(TranRuleID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	    table_ind.TranRuleID = TranRuleID; 
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var whereObj = {'where':'lbtranruleitem.LBTranRule.Id='+TranRuleID};
		me.instance.reload({
			url:GET_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	Class.pt.clearData = function(id){
		var  me = this;
		
		me.instance.reload({
			url:'',
			data:[]
		});
	};
	 //删除方法 
	Class.pt.onDelClick = function(){
		var me = this;
		//获取选择行
//		var checkStatus = table.checkStatus('idTest')
//         ,data = checkStatus.data;
//      if(!id)return;
//  	var url = DEL_URL +'?id='+ id;
//	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
//	        uxutil.server.ajax({
//				url: url
//			}, function(data) {
//				layer.closeAll('loading');
//				if(data.success === true) {
//					layer.close(index);
//                  layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
//                  table_ind.loadData({});
//				}else{
//					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
//				}
//			});
//      });
	};
	//打开选择项目窗体
    Class.pt.openWinForm = function(RuleID,RuleName,SectionID){
		layer.open({
			title:'分发规则项目',
			type:2,
			content:'tranruleitem/transfer/app.html?RuleID=' + RuleID +'&RuleName=' + RuleName +'&SectionID=' + SectionID +'&t=' + new Date().getTime(),
			maxmin:false,
			toolbar:true,
			resize:false,
			area:['95%','95%']
		});
	};
	//联动监听
	Class.pt.initListeners = function(){
		var me = this;
	};
	//主入口
	RuleItemList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.clearData= me.clearData;
		result.onDelClick = me.onDelClick;
		result.openWinForm = me.openWinForm;
		me.initListeners();
		return result;
	};
	//选择项目保存后刷新
    function afterUpdateTranRuleItem(){
		table_ind.loadData(table_ind.TranRuleID);
	}
	window.afterUpdateTranRuleItem = afterUpdateTranRuleItem;
	//暴露接口
	exports('RuleItemList',RuleItemList);
});