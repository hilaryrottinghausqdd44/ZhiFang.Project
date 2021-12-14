/**
	@name：采样组项目关系的采样组信息
	@author：liangyl
	@version 2019-10-10
 */
layui.extend({
	
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
	
		
	//获取采样组列表数据
	var GET_SAMPLINGGROUP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryLBSamplingItemByHQL?isPlanish=true';
    //采样组项目更新
    var UPDATE_SAMPLINGITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSamplingItemByField';

	var sigrouptable = {
		//参数配置
		config:{
              page: false,
			limit: 5000,
			loading : true,
			defaultLoad:false,
			defaultOrderBy:"[{property: 'LBSamplingItem_LBSamplingGroup_DispOrder',direction: 'ASC'}]",
			cols:[[
//			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBSamplingItem_LBSamplingGroup_CName',title:'采样组名称',width:150,sort:true},
				{field:'LBSamplingItem_LBSamplingGroup_LBTcuvete_CName',title:'采样管',width:90,sort:true},
				{field:'LBSamplingItem_LBSamplingGroup_LBTcuvete_ColorValue',title:'颜色',width:90,hide:true},
				{field:'LBSamplingItem_LBSamplingGroup_Id',title:'采样组编号',width:250,sort:true,hide:true},
				{field:'LBSamplingItem_Id',title:'采样组项目关系id',width:250,sort:true,hide:true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
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
		},me.config,sigrouptable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,LBItemID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];

		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SAMPLINGGROUP_LIST_URL;
		
		var obj = {
			fields:fields.join(','),
			sort:me.config.defaultOrderBy
		};
		if(LBItemID)obj.where =  "lbsamplingitem.LBItem.Id="+LBItemID + ' or lbitem.Id='+LBItemID;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
	Class.pt.clearData = function(){
		var  me = this;		
		me.instance.reload({
			url:'',
			data:[]
		});
	};
	Class.pt.move = function (Id,type,callback) {
        var me = this,
            Id = Id,
            type = type || 'up', // up:上移，down：下移
            LAY_TABLE_INDEX = null,//记录当前行下标
            data = [],//发送数据
            saveCount = 0,
            saveSuccessCount = 0,
            saveErrorCount = 0,
            tableCache = table.cache['threerow-sgroupitem-table'];
        if (!Id) return;
        $.each(tableCache, function (i,item) {
            if (Id == item["LBSamplingItem_Id"]) {
                LAY_TABLE_INDEX = item["LAY_TABLE_INDEX"];
                return false;
            }
        });
        if (LAY_TABLE_INDEX == null) return;
        if (type == 'up') {
            if (LAY_TABLE_INDEX == 0) {
                layer.msg("已经是第一条!");
                return;
            }
            var DispOrder1= tableCache[LAY_TABLE_INDEX - 1]["LBSamplingItem_DispOrder"];
            var DispOrder2= tableCache[LAY_TABLE_INDEX]["LBSamplingItem_DispOrder"] ;
            if(DispOrder1 == DispOrder2)DispOrder1 = Number(DispOrder1)-1;
            data.push({ Id: Id, DispOrder: DispOrder1 });
            data.push({ Id: tableCache[LAY_TABLE_INDEX - 1]["LBSamplingItem_Id"], DispOrder: DispOrder2 });
        } else if (type == 'down') {
            if (LAY_TABLE_INDEX == tableCache.length - 1) {
                layer.msg("已经是最后一条!");
                return;
            }
            var DispOrder1= tableCache[LAY_TABLE_INDEX + 1]["LBSamplingItem_DispOrder"];
            var DispOrder2= tableCache[LAY_TABLE_INDEX]["LBSamplingItem_DispOrder"];
            if(DispOrder1 == DispOrder2)DispOrder2 = Number(DispOrder2)-1;

            data.push({ Id: Id, DispOrder: tableCache[LAY_TABLE_INDEX + 1]["LBSamplingItem_DispOrder"] });
            data.push({ Id: tableCache[LAY_TABLE_INDEX + 1]["LBSamplingItem_Id"], DispOrder: tableCache[LAY_TABLE_INDEX]["LBSamplingItem_DispOrder"] });
        }
        saveCount = data.length;
        var load = layer.load();
        $.each(data, function (i, item) {
            var params = {
                entity: {
                    Id: item["Id"],
                    DispOrder: item["DispOrder"]
                },
                fields: "Id,DispOrder"
            };
            var config = {
                type: "POST",
                url: UPDATE_SAMPLINGITEM_URL,
                data: JSON.stringify(params)
            };
            uxutil.server.ajax(config, function (data) {
                if (data.success) {
                    saveSuccessCount++;
                } else {
                    saveErrorCount++;
                }
                if (saveSuccessCount + saveErrorCount == saveCount) {
                    layer.close(load);
                    callback();
                }
            });
        });
    };
   	//主入口
	sigrouptable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.loadData = me.loadData;
		result.move= me.move;
        if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
        result.clearData = me.clearData;

		return result;
	};

	//暴露接口
	exports('sigrouptable',sigrouptable);
});