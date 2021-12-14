layui.extend({
	uxutil:'/ux/util',
    uxtable:'/ux/table',
	lefttable:'app/dic/section/hiscomp/transfer/leftlist',
	righttable:'app/dic/section/hiscomp/transfer/rightlist'
}).use(['uxutil','table','lefttable','righttable'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		lefttable = layui.lefttable,
		righttable = layui.righttable,
		table = layui.table;
		
	//自定义变量
	var config={
		checkRowData:[],//选中行
		//小组项目复制服务
		addUrl: uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddDelLBSectionHisComp',
		//左列表选择项
		leftData:[],
		//右列表选择项
		rightData:[],
		tableInsLeft:null,
		tableInsRight:null
	};
	//初始化
    function init(){
        //左列表
		var leftobj={
		 	elem:'#lefttable',
	    	title:'小组历史对比列表',
	    	height:'full-75',
	    	where:"lbsectionhiscomp.LBSection.Id="+ document.getElementById("sectionID").value,
	    	defaultOrderBy: JSON.stringify([{property: 'LBSectionHisComp_DataAddTime',direction: 'ASC'}])
		};
		//小组历史对比列表初始化(左列表)
		config.tableInsLeft=lefttable.render(leftobj);
		//右列表
		var rightobj={
		 	elem:'#righttable',
	    	title:'小组表',
	    	height:'full-80',
	    	groupItemID:document.getElementById("groupItemID").value,
	    	defaultOrderBy: JSON.stringify([{property: 'LBSection_DispOrder',direction: 'ASC'}])
		};
		//小组列表初始化(右列表)
		config.tableInsRight =  righttable.render(rightobj);
		
		//默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
	    //事件监听
	    initListeners();
    }
   
    //事件监听
    function initListeners(){
    	var searchText = document.getElementById('searchText').value;
    	config.tableInsRight.loadData({},searchText,document.getElementById("groupItemID").value);
    	//左列表监听
    	//监听行双击事件
	    table.on('rowDouble(lefttable)', function (obj) {
	    	var tableBox = $(this).parents('.layui-table-box');
	    	var index = $(this).attr('data-index');
	        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
	    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
		    if (checkCell.length>0) {
		        checkCell.click();
		    }
	        changeData('right');
	    });
	    //选中状态
	    table.on('checkbox(lefttable)', function(obj){
	        var leftcheckStatus = table.checkStatus('lefttable');
	        config.leftData = leftcheckStatus.data;
	    });
	    
	    //右列表监听
	    //监听行双击事件
	    table.on('rowDouble(righttable)', function (obj) {
	        var tableBox = $(this).parents('.layui-table-box');
	    	var index = $(this).attr('data-index');
	        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
	    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
		    if (checkCell.length>0) {
		        checkCell.click();
		    }
	        changeData('left');
	    });
	    //选中状态
	    table.on('checkbox(righttable)', function(obj){
	        var rightcheckStatus = table.checkStatus('righttable');
	        config.rightData = rightcheckStatus.data;
	    });
    
    	// 窗体大小改变时，调整高度显示
		$(window).resize(function() {
			var width = $(this).width();
			var height = $(this).height();
			 //表单高度
			var height = $(window).height() - $(".fiexdHeight").offset().top-35+ "px";
		    $('.fiexdHeight').css("height",height);
		});
		 
		// 按钮事件
		//向左移动
		$('#add').on('click',function(){
			changeData('left');
		});
		//向右移动
		$('#remove').on('click',function(){
			changeData('right');
		});
		
		//重置
		$('#reset').on('click',function(){
	        table.reload('lefttable', {
	            url: '',
	            data: config.tableInsLeft.config.oldListData
	        });
	        table.reload('righttable', {
	            url: '',
	            data: config.tableInsRight.config.oldListData
	        });
		});
		//保存
		$('#save').on('click',function(){
	   		var url =config.addUrl;
			onSaveClick(url);
		});
		//查询
		 //监听查询，小组列表
		$('#search').on('click',function(){ 
			var searchText = document.getElementById('searchText').value;
			//已选择的采样项目
			var leftData = table.cache['lefttable'];
			var strids = [];
            for(var i=0;i<leftData.length;i++){
            	strids.push(leftData[i].LBSectionHisComp_HisComp_Id);
            }
            config.tableInsRight.config.groupItemID = strids.join(',');
			config.tableInsRight.loadData({},searchText,strids.join(','));
		});
		
    }
    //type向那边添加数据
    function changeData(type){
    	//左列表现有数据
    	var leftData = table.cache['lefttable'];
        var rightData =  table.cache['righttable'];
    	if(type=='left'){//向左移动数据
		   var obj = addLeft(leftData,rightData);
    	}
    	if(type=='right'){//向右移动数据
		   var obj = addRight(leftData,rightData);
    	}
    	
    	if(!obj)return;
    	table.reload('lefttable', {
            data: obj.leftData
        });
        table.reload('righttable', {
            data: obj.rightData
        });
        config.leftData=[];
        config.rightData=[];
    }
   
   //向左列表添加数据
    function addLeft(leftData,rightData){
   	    //获取右列表选择数据
	    if(config.rightData.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var r_d=[]; 
        leftData.reverse()
        var d1 = [];
        for (var i = 0; i < rightData.length; i++) {
            if(rightData[i].LAY_CHECKED && rightData[i].LAY_CHECKED===true){
                delete rightData[i].LAY_CHECKED
                rightData[i].LAY_TABLE_INDEX && delete rightData[i].LAY_TABLE_INDEX
                var obj = {
                    'LBSectionHisComp_HisComp_Id':  rightData[i].LBSection_Id,
                    'LBSectionHisComp_HisComp_CName':  rightData[i].LBSection_CName,
                    'LBSectionHisComp_HisComp_SName':  rightData[i].LBSection_SName,
                    'LBSectionHisComp_HisComp_UseCode':  rightData[i].LBSection_UseCode,
                    'LBSectionHisComp_Id':  rightData[i].LBSectionHisComp_Id
                };
                leftData.push(obj)
            }else{
                delete rightData[i].LAY_TABLE_INDEX
                r_d.push(rightData[i])
            }
        }
        rightData = r_d;
        return {leftData:leftData,rightData:rightData};
    }
    
    //向右边列表添加数据
    function addRight(leftData,rightData){
    	//获取右列表选择数据
	    if(config.leftData.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var l_d=[]; 
        rightData.reverse();
        var d1 = [];
        for (var i = 0; i < leftData.length; i++) {
            if(leftData[i].LAY_CHECKED && leftData[i].LAY_CHECKED===true){
                delete leftData[i].LAY_CHECKED
                leftData[i].LAY_TABLE_INDEX && delete leftData[i].LAY_TABLE_INDEX
                 var obj = {
                    'LBSection_Id': leftData[i].LBSectionHisComp_HisComp_Id,
                    'LBSection_CName': leftData[i].LBSectionHisComp_HisComp_CName,
                    'LBSection_SName': leftData[i].LBSectionHisComp_HisComp_SName,
                    'LBSection_UseCode': leftData[i].LBSectionHisComp_HisComp_UseCode,
                    'LBSectionHisComp_Id' : leftData[i].LBSectionHisComp_Id
                };
                rightData.push(obj)
            }else{
                delete leftData[i].LAY_TABLE_INDEX
                l_d.push(leftData[i])
            }
        }
        leftData = l_d;
        return {leftData:leftData,rightData:rightData};
    }
    function getEntity(){
    	var entity={},addList=[],delIDList=[];
    	var leftData = table.cache['lefttable'];
    	var oldData = config.tableInsLeft.config.oldListData;
    	//新增
    	for (var i = 0; i < leftData.length; i++) {
            var addFlag = true;//是新增的
            for (var j = 0; j < oldData.length; j++) {
                if (leftData[i].LBSectionHisComp_Id == oldData[j].LBSectionHisComp_Id) {
                   addFlag = false;
                    break;
                }
            }
            if (addFlag) {
                var DataTimeStamp = [0,0,0,0,0,0,0,0];
                addList.push({
                    LBSection: { Id: document.getElementById("sectionID").value, DataTimeStamp: DataTimeStamp },
                    HisComp: { Id: leftData[i].LBSectionHisComp_HisComp_Id, DataTimeStamp: DataTimeStamp }
                });
            }
        }
    	//删除 
    	for (var i = 0; i < oldData.length; i++) {
            var delFlag = true;//是删除的
            for (var j = 0; j < leftData.length; j++) {
                if (oldData[i].LBSectionHisComp_Id == leftData[j].LBSectionHisComp_Id) {
                    delFlag = false;//存在 不是删除的
                    break;
                }
            }
            if (delFlag) {
                delIDList.push(oldData[i].LBSectionHisComp_Id);
            }
        }
    	delIDList  = delIDList.join();
    	//第二个如果为true新增时 则判断实体是否已经存在 否则不判断
        var isCheckEntityExist = true;
    	entity={addEntityList:addList,isCheckEntityExist,delIDList:delIDList};
    	return entity;
    }
    
    //保存
	function onSaveClick(url) {
		//新增实体
		var entity = getEntity();
		var params = JSON.stringify(entity);
		//显示遮罩层
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				parent.afterHisCompTransferUpdate(data);
				
			} else {
				if(!data.msg)data.msg='保存失败';
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	}
	//初始化
	init();
});