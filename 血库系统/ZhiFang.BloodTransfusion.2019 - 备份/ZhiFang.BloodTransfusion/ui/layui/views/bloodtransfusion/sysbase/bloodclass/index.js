layui.extend({
	uxutil:'ux/util',
	dataadapter:'ux/dataadapter',	
    bloodClassTable:'views/bloodtransfusion/sysbase/bloodclass/bloodClassTable',
    bloodClassForm:'views/bloodtransfusion/sysbase/bloodclass/bloodClassForm'	
}).use(['form', 'table', 'uxutil', 'dataadapter', 'bloodClassTable', 'bloodClassForm'], function(){
	"use strict";
	
	var $ = layui.$,
	    form = layui.form,
	    table = layui.table,
	    uxutil = layui.uxutil,
	    dataadapter = layui.dataadapter,
	    bloodClassTable = layui.bloodClassTable,
	    bloodClassForm = layui.bloodClassForm;
	
	//初始表单事件
	function InitBloodClassFormEvent(){
		var submitFilter = bloodClassForm.formConfig.form_submit_filter;
		//监听提交事件
	    form.on('submit(' + submitFilter +')', function(data){
	    	bloodClassForm.doSave(data.field, function(result) {
				var success = false;
				if(result) success = result.success;
				if(success == true) {
					OnRefreshData();
				}; 
			});
	    }) 
	};
	    
    //初始操作按钮监听事件
    function InitActionButtonEvent(){
    	//按钮监听事件
		$(".layui-btn-group>.layui-btn").on("click", function() {
			var type = $(this).data('type'); 
			OnButtonEvent[type] ? OnButtonEvent[type].call(this) : '';
		});
		
		var searchTextId = bloodClassForm.searchConfig.search_text_mixed_id;
		$('#' + searchTextId).on('keydown', function(event) {
			if(event.keyCode == 13) OnSearchData();
		});
    };
    
    //初始化列表事件
    function InitTableEvent(){
	    //监听表格单击事件,赋值表单元素
	    var filter = 'bloodclass_table_filter';
	    table.on("row(" + filter + ")", function(obj){
	    	var id = obj.data["BloodClass_Id"];
	    	bloodClassForm.PK = id;
	    	setTimeout(function(){
	    		bloodClassForm.doEdit(id);
	    	}, 300);
	    })    	
    }
    //按钮操作
    var OnButtonEvent = {
      	add: function(){
      		bloodClassForm.doNewAdd();
    	},
    	edit: function(id){
            bloodClassForm.doEdit(bloodClassForm.PK);
    	},
    	search: function(){
    		OnSearchData();
     	} 
    };
    
    //检索数据
    function OnSearchData(){
      	var me = this, 
    		searchTextId = bloodClassForm.searchConfig.search_text_mixed_id,
    		searchVisibleId = bloodClassForm.searchConfig.search_select_visible_id,
    		Value = $('#' + searchTextId).val(),
    		LikeWhere = bloodClassTable.getLikeWhere.call(bloodClassTable, Value),
    		visible = $('#' + searchVisibleId).val() ? $('#' + searchVisibleId).val() : 1,
   		    internalWhere = (visible == -1) ?  LikeWhere : LikeWhere ? 
		        ' ( ' + LikeWhere + ' ) and bloodclass.Visible = ' + visible : 'bloodclass.Visible = ' + visible;
			bloodClassTable.config.where = {"where": internalWhere};
			bloodClassTable.config.url = bloodClassTable.config.defLoadUrl;
			table.render(bloodClassTable.config);  	
    };
    
    //刷新数据
    function OnRefreshData(){
    	var tableIns = bloodClassTableIns.tableIns;
    	var id = tableIns.config.id;
    	var config = tableIns.config;
    	config.url = config.defLoadUrl;
    	bloodClassTableIns.tableIns.reload(config);
    };
    
    InitBloodClassFormEvent();
    InitActionButtonEvent();
    InitTableEvent();
    
    var bloodClassTableIns = bloodClassTable.render(); 
    

	
})
