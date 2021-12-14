layui.extend({
	uxutil:'ux/util',
	dataadapter:'ux/dataadapter',
	bloodTestItemForm:'views/bloodtransfusion/sysbase/btestitem/bloodTestItemForm',
	bloodTestItemTable:'views/bloodtransfusion/sysbase/btestitem/bloodTestItemTable'
}).use(['form', 'table', 'layer', 'uxutil', 'dataadapter', 'bloodTestItemTable', 'bloodTestItemForm'], function(){
	"use strict";
	var $ = layui.$,
	    form = layui.form,
	    layer = layui.layer,
	    table = layui.table,
	    uxutil = layui.uxutil,
	    dataadapter = layui.dataadapter,
	    bloodTestItemForm = layui.bloodTestItemForm,
	    bloodTestItemTable = layui.bloodTestItemTable;
	    
    //按钮监听事件
    $(".layui-btn-group>.layui-btn").on("click", function() {
			var type = $(this).data('type'),
			    fieldObj = bloodTestItemForm.getFormFields("LAY-bloodbtestitem-form-filter") || {};
			buttonEvent[type] ? buttonEvent[type].call(bloodTestItemTable, fieldObj, type) : '';
		})
    
    //按钮事件操作
    var buttonEvent = {
      	add: function(obj, ename){
      		var me = this;
      		form.val("LAY-bloodbtestitem-form-filter", obj);  //初始化表单元素
    		$("#form-save").attr("event-url", me.config.addUrl + ',' + ename);
    	},
    	
    	edit: function(obj, ename){
    		var me = this;
    		$("#form-save").attr("event-url", me.config.editUrl + ',' + ename);
    	},
    	
    	search: function(obj, ename){
    		var me = this,
    		    Value = $('#LAY-search-text').val(),
    		    LikeWhere = me.getLikeWhere.call(me, Value),
    		    visible = $('#LAY-search-select-visible').val() ? $('#LAY-search-select-visible').val() : 1,
		        internalWhere = (visible == -1) ?  LikeWhere : LikeWhere ?
		         ' ( ' + LikeWhere + ' ) and bloodbtestitem.Visible = ' + visible : 'bloodbtestitem.Visible = ' + visible ;
			    me.config.where = {"where": internalWhere};
			    me.config.url = me.config.defLoadUrl;
			    table.render(me.config);
    	} 
    };
    
	//监听表格单击事件,赋值表单元素
    table.on("row(LAY-bloodbtestitem-table-filter)", function(obj){
    	var url = $("#form-save").attr("event-url");
    	if (url){
    		layer.confirm('是否取消当前的编辑操作？', {
		        btn: ['是','否'], //按钮
		        btnAlign:'c'
		        }, function(index, layero){
			        $("#form-save").attr("event-url", "");
			        form.val("LAY-bloodbtestitem-form-filter", obj.data);
			        layer.close(index);
		        }, function(index, layero){
		        	layer.close(index);
     	        }
		    );
		}else {
    		form.val("LAY-bloodbtestitem-form-filter", obj.data);
    	}
    });
    
	//监听提交事件
    form.on('submit(save)', function(data){
    	var opType,
    	    Url,
    	    dataParams,
    	    OpUrl = $("#form-save").attr("event-url");
    	    var UrlArr = OpUrl.split(','),
    	    Url = UrlArr[0];
    	    opType = UrlArr[1];
    	if (!Url){
    		layer.msg("不在编辑状态！");
    		return;
    	}
    	dataParams = (opType === 'add') ? 
    		bloodTestItemForm.getAddParams('BloodBTestItem', 'LAY-bloodbtestitem-form-filter', data.field) : 
    		bloodTestItemForm.getEditParams('BloodBTestItem', 'LAY-bloodbtestitem-form-filter', data.field);
    	
        bloodTestItemForm.onSave(Url, dataParams, function(){
        	$("#form-save").attr("event-url", "");
        	table.reload("LAY-bloodbtestitem-table", bloodTestItemTable.config);
        });
        return false;
    });
    
	//渲染表格数据
	bloodTestItemTable.render({});

})
