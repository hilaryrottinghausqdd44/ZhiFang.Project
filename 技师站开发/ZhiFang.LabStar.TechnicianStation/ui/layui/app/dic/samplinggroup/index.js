layui.extend({
	uxutil:'/ux/util',
	uxtable:'ux/table',
	SamplingGroupForm:'app/dic/samplinggroup/form',
	SamplingGroupList:'app/dic/samplinggroup/list',
	SamplingItemList:'app/dic/samplinggroup/item/list'
}).use(['SamplingGroupList','SamplingGroupForm','SamplingItemList','table','form','element'],function(){
	var $ = layui.$,
		element = layui.element,
		table = layui.table,
		form = layui.form,
		SamplingGroupList = layui.SamplingGroupList,
		SamplingGroupForm = layui.SamplingGroupForm,
		SamplingItemList = layui.SamplingItemList;
	
	var config ={
		//当前选择行数据
	    checkRowData:[],
	    //保存后返回的行id
	    PK:null,
	    //当前激活页签,默认表单,自定义变量0-表单，1-采样组项目
        currTabIndex: 0,
        //已激活页签，用于判断页签是否已加载
        isLoadTabArr: []
	};
	//采样组列表实例
	var table_ind0=null;
	//采样组项目列表实例
	var table_ind1=null;
	//采样组表单实例
	var form_ind =null;

    //采样组列表初始化
	table_ind0 = SamplingGroupList.render({
    	elem:'#samplinggroup-table',
    	title:'采样组列表',
    	height:'full-70',
    	size:'sm',
    	defaultOrderBy: JSON.stringify([{property: 'LBSamplingGroup_DispOrder',direction: 'ASC'}]),
    	done: function(res, curr, count) {
			if(count>0){
				var filter = this.elem.attr("lay-filter");
				//默认选择第一行
				var rowIndex = 0;
	            for (var i = 0; i < res.data.length; i++) {
	                if (res.data[i].LBSamplingGroup_Id == config.PK) {
	              	    rowIndex=res.data[i].LAY_TABLE_INDEX;
	              	  break;
	                }
	            }
	           //默认选择行	
	            var tableDiv = $("#samplinggroup-table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();
			}else{
				
				//采样组数据为空，表单是新增状态
				if(form_ind)form_ind.isAdd();
				if(table_ind1)table_ind1.clearData();
			}
		}
	});
	
	//采样组表单初始化
    form_ind = SamplingGroupForm.render();
	
    //采样组项目初始化
    function initSamplingItemList(){
    	//采样组项目初始化
	    var itemObj={
	    	elem:'#samplingitem_table',
	    	title:'采样组项目',
	    	height:'full-120',
            size:'sm'
	    };	
	    table_ind1 = SamplingItemList.render(itemObj);
    }
	
	//采样组列表监听
	table_ind0.table.on('row(samplinggroup-table)', function(obj){
		config.checkRowData=[];
		config.checkRowData.push(obj.data);
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        onSearch(config.checkRowData);
	});
	//采样组保存
	form.on('submit(save)',function(obj){
		form_ind.onSaveClick(obj,function(id){
			config.PK = id;
			//采样组列表数据加载
	        table_ind0.loadData();
		});
	});

	//删除
	$('#del').on('click',function(){
		form_ind.onDelClick(function(id){
			config.PK = null;
			//采样组列表数据加载
	        table_ind0.loadData();
		});
	});
	//列表查询
    form.on('submit(search)', function (data) {
    	  //采样组列表数据加载
	    table_ind0.loadData();
    });
       //回车事件
    $("#search_val").on('keydown', function (event) {
        if (event.keyCode == 13) {
            //采样组列表数据加载
	        table_ind0.loadData();
            return false;
        }
    });
    $('#edit').on('click',function(){
		if(!config.checkRowData[0].LBSamplingGroup_Id){
			layer.msg("请重新选择行再做编辑操作！", { icon: 5, anim: 6 });
			return;
		}
 		form_ind.isEdit(config.checkRowData[0].LBSamplingGroup_Id);
	});

	//tab页签切换时联动
    element.on('tab(tabs)', function(obj){
    	config.currTabIndex=obj.index;
        var isLoad = false;
        //上一个选择的采样组
        var groupId = '';
    	//判断当前页签是否已加载过数据
    	for(var i =0;i<config.isLoadTabArr.length;i++){
    		if(config.isLoadTabArr[i].index==obj.index){
    			groupId=config.isLoadTabArr[i].curRowId;
    			isLoad = true;
    			break;
    		}
    	}	        
    	switch (config.currTabIndex){
    		case 0:   //表单
    			break;
    		case 1 ://采样组项目维护
    		    if(!isLoad && config.checkRowData.length>0){
				    //用于判断是否已加载
				    var obj1 = {index:obj.index,curRowId:config.checkRowData[0].LBSamplingGroup_Id};
    		    	config.isLoadTabArr.push(obj1);
    		    	initSamplingItemList();	//初始化采样组项目
    		    }
    		    break;
    		default:
    			break;
    	}
    	if(config.checkRowData.length>0 && groupId!=config.checkRowData[0].LBSamplingGroup_Id){
    		onSearch();
		}
    });
	 // 查询
	function onSearch(recs){
    	for(var i =0;i<config.isLoadTabArr.length;i++){
    		//当前页签
    		if(config.isLoadTabArr[i].index == config.currTabIndex){
    			config.isLoadTabArr.splice(i, 1); //删除下标为i的元素
    			var obj1 = {index:config.currTabIndex,curRowId:config.checkRowData[0].LBSamplingGroup_Id};
    	        config.isLoadTabArr.push(obj1);
    		}
    	}
    	//初始化，默认页签为表单页签
    	if(config.isLoadTabArr.length==0){
    	    var obj1 = {index:config.currTabIndex,curRowId:config.checkRowData[0].LBSamplingGroup_Id};
    		config.isLoadTabArr.push(obj1);
    	}
    	
    	switch (config.currTabIndex){
    	    case 0 ://表单
    	        form_ind.isEdit(config.checkRowData[0].LBSamplingGroup_Id);
    	     break;
    		case 1 ://采样组项目维护
    		    var SamplingGroupID=config.checkRowData[0].LBSamplingGroup_Id;
		        var SamplingGroupCName=config.checkRowData[0].LBSamplingGroup_CName;
    		    if(SamplingGroupID && table_ind1)table_ind1.loadData(SamplingGroupID,SamplingGroupCName);
    		        else
    		            table_ind1.clearData();
    		    break;
    		case 2 ://打印格式
    		    break;    
    		default:
    			break;
    	}
    }

	//初始化
	function init(){
		//表单初始化高度
		$(".tabHeight").css("height", ($(window).height() - 35) + "px");//设置表单容器高度
		var height = $(document).height() - $("#LBSamplingGroup").offset().top-65;
	    $('#LBSamplingGroup').css("height",height);
	    //采样组列表数据加载
	    table_ind0.loadData();
	    
	}
	// 窗体大小改变时，调整高度显示
	$(window).resize(function() {
        $(".tabHeight").css("height", ($(window).height() - 35) + "px");//设置表单容器高度
		var height = $(document).height() - $("#LBSamplingGroup").offset().top-65;
	    $('#LBSamplingGroup').css("height",height);
	});
  
	//初始化
	init();
});
