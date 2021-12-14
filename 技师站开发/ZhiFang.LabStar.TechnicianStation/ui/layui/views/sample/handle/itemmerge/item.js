/**
 * 合并项目列表
 * @author liangyl	
 * @version 2021-05-26
 */
layui.extend({
}).define(['uxutil','uxbase','uxtable','table','form','uxbasic','laytpl'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		laytpl = layui.laytpl,
		uxbasic = layui.uxbasic,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
	
	//获取合并项目信息列表数据
	var GET_ITEM_INFO_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryItemMergeInfo';
      var table_Ind = null;
    
	var itemtable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			cols:[[		
			    {field:'LBMergeItemVO_ChangeItemID', minWidth:120,flex:1,title: '转化项目', templet: '#itemTpl'},
				{field:'LBMergeItemVO_ChangeItemName', minWidth:120,flex:1, title: '转化项目',hide:true},
                {field:'LBMergeItemVO_ParItemID', width:100, title: '组合ID',hide:true},
				{field:'LBMergeItemVO_ParItemName', minWidth:100,flex:1, title: '组合'},
				{field:'LBMergeItemVO_LisTestItem_LBItem_CName', minWidth:100,flex:1, title: '单项'},
				{field:'LBMergeItemVO_LisTestItem_LBItem_Id', width:80, title: '单项ID',hide:true},
				{field:'LBMergeItemVO_LisTestItem_LisTestForm_GSampleNo', width:90, title: '样本号'},
				{field:'LBMergeItemVO_LisTestItem_Id', width:80, title: '检验单项目ID',hide:true},
				{field:'LBMergeItemVO_LisTestItem_ReportValue', width:100, title: '项目结果'},
			    {field:'LBMergeItemVO_IsMerge', width:70, title: '合并',align:'center', templet: '#ismergeTpl'},
				{field:'LBMergeItemVO_LisTestItem_TestTime', width:100, title: '时间'},
				{field:'LBMergeItemVO_LisTestItem_ReceiveTime', width:100, title: '核收日期'},
				{field:'LBMergeItemVO_LisTestItem_LisTestForm_Id', width:80, title: '检验单ID',hide:true},
				{field:'LBMergeItemVO_LisTestItem_LisTestForm_MainStatusID', width:80, title: '检验单主状态',hide:true},		
				{field:'LBMergeItemVO_LisTestItem_LisTestForm_PatNo', width:90, title: '病历号',hide:true},
				{field:'LBMergeItemVO_LisTestItem_EquipID', width:70, title: 'EquipID',hide:true},
				{field:'LBMergeItemVO_ChangeItemDispOrder', width:70, title: '排序字段',hide:true},
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				
              
            }
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
		},me.config,itemtable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//获取检验样本信息
	Class.pt.ItemMergeInfo =  function(obj,callback){
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
			itemID:$('#ItemID').val(),
			cName:obj.LBMergeItemFormVO_CName,
			isMerge:obj.LBMergeItemFormVO_IsMerge,
			patNo:obj.LBMergeItemFormVO_PatNo
		};
		var config = {
			type:'post',
			url:GET_ITEM_INFO_LIST_URL,
			data:JSON.stringify(params),
			async:false
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
		Class.pt.ItemMergeInfo(whereObj,function(list){
			table_Ind.instance.reload({data:list});
		});
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
	
	  
        
	};

	//保存前校验
	Class.pt.isValid = function(){
		var me = this;
		var items = table_Ind.table.cache.item_table;
		var isExec = false;
		for(var i = 0 ;i<items.length;i++){
			if(items[i].LBMergeItemVO_IsMerge=='1'){
				isExec= true;
				break;
			}
		}
		return isExec;
	};
	//清空
	Class.pt.clearData= function(whereObj){
		table_Ind.instance.reload({data:[]});
	};
	 //样本单列表更新一行数据 -- fields:{ "LisTestForm_Id": '5598045837466289641',"LisTestForm_CName": "123" }, key: "LisTestForm_Id"
	Class.pt.updateRowItem = function(fields, key) {
        var me = this;
        var that = me.instance.config.instance,
            list = table.cache[that.key] || [],
            len = list.length,
            index = null;
        for (var i = 0; i < len; i++) {
            if (list[i][key] == fields[key]) {
                index = i;
                break;
            }
        }

        if (index == null) {//不存在
            return false;
        } else {
            var tr = that.layBody.find('tr[data-index="' + index + '"]'),
                data = list[index],
                cacheData = table.cache[that.key][index];
            //将变化的字段值赋值到data  覆盖原先值
            data = $.extend({}, data, fields);

            fields = fields || {};
            layui.each(fields, function (ind, value) {
                if (ind in data) {
                    var templet, td = tr.children('td[data-field="' + ind + '"]');
                    data[ind] = value;
                    cacheData[ind] = value;
                    that.eachCols(function (i, item2) {
                        if (item2.field == ind && item2.templet) {
                            templet = item2.templet;
                        }
                    });
                    td.children(".layui-table-cell").html(function () {
                        return templet ? function () {
                            return typeof templet === 'function'
                                ? templet(data)
                                : laytpl($(templet).html() || value).render(data)
                        }() : value;
                    }());
                    td.data('content', value);
                }
            });
            return true;
        }
    };
	//主入口
	itemtable.render = function(options){
		var me = new Class(options);
		table_Ind = uxtable.render(me.config);
		table_Ind.updateRowItem = me.updateRowItem;
		table_Ind.loadData = me.loadData;
        table_Ind.clearData = me.clearData;
        table_Ind.isValid = me.isValid;
        me.initListeners(table_Ind);
		return table_Ind;
	};
	//暴露接口
	exports('itemtable',itemtable);
});