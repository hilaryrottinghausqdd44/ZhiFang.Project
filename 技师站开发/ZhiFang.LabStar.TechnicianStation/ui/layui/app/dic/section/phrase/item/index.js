/**
	@name：项目短语页签
	@author：liangyl
	@version 2019-10-30
 */
layui.extend({
    itemphrasetable:'app/dic/section/phrase/item/list',
	phrasecard:'app/dic/section/phrase/item/card'
}).define(['table','uxutil','itemphrasetable','phrasecard'],function(exports){
	"use strict";
	var $=layui.$,
	    itemphrasetable = layui.itemphrasetable,
	    phrasecard = layui.phrasecard,
	    uxutil = layui.uxutil,
		table = layui.table;

	var itemphrasetab  = {
		//全局项
		config:{
			itemphrasetabTable:null,
		    card:null,
			SectionID:1,
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
		me.config = $.extend({},me.config,itemphrasetab.config,setings);
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
	Class.pt.initFilterListeners =  function(){
		var me = this;
		//行 监听行单击事件
		table.on('row(phrase_section_item_table)', function(obj){
			me.config.checkRowData=[];
			me.config.checkRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        me.config.card.loadData(obj.data.LBSectionItemVO_LBItem_Id);
		});
		    // 窗体大小改变时，调整高度显示
    	$(window).resize(function() {
			var tabHeight = $(window).height()- 150;
	       	$('#itemphraseBox').css("height",tabHeight);
    	});
	};
	Class.pt.load  =  function(id){
		var me = this;
		me.config.SectionID=id;
		me.config.itemphrasetabTable.loadData({},id);
	};
	Class.pt.init = function(){
		var me = this;
		var obj = {
			elem:'#phrase_section_item_table',
	    	title:'小组项目',
	    	height:'full-148',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
					//默认选择第一行
					var rowIndex = 0;
		            //默认选择行
				    me.doAutoSelect(this,rowIndex);
			    }else{
			    	//清空
			    	me.config.card.loadData(null);
			    }
			}
		};
		me.config.itemphrasetabTable = itemphrasetable.render(obj);
		$('#itemphraseBox').css("height",me.config.itemphrasetabTable.instance.config.height);
		
		var phraseobj={
			titile:'项目短语',
			ObjectType:2
		};
		me.config.card = phrasecard.render(phraseobj);
	};
    //核心入口
	itemphrasetab.render = function(options){
		var me = new Class(options);
		me.init();
		me.initFilterListeners();
		me.loadData = Class.pt.load;
		return me;
	};

    //暴露接口
	exports('itemphrasetab',itemphrasetab);
});