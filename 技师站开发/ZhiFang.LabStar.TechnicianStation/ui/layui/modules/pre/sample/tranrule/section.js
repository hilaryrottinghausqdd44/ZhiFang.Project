/**
 * @name：modules/pre/sample/tranrule/sectionlist 分发规则对应小组 
 * @author：liangyl
 * @version 2021-10-14
 */
layui.extend({
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'TranRuleHostSectionList';
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
		    '<div class="layui-row" id ="{tableId}-toolbar">',
		      '<button id="{tableId}-refresh" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-refresh-3"></i>&nbsp;刷新</button>', 
		      '<button type="button" id="{tableId}-btn" class="layui-btn layui-btn-xs layui-hide"> <i class="iconfont" >&#xe713;</i>&nbsp;保存</button>',
		    '</div>',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		     '<script type="text/html" id="barDemo">',
	       	  '<a class="layui-btn  layui-btn-xs" lay-event="up"><i class="layui-icon layui-icon-up"></i></a>',
	          '<a class="layui-btn  layui-btn-xs" lay-event="down"><i class="layui-icon layui-icon-down"></i></a>',
			 '</script>', 
		'</div>',
		'<style>',
			'.{tableId}-table .layui-table-select{background-color:#5FB878;}',
			'.{tableId}-table .layui-table-cell {overflow: visible !important;}',
		'</style>'
	];
	//查询分发规则小组
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleHostSectionByHQL?isPlanish=true";
    //查询检验小组
    var GET_SECTION_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true"; 
	//修改站点类型对应小组显示次序
	var UPDATE_TRANRULEHOSTSECTION_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleHostSectionByField';

	//检验小组枚举自定义,方便匹配小组名称
    var ENUM = {};
	//样本单列表
	var TranRuleHostSectionList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			page: false,
			limit: 1000,
			nodetype:null
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			height:'full-110',
			where:{},
			page: false,
			limit: 1000,
			limits: [100, 200, 500, 1000, 1500],
			cols:[[
				{field:'LBTranRuleHostSection_Id',title:'分发规则小组id',width:80,hide:true},
				{field:'LBTranRuleHostSection_SectionID',title:'小组名称',flex:1,templet: function(data) {
					var v = data.LBTranRuleHostSection_SectionID;
					if(ENUM != null)v = ENUM[v];
                    return v;
                }},
				{field:'LBTranRuleHostSection_DispOrder',title:'次序',width:100,hide:true},
				{ title:'操作', toolbar: '#barDemo', width:90,fixed: 'right'}
			]],
			text: {none: '暂无相关数据'},
			defaultOrderBy: [{
				"property": 'LBTranRuleHostSection_DispOrder',
				"direction": 'ASC'
			}]
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,TranRuleHostSectionList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,TranRuleHostSectionList.tableConfig);
		if(me.config.height){
			me.tableConfig.height = me.config.height - 25;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		//监听工具条
		me.uxtable.table.on('tool('+me.tableId+')', function(obj){
		    var data = obj.data;
		    if (obj.event === 'up') { //上移
	            me.move(data.LBTranRuleHostSection_Id, obj.event);
	        } else if (obj.event === 'down') { //下移
	            me.move(data.LBTranRuleHostSection_Id, obj.event);
	        }
		});
	    //刷新
		$('#'+me.tableId+'-refresh').on('click',function(){
			me.loadData();
		});
	};
    //获取查询字段
	Class.prototype.getFields = function(){
		var me = this,
		    cols = me.tableConfig.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
	    return fields.join(',');
	};

	//数据加载
	Class.prototype.loadData = function(height){
		var me = this;
		var whereObj ={"where":"lbtranrulehostsection.HostTypeID="+me.config.nodetype};
		me.uxtable.instance.reload({
			url: GET_LIST_URL,
			where:$.extend({},whereObj,{
				fields:me.getFields(),
				page: {
                    curr: 1 //重新从第 page 页开始
                },
				sort: JSON.stringify(me.tableConfig.defaultOrderBy)
			})
		});
		me.changeSize(height);
	};
	 //改变高度
	Class.prototype.changeSize = function(height){
		var me = this;
		height = height-30;
        $('#'+me.config.domId).find('div.layui-table-body.layui-table-main').css('height',height+'px');
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	 //获取所有小组名称
	Class.prototype.SectionList = function(callback){
		var fields = ['LBSection_Id','LBSection_CName'],
			url = GET_SECTION_LIST_URL;
		url += '&fields='+fields.join(',');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				for(var i=0;i<list.length;i++){
					ENUM[list[i].LBSection_Id] = list[i].LBSection_CName;
				}
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};

	Class.prototype.move = function (Id,type) {
        var me = this,
            Id = Id,
            type = type || 'up', // up:上移，down：下移
            LAY_TABLE_INDEX = null,//记录当前行下标
            data = [],//发送数据
            saveCount = 0,
            saveSuccessCount = 0,
            saveErrorCount = 0,
            tableCache = me.uxtable.table.cache[me.tableId];
        if (!Id) return;
        $.each(tableCache, function (i,item) {
            if (Id == item["LBTranRuleHostSection_Id"]) {
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
            var preDispOrder =  tableCache[LAY_TABLE_INDEX - 1]["LBTranRuleHostSection_DispOrder"];
            var nextDispOrder =  tableCache[LAY_TABLE_INDEX]["LBTranRuleHostSection_DispOrder"];
            if(preDispOrder == nextDispOrder)preDispOrder = Number(preDispOrder)-1;
            data.push({ Id: Id,DispOrder:preDispOrder});
            data.push({ Id: tableCache[LAY_TABLE_INDEX - 1]["LBTranRuleHostSection_Id"], DispOrder:nextDispOrder  });
        } else if (type == 'down') {
            if (LAY_TABLE_INDEX == tableCache.length - 1) {
                layer.msg("已经是最后一条!");
                return;
            }
            var nextDispOrder =  tableCache[LAY_TABLE_INDEX + 1]["LBTranRuleHostSection_DispOrder"] ;
            var preDispOrder= tableCache[LAY_TABLE_INDEX]["LBTranRuleHostSection_DispOrder"];
            if(preDispOrder == nextDispOrder)nextDispOrder = Number(nextDispOrder)+1;

            data.push({ Id: Id,DispOrder:nextDispOrder });
            data.push({ Id: tableCache[LAY_TABLE_INDEX + 1]["LBTranRuleHostSection_Id"], DispOrder:preDispOrder  });
        }
        saveCount = data.length;
        var load = layer.load();
        $.each(data, function (i, item) {
            var entity = entity = {
                entity: {
                    Id: item["Id"],
                    DispOrder: item["DispOrder"]
                },
                fields: "Id,DispOrder"
            };
            var config = {
                type: "POST",
                url: UPDATE_TRANRULEHOSTSECTION_URL,
                data: JSON.stringify(entity)
            };
            uxutil.server.ajax(config, function (data) {
                if (data.success) {
                    saveSuccessCount++;
                } else {
                    saveErrorCount++;
                }
                if (saveSuccessCount + saveErrorCount == saveCount) {
                    layer.close(load);
                    me.loadData();
                }
            })
        });
    };
	//核心入口
	TranRuleHostSectionList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();     
		me.uxtable = uxtable.render(me.tableConfig);
		me.SectionList(function(){
			me.loadData();
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	//暴露接口
	exports(MOD_NAME,TranRuleHostSectionList);
});