/**
	@name：检验样本信息列表
	@author：liangyl	
	@version 2021-05-26
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','table','form','uxbasic','layer'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbasic = layui.uxbasic,
		layer = layui.layer,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
		
	//获取检验样本信息列表数据
	var GET_FORM_INFO_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryItemMergeFormInfo';
    
    var table_Ind = null;
	var patienttable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			cols:[[
			    {type:'checkbox'},
                {field:'LBMergeItemFormVO_PatNo', width:100, title: '病历号', sort: true},
				{field:'LBMergeItemFormVO_CName', width:80, title: '姓名', sort: true},
				{field:'LBMergeItemFormVO_IsMerge', width:70, title: '合并', sort: true,
				templet:function(record){
					var v = record["LBMergeItemFormVO_IsMerge"];
					if(v =='1')v= '<span style="color:green;">是</span>';
					else
					   v= '<span style="color:red;">否</span>';
	                return v;
				}}
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
		},me.config,patienttable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//获取检验样本信息
	Class.pt.ItemMergeFormInfo =  function(callback){
		var  me = this,
    		cols = table_Ind.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var GTestDate = $('#GTestDate').val();
	    var StartDate = GTestDate.split(" - ")[0],
            EndDate = GTestDate.split(" - ")[1];
		var params ={
			beginDate:StartDate,
			endDate:EndDate,
			fields:fields.join(','),
			isPlanish:true,
			itemID:$('#ItemID').val()
		};
		var config = {
			type:'post',
			url:GET_FORM_INFO_LIST_URL,
			data:JSON.stringify(params)
		};
		uxutil.server.ajax(config,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
		
	//数据加载
	Class.pt.loadData = function(whereObj){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		Class.pt.ItemMergeFormInfo(function(list){
			table_Ind.instance.reload({data:list});
		});
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		//监听查询，小组列表
	    form.on('submit(search)', function(data){
	    	var GTestDate = data.field.GTestDate;
			if(!GTestDate){
				uxbase.MSG.onWarn("检验日期不能为空!");
	            return false;
			}
			if(!data.field.ItemID){
				uxbase.MSG.onWarn("合并项目不能为空!");
	            return false;
			}
			var msg = uxbasic.isDateValid({GTestDate:data.field.GTestDate});
	        if (msg != "") {
				uxbase.MSG.onWarn(msg);
	            return false;
	        }
	    	me.loadData();
	    });
	   
	};
	//主入口
	patienttable.render = function(options){
		var me = new Class(options);
		table_Ind = uxtable.render(me.config);
		
		table_Ind.loadData = me.loadData;

        me.initListeners(table_Ind);
		return table_Ind;
	};
	//暴露接口
	exports('patienttable',patienttable);
});