/**
   @Name：测试专用
   @Author：Jcall
   @version 2021-11-05
 */
layui.extend({
	uxform:'ux/form',
	formSelects:'ux/other/form_select/form_selects_v4',
	soulTable:'ux/other/soulTable/soulTable'
}).use(['uxform','formSelects','table','soulTable'], function () {
    "use strict";
    var $ = layui.$,
        form = layui.uxform,
        formSelects = layui.formSelects,
        table = layui.table,
        soulTable = layui.soulTable;

    form.render();
    
    //formSelects拼音字头性能测试
    var data = [];
    for(var i=0;i<100;i++){
    	data.push({value:i,name:'测试数据项'+i});
    }
	formSelects.data('select1', 'local', {
		arr: data
	});
	
	var tableIndex = table.render({
		elem:$("#table"),
		toolbar:"#table-toolbar-top",
		cols:[[
			{field:'id',width:160,title:'ID',sort:true},
			{field:'name',width:160,title:'名称',sort:true},
			{field:'code',width:160,title:'编码',sort:true},
		]],
		data:(function(){
			var list = [];
			for(var i=0;i<10;i++){
				list.push({id:i+1,name:'名称'+(i+1),code:'编码'+(i+1)});
			}
			return list;
		})(),
		filter: {
			//items:['column','data','condition','editCondition','excel','clearCache'],
            cache: true
        },
		done:function(){
			soulTable.render(this);
		}
	});
	//头工具栏事件
	table.on('toolbar(table)', function(obj){
		switch(obj.event){
			case 'clearCache':
				soulTable.clearCache(tableIndex.config.id);
				layer.msg('已还原！', {icon: 1, time: 1000});
				break;
		}
	});
});