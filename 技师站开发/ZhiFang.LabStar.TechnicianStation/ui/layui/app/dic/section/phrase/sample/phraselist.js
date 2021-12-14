/**
	@name：短语列表
	@author：liangyl	
	@version 2019-10-30
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
	//获取短语列表数据
	var GET_BPHRASE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true';
    var config ={
    	ObjectID : "",
    	checkRowData:[{
    		table:'',
    		data:[]
    	}],
    	tableIns:null
    };
	var samplephrasetable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBPhrase_DispOrder',direction: 'ASC'}]",
			defaultLoad:false,
			ObjectType:'1',//针对类型1：小组样本 2：检验项目
			data:[],
			toolbar: false,
			cols:[[
				{field: 'LBPhrase_Id', width: 60,title: '主键ID', hide: true},
				{field: 'LBPhrase_TypeName',title: '短语类型',minWidth: 100}, 
				{field: 'LBPhrase_ObjectType', title: '针对类型',minWidth: 80,
	                templet: function (data) {
	                    var str = "";
	                    if (data.LBPhrase_ObjectType == 1) {
	                        str = "<span  style='color:red'>小组样本</span>";
	                    } else if (data.LBPhrase_ObjectType == 2) {
	                        str = "<span  style='color:red'>检验项目</span>";
	                    }
	                    return str;
	                }
	            }, 
	            {field: 'LBPhrase_ObjectID',title: '对象Id', minWidth: 120,hide:true}, 
	            {field: 'LBPhrase_SampleTypeID',title: '样本类型Id',minWidth: 100,hide:true},
	            {field: 'LBPhrase_CName', title: '短语名称',minWidth: 100},
	            {field: 'LBPhrase_Shortcode',title: '快捷码',minWidth: 80},
	            {field: 'LBPhrase_PinYinZiTou',title: '拼音字头',minWidth: 80},
	            {field: 'LBPhrase_TypeCode',title: '短语类型',minWidth: 100,hide:false}, 
	            {field: 'LBPhrase_DispOrder',title: '显示次序',minWidth: 80,hide:true}

	        ]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
					//默认选择第一行
					var rowIndex = 0;
		            //默认选择行
				    Class.pt.doAutoSelect(this,rowIndex);
			    }
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
		},me.config,samplephrasetable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,SectionID,TypeCode){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		config.ObjectID = SectionID;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_BPHRASE_LIST_URL+'&fields='+fields.join(',')+'&sort='+me.config.defaultOrderBy;
		var obj ={};
		var where = "lbphrase.TypeCode='" + TypeCode +"' and lbphrase.ObjectType="+me.config.ObjectType+ ' and lbphrase.ObjectID='+SectionID;
		obj.where=where;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};

	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		
	};
	//主入口
	samplephrasetable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		if(me.config.defaultLoad){
			result.loadData(me.config.where,1);
		}
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('samplephrasetable',samplephrasetable);
});