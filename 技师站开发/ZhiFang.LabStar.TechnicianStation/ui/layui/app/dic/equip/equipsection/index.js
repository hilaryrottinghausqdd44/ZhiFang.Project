/**
	@name：仪器特定小组
	@author：liangyl
	@version 2020-04-14
 */
layui.extend({
    equipsectionTable:'app/dic/equip/equipsection/list',
    equipsectionform:'app/dic/equip/equipsection/form'
}).define(['table','uxutil','equipsectionform','equipsectionTable','form'],function(exports){
	"use strict";
	var $=layui.$,
	    equipsectionTable = layui.equipsectionTable,
	    equipsectionform = layui.equipsectionform,
	    form = layui.form,
	    uxutil = layui.uxutil,
		table = layui.table;
		
    //仪器特定小组选择行数据
    var CHECKROWS = [];
    
	var equipsectiontab  = {
		//全局项
		config:{
			List:null,
			Form:null,
			EquipID:null
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
		me.config = $.extend({},me.config,equipsectiontab.config,setings);
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
	//数据加载
	Class.pt.load = function(id){
		var me = this;
	    me.config.EquipID = id;
		me.config.List.loadData(me.config.EquipID);
		//仪器项目加载
		me.config.Form.loadEquipItem(me.config.EquipID);
	};
	Class.pt.initFilterListeners =  function(){
		var me = this;
		//行 监听行单击事件
		table.on('row(equipsectiontable)', function(obj){
			CHECKROWS = [];
			CHECKROWS.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        setTimeout(function(){
				me.config.Form.isShow(obj.data.LBEquipSection_Id,me.config.EquipID);
			},200);
	       
		});
		//新增
    	$('#addEquipSection').on('click',function(){
    		me.config.Form.isAdd(me.config.EquipID);
		});
		//编辑
		$('#editEquipSection').on('click', function () {
			if (CHECKROWS.length == 0) return;
			me.config.Form.isEdit(CHECKROWS[0]["LBEquipSection_Id"], me.config.EquipID);
		});

		//删除
    	$('#delEquipSection').on('click',function(){
    		me.config.Form.onDelClick(function(id){
    			if(id)me.config.List.loadData(me.config.EquipID);
    		});
    	});
    	//保存
    	form.on('submit(saveEquipSection)',function(data){
			me.config.Form.onSaveClick(data,function(FORMTYPE,id){
				if(FORMTYPE=='add'){
					layer.msg('新增成功!',{ icon: 6, anim: 0 ,time:2000 });
				} else {
					layer.msg('修改成功!',{ icon: 6, anim: 0 ,time:2000 });
				}
				me.config.List.loadData(me.config.EquipID);
			});
		});
	};

	Class.pt.init = function(){
		var me = this;
		var obj = {
			elem:'#equipsectiontable',
	    	title:'仪器特定小组',
	    	height:'full-300',
	    	done: function(res, curr, count) {
				if(count>0){
					//默认选择第一行
					var rowIndex = 0;
		            //默认选择行
				    me.doAutoSelect(this,rowIndex);
			    }else{
			    	CHECKROWS=[];
			    	me.config.Form.isAdd(me.config.EquipID);
			    }
			}
		};
		//仪器特定小组渲染
		me.config.List = equipsectionTable.render(obj);
		//表单渲染
		me.config.Form = equipsectionform.render({});
	};
    //核心入口
	equipsectiontab.render = function(options){
		var me = new Class(options);
		me.init();
		me.initFilterListeners();
		me.loadData = Class.pt.load;
		return me;
	};
	
    //暴露接口
	exports('equipsectiontab',equipsectiontab);
});