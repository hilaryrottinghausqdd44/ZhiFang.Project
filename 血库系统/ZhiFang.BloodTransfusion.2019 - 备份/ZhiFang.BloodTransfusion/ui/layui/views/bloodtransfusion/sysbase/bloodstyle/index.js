layui.extend({
	uxutil: "ux/util",
	dataadapter: "ux/dataadapter",
	bloodStyleTable: "views/bloodtransfusion/sysbase/bloodstyle/bloodStyleTable",
	editForm: "views/bloodtransfusion/sysbase/bloodstyle/editForm"
}).use(['form', 'table','uxutil','dataadapter', 'bloodStyleTable', 'editForm'], function(){
	"use strict";
	
	var $ = layui.$;
	var form = layui.form;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodStyleTable = layui.bloodStyleTable;
	var editForm = layui.editForm;
	
	
	//按钮监听事件
    $(".layui-btn-group>.layui-btn").on("click", function() {
		var type = $(this).data('type');
		var obj = table.checkStatus('LAY-bloodstyle-table');
		buttonEvent[type] && buttonEvent[type].call(bloodStyleTable, obj);
	})
    
    //列表按钮监听
    table.on("tool(LAY-bloodstyle-table-filter)", function(obj){
    	var data = obj.data; //获得当前行数据
    	var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
    	var tr = obj.tr; //获得当前行 tr 的DOM对象	
    	buttonEvent[layEvent] && buttonEvent[layEvent].call(bloodStyleTable, obj);
    })
    
    function isArrayfn(value){
    	if (typeof Array.isArray === "function") {
			return Array.isArray(value);
		}else{
			return Object.prototype.toString.call(value) === "[object Array]";
		}
    }
    
    function OnSave(index, layero){
		var submitID = 'LAY-bloodstyle-submit';
		var submit = layero.find('iframe').contents().find('#' + submitID);
    	submit.attr("iframeIndex", index);
		//触发新增或编辑表单的提交事件
		submit.trigger('click');    	
    }
    //打开编辑表单窗口
    function openForm(formtype, content, callback){
    	var me = this;
    	layer.open({
			type: 2,
			title: formtype == "add" ? "新增血制品" : "编辑血制品",
			area: ['55%', '80%'],
			content: content,
			id: "LAY-bloodStyleEdit-form",
			btn: ['确认提交', '关闭'],
			yes: function (index, layero) {
				OnSave(index, layero);
				return false;
			},
			btn2: function (index, layero) {
			    //按钮【关闭】的回调
				layer.close(index); //关闭弹层
			},
			cancel: function (index, layero) {
				//弹出层关闭后处理
				if (callback) callback(true);
			},
			end: function(){
				//需要加一个状态码来识别操作结果，是否需要重载表格
				table.reload("LAY-bloodstyle-table", bloodStyleTable.config);
			}
		});
    	
    }
    
    //按钮操作
    var buttonEvent = {
      	add: function(obj, ename){
      		var me = this;
      		var content = 'edit.html?formType=add&Url=' + bloodStyleTable.config.addUrl;
            openForm.call(editForm, 'add', content);
    	},
    	
    	edit: function(obj, ename){
    		var me = this;
    		var type = isArrayfn(obj.data);
    		if (type && obj.data.length !== 1) return;
    		var data = type ? obj.data[0]: obj.data;
    		var content = 'edit.html?formType=edit&Url=' + bloodStyleTable.config.editUrl;
    		var dataArr = [];
    		for(var name in data){
    			dataArr.push(name + '=' + data[name]);
    		}
    		content = dataArr.length > 0 ? content + '&' + dataArr.join('&'): content;
    		openForm.call(editForm, 'edit', content);
    	},
    	del: function(obj, ename){
    		var me = this;
    		var type = isArrayfn(obj.data);
    		if (type && obj.data.length !== 1) return;
    		var data = type ? obj.data[0]: obj.data;
    		var Id = data.Bloodstyle_Id;
    		var entity = {"Id":Id, "Visible": "0"};
    		var param = {entity: entity, fields: "Id,Visible"};	
    		var editUrl = bloodStyleTable.config.editUrl;
    		var isDelete = (obj.del) && (typeof obj.del === 'function')
    		layer.confirm('真的删除行么', function(index){
    			//向服务端发送删除指令,其实修改为不可用没有物理删除
    			editForm.onSave(editUrl, param, function(){
    		        isDelete && obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
    				isDelete ? 
    					table.reload("LAY-bloodstyle-table", bloodStyleTable.config) : 
    					table.render(bloodStyleTable.config);
	    		    layer.close(index);	
    			});
	    	});	
    	},
    	search: function(obj, ename){
    		var me = this,
    		    Value = $('#LAY-search-text').val(),
    		    LikeWhere = me.getLikeWhere.call(me, Value),
    		    visible = $('#LAY-search-select-visible').val() ? $('#LAY-search-select-visible').val() : 1,
   		    
		        internalWhere = (visible == -1) ?  LikeWhere : LikeWhere ? 
		        ' ( ' + LikeWhere + ' ) and bloodstyle.Visible = ' + visible : 'bloodstyle.Visible = ' + visible;
			    me.config.where = {"where": internalWhere};
			    me.config.url = me.config.defLoadUrl;
			    table.render(me.config);

    	} 
    };
    
	bloodStyleTable.render();
})
