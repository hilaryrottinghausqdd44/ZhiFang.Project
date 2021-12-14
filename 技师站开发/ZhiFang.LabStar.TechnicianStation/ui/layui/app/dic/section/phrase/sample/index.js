/**
	@name：样本短语页签
	@author：liangyl
	@version 2019-10-31
 */
layui.extend({
    sampletable:'app/dic/section/phrase/sample/list',
    samplephrasetable:'app/dic/section/phrase/sample/phraselist',
    phraseform:'app/dic/section/phrase/sample/form'
}).define(['table','uxutil','sampletable','samplephrasetable','commonzf','phraseform','form'],function(exports){
	"use strict";
	var $=layui.$,
	    commonzf = layui.commonzf,
	    sampletable = layui.sampletable,
	    samplephrasetable = layui.samplephrasetable,
	    phraseform = layui.phraseform,
	    form = layui.form,
	    uxutil = layui.uxutil,
		table = layui.table;
		
    var SAMPLEROWDATAS = [],phrasetabTable=null,SectionID=null;
    
	var samplephrasetab  = {
		//全局项
		config:{
			samplephrasetabTable:null,
			phrasetabTable:null,
			phraseForm :null,
		    card:null,
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
		me.config = $.extend({},me.config,samplephrasetab.config,setings);
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
	Class.pt.load = function(id){
		var me = this;
	    me.config.SectionID = id;
		SectionID = id;
		me.config.phrasetabTable.loadData({},me.config.SectionID,SAMPLEROWDATAS[0].Code);
	};
	Class.pt.initFilterListeners =  function(){
		var me = this;
		//行 监听行单击事件
		table.on('row(section_sample_table)', function(obj){
			SAMPLEROWDATAS = [];
			SAMPLEROWDATAS.push(obj.data);
			SectionID = me.config.SectionID;
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        me.config.phrasetabTable.loadData({},me.config.SectionID,obj.data.Code);
		});
		table.on('row(phrase_section_sample_table)', function(obj){
			me.config.phraseForm.loadData(obj.data.LBPhrase_Id,me.config.SectionID,SAMPLEROWDATAS[0].Name,SAMPLEROWDATAS[0].Code);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
		//批量新增
    	$('#batchAdd').on('click',function(){
    		var win = $(window),
				maxWidth = win.width()-100,
				maxHeight = win.height()-80,
				width = maxWidth > 800 ? maxWidth : 800,
				height = maxHeight > 600 ? maxHeight : 600;
			layer.open({
				title:'批量新增',
				type:2,
				content: 'phrase/sample/transfer/app.html?SectionID='+me.config.SectionID+'&TypeName='+SAMPLEROWDATAS[0].Name+'&TypeCode='+SAMPLEROWDATAS[0].Code,
				maxmin:true,
				toolbar:true,
				resize:true,
				area:[width+'px',height+'px'],
			    success: function(layero, index){
	       	        var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
	       	        body.find('#sectionID').val(me.config.SectionID);
	       	        body.find('#sectionCName').html(SAMPLEROWDATAS[0].Name);
	       	        var filter = $(me.config.phrasetabTable.config.elem).attr("lay-filter");
                    var tableData = table.cache[filter] ? table.cache[filter] : [];
	       	        body.find('#groupItemID').val(JSON.stringify(tableData));
		        }
			});
    	});
		//新增
    	$('#addsamplephrase').on('click',function(){
    		me.config.phraseForm.isAdd(me.config.SectionID,SAMPLEROWDATAS[0].Name,SAMPLEROWDATAS[0].Code);
    		//显示次序+1
    		$('#LBPhrase_DispOrder').val(table.cache['phrase_section_sample_table'].length+1);
    	});

		//删除
    	$('#delsamplephrase').on('click',function(){
    		me.config.phraseForm.onDelClick(function(id){
    			if(id)me.config.phrasetabTable.loadData({},me.config.SectionID,SAMPLEROWDATAS[0].Code);
    		});
    	});
    	//保存
    	form.on('submit(savesamplephrase)',function(data){
			me.config.phraseForm.onSaveClick(data,function(FORMTYPE,id){
				if(FORMTYPE=='add'){
					layer.msg('新增成功!',{ icon: 6, anim: 0 ,time:2000 });
				} else {
					layer.msg('编辑成功!',{ icon: 6, anim: 0 ,time:2000 });
				}
				me.config.PK = id;
				me.config.phrasetabTable.loadData({},me.config.SectionID,SAMPLEROWDATAS[0].Code);
			});
		});
		//短语名称改变填写快捷码
		
	};

	Class.pt.init = function(){
		var me = this;
		var obj = {
			elem:'#section_sample_table',
	    	title:'小组样本短语',
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
			    	SAMPLEROWDATAS=[];
			    }
			}
		};
		//样本类型渲染
		me.config.samplephrasetabTable = sampletable.render(obj);
		//表单渲染
		me.config.phraseForm = phraseform.render({});
		
		var phraseobj = {
			elem:'#phrase_section_sample_table',
	    	title:'样本短语',
	    	height:'full-300',
	    	ObjectType:'1',
	    	size: 'sm', //小尺寸的表格
	    	done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
					//默认选择第一行
					var rowIndex = 0;
					for (var i = 0; i < res.data.length; i++) {
		                if (res.data[i].LBPhrase_Id == me.config.PK) {
		              	   rowIndex=res.data[i].LAY_TABLE_INDEX;
		              	  break;
		                }
		            }
		            //默认选择行
				    me.doAutoSelect(this,rowIndex);
			    }else{
			    	setTimeout(function() {
						me.config.phraseForm.isAdd(me.config.SectionID,SAMPLEROWDATAS[0].Name,SAMPLEROWDATAS[0].Code);
						$('#LBPhrase_DispOrder').val(1);
					}, 200);
			    }
			}
		};
		//短语渲染
		me.config.phrasetabTable = samplephrasetable.render(phraseobj);
		phrasetabTable = me.config.phrasetabTable ;
	};
    //核心入口
	samplephrasetab.render = function(options){
		var me = new Class(options);
		me.init();
		me.initFilterListeners();
		me.loadData = Class.pt.load;
		return me;
	};
	Class.pt.afterPhraseTransferUpdate = function(data){
        var me = this;
        layer.msg('保存成功!',{ icon: 6, anim: 0 ,time:2000 });
    	if(data)phrasetabTable.loadData({},SectionID,SAMPLEROWDATAS[0].Code);
    };
    window.afterPhraseTransferUpdate = Class.pt.afterPhraseTransferUpdate;
    //暴露接口
	exports('samplephrasetab',samplephrasetab);
});