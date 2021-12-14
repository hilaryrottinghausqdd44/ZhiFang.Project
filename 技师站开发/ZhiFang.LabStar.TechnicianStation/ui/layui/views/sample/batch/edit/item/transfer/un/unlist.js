/**
	@name：未选项目
	@author：liangyl	
	@version 2021-05-19
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
	//获取项目列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';

	var untable = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			/**项目类型,0-全部项目,1-医嘱项目，2-组合项目*/
	        type:'0' ,
			defaultOrderBy: [{property:'LBSectionItem_LBSection_DispOrder',direction:'ASC'},{property:'LBSectionItem_LBItem_DispOrder',direction:'ASC'}],
			cols:[[
				{type: 'checkbox', fixed: 'left'},
				{field: 'LBSectionItem_LBItem_Id',width: 60,title: '项目id',sort: true,hide: true},
                {field:'LBSectionItem_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
				{field:'LBSectionItem_LBItem_SName', width:100, title: '项目简称', sort: true},
				{field:'LBSectionItem_LBItem_IsOrderItem', width:90, title: '医嘱项目', sort: true, templet:function(record){
					var v = record["LBSectionItem_LBItem_IsOrderItem"];
				    if(v=='true')v='<b>医嘱</b>';
				    else  v='';
	                return v;
				}},
				{field:'LBSectionItem_LBItem_UseCode', width:90, title: '用户代码', sort: true},
				{field:'LBSectionItem_LBItem_PinYinZiTou', width:100, title: '拼音字头', sort: true},
				{field:'LBSectionItem_LBItem_GroupType', minWidth:100,flex:1, title: '是否组合项目', sort: true,hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function (res, curr, count) {
				
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
		},me.config,untable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(SectionID,type,searchValue){
		var  me = this,
    		cols = me.config.cols[0],
    		params =[],where="",
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
	    //小组Id
		if(SectionID) {
			params.push("lbsectionitem.LBSection.Id=" + SectionID + "");
		}
		if(type=='1'){//按医嘱项目查询
			params.push("lbsectionitem.LBItem.IsOrderItem=1");
		}
		if(type=='2'){//按组合项目查询
			params.push("lbsectionitem.LBItem.GroupType=1");
		}
		if(params.length > 0) {
			where=params.join(' and ');
		}
		if(searchValue){
			var hql=" and (lbsectionitem.LBItem.CName like '%" + searchValue + 
	    		"%' or lbsectionitem.LBItem.SName like '%" + searchValue + "%' or lbsectionitem.LBItem.UseCode like '%" + 
	    		searchValue + "%' or lbsectionitem.LBItem.PinYinZiTou like '%" +
	    		searchValue + "%')";
			where+=hql;
		}
        var obj ={"where":where};
		
		me.instance.reload({
			url:url,
			where:$.extend({},obj,{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
		//获取选择行
	Class.pt.getSelection= function(){
		var me =  this;
		
	};
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
	};
	//td 背景色改变
	Class.pt.cellBgColor =  function(data,filter){
		data.forEach(function (item, index) {
			//项目名称背景色
			if (item.LBSectionItem_LBItem_GroupType == '1'){
				$('div[lay-id="'+filter+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LBSectionItem_LBItem_CName"]').
				css('background-color', '#F08080');
		    }
			//项目简称背景色
			if (item.LBSectionItem_LBItem_IsOrderItem == 'true'){
				//背景色-绿色
				$('div[lay-id="'+filter+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LBSectionItem_LBItem_SName"]').
				css('background-color', '#3CB371');
			}
			//医嘱背景色
			$('div[lay-id="'+filter+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LBSectionItem_LBItem_IsOrderItem"]').
				css('background-color', '#3CB371');
		
		});
	};
	//主入口
	untable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		result.instance.reload({data:[]});
		result.loadData = me.loadData;
		result.cellBgColor = me.cellBgColor;
		result.getSelection  = me.getSelection;
//		result.instance.reload({data:[]});
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('untable',untable);
});