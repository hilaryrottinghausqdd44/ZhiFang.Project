/**
	@name：小组项目列表
	@author：liangyl	
	@version 2019-08-03
 */
layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable','layer','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
	//获取项目列表服务地址
    var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemVOByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]';
    //修改项目服务地址
    var GET_EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipItemByField';
	
	var sortlist = {
		//参数配置
		config:{
            page: false,
			limit: 10000,
			loading : true,			
			cols:[[
				{type: 'checkbox',fixed: 'left'},
                { field: 'LBEquipItemVO_LBEquipItem_Id', width: 150, title: '主键', sort: true, hide: true },
                { field: 'LBEquipItemVO_LBItem_Id', width: 150, title: '项目编号', sort: true, hide: true },
                { field:'LBEquipItemVO_LBEquipItem_DispOrderQC',width: 120,title: '质控显示次序',sort: true, edit: 'text',event: "setDispOrder"},
                { field:'LBEquipItemVO_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
                { field:'LBEquipItemVO_LBItem_EName', width:100,title: '英文名称', sort: true},
                { field:'LBEquipItemVO_LBItem_SName', width:100,title: '简称', sort: true},
                { field:'Temporary_Tab', width:100,title: '是否已修改',sort: true,hide:true},
                { field:'Temporary_DispOrder', width:100,title: '原始值显示次序',sort: true,hide:true}
			]],
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				var list = [];
				if(data && data.list){
					for(var i=0;i<data.list.length;i++){
                        data.list[i].Temporary_Tab ="";
                        data.list[i].Temporary_DispOrder = data.list[i].LBEquipItemVO_LBEquipItem_DispOrderQC;
						list.push(data.list[i]);
					}
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
				};
			},
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
					//监听数据是否修改
					that.table.on('edit(' + filter + ')', function(obj){
						var value = obj.value, //得到修改后的值
						data = obj.data , //得到所在行所有键值
						field = obj.field; //得到字段
						//改变后的数据
						obj.update({
                            Temporary_Tab: '1'
	                    });
	               });
				}
			}
		},me.config,sortlist.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
    Class.pt.loadData = function (whereObj){
		var me = this,
			cols = me.config.cols[0],
			fields = [],
            url = url || GET_LIST_URL;
		for(var i in cols){
			fields.push(cols[i].field);
		}
        me.instance.reload({
            url: url,
            where: $.extend({}, whereObj, {
                fields: fields.join(',')
            })
        });
	};

  	/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	Class.pt.doAutoSelect = function (rowIndex,that) {
		var  me = this;
        var that = sortlist.render(me.config);
		var data = that.table.cache[that.id] || [];
		if (!data || data.length <= 0) return;
		rowIndex = rowIndex || 0;
		var tableDiv = $(that.elem+"+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		var filter = $(that.elem).find('lay-filter');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function () {
				table.cache[that.id][index] = [];
				tr.remove();
				that.scrollPatch();
			},
			updte:{}
		};
		layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		thatrow.click();
	};
	
	//保存方法
	Class.pt.onSaveClick =  function(){
		var me = this;
		var records = me.getModifiedRecords();
        if(records.length==0){
			layer.msg('没有修改的数据不需要保存！');
            return;
		}		
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		//显示遮罩
		if(records.length==0)return;
		var indexs=layer.load();
		//获取列表数据
	    for(var i = 0;i<records.length;i++){
	    	//找到修改过数据的行
	    	me.updateOne(i,records[i]);
	    }
	};
	//获取修改过的行记录
    Class.pt.getModifiedRecords =  function(){
    	var me = this,list=[];
    	var filter = $(me.config.elem).attr("lay-filter");
    	//获取列表数据
		var tableCache = table.cache[filter];
	    for(var i = 0;i<tableCache.length;i++){
	    	//找到修改过数据的行
            if (tableCache[i].Temporary_DispOrder != tableCache[i].LBEquipItemVO_LBEquipItem_DispOrderQC){
	    		list.push(tableCache[i]);
	    	}
	    }
	    return list;
    };
    Class.pt.updateOne =  function(index,obj){
   		var me = this;
   		setTimeout(function() {
   	        var  id = obj.LBEquipItemVO_LBEquipItem_Id;
            var DispOrderQC = obj.LBEquipItemVO_LBEquipItem_DispOrderQC;
            var entity ={
            	Id:id,
                DispOrderQC: DispOrderQC
            };
            var fields ="Id,DispOrderQC";
            var params={entity:entity,fields:fields};
		    params = JSON.stringify(params);
           //显示遮罩层
			var config = {
				type: "POST",
				url: GET_EDIT_URL,
				data: params
			};
   			uxutil.server.ajax(config, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}				
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					//隐藏遮罩层
					layer.closeAll('loading');
					if (me.saveErrorCount == 0){
						layer.msg('保存成功！',{icon:1,time:2000});
						var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                        parent.layer.close(index); //再执行关闭
					}else{
						layer.msg('存在失败信息，具体错误内容请查看数据行的失败提示！',{ icon: 5, anim: 6 });
					}
				}
			})
		}, 100 * index);
   	};
	//主入口
	sortlist.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
        //加载数据
        result.loadData(me.config.where);
		//保存
	    $('#save').on('click',function(){
	      	me.onSaveClick();
	    });
		return result;
	};
	//暴露接口
	exports('sortlist',sortlist);
});