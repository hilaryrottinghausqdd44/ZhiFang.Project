/**
	@name：打印格式页签
	@author：liangyl
	@version 2019-10-28
 */
layui.extend({
	sectionprinttable:'app/dic/section/printformat/list',
	printformatform:'app/dic/section/printformat/form'
}).define(['table','form','sectionprinttable','printformatform'],function(exports){
	"use strict";
	
	var $=layui.$,
	    sectionprinttable = layui.sectionprinttable,
	    printformatform = layui.printformatform,
	    form = layui.form,
		table = layui.table;
	
	var printformattab  = {
		//全局项
		config:{
			PrintFormatTabTable:null,
			PrintFormatTabForm:null,
			SectionID:null,
			PK:null
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
		me.config = $.extend({},me.config,printformattab.config,setings);
	};
	Class.pt = Class.prototype;
		
		/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	Class.pt.doAutoSelect = function (rowIndex,that) {
		var  me = this;
		var data = table.cache[that.id] || [];
		if (!data || data.length <= 0) return;
		rowIndex = rowIndex || 0;
		var tableDiv = $("#"+that.id+"+div .layui-table-body.layui-table-body.layui-table-main");
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
	Class.pt.initFilterListeners =  function(){
		var me = this;
		//行 监听行单击事件
		table.on('row(printformattable)', function(obj){
			me.config.checkRowData=[];
			me.config.checkRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        me.config.PrintFormatTabForm.loadData(obj.data.LBSectionPrint_Id,me.config.SectionID);
		});
		$('#addPrint').on('click',function(){
			me.config.PrintFormatTabForm.isAdd(me.config.SectionID);
    	});
		//保存
		form.on('submit(savePrint)',function(obj){
			me.config.PrintFormatTabForm.onSaveClick(me.config.SectionID,obj,function(id,formtype){
				//隐藏遮罩层
				if (id) {
					var msg = "编辑成功!";
					if(formtype=='add')msg = "新增成功!";
					layer.msg(msg,{ icon: 6, anim: 0 ,time:2000 });
					me.config.PK = id;
					me.config.PrintFormatTabTable.loadData({},me.config.SectionID);
				} else {
					layer.msg(data.msg,{ icon: 5, anim: 6 });
				}
			});
		});
		
    	//删除
    	$('#delPrint').on('click',function(){
    		me.config.PrintFormatTabForm.onDelClick(function(id){
    			if(id)me.config.PrintFormatTabTable.loadData({},me.config.SectionID);
    		});
    	});
		
	};
	Class.pt.load  =  function(id){
		var me = this;
		me.config.SectionID=id;
		me.config.PrintFormatTabTable.loadData({},id);
	};
    //核心入口
	printformattab.render = function(options){
		var me = new Class(options);
			
		me.config.PrintFormatTabForm = printformatform.render();
		var obj = {
			elem:'#printformattable',
	    	title:'打印格式列表',
	    	height:125,//'full-600',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
					//默认选择第一行
					var rowIndex = 0;
		            for (var i = 0; i < res.data.length; i++) {
		                if (res.data[i].LBSectionPrint_Id == me.config.PK) {
		              	    rowIndex=res.data[i].LAY_TABLE_INDEX;
		              	  break;
		                }
		            }
		            //默认选择行
				    me.doAutoSelect(rowIndex,this);
				}else{
					me.config.PrintFormatTabForm.isAdd(me.config.SectionID);
				}
			}

		};
		me.config.PrintFormatTabTable = sectionprinttable.render(obj);
     	
		me.initFilterListeners();
		me.loadData = Class.pt.load;
		return me;
	};
	//暴露接口
	exports('printformattab',printformattab);
});