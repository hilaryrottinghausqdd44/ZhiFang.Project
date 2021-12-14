/**
 * 选择项目
 * @author liangyl
 * @version 2021-05-17
 */layui.extend({
     uxutil: 'ux/util',
     uxbase: 'ux/base',
	uxtable :'ux/table',
	listtable: 'views/sample/batch/edit/item/transfer/list',
	unapp: 'views/sample/batch/edit/item/transfer/un/app'
 }).use(['element', 'uxutil','uxbase','form','listtable','unapp','table'], function(){
	var $ = layui.$,
		form = layui.form,
		element = layui.element,
		listtable = layui.listtable,
		unapp = layui.unapp,
        table = layui.table,
        uxbase = layui.uxbase,
		uxutil = layui.uxutil;
		
	//小组ID
//	var SECTIONID  = '10000000027';
	var SECTIONID  = uxutil.params.get(true).SECTIONID;
   //查询条件内容
    var SERACHOBJ={};
    
    //页签是否已加载
    var isTabLoadArr=[];
    //当前激活页签-- 默认医嘱项目
    var ActivateTab = 0;
    
    var DEFAULT_DATA = {},
	    AFTER_SAVE = function(){};//保存后回调函数
	    
	//已选项目列表实例
	var table0_Ind = listtable.render({
		elem:'#table',
		height:'full-70',
		title:'已选项目列表',
		size: 'sm',
		done: function (res, curr, count) {
			if(count>0){
				setTimeout(function () {
		        }, 100);
			}
		}
	});
	//待选项目实例 ---医嘱项目
	var unapp_Ind = unapp.render({SECTIONID:SECTIONID,elem:'#orderitem_table',type:'1',title:'医嘱项目'});

	//待选项目实例 ---组合项目
	var unapp1_Ind = unapp.render({SECTIONID:SECTIONID,elem:'#groupitem_table',elem1:'#child_table',type:'2',title:'组合项目'});
   	//默认加载医嘱项目页签
   	onSearch(ActivateTab);
	//待选项目实例 ---所有项目
	var unapp2_Ind = unapp.render({SECTIONID:SECTIONID,elem:'#item_table',type:'0',title:'所有项目'});

	element.on('tab(tabs)', function(obj){
		ActivateTab=obj.index;
     	onSearch(obj.index);
    });
    
    //选择到左列表（左列表新增数据）
    $('#left').on('click',function(){
    	var comTab = getActivateTab();
    	var arr = comTab.getSelection();
    	if(arr.length==0)return false;
    	addLeft(arr);
    });
     //选择到右列表（左列表删除数据）
    $('#right').on('click',function(){
    	var arr = table0_Ind.table.checkStatus('table').data;
    	if(arr.length==0)return false;
        addRight(arr);
    });
      //确定
    $('#accept').on('click',function(){
    	var data = table0_Ind.table.cache.table;
        	 
    	if(uxutil.params.get(true).MODULE=='additem'){// 添加项目页签
    		 parent.afterAddUpdate(data);
    	}	
    	if(uxutil.params.get(true).MODULE=='delitem'){ // 刪除项目页签
    		 parent.afterDelUpdate(data);
    	}

    });
   
    //取消
    $('#cancel').on('click',function(){
    	parent.layer.closeAll('iframe');
    });
     //医嘱项目查询
    $('#orderitem_search').on('click',function(){
    	unapp_Ind.loadData($("input[name='orderitem_text']").val());
    });
    //组合项目查询
    $('#groupitem_search').on('click',function(){
    	unapp1_Ind.loadData($("input[name='groupitem_text']").val());
    });
    //所有项目查询
    $('#item_search').on('click',function(){
    	unapp2_Ind.loadData($("input[name='item_text']").val());
    });
	    
	table0_Ind.table.on('rowDouble(table)', function(obj){
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        addRight([obj.data]);
	});
   //医嘱项目   ---双击行
	table.on('rowDouble(orderitem_table)', function(obj){
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		var comTab = getActivateTab();
    	var arr = comTab.selection([obj.data]);
    	if(arr.length==0)return false;
		addLeft(arr);
	});
	//组合项目   ---双击行
	table.on('rowDouble(groupitem_table)', function(obj){
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		var comTab = getActivateTab();
    	var arr = comTab.selection([obj.data]);
    	if(arr.length==0)return false;
    	addLeft(arr);
	});
	//所有项目   ---双击行
	table.on('rowDouble(item_table)', function(obj){
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		var comTab = getActivateTab();
    	var arr = comTab.selection([obj.data]);
    	if(arr.length==0)return false;
		addLeft(arr);
	});
	//数据添加到已选列表
    function addLeft(addlist){
    	var data = table0_Ind.table.cache.table,
    	    list = data;
        //判断新增的数据是否已被选择，没有则加入
        for(var i=0;i<addlist.length;i++){
        	var isAdd = true;
        	for(var j=0;j<data.length;j++){
        		if(addlist[i].LBSectionItem_LBItem_Id  == data[j].LBSectionItem_LBItem_Id){
        			isAdd = false;
        		}
        	}
        	if(isAdd)list.push(addlist[i]);
        }
        table0_Ind.DATA_LIST = list;
        table0_Ind.instance.reload({data:list});
    }
	//删除已选数据
	function addRight(dellist){
		var data = table0_Ind.table.cache.table;
         //判断新增的数据是否已被选择，没有则加入
        for(var i=0;i<data.length;i++){
        	for(var j=0;j<dellist.length;j++){
        		if(dellist[j].LBSectionItem_LBItem_Id  == data[i].LBSectionItem_LBItem_Id){
        			data.splice(i,1); //删除下标为i的元素
        		}
        	}
        }
        table0_Ind.DATA_LIST = data;
        table0_Ind.instance.reload({data:data});
	}
	//判断当前激活的页签
	function getActivateTab(){
		var activate_Ind = null;
		switch (ActivateTab){
    		case 0:
    		    activate_Ind = unapp_Ind;
    			break;
              case 1:
    		    activate_Ind = unapp1_Ind;
    			break;
    		default:    		    
    		    activate_Ind = unapp2_Ind;
    			break;
    	}
		return activate_Ind;
	}
    //查询    
    function onSearch(index){
    	var isload=isTabLoadArr.indexOf(index);
        if(isload>-1)return false;

    	switch (index){
    		case 0:
    		    unapp_Ind.loadData($("input[name='orderitem_text']").val());
    			break;
              case 1:
    		    unapp1_Ind.loadData($("input[name='groupitem_text']").val());
    			break;
    		default:    		    
    		    unapp2_Ind.loadData($("input[name='item_text']").val());
    			break;
    	}
    	isTabLoadArr.push(index);
    }
   
	//初始化数据
    function init(){
    	//默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
    }
    //初始化数据
    init();
});