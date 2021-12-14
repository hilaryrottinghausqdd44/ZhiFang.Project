/**
 * 组合项目子项目与样本单项目合并列表
 * @author liangyl	
 * @version 2021-05-27
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
	
		
    /**获取组合项目子项目*/
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true';
    //获取检验单结果服务
    var GET_TESTITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
    var defaultwhere ='listestitem.MainStatusID in (0,-1)';
    //实例
    var table_ind = null;
	var itemgrouptable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
		    defaultOrderBy:[{property:"LBItemGroup_LBItem_DispOrder",direction:"ASC"}],
			cols:[[
			    {field:'LisTestItem_LBItem_Id', width:150, title: '检验结果项目id',hide:true},
			    {field:'LBItemGroup_LBItem_Id', width:150, title: '组合项目项目id',hide:true},
                {field:'LBItemGroup_LBItem_CName', minWidth:150,flex:1, title: '项目名称'},
				{field:'LBItemGroup_LBItem_SName', minWidth:100,flex:1, title: '项目简称'},
				{field:'LisTestItem_IsExist', width:80,title: '存在',align:'center', templet: '#existTpl'},
				{field:'LisTestItem_ReportValue', width:100, title: '报告值'},
				{field:'LisTestItem_ResultStatus', width:100,title: '状态'},
				{field:'LBItemGroup_LBItem_ItemCharge', width:90, title: '价格'}
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {	
				res.data.forEach(function (item, index) {
					var filter = $(table_ind.config.elem).attr("lay-filter");
					if (item.LisTestItem_IsExist != '1'){
						//背景色-红色
						$('div[lay-id="'+filter+'"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LisTestItem_IsExist"]').
						css('background-color', 'red');
					}
				});
            }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var type = typeof res.ResultDataValue,
					data = res.ResultDataValue || {},
					list = [];
					
				if(res.success){
					if(type == 'string'){
						data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					}
					list = me.changeData(data);
					
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
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
		},me.config,itemgrouptable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据处理
	Class.pt.changeData =function(data){
		var list = [];
		var itemlist = Class.pt.testItemList(function(data2){
			for(var i in data.list){
				data.list[i].LisTestItem_IsExist = '';
				for(var j=0;j<data2.length;j++){
					if(data.list[i].LBItemGroup_LBItem_Id == data2[j].LisTestItem_LBItem_Id){
						data.list[i].LisTestItem_LBItem_Id = data2[j].LisTestItem_LBItem_Id;
						data.list[i].LisTestItem_ReportValue = data2[j].LisTestItem_ReportValue;
						data.list[i].LisTestItem_ResultStatus = data2[j].LisTestItem_ResultStatus;
						data.list[i].LisTestItem_IsExist = '1';
						break;
					}
				}
				list.push(data.list[i]);
			}
		});
		return list;
	};
	//数据加载
	Class.pt.loadData = function(whereObj){
		var  me = this,
    		cols = me.config.cols[0],
			fields = ['LBItemGroup_Id','LBItemGroup_LBGroup_Id','LBItemGroup_LBItem_Id','LBItemGroup_LBItem_CName','LBItemGroup_LBItem_SName','LBItemGroup_LBItem_ItemCharge'];
	var fi ='LBItemGroup_Id,LBItemGroup_DataAddTime,LBItemGroup_LBGroup_Id,LBItemGroup_LBItem_Id,LBItemGroup_LBItem_CName,LBItemGroup_LBItem_EName,LBItemGroup_LBItem_SName,LBItemGroup_LBItem_GroupType,LBItemGroup_LBItem_ItemCharge,LBItemGroup_LBItem_DispOrder';
	    var url = GET_LIST_URL+'&fields='+fields.join(',')+'&where=lbgroup.Id='+table_ind.config.ID;
	    url+='&sort='+	JSON.stringify(me.config.defaultOrderBy);
	    me.instance.reload({
			url:url
		});
	};
		//数据加载
	Class.pt.clearData= function(whereObj){
		var filter = $(table_ind.config.elem).attr("lay-filter");
		table_ind.instance.config.instance.layMain.html('<div class="layui-none">暂无数据</div>');
	};
	Class.pt.testItemList =  function(callback){
		var fields = ['LBItem_Id','ReportValue','ResultStatus'],
			url = GET_TESTITEM_LIST_URL + "&where=listestitem.LisTestForm.Id="+table_ind.config.TESTFORMID + ' and '+defaultwhere;
		url += '&fields=LisTestItem_' + fields.join(',LisTestItem_');
		uxutil.server.ajax({
			url:url,
			async:false   //同步加载
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	}
	//主入口
	itemgrouptable.render = function(options){
		var me = new Class(options);
		table_ind = uxtable.render(me.config);
		table_ind.loadData = me.loadData;
		table_ind.clearData = me.clearData;
		return table_ind;
	};
	
	//暴露接口
	exports('itemgrouptable',itemgrouptable);
});