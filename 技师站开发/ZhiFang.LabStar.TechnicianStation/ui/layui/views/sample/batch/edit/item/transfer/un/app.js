/**
	@name：未选项目
	@author：liangyl
	@version 2021-05-19
 */
layui.extend({
	untable: 'views/sample/batch/edit/item/transfer/un/unlist',
	childtable: 'views/sample/batch/edit/item/transfer/un/childlist'
}).define(['table', 'uxutil','uxbase','untable','childtable'],function(exports){
	"use strict";
	var $=layui.$,
	    untable = layui.untable,
	    childtable = layui.childtable,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		table = layui.table;
     
    /**获取组合项目子项服务路径*/
	var GET_ITEMGROUP_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true';
    // fileds
    var ITEMGROUP_FILEDS = "LBItemGroup_LBItem_Id,LBItemGroup_LBItem_CName,LBItemGroup_LBItem_SName,LBItemGroup_LBItem_UseCode,LBItemGroup_LBItem_PinYinZiTou,LBItemGroup_LBItem_GroupType,LBItemGroup_LBItem_IsOrderItem";
	
	var table0_Ind = null;
	//组合子项目实例
	var table1_Ind = null;
    
	var unapp  = {
		//全局项
		config:{
			elem:"",
			elem1:"",
			title:'',
			SECTIONID:null,
			type:'0' //项目类型,0-全部项目,1-医嘱项目，2-组合项目
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,unapp.config,setings);
	};
	Class.pt = Class.prototype;

	/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	Class.pt.doAutoSelect = function(that, rowIndex) {
		var me = this;	
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var cellTop11 = thatrow.offset().top;
		$(that.instance.layBody[0]).scrollTop(cellTop11 - 160);

		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 100);
	};

	Class.pt.init = function(searchText){
		var me = this;
		table0_Ind = untable.render({
			elem:me.config.elem,
			type:me.config.type,
	    	title:me.config.title,
	    	height:'full-105',
	    	size: 'sm',
	    	done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
			
					//默认选择第一行
					var rowIndex = 0;
		            //默认选择行
				    me.doAutoSelect(this,rowIndex);
				    //单元格背景色
				    table0_Ind.cellBgColor(res.data,filter);
			    }
			}
		});
		
		table0_Ind.loadData(me.config.SECTIONID,me.config.type,searchText);

       
		//渲染组合子项目
		if(me.config.type=='2'){
			table1_Ind = childtable.render({
				elem:me.config.elem1,
				type:me.config.type,
		    	title:'组合子项目',
		    	height:'full-105',
		    	size: 'sm', //小尺寸的表格
		    	done: function(res, curr, count) {
					if(count>0){
						var filter = this.elem.attr("lay-filter");
						//默认选择第一行
						var rowIndex = 0;
			            //默认选择行
					    me.doAutoSelect(this,rowIndex);
				    }
				}
			});
			var filter = $(me.config.elem).attr("lay-filter");
			table0_Ind.table.on('row('+filter+')', function(obj){
			    //标注选中样式
			    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				var data = obj.data;
				table1_Ind.loadData(data.LBSectionItem_LBItem_Id);
			});
		}
	};
    Class.pt.loadData = function(searchText){
		var me = this;
		me.init(searchText);
	};
		//判断对象是否已存在数组中
	Class.pt.isExist = function(arr,id){
		var me = this,
		    isExist=false;
		for (var i = 0; i < arr.length; i++) {
            if ((arr[i].LBSectionItem_LBItem_Id).indexOf(id) > -1) {//判断key为id的对象是否存在，
	              isExist = true;
	              break;
            }
        }
		return isExist;
	};
	//或者组合项目子项
	Class.pt.getItemGroup = function(GroupItemID,callback){
		var me = this;
		var url = GET_ITEMGROUP_URL + "&where=GroupItemID="+GroupItemID;
		url += '&fields='+ITEMGROUP_FILEDS;
		url += '&sort=[{"property":"LBItemGroup_LBItem_DispOrder","direction":"ASC"}]';
		uxutil.server.ajax({
			url:url,
			async: false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//获取选择子项处理
	Class.pt.selection= function(data){
		var me = this,
		    arr =[];
		for(var i=0;i<data.length;i++){
    		var itemobj = data[i];
    		//组合项目,循环子项
			if(data[i].LBSectionItem_LBItem_GroupType == '1'){
        		me.getItemGroup(data[i].LBSectionItem_LBItem_Id,function(list){
					for(var j=0;j<list.length;j++){
						list[j].LBSectionItem_LBItem_GroupItemCName = itemobj.LBSectionItem_LBItem_CName;
						list[j].LBSectionItem_LBItem_GroupItemID = itemobj.LBSectionItem_LBItem_Id;
						list[j].LBSectionItem_LBItem_GroupItemUseCode = itemobj.LBSectionItem_LBItem_UseCode;
					   	list[j].LBSectionItem_LBItem_GroupType = itemobj.LBSectionItem_LBItem_GroupType;
					    var result = JSON.stringify(list[j]);
						var obj = result.replace(/LBItemGroup/g,"LBSectionItem");
						obj = $.parseJSON(obj);
						var isExist = me.isExist(arr,obj.LBSectionItem_LBItem_Id);
						if(!isExist)arr.push(obj);
					}
				});
        	}else{
        		var isExist = me.isExist(arr,data[i].LBSectionItem_LBItem_Id);
		        if(!isExist)arr.push(data[i]);
        	}
        }
        return arr;
	};
	//获取选择行
	Class.pt.getSelection= function(searchText){
		var me = this,
		    arr =[];
		var filter = table0_Ind.instance.config.elem.attr("lay-filter");
        var data = table0_Ind.table.checkStatus(filter).data;
    	arr = me.selection(data);
		return arr;
	};
    //核心入口
	unapp.render = function(options){
		var me = new Class(options);
        me.loadData = Class.pt.loadData;
        me.getSelection =  Class.pt.getSelection;
         me.selection = Class.pt.selection;
		return me;
	};
	
		//联动
	Class.pt.initListeners= function(result){
		var me = this;
		
	};
	    //暴露接口
	exports('unapp',unapp);
});