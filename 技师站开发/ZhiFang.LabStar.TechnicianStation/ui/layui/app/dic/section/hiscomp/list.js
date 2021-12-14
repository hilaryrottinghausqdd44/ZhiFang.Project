/**
	@name：小组历史对比列表
	@author：liangyl	
	@version 2019-11-05
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
	//获取小组历史对比列表数据
	var GET_HIS_COMP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionHisCompByHQL?isPlanish=true';
    
    var config={
    	SectionID:null,
    	SectionCName:null,
    	tableIns:null
    };
	var hiscomptable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBSectionHisComp_DataAddTime',direction: 'ASC'}]",
			defaultLoad:false,
			cols:[[
			    {field:'LBSectionHisComp_Id',width: 150,title: '主键',sort: true,hide:true},
				{field:'LBSectionHisComp_HisComp_Id',width: 150,title: '小组编号',sort: true,hide:true},
                {field:'LBSectionHisComp_HisComp_CName', width:200, title: '小组名称', sort: false}
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
				config.tableIns = that;
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,hiscomptable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,SectionID,SectionCName){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	    config.SectionID = SectionID;
	    config.SectionCName = SectionCName;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_HIS_COMP_LIST_URL+'&fields='+fields.join(',')+'&sort='+me.config.defaultOrderBy;
		var obj ={};
		var where ="";
		if(SectionID)where ="lbsectionhiscomp.LBSection.Id="+SectionID;
		
		obj.where=where;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		var win = $(window),
				maxWidth = win.width()-100,
				maxHeight = win.height()-80,
				width = maxWidth > 800 ? maxWidth : 800,
				height = maxHeight > 600 ? maxHeight : 600;
		//选择小组
	    $('#select_section_hiscomp').on('click',function(){
			layer.open({
	            type: 2,
	            area:[width+'px',height+'px'],
	            fixed: false,
	            maxmin: false,
	            title:'选择小组历史对比',
	            content: 'hiscomp/transfer/app.html',
	            success: function(layero, index){
	       	        var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
	       	        body.find('#sectionID').val(config.SectionID);
	       	        body.find('#sectionCName').html(config.SectionCName);
	       	        var filter = $(me.config.elem).attr("lay-filter");
                    var tableData = table.cache[filter] ? table.cache[filter] : [];
                    var strId=[];
                    for(var i=0;i<tableData.length;i++){
                    	strId.push(tableData[i].LBSectionHisComp_HisComp_Id);
                    }
	       	        body.find('#groupItemID').val(strId.join(","));
		        },
		        cancel: function (index, layero) {
		        	layer.closeAll('iframe');
	            }
	        });
		});
	};
	//主入口
	hiscomptable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		if(me.config.defaultLoad)result.loadData(me.config.where,1);
        me.initListeners(result);
		return result;
	};
	Class.pt.afterHisCompTransferUpdate = function(data){
        var me = this;
        layer.closeAll('iframe');
        layer.msg('保存成功!',{ icon: 6, anim: 0 ,time:2000 });
        config.tableIns.loadData({},config.SectionID, config.SectionCName);
    };
    window.afterHisCompTransferUpdate = Class.pt.afterHisCompTransferUpdate;
	//暴露接口
	exports('hiscomptable',hiscomptable);
});